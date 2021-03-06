using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace POS
{
    public partial class Form2 : Form
    {
        Soporte soporte;
        List<Soporte_info> listaSoporte;
        List<Cliente_info> listaCliente;
        List<Equipo_info> listaEquipo;
        String token;
        bool userClosed = true;
        public Form2(Soporte Soporte, String Token)
        {
            InitializeComponent();
            dateTimePicker1.Visible = false;
            label3.Visible = false;
            soporte = Soporte;
            token = Token;
            if (soporte.Solucionado == "1") 
            {
                dateTimePicker1.Visible = true;
                label3.Visible = true;
            }
            SoporteInfo();
            ClienteInfo();
            EquipoInfo();
            Console.WriteLine(listaSoporte[0].Final.ToString());
        }

        private void EquipoInfo()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/buscar_equipo/" + listaSoporte[0].Equipo_id;
            Uri MoodysWebAddress = new Uri(url);
            var webRequest = System.Net.WebRequest.Create(MoodysWebAddress);
            webRequest.Method = "GET";
            webRequest.Timeout = 20000;
            webRequest.ContentType = "application/json";
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", token);
            HttpWebResponse response;
            //Solicita Request
            using (response = webRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
                //Intenta obtener el token
                try
                {
                    listaEquipo = JsonConvert.DeserializeObject<List<Equipo_info>>(results);
                }
                //Si no obtiene el token manda un mensaje de error
                catch (Exception ex)
                {
                    if (MessageBox.Show(ex.ToString(), "error",
                        MessageBoxButtons.OK) == DialogResult.OK)
                        System.Environment.Exit(0);
                }

            }
        }
        private void ClienteInfo()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/buscar_cliente/" + listaSoporte[0].Cliente_id;
            Uri MoodysWebAddress = new Uri(url);
            var webRequest = System.Net.WebRequest.Create(MoodysWebAddress);
            webRequest.Method = "GET";
            webRequest.Timeout = 20000;
            webRequest.ContentType = "application/json";
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", token);
            HttpWebResponse response;
            //Solicita Request
            using (response = webRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
                //Intenta obtener el token
                try
                {
                    listaCliente = JsonConvert.DeserializeObject<List<Cliente_info>>(results);
                }
                //Si no obtiene el token manda un mensaje de error
                catch (Exception ex)
                {
                    if (MessageBox.Show(ex.ToString(), "error",
                        MessageBoxButtons.OK) == DialogResult.OK)
                        System.Environment.Exit(0);
                }

            }
        }
        private void SoporteInfo()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/buscar_soporte/"+soporte.Id;
            Uri MoodysWebAddress = new Uri(url);
            var webRequest = System.Net.WebRequest.Create(MoodysWebAddress);
            webRequest.Method = "GET";
            webRequest.Timeout = 20000;
            webRequest.ContentType = "application/json";
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", token);
            HttpWebResponse response;
            //Solicita Request
            using (response = webRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
                //Intenta obtener el token
                try
                {
                    Console.WriteLine(results);
                    listaSoporte = JsonConvert.DeserializeObject<List<Soporte_info>>(results);
                }
                //Si no obtiene el token manda un mensaje de error
                catch (Exception ex)
                {
                    if (MessageBox.Show(ex.ToString(), "error",
                        MessageBoxButtons.OK) == DialogResult.OK)
                        System.Environment.Exit(0);
                }

            }
        }
        private void Resolver()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/cerrar_soporte/" + soporte.Id+ "?Cierre="+richTextBox2.Text;
            Uri MoodysWebAddress = new Uri(url);
            var webRequest = System.Net.WebRequest.Create(MoodysWebAddress);
            webRequest.Method = "GET";
            webRequest.Timeout = 20000;
            webRequest.ContentType = "application/json";
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", token);
            HttpWebResponse response;
            //Solicita Request
            using (response = webRequest.GetResponse() as HttpWebResponse)
            {
                try
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    results = reader.ReadToEnd();            
                    MessageBox.Show("Soporte Resuelto con exito");
                    Cerrar();

                }
                //Si no obtiene el token manda un mensaje de error
                catch (Exception ex)
                {
                    if (MessageBox.Show(ex.ToString(), "error",
                        MessageBoxButtons.OK) == DialogResult.OK)
                        System.Environment.Exit(0);
                }

            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
            foreach(Soporte_info item in listaSoporte)
            {
                dateTimePicker2.Value = item.Inicio;
                richTextBox1.Text = item.Comentarios;
                if (item.Solucionado == 1)
                {
                    checkBox1.Checked = true;
                    richTextBox2.Text = item.Comentarios_cierre;
                    dateTimePicker1.Value = (DateTime)item.Final;
                }


            }
            foreach(Cliente_info item in listaCliente) 
            {
                label5.Text = item.Nombre;
                label6.Text = item.Phone.ToString();
                label7.Text = item.Email;
            }
            foreach (Equipo_info item in listaEquipo)
            {
                label9.Text = item.Marca;
                label10.Text = item.Modelo;
                label8.Text = item.SN;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Cerrar();
        }
        private void Cerrar(){
            Program.form1.form4.Show();
            userClosed = false;
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
          
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "")
                MessageBox.Show("Porfavor ingrese comentarios de cierre", "Error");
            else
                Resolver();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (userClosed)
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

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(token, listaCliente[0]);
            form3.Show();
            userClosed = false;
            this.Close();

        }
    }
}
