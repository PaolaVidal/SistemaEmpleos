using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaEmpleos.LogicaVerPostulante;
using SistemaEmpleos.LogicaVerPostulantes; // Importamos la clase

namespace SistemaEmpleos.Formularios
{
    public partial class FormverPostulantes : Form
    {
        private verPostulantesCRUD crud; // Instancia de VerPostulanteCRUD
        private int idOferta; // ID de la oferta para la que se listan postulantes
        private int paginaActual; // Para controlar la paginación


        public FormverPostulantes(int idOferta)
        {
            InitializeComponent();
            this.idOferta = idOferta;
            crud = new verPostulantesCRUD(@"(Localdb)\rodolfo server", "Empleo"); // Ejemplo de conexión
            crud.VerificarConexion(); // Verificar conexión
            CargarPostulantes();

            
            lbNombrePostulante.Click += lbNombrePostulante_Click;
            lbNombrePostulante2.Click += lbNombrePostulante_Click;
            lbNombrePostulante3.Click += lbNombrePostulante_Click;
            lbNombrePostulante4.Click += lbNombrePostulante_Click;
        }

        private void CargarPostulantes()
        {
            var postulantes = crud.ObtenerPostulantesPorOferta(idOferta, paginaActual); // Obtener postulantes según la página actual
            ActualizarInterfaz(postulantes);
        }

        private void ActualizarInterfaz(List<SistemaEmpleos.LogicaVerPostulantes.verPostulantes> postulantes)
        {
            lbNombrePostulante.Text = postulantes.Count > 0 ? postulantes[0].Nombre : "Sin postulante";
            lbNombrePostulante2.Text = postulantes.Count > 1 ? postulantes[1].Nombre : "Sin postulante";
            lbNombrePostulante3.Text = postulantes.Count > 2 ? postulantes[2].Nombre : "Sin postulante";
            lbNombrePostulante4.Text = postulantes.Count > 3 ? postulantes[3].Nombre : "Sin postulante";
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

        }

        private void cbFiltro_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void txtDatoFiltrar_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void lbNombreTrabajo4_Click(object sender, EventArgs e)
        {

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            var postulantes = crud.SiguientePagina(idOferta); // Ir a la siguiente página
            ActualizarInterfaz(postulantes);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            var postulantes = crud.PaginaAnterior(idOferta); // Ir a la página anterior
            ActualizarInterfaz(postulantes);
        }

        private void lbNombrePostulante_Click(object sender, EventArgs e)
        {
            Form_Perfil_Postulante perfil = new Form_Perfil_Postulante();
            this.Close();
            perfil.Show();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            VerOfertasEmpleoEmpresa verOfertas = new VerOfertasEmpleoEmpresa();
            this.Close();
            verOfertas.Show();
        }
    }
}
