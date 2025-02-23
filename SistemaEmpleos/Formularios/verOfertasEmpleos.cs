using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaEmpleos.LogicaVerOfertasEmpleo; // Importamos la clase

namespace SistemaEmpleos.Formularios
{
    public partial class verOfertasEmpleos : Form
    {
        private verOfertaEmpleoCRUD crud; // Instancia de VerPostulanteCRUD
        public verOfertasEmpleos()
        {
            InitializeComponent();
            // Instanciamos la conexión con los parámetros del servidor y base de datos
            crud = new verOfertaEmpleoCRUD(@"(Localdb)\rodolfo server", "Empleo");
            crud.VerificarConexion(); // Verificamos la conexión
        }

        private void verOfertasEmpleos_Load(object sender, EventArgs e)
        {

        }
    }
}
