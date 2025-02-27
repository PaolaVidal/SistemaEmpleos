using SistemaEmpleos.ModuloConexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;
using SistemaEmpleos.Formularios;

namespace SistemaEmpleos.ModuloUsuario
{
	internal class Empresa:Usuario
	{
		private Conexion conexion = new Conexion(@"VALERIAV\MSSQLSERVER01", "Empleo2");
		private int id_empresa;
		private int id_usuario;
		private string nombre;
		private string direccion;
		private string telefono;
		private string sector;
		private string descripcion;


		public Empresa(int id_empresa, int id_usuario, string nombre, string direccion, string telefono, string sector, string descripcion)
		{
			this.Id_empresa = id_empresa;
			this.Id_usuario = id_usuario;
			this.Nombre = nombre;
			this.Direccion = direccion;
			this.Telefono = telefono;
			this.Sector = sector;
			this.Descripcion = descripcion;
		}

		public Empresa() { }

		public Conexion Conexion { get => conexion; }
		public int Id_empresa { get => id_empresa; set => id_empresa = value; }
		public int Id_usuario { get => id_usuario; set => id_usuario = value; }
		public string Nombre { get => nombre; set => nombre = value; }
		public string Direccion { get => direccion; set => direccion = value; }
		public string Telefono { get => telefono; set => telefono = value; }
		public string Sector { get => sector; set => sector = value; }
		public string Descripcion { get => descripcion; set => descripcion = value; }

		public void CrearUsuarioEmpresa(string nombre, string direccion, string telefono, string sector, string descripcion, TextBox txtEmail, Form RegistroEmpresa)
		{
			try
			{
				int nuevoIdUsuario = -1;
				using (SqlCommand command = new SqlCommand("agregar_usuario", Conexion.Conexion_))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@email", Email);
					command.Parameters.AddWithValue("@contrasenia", Contrasenia);
					command.Parameters.AddWithValue("@tipo_usuario", "E"); 

					SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int)
					{
						Direction = ParameterDirection.Output
					};
					command.Parameters.Add(returnValue);

					Conexion.AbrirConexion();
					command.ExecuteNonQuery();

					nuevoIdUsuario = Convert.ToInt32(command.Parameters["@ReturnValue"].Value);

					if (nuevoIdUsuario <= 0)
					{
						throw new Exception("El ID de usuario no fue generado correctamente.");
					}
				}

				if (nuevoIdUsuario > 0)
				{
					using (SqlCommand command = new SqlCommand("agregar_empresa", Conexion.Conexion_))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@id_usuario", nuevoIdUsuario);
						command.Parameters.AddWithValue("@nombre", nombre);
						command.Parameters.AddWithValue("@direccion", direccion);
						command.Parameters.AddWithValue("@telefono", telefono);
						command.Parameters.AddWithValue("@sector_empresa", sector);
						command.Parameters.AddWithValue("@descripcion_empresa", descripcion);

						command.ExecuteNonQuery();
					}

					MessageBox.Show("¡Empresa registrada con éxito!", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

					RegistroEmpresa.Hide(); 

					Registro1 registro1 = new Registro1();
					registro1.ShowDialog(); 

					RegistroEmpresa.Close();


				}
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2627)
				{
					MessageBox.Show("El correo ya está en uso, prueba con un correo diferente.", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtEmail.Clear();
					txtEmail.Focus();
				}
				else
				{
					MessageBox.Show($"Error en CrearUsuarioEmpresa: {ex.Message}");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error en CrearUsuarioEmpresa: {ex.Message}");
			}
			finally
			{
				Conexion.CerrarConexion();
			}
		}


	}


	
}
