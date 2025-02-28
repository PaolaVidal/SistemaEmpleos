using SistemaEmpleos.ModuloConexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaEmpleos.ModuloUsuario
{
	internal class Usuario
	{
		private Conexion conexion = new Conexion(@"VALERIAV\MSSQLSERVER01", "Empleo3");
		private int id_usuario;
		private string email;
		private string contrasenia;
		private string tipo_usuario;

		public Usuario(int id_usuario, string email, string contrasenia, string tipo_usuario)
		{
			this.Id_usuario = id_usuario;
			this.Email = email;
			this.Contrasenia = contrasenia;
			this.Tipo_usuario = tipo_usuario;
		}

		public Usuario(string email, string contrasena, string tipo_usuario)
		{
			this.Email = email;
			this.Contrasenia = contrasenia;
			this.Tipo_usuario = tipo_usuario;
		}

		public Usuario()
		{
		}

		 
		public Conexion Conexion { get => conexion; } 
		public int Id_usuario { get => id_usuario; set => id_usuario = value; } 
		public string Email { get => email; set => email = value; }
		public string Contrasenia { get => contrasenia; set => contrasenia = value; }
		public string Tipo_usuario { get => tipo_usuario; set => tipo_usuario = value; }

		public void CrearUsuario()
		{
			try
			{
				using (SqlCommand command = new SqlCommand("agregar_usuario", Conexion.Conexion_))
				{
					command.CommandType = CommandType.StoredProcedure;

					
					command.Parameters.AddWithValue("@email", Email);
					command.Parameters.AddWithValue("@contrasenia", Contrasenia);
					command.Parameters.AddWithValue("@tipo_usuario", Tipo_usuario); 

					
					SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int)
					{
						Direction = ParameterDirection.ReturnValue
					};
					command.Parameters.Add(returnValue);

					Conexion.AbrirConexion();
					command.ExecuteNonQuery();

					
					int returnValueResult = Convert.ToInt32(command.Parameters["@ReturnValue"].Value);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error en CrearUsuario: {ex.Message}");
			}
		}



		
		public bool VerificarContrasena(string email, string contrasenia)
		{
			bool valido = false;
			string query = "SELECT COUNT(*) FROM Usuario WHERE email = @email AND contrasenia = @contrasenia";

			using (SqlConnection conn = new SqlConnection(Conexion.CadenaConexion))
			{
				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.AddWithValue("@email", email);
					cmd.Parameters.AddWithValue("@contrasenia", contrasenia);

					conn.Open();
					int count = (int)cmd.ExecuteScalar();
					valido = count > 0;
				}
			}

			return valido;
		}


		public bool ObtenerUsuario(string email)
		{
			try
			{
				using (SqlCommand command = new SqlCommand("obtener_usuario", Conexion.Conexion_))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@email", email);

					Conexion.AbrirConexion();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							Id_usuario = Convert.ToInt32(reader["id_usuario"]);
							Email = reader["email"].ToString();
							Contrasenia = reader["contrasenia"].ToString();
							Tipo_usuario = reader["tipo_usuario"].ToString();

							return true;
						}
						else
						{
							return false;
						}
					}
					Conexion.CerrarConexion();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error al obtener usuario: {ex.Message}");
				return false;
			}
		}

		


	}
}
