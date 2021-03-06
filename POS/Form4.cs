using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace POS
{
    public partial class Form4 : Form
    {
        String Token;
        List<Soporte> lista;
        List<Soporte> listaFiltrada;
        String user;
        public Form4(string token, string User)
        {
            InitializeComponent();
            Token = "Token "+token;
            user = User;
            Inicio();
            llenarComboBox();

        }
        private void Inicio() {
            try
            {
                Historial();
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
        
        private void Historial()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/historial/";
            Uri MoodysWebAddress = new Uri(url);
            var webRequest = System.Net.WebRequest.Create(MoodysWebAddress);
            webRequest.Method = "GET";
            webRequest.Timeout = 20000;
            webRequest.ContentType = "application/json";
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", Token);
            HttpWebResponse response;
            //Solicita Request
            using (response = webRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
                //Intenta obtener el token
                try
                {
                    lista = JsonConvert.DeserializeObject<List<Soporte>>(results);
                    dataGridView1.DataSource = lista;
                    dataGridView1.Columns[0].Visible = false;
                }
                //Si no obtiene el token manda un mensaje de error
                catch (Exception ex)
                {
                    if(MessageBox.Show(ex.ToString(), "error",
                        MessageBoxButtons.OK) == DialogResult.OK)
                        System.Environment.Exit(0);
                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(Token);
            form3.Show();
            this.Hide();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Soporte s = new Soporte();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                s.Id = row.Cells[0].Value.ToString();
                s.Inicio = Convert.ToDateTime(row.Cells[1].Value.ToString());
                s.Cliente=row.Cells[2].Value.ToString();
                s.Modelo=row.Cells[3].Value.ToString();
                s.Problema=row.Cells[4].Value.ToString();
                s.SN=row.Cells[5].Value.ToString();
                s.Tecnico=row.Cells[6].Value.ToString();
                s.Solucionado = row.Cells[7].Value.ToString();
                s.Cancelado = row.Cells[8].Value.ToString();
                Console.WriteLine(s.Id);
                Form2 form2 = new Form2(s, Token);
                form2.Show();
                this.Hide();
            }

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                if (MessageBox.Show("Desea salir de la aplicacion?", "Salir",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form4_VisibleChanged(object sender, EventArgs e)
        {
            Inicio();
        }

        private void filterCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void llenarComboBox()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if((i != 0) && (i != 1) && (i != 3) && (i != 5))
                {
                    filterCombo.Items.Add(dataGridView1.Columns[i].HeaderText.ToString());
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string columnFilter;
            if (filterCombo.SelectedItem == null)
            {
                if (MessageBox.Show("Seleccione una Columna valida", "error",
                        MessageBoxButtons.OK) == DialogResult.OK)
                    this.Close();
                columnFilter = "";
            } else
            {
                columnFilter = filterCombo.SelectedItem.ToString();
            }
                
            
            
            string filterSearch = filterText.Text.ToString();
            if(filterSearch == "") {
                dataGridView1.DataSource = lista;
            }
            else
            {
                switch (columnFilter)
                {
                    case "Cliente":
                        FiltrarLista(2, filterSearch);
                        break;
                    case "SN":
                        FiltrarLista(4, filterSearch);
                        break;
                    case "Tecnico":
                        FiltrarLista(6, filterSearch);
                        break;
                    case "Solucionado":
                        FiltrarLista(7, filterSearch);
                        break;
                    case "Cancelado":
                        FiltrarLista(8, filterSearch);
                        break;
                    default:
                        dataGridView1.DataSource = lista;
                        break;
                }
            }
            
            Debug.Print(columnFilter);
            Debug.Print(filterSearch);
        }

        private void FiltrarLista(int columna, string filtro)
        {
            listaFiltrada = new List<Soporte>();
            dataGridView1.DataSource = lista;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[columna].Value.ToString() == filtro)
                {
                    Soporte s = new Soporte();
                    s.Id = row.Cells[0].Value.ToString();
                    s.Inicio = Convert.ToDateTime(row.Cells[1].Value.ToString());
                    s.Cliente = row.Cells[2].Value.ToString();
                    s.Modelo = row.Cells[3].Value.ToString();
                    /*s.Problema = row.Cells[4].Value.ToString();
                    s.SN = row.Cells[5].Value.ToString();*/
                    s.SN = row.Cells[4].Value.ToString();
                    s.Problema = row.Cells[5].Value.ToString();
                    s.Tecnico = row.Cells[6].Value.ToString();
                    s.Solucionado = row.Cells[7].Value.ToString();
                    s.Cancelado = row.Cells[8].Value.ToString();
                    listaFiltrada.Add(s);

                }
            }
            dataGridView1.DataSource = listaFiltrada;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(Token, user);
            form5.Show();
            this.Hide();
        }
    }
}
