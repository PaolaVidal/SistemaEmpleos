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
			verOfertasEmpleos verOfertasEmpleos = new verOfertasEmpleos();
			//VerOfertasEmpleoEmpresa verOfertasEmpleoEmpresa = new VerOfertasEmpleoEmpresa();
			//Application.Run(new VerOfertasEmpleoEmpresa());


			Registro1 registro1 = new Registro1();
			Application.Run(new Registro1());

			//Form_Perfil_Postulante form_Perfil_Postulante = new Form_Perfil_Postulante();
			//Application.Run(new Form_Perfil_Postulante());


		}
	}
}
