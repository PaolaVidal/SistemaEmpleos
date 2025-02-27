using SistemaEmpleos.ModuloConexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using SistemaEmpleos.Formularios;

namespace SistemaEmpleos.ModuloUsuario
{
	internal class Postulante:Usuario
	{
		private Conexion conexion = new Conexion(@"VALERIAV\MSSQLSERVER01", "Empleo2");
		private int id_postulante;
		private int id_usuario;
		private string nombre;
		private string direccion;
		private string telefono;


		public Postulante(int id_postulante, int id_usuario, string nombre, string direccion, string telefono)
		{
			this.Id_postulante = id_postulante;
			this.Id_usuario = id_usuario;
			this.Nombre = nombre;
			this.Direccion = direccion;
			this.telefono = telefono;
		}
		public Postulante() { }

		// Getters y Setters 
		public Conexion Conexion { get => conexion; }
		public int Id_postulante { get => id_postulante; set => id_postulante = value; }
		public int Id_usuario { get => id_usuario; set => id_usuario = value; }
		public string Nombre { get => nombre; set => nombre = value; }
		public string Direccion { get => direccion; set => direccion = value; }
		public string Telefono { get => telefono; set => telefono = value; }


		public void CrearUsuarioPostulante(string nombre, string direccion, TextBox txtEmail, string telefono, Form RegistroNormal)
		{
			try
			{
				int nuevoIdUsuario = -1;
				using (SqlCommand command = new SqlCommand("agregar_usuario", Conexion.Conexion_))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@email", Email);
					command.Parameters.AddWithValue("@contrasenia", Contrasenia);
					command.Parameters.AddWithValue("@tipo_usuario", "P");

					SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int)
					{
						Direction = ParameterDirection.Output
					};
					command.Parameters.Add(returnValue);

					Conexion.AbrirConexion();
					command.ExecuteNonQuery();

					nuevoIdUsuario = Convert.ToInt32(command.Parameters["@ReturnValue"].Value);

					// Verificar que el id de usuario sea bueno
					if (nuevoIdUsuario <= 0)
					{
						throw new Exception("El ID de usuario no fue generado correctamente.");
					}
				}

				if (nuevoIdUsuario > 0)
				{
					using (SqlCommand command = new SqlCommand("agregar_postulante", Conexion.Conexion_))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@id_usuario", nuevoIdUsuario);
						command.Parameters.AddWithValue("@nombre", nombre);
						command.Parameters.AddWithValue("@direccion", direccion);
						command.Parameters.AddWithValue("@telefono", telefono);

						command.ExecuteNonQuery();
					}

					MessageBox.Show("¡Cuenta creada con éxito!", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

					RegistroNormal.Hide();
					Registro1 registro1 = new Registro1();
					registro1.ShowDialog();




				}
			}
			catch (SqlException ex)
			{
				
				if (ex.Number == 2627)
				{
					MessageBox.Show("El correo ya está enlazado a una cuenta, prueba con un correo diferente para registrarte.", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtEmail.Clear();
					txtEmail.Focus();
				}
				else
				{
					MessageBox.Show($"Error en CrearUsuarioPostulante: {ex.Message}");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error en CrearUsuarioPostulante: {ex.Message}");
			}
			finally
			{
				Conexion.CerrarConexion();
			}
		}
	}
}
