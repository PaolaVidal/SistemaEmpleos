using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaEmpleos.ModuloConexion; // Asegúrate de tener la clase Conexion

namespace SistemaEmpleos.LogicaVerOfertasEmpleo
{
    internal class verOfertaEmpleoCRUD
    {
        private Conexion conexion;
        private int paginaActual = 0;
        private const int registrosPorPagina = 4; // Muestra 4 ofertas por página

        public verOfertaEmpleoCRUD(string servidor, string baseDatos)
        {
            conexion = new Conexion(servidor, baseDatos);
        }

        public void VerificarConexion()
        {
            try
            {
                if (conexion.AbrirConexion())
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
        /// Obtiene una lista de ofertas de trabajo para una empresa específica con paginación.
        /// </summary>
        public List<(string titulo, DateTime fechaPublicacion)> ObtenerOfertasPorEmpresa(int idEmpresa, int pagina)
        {
            List<(string, DateTime)> ofertas = new List<(string, DateTime)>();
            try
            {
                if (conexion.AbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT titulo, fecha_publicacion FROM OfertaEmpleo WHERE Id_empresa = @IdEmpresa ORDER BY fecha_publicacion DESC OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY", conexion.Conexion_))
                    {
                        cmd.Parameters.AddWithValue("@IdEmpresa", idEmpresa);
                        cmd.Parameters.AddWithValue("@Offset", pagina * registrosPorPagina);
                        cmd.Parameters.AddWithValue("@Limit", registrosPorPagina);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string titulo = reader.GetString(0);
                                DateTime fecha = reader.GetDateTime(1);
                                ofertas.Add((titulo, fecha));
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
        /// Avanza a la siguiente página de ofertas.
        /// </summary>
        public List<(string titulo, DateTime fechaPublicacion)> SiguientePagina(int idEmpresa)
        {
            paginaActual++;
            return ObtenerOfertasPorEmpresa(idEmpresa, paginaActual);
        }

        /// <summary>
        /// Retrocede a la página anterior de ofertas.
        /// </summary>
        public List<(string titulo, DateTime fechaPublicacion)> PaginaAnterior(int idEmpresa)
        {
            if (paginaActual > 0) paginaActual--;
            return ObtenerOfertasPorEmpresa(idEmpresa, paginaActual);
        }
    }
}
