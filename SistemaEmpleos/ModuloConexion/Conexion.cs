using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpleos.ModuloConexion
{
	internal class Conexion
	{
		private SqlConnection conexion;
		private string cadenaConexion;

		public Conexion(string servidor, string baseDatos)
		{
			// Cadena de conexión para SQL Server
			cadenaConexion = $"Data Source={servidor};Initial Catalog={baseDatos}; Integrated Security=True";
			conexion = new SqlConnection(cadenaConexion);
		}

		public SqlConnection Conexion_ { get => conexion; set => conexion = value; }

		public bool AbrirConexion()
		{
			try
			{
				conexion.Open();
				Console.WriteLine("Conexión establecida correctamente.");
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
				return false;
			}
		}

		public void CerrarConexion()
		{
			conexion.Close();
			Console.WriteLine("Conexión cerrada correctamente.");
		}
	}
}
