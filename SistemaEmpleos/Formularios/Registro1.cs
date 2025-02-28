using SistemaEmpleos.Clases;
using SistemaEmpleos.Formularios;
using SistemaEmpleos.ModuloConexion;
using SistemaEmpleos.ModuloUsuario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaEmpleos
{
	public partial class Registro1 : Form
	{
		public Registro1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{

			string EmailTxt = txt_email.Text.Trim();
			string contrasenaTxt = txt_Contrasena.Text.Trim();

			Usuario user = new Usuario();

			if (user.ObtenerUsuario(EmailTxt))
			{
				if (user.VerificarContrasena(EmailTxt, contrasenaTxt))
				{
					MessageBox.Show("Ingreso exitoso");
					this.Hide();
					Obj_Usuario.id_usuario = user.Id_usuario;  // Se asigna el ID del usuario

					// Verificar tipo de usuario
					if (user.Tipo_usuario == "P")
					{
						FormHome formHomePostulante = new FormHome();
						formHomePostulante.Show();
					}
					else if (user.Tipo_usuario == "E")
					{
						Obj_Usuario.id_empresa = user.Id_usuario;  // Asignamos el ID de la empresa
						VerOfertasEmpleoEmpresa formEmpresa = new VerOfertasEmpleoEmpresa(Obj_Usuario.id_empresa);
						formEmpresa.Show();
					}
					else
					{
						MessageBox.Show("Tipo de usuario no reconocido.");
						this.Show();
					}
				}
				else
				{
					MessageBox.Show("Correo o contraseña incorrectos");
				}
			}
			else
			{
				MessageBox.Show("El correo no está registrado. Por favor, regístrate antes de iniciar sesión.");
			}
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			RegistroNormal registroForm = new RegistroNormal();
			this.Hide();
			registroForm.Show();
			

			
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

        private void Registro1_Load(object sender, EventArgs e)
        {

        }
    }
}
