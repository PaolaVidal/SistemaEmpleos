using SistemaEmpleos.Formularios;
using SistemaEmpleos.LogicaVerPostulante;
using SistemaEmpleos.ModuloConexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaEmpleos.LogicaVerPostulantes
{
    internal class verPostulantesCRUD
    {
        private Conexion conexion;
        private string connectionString;
        private int paginaActual = 0;
        private const int registrosPorPagina = 4;

        public verPostulantesCRUD(string servidor, string baseDatos)
        {
            connectionString = $"Server={servidor};Database={baseDatos};Integrated Security=True;";
            conexion = new Conexion(servidor, baseDatos);
        }

        /// <summary>
        /// Verifica la conexión con la base de datos.
        /// </summary>
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
                MessageBox.Show($"Error al verificar la conexión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<verPostulantes> ObtenerPostulantesPorOferta(int idOferta)
        {
            List<verPostulantes> postulantes = new List<verPostulantes>();

            try
            {
                if (conexion.AbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT 
                            p.id_postulante, 
                            p.nombre, 
                            u.email AS email_usuario, 
                            c.telefono
                        FROM Postulante p
                        INNER JOIN Usuario u ON p.id_usuario = u.id_usuario
                        INNER JOIN Contactos c ON u.id_usuario = c.id_usuario
                        INNER JOIN OfertaCandidatos oc ON p.id_usuario = oc.id_usuario
                        WHERE oc.id_oferta_empleo = @IdOferta", conexion.Conexion_))
                    {
                        cmd.Parameters.AddWithValue("@IdOferta", idOferta);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                verPostulantes postulante = new verPostulantes
                                {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Telefono = reader.GetString(3)
                                };
                                postulantes.Add(postulante);
                            }
                        }
                    }
                    conexion.CerrarConexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener postulantes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return postulantes;
        }

        public List<verPostulantes> ObtenerPostulantesPorOferta(int idOferta, int pagina)
        {
            List<verPostulantes> postulantes = new List<verPostulantes>();

            try
            {
                if (conexion.AbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(@"
            SELECT 
                p.id_postulante, 
                p.nombre, 
                u.email AS email_usuario, 
                c.telefono
            FROM OfertaCandidatos po
            INNER JOIN Postulante p ON po.id_usuario = p.id_usuario  -- Relacionamos por id_usuario
            INNER JOIN Usuario u ON p.id_usuario = u.id_usuario
            LEFT JOIN Contactos c ON u.id_usuario = c.id_usuario
            WHERE po.id_oferta_empleo = @IdOferta
            ORDER BY p.nombre
            OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY", conexion.Conexion_))
                    {
                        cmd.Parameters.AddWithValue("@IdOferta", idOferta);
                        cmd.Parameters.AddWithValue("@Offset", pagina * registrosPorPagina);
                        cmd.Parameters.AddWithValue("@Limit", registrosPorPagina);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                verPostulantes postulante = new verPostulantes
                                {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Telefono = reader.IsDBNull(3) ? null : reader.GetString(3) // Manejo de nulos en teléfono
                                };
                                postulantes.Add(postulante);
                            }
                        }
                    }
                    conexion.CerrarConexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener postulantes 2: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return postulantes;
        }


        public List<verPostulantes> SiguientePagina(int idOferta)
        {
            paginaActual++;
            return ObtenerPostulantesPorOferta(idOferta, paginaActual);
        }

        public List<verPostulantes> PaginaAnterior(int idOferta)
        {
            if (paginaActual > 0) paginaActual--;
            return ObtenerPostulantesPorOferta(idOferta, paginaActual);
        }
    }
}
