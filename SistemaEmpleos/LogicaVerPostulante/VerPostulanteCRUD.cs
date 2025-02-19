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

        // Método para llenar el DataGridView con los datos de postulantes
        public void LlenarPostulantes(DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_postulante, nombre, direccion FROM Postulante";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar postulantes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
