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
        public Form2(Soporte Soporte, String Token)
        {
            InitializeComponent();
            soporte = Soporte;
            token = Token;
            SoporteInfo();
            ClienteInfo();
            EquipoInfo();
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
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/cerrar_soporte/" + soporte.Id+ "?Cierre="+richTextBox1.Text;
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
                    Form2 form2 = new Form2(soporte, token);
                    form2.Show();
                    this.Close();

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
                dateTimePicker1.Value = item.Inicio;
                richTextBox1.Text = item.Comentarios;
                if (item.Solucionado == 1)
                {
                    checkBox1.Checked = true;
                    dateTimePicker2.Value = (DateTime)item.Final;
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
            Form4 form4 = new Form4("123");
            form4.Show();
            this.Hide();
            


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
            Resolver();
        }
    }
}
