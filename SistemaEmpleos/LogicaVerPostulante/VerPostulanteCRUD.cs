using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaEmpleos.ModuloConexion; // Asegúrate de que la clase Conexion esté aquí

namespace SistemaEmpleos.LogicaVerPostulante
{
    internal class VerPostulanteCRUD
    {
        private Conexion conexion;  // Instancia de la clase Conexion
        private string connectionString;

        // Variable global para almacenar los datos
        private DataTable dtPostulantes;

        public VerPostulanteCRUD(string servidor, string baseDatos)
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

        // Método para llenar el DataGridView con los postulantes inscritos en una oferta
        public void GetAllPostulantes(DataGridView dgv, int idOfertaEmpleo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    p.nombre, 
                    c.telefono,  
                    COALESCE(ci.habilidades, 'No registradas') AS habilidades, 
                    COALESCE(ci.idiomas, 'No registrados') AS idiomas, 
                    COALESCE(fa.titulo, 'Sin formación académica') AS titulo, 
                    COALESCE(fa.institucion, 'No especificada') AS institucion, 
                    COALESCE(CONVERT(varchar, fa.fecha, 103), 'No registrada') AS fecha, 
                    COALESCE(ep.puesto, 'Sin experiencia laboral') AS puesto, 
                    COALESCE(ep.empresa, 'No especificada') AS empresa
                FROM Postulante p
                LEFT JOIN Contactos c ON p.id_usuario = c.id_usuario
                LEFT JOIN Curriculum ci ON p.id_postulante = ci.id_postulante
                LEFT JOIN FormacionAcademica fa ON ci.id_curriculum = fa.id_curriculum
                LEFT JOIN ExperienciaProfesional ep ON ci.id_curriculum = ep.id_curriculum
                LEFT JOIN OfertaCandidatos oc ON p.id_usuario = oc.id_usuario
                WHERE oc.id_oferta_empleo = @idOfertaEmpleo OR oc.id_oferta_empleo IS NULL";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idOfertaEmpleo", idOfertaEmpleo);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;  // Llenamos el DataGridView con los datos obtenidos

                    // Ajustamos las columnas al contenido
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    // Ajustamos las filas al contenido
                    dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar postulantes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FiltrarPostulantes(DataGridView dgv, string filtro, string columna, int idOfertaEmpleo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(columna) || string.IsNullOrWhiteSpace(filtro))
                {
                    GetAllPostulantes(dgv, idOfertaEmpleo);
                    return;
                }

                // Lista de columnas permitidas
                string[] columnasPermitidas = { "habilidades", "idiomas", "nombre", "direccion", "telefono", "email", "titulo", "institucion", "puesto", "empresa" };

                // Validar que la columna ingresada es válida
                if (!Array.Exists(columnasPermitidas, c => c.Equals(columna, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Columna no permitida para el filtro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Si idOfertaEmpleo no es válido, salir de la función
                if (idOfertaEmpleo <= 0)
                {
                    MessageBox.Show("ID de oferta de empleo no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Usar parámetro dinámico para la columna
                    string query = $@"
            SELECT p.nombre, p.direccion, c.telefono, c.email, 
                   ci.habilidades, ci.idiomas, fa.titulo, fa.institucion, fa.fecha, 
                   ep.puesto, ep.empresa
            FROM Postulante p
            JOIN Contactos c ON p.id_usuario = c.id_usuario
            JOIN Curriculum ci ON p.id_postulante = ci.id_postulante
            JOIN FormacionAcademica fa ON ci.id_curriculum = fa.id_curriculum
            JOIN ExperienciaProfesional ep ON ci.id_curriculum = ep.id_curriculum
            JOIN OfertaCandidatos oc ON p.id_usuario = oc.id_usuario
            WHERE oc.id_oferta_empleo = @idOfertaEmpleo
            AND {columna} LIKE @filtro";  // Evitamos funciones SQL dentro de la consulta

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idOfertaEmpleo", idOfertaEmpleo);
                        cmd.Parameters.AddWithValue("@filtro", $"%{filtro.Trim().ToLower()}%");

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv.DataSource = dt;

                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar postulantes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
