using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaEmpleos.LogicaVerPostulante; // Importamos la clase

namespace SistemaEmpleos.Formularios
{
    public partial class verPostulantes : Form
    {
        private VerPostulanteCRUD crud; // Instancia de VerPostulanteCRUD

        public verPostulantes()
        {
            InitializeComponent();

            // Instanciamos la conexión con los parámetros del servidor y base de datos
            crud = new VerPostulanteCRUD(@"(Localdb)\rodolfo server", "Empleo");

            // Ahora sí podemos llamar a VerificarConexion
            crud.VerificarConexion();
            crud.LlenarPostulantes(this.dataGridView1);

            this.cbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void verPostulantes_Load(object sender, EventArgs e)
        {

        }
    }
}
