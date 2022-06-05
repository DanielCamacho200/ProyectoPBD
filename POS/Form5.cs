using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace POS
{
    public partial class Form5 : Form
    {
        String user;
        String token;
        bool userClosed = true;
        List<Clientes> listaCl;
        List<Equipos> listaEq;
        List<tempId> ten;
        int clid;
        int eqid;
        public Form5(string Token, string User)
        {
            user = User;
            token = Token;
            InitializeComponent();
            Clientesg();
            Equiposg();
            initData();
            getUserId();
        }
        private void initData()
        {
            comboBox2.DataSource = new BindingSource(listaCl, null);
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "Id";

            comboBox3.DataSource = new BindingSource(listaEq, null);
            comboBox3.DisplayMember = "SN";
            comboBox3.ValueMember = "Id";

        }
        private void Clientesg()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/clientes/";
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
                    listaCl = JsonConvert.DeserializeObject<List<Clientes>>(results);
                    Console.WriteLine(results);
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
        private void Equiposg()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/equipos/";
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
                    listaEq = JsonConvert.DeserializeObject<List<Equipos>>(results);
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
        public class tempId
        {
            public int Id { get; set; }
        }
        private void getUserId()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/buscar_usuario/?username="+user;
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
                    ten = JsonConvert.DeserializeObject<List<tempId>>(results);
                    
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
        private void CrearSop()
        {
            string results = string.Empty;
            //Genera request
            string url = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/crear_soporte/?Problema="+richTextBox2.Text+"&Comentarios="+richTextBox1.Text+"&Tecnico_id="+ ten[0].Id + "&Equipo_id="+eqid+"&Cliente_id="+clid;
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
                    MessageBox.Show(results, "aviso");
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
        private void Cerrar()
        {
            Program.form1.form4.Show();
            userClosed = false;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clientes x = (Clientes)comboBox2.SelectedItem;
            foreach (Clientes cl in listaCl)
            {
                if (x.Id == cl.Id)
                {
                    clid = cl.Id;
                    label5.Text = x.Nombre;
                    label6.Text = x.Phone.ToString();
                    label7.Text = x.Email;
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Equipos x = (Equipos)comboBox3.SelectedItem;
            foreach (Equipos eq in listaEq)
            {
                if (x.Id == eq.Id)
                {
                    eqid = eq.Id;
                    label9.Text = eq.Marca;
                    label10.Text = eq.Modelo;
                    label8.Text = eq.SN;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "" || richTextBox2.Text == "")
                MessageBox.Show("Porfavor llene el formulario", "Error");
            else
            {
                CrearSop();
                Cerrar();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Cerrar();
        }
    }
}
