using SistemaEmpleos.Clases;
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
	public partial class FormHome : Form
	{
		public FormHome()
		{
			InitializeComponent();
		}

		private void FormHome_Load(object sender, EventArgs e)
		{

		}

		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void panel3_Paint(object sender, PaintEventArgs e)
		{

		}

		private void panel4_Paint(object sender, PaintEventArgs e)
		{

		}

		//no da eror porque aca no hay evento buton 2
        private void btnVerMisPostulaciones_Click(object sender, EventArgs e)
        {
			int id_usuario = Obj_Usuario.id_usuario;
            verOfertasEmpleos verOfertasEmpleos = new verOfertasEmpleos(id_usuario);
        }

		private void button3_Click(object sender, EventArgs e)
		{
			Registro1 formRegistro1 = new Registro1();
			this.Close();
			formRegistro1.Show();
		}
	}
}
