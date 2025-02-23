using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaEmpleos.ModuloConexion; // Asegúrate de que la clase Conexion esté aquí

namespace SistemaEmpleos.LogicaVerOfertasEmpleo
{
    internal class verOfertaEmpleoCRUD
    {
        private Conexion conexion;  // Instancia de la clase Conexion
        private string connectionString;

        public verOfertaEmpleoCRUD(string servidor, string baseDatos)
        {
            connectionString = $"Server={servidor};Database={baseDatos};Integrated Security=True;";
            conexion = new Conexion(servidor, baseDatos); // Asegúrate de que el constructor de la clase Conexion acepte estos parámetros
        }

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

    }
}
