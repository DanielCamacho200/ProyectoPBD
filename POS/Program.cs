using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public class Soporte
    {
        public string Id { get; set; }
        public DateTime Inicio { get; set; }
        public string Cliente { get; set; }
        public string Modelo { get; set; }
        public string SN { get; set; }
        public string Problema { get; set; }
        public string Tecnico { get; set; }
    }
    public class Soporte_info
    {
        public int Id { get; set; }
        public string Problema { get; set; }
        public string Comentarios { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Final { get; set; }
        public int Solucionado { get; set; }
        public int Cliente_id { get; set; }
        public int Equipo_id { get; set; }
        public int Tecnico_id { get; set; }
        public string Comentarios_cierre { get; set; }
        public int Soporte_cancelado { get; set; }

    }
    public class Cliente_info
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
    }
    public class Equipo_info
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string SN { get; set; }
    }

    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Form1().Show(); 
            Application.Run();
        }
    }
}
