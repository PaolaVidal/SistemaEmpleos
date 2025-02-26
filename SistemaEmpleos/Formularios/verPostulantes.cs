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
            crud.GetAllPostulantes(this.dataGridView1,2);

            this.cbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFiltro.Items.Add("nombre");
            cbFiltro.Items.Add("telefono");
            cbFiltro.Items.Add("habilidades");
            cbFiltro.Items.Add("idiomas");
            cbFiltro.Items.Add("titulo");
            cbFiltro.Items.Add("institucion");
            cbFiltro.Items.Add("fecha");
            cbFiltro.Items.Add("puesto");
            cbFiltro.Items.Add("empresa");
            cbFiltro.SelectedIndex = 0; // Seleccionar el primer filtro por defecto
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

        private void button1_Click(object sender, EventArgs e)
        {
            string filtro = cbFiltro.SelectedItem?.ToString();  // Obtenemos el filtro seleccionado como string
            string textoBusqueda = txtDatoFiltrar.Text;  // Obtenemos el texto ingresado en el TextBox

        }

        private void cbFiltro_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            crud.GetAllPostulantes(this.dataGridView1, 2);
        }

        private void txtDatoFiltrar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Bloquea la tecla si no cumple la condición
            }
        }
    }
}
