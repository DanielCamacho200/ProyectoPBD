using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Linq;


namespace POS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            label5.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            string word = textBox1.Text;
            string SDate = textBox2.Text;
            string results = string.Empty;
            JObject json=null;
            //Genera request
            string MoodysWebstring = @"http://proyecto-dev.us-east-1.elasticbeanstalk.com/getlogin/?User=" + word + "&Pwd=" + SDate;
            Uri MoodysWebAddress = new Uri(MoodysWebstring);
            HttpWebRequest request = WebRequest.Create(MoodysWebAddress) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "text/xml";
            HttpWebResponse response;
            //Solicita Request
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
                //Intenta obtener el token
                try
                {
                    json = JObject.Parse(results);
                    Console.WriteLine((string)json["Token"]);
                }
                //Si no obtiene el token manda un mensaje de error
                catch (Exception ex)
                {
                    label5.Text = results;
                    label5.Show();
                    Console.WriteLine(ex);
                }
                if (json != null)
                {
                    Form4 form4 = new Form4((string)json["Token"]);
                    form4.Show();
                    this.Hide();
                }
                
            }

 
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
                if (MessageBox.Show("Desea salir de la aplicacion?", "Salir",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.Environment.Exit(0);
                }
                else{
                    e.Cancel = true;
                }
        }
    }
}
