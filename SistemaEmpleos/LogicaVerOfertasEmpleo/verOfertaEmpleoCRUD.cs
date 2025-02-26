using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaEmpleos.LogicaVerPostulante;
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
        /// Obtiene una lista de ofertas en las que un candidato se ha inscrito, con paginación.
        /// </summary>
        public List<OfertaEmpleo> ObtenerOfertasPorCandidato(int idUsuario, int pagina)
        {
            List<OfertaEmpleo> ofertas = new List<OfertaEmpleo>();
            try
            {
                if (conexion.AbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT oe.titulo, oe.fecha_publicacion, oe.estado 
                        FROM OfertaCandidatos oc 
                        JOIN OfertaEmpleo oe ON oc.id_oferta_empleo = oe.id_oferta_empleo 
                        WHERE oc.id_usuario = @IdUsuario 
                        ORDER BY oe.fecha_publicacion DESC 
                        OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY", conexion.Conexion_))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@Offset", pagina * registrosPorPagina);
                        cmd.Parameters.AddWithValue("@Limit", registrosPorPagina);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OfertaEmpleo oferta = new OfertaEmpleo
                                {
                                    Titulo = reader.GetString(0),
                                    FechaPublicacion = reader.GetDateTime(1),
                                    Estado = reader.GetString(2) // Aquí se obtiene correctamente la columna 'estado' de la tabla 'OfertaEmpleo'
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
        /// Avanza a la siguiente página de ofertas en las que el candidato está inscrito.
        /// </summary>
        public List<OfertaEmpleo> SiguientePagina(int idUsuario)
        {
            paginaActual++;
            return ObtenerOfertasPorCandidato(idUsuario, paginaActual);
        }

        /// <summary>
        /// Retrocede a la página anterior de ofertas en las que el candidato está inscrito.
        /// </summary>
        public List<OfertaEmpleo> PaginaAnterior(int idUsuario)
        {
            if (paginaActual > 0) paginaActual--;
            return ObtenerOfertasPorCandidato(idUsuario, paginaActual);
        }
    }
}
