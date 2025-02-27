using SistemaEmpleos.Formularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaEmpleos
{
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
            verPostulantes verPostulantes = new verPostulantes();
			verOfertasEmpleos verOfertasEmpleos = new verOfertasEmpleos();
			VerOfertasEmpleoEmpresa verOfertasEmpleoEmpresa = new VerOfertasEmpleoEmpresa();
           // Application.Run(new VerOfertasEmpleoEmpresa());


			Registro1 registro1 = new Registro1();
			Application.Run(new Registro1());


		}
	}
}
