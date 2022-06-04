using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace POS
{
    public partial class Form3 : Form
    {
        String token;
        bool userClosed=true;
        public Form3(string Token, Soporte Soporte) {
            InitializeComponent();
            //Editar
        }
        public Form3(string Token)
        {
            InitializeComponent();
            token = Token;
            label10.Text = "";
            label12.Text = "";
            //Nuevo
        }
        private void CrearCliente()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/crear_cliente/?Nombre="+textBox1.Text+"&Email="+textBox3.Text + "&Tel="+textBox2.Text;
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
                    label10.Text = results;
                }
                //Si no obtiene el token manda un mensaje de error
                catch (Exception ex)
                {
                    label10.Text = results;
                    if (MessageBox.Show(ex.ToString(), "error",
                        MessageBoxButtons.OK) == DialogResult.OK)
                        System.Environment.Exit(0);
                }

            }
        }
        private void CrearEquipo()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/crear_equipo/?Marca=" + textBox4.Text + "&Modelo=" + textBox5.Text + "&SN=" + textBox6.Text;
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
                    label12.Text = results;
                }
                //Si no obtiene el token manda un mensaje de error
                catch (Exception ex)
                {
                    label12.Text = results;
                    if (MessageBox.Show(ex.ToString(), "error",
                        MessageBoxButtons.OK) == DialogResult.OK)
                        System.Environment.Exit(0);
                }

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Program.form1.form4.Show();
            userClosed = false;
            this.Close();
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrearEquipo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CrearCliente();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if(userClosed)
                if (MessageBox.Show("Desea salir de la aplicacion?", "Salir",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }

            // Then assume that X has been clicked and act accordingly.
        }
    }
}
