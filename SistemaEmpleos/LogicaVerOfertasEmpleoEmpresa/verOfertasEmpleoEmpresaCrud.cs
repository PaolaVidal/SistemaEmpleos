using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaEmpleos.ModuloConexion; // Asegúrate de que la clase Conexion esté aquí

namespace SistemaEmpleos.LogicaVerPostulante
{
    internal class verOfertasEmpleoEmpresaCrud
    {
        private Conexion conexion;  // Instancia de la clase Conexion
        private string connectionString;
        private int paginaActual = 0;
        private const int registrosPorPagina = 4; // Muestra 4 ofertas por página

        public verOfertasEmpleoEmpresaCrud(string servidor, string baseDatos)
        {
            connectionString = $"Server={servidor};Database={baseDatos};Integrated Security=True;";
            conexion = new Conexion(servidor, baseDatos); // Asegúrate de que el constructor de la clase Conexion acepte estos parámetros
        }

        // Método para verificar la conexión
        public void VerificarConexion()
        {
            try
            {
                if (conexion.AbrirConexion()) // Verificamos la conexión utilizando la instancia
                {
                    MessageBox.Show("Conexión establecida correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conexion.CerrarConexion();
                }
                else
                {
                    MessageBox.Show("No se pudo conectar a la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar la conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene una lista de ofertas publicadas por una empresa, con paginación.
        /// </summary>
        public List<verOfertasEmpleoEmpresa> ObtenerOfertasPorEmpresa(int idEmpresa, int pagina)
        {
			List<verOfertasEmpleoEmpresa> ofertas = new List<verOfertasEmpleoEmpresa>();
			try
			{
				if (conexion.AbrirConexion())
				{
					int offset = pagina * registrosPorPagina;
					Console.WriteLine("Offset: " + offset);
					Console.WriteLine("Limit: " + registrosPorPagina);

					using (SqlCommand cmd = new SqlCommand(@"
                SELECT oe.id_oferta_empleo, oe.titulo, oe.fecha_publicacion, oe.estado, oe.id_empresa 
                FROM OfertaEmpleo oe
                WHERE oe.id_empresa = @IdEmpresa
                ORDER BY oe.fecha_publicacion DESC
                ", conexion.Conexion_)) //upset y limit quita2, más ofertas o error devolver
					{
						cmd.Parameters.AddWithValue("@IdEmpresa", idEmpresa);
						//cmd.Parameters.AddWithValue("@Offset", offset);
						//cmd.Parameters.AddWithValue("@Limit", registrosPorPagina);

						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								verOfertasEmpleoEmpresa oferta = new verOfertasEmpleoEmpresa
								{
									Id = reader.GetInt32(0),
									Titulo = reader.GetString(1),
									FechaPublicacion = reader.GetDateTime(2),
									Estado = reader.GetString(3),
									IdEmpresa = reader.GetInt32(4)
								};
								ofertas.Add(oferta);
							}
						}
					}
					conexion.CerrarConexion();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al obtener ofertas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return ofertas;
		}

        /// <summary>
        /// Avanza a la siguiente página de ofertas publicadas por la empresa.
        /// </summary>
        public List<verOfertasEmpleoEmpresa> SiguientePagina(int idEmpresa)
        {
            paginaActual++;
            return ObtenerOfertasPorEmpresa(idEmpresa, paginaActual);
        }

        /// <summary>
        /// Retrocede a la página anterior de ofertas publicadas por la empresa.
        /// </summary>
        public List<verOfertasEmpleoEmpresa> PaginaAnterior(int idEmpresa)
        {
            if (paginaActual > 0) paginaActual--;
            return ObtenerOfertasPorEmpresa(idEmpresa, paginaActual);
        }
    }





}
