using SistemaEmpleos.ModuloUsuario;
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
	public partial class RegistroNormal : Form

	{

		private List<string> ValidarContrasenia(string contrasenia)
		{
			List<string> errores = new List<string>();

			if (contrasenia.Length < 8)
				errores.Add("Debe tener al menos 8 caracteres.");

			if (!contrasenia.Any(char.IsUpper))
				errores.Add("Debe contener al menos una letra mayúscula.");

			if (!contrasenia.Any(char.IsDigit))
				errores.Add("Debe contener al menos un número.");

			if (!contrasenia.Any(ch => !char.IsLetterOrDigit(ch))) 
				errores.Add("Debe contener al menos un carácter especial (Ejemplo: !@#$%^&*).");

			return errores;
		}

		public RegistroNormal()
		{
			InitializeComponent();
		
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		
		private void btnRegistrarseP_Click(object sender, EventArgs e)
		{

			string nombre = txtNombre.Text.Trim();
			string email = txtEmail.Text.Trim();
			string contrasenia = txtContrasena.Text.Trim();
			string confirmarContrasenia = txtConfirmarContrasena.Text.Trim();
			string direccion = txtDireccion.Text.Trim();
			string telefono = txtTelefono.Text.Trim();

			
			List<string> camposFaltantes = new List<string>();

			
			if (string.IsNullOrEmpty(nombre)) camposFaltantes.Add("Nombre");
			if (string.IsNullOrEmpty(email)) camposFaltantes.Add("Email");
			if (string.IsNullOrEmpty(contrasenia)) camposFaltantes.Add("Contraseña");
			if (string.IsNullOrEmpty(confirmarContrasenia)) camposFaltantes.Add("Confirmar Contraseña");
			if (string.IsNullOrEmpty(direccion)) camposFaltantes.Add("Dirección");
			if (string.IsNullOrEmpty(telefono)) camposFaltantes.Add("Teléfono");

			
			if (camposFaltantes.Count > 0)
			{
				MessageBox.Show($"Por favor, completa los siguientes campos:\n- {string.Join("\n- ", camposFaltantes)}",
					"Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			
			List<string> erroresContrasenia = ValidarContrasenia(contrasenia);

			if (erroresContrasenia.Count > 0)
			{
				MessageBox.Show($"La contraseña no cumple con los siguientes requisitos:\n- {string.Join("\n- ", erroresContrasenia)}",
					"Contraseña Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtContrasena.Clear();
				txtConfirmarContrasena.Clear();
				txtContrasena.Focus();
				return;
			}

			
			if (contrasenia != confirmarContrasenia)
			{
				MessageBox.Show("Las contraseñas no coinciden. Por favor, verifica e inténtalo de nuevo.",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtContrasena.Clear();
				txtConfirmarContrasena.Clear();
				txtContrasena.Focus();
				return;
			}

			
			Postulante nuevoPostulante = new Postulante
			{
				Email = email,
				Contrasenia = contrasenia
			};
			nuevoPostulante.CrearUsuarioPostulante(nombre, direccion, txtEmail, telefono, this);

		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			

			RegistroEmpresa registroEmpresaForm = new RegistroEmpresa();
			this.Hide(); 
			registroEmpresaForm.ShowDialog();
			

		}

		private void label9_Click(object sender, EventArgs e)
		{

		}

		private void txtConfirmarContrasena_TextChanged(object sender, EventArgs e)
		{

		}

		private void label6_Click(object sender, EventArgs e)
		{

		}

		private void btnAnterior_Click(object sender, EventArgs e)
		{
			Registro1 formRegistro1 = new Registro1(); 
			formRegistro1.Show();  
			this.Close();

		}
	}
}
