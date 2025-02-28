using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaEmpleos.Formularios
{
	public partial class Form_Perfil_Postulante : Form
	{
		public Form_Perfil_Postulante()
		{
			InitializeComponent();
			
		}

		private void button2_Click(object sender, EventArgs e)
		{
			
		}

		private void atras_Click(object sender, EventArgs e)
		{
			FormHome homePostulante = new FormHome(); 
			homePostulante.Show(); 
			this.Hide(); 

		}

		private void Form_Perfil_Postulante_Load(object sender, EventArgs e)
		{

		}
	}
}
