using SistemaEmpleos.LogicaVerPostulante;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SistemaEmpleos.Formularios
{
    public partial class VerOfertasEmpleoEmpresa : Form
    {
        private verOfertasEmpleoEmpresaCrud crud; // Instancia de VerPostulanteCRUD
        private int idEmpresa = 1; // ID de la empresa (puede cambiarse dinámicamente)
        public VerOfertasEmpleoEmpresa()
        {
            InitializeComponent();
            crud = new verOfertasEmpleoEmpresaCrud(@"(Localdb)\rodolfo server", "Empleo");
            //crud.VerificarConexion(); // Verificamos la conexión
            CargarOfertasEmpresa(); // Cargar las ofertas de la empresa al iniciar

            // Asignar eventos Click a los labels
            lbNombreTrabajo.Click += lbNombreTrabajo_Click;
            lbNombreTrabajo2.Click += lbNombreTrabajo_Click;
            lbNombreTrabajo3.Click += lbNombreTrabajo_Click;
            lbNombreTrabajo4.Click += lbNombreTrabajo_Click;
        }

        private void CargarOfertasEmpresa()
        {
            var ofertas = crud.ObtenerOfertasPorEmpresa(idEmpresa, 0); // Obtener las ofertas de la empresa
            ActualizarInterfaz(ofertas);
        }

        private void ActualizarInterfaz(List<verOfertasEmpleoEmpresa> ofertas)
        {
            // Actualizamos los títulos de las ofertas
            // Asignar títulos y almacenar ID en Tag
            lbNombreTrabajo.Text = ofertas.Count > 0 ? ofertas[0].Titulo : "Sin oferta";
            lbNombreTrabajo.Tag = ofertas.Count > 0 ? ofertas[0].Id : (object)null;

            lbNombreTrabajo2.Text = ofertas.Count > 1 ? ofertas[1].Titulo : "Sin oferta";
            lbNombreTrabajo2.Tag = ofertas.Count > 1 ? ofertas[1].Id : (object)null;

            lbNombreTrabajo3.Text = ofertas.Count > 2 ? ofertas[2].Titulo : "Sin oferta";
            lbNombreTrabajo3.Tag = ofertas.Count > 2 ? ofertas[2].Id : (object)null;

            lbNombreTrabajo4.Text = ofertas.Count > 3 ? ofertas[3].Titulo : "Sin oferta";
            lbNombreTrabajo4.Tag = ofertas.Count > 3 ? ofertas[3].Id : (object)null;

            // Actualizamos las fechas de publicación
            lbFechaPostulacion.Text = ofertas.Count > 0 ? ofertas[0].FechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaPostulacion2.Text = ofertas.Count > 1 ? ofertas[1].FechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaPostulacion3.Text = ofertas.Count > 2 ? ofertas[2].FechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaPostulacion4.Text = ofertas.Count > 3 ? ofertas[3].FechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
        }

        private void lbNombreTrabajo_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null && label.Tag != null)
            {
                int idOferta = (int)label.Tag;
                //MessageBox.Show($"ID de la oferta seleccionada: {idOferta}");

                // Asegúrate de pasar idOferta al constructor de FormverPostulantes
                this.Hide();
                new FormverPostulantes(idOferta).Show();
                

            }
        }



        private void VerOfertasEmpleoEmpresa_Load(object sender, EventArgs e)
        {

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            var ofertas = crud.SiguientePagina(idEmpresa); // Ir a la siguiente página
            ActualizarInterfaz(ofertas);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            var ofertas = crud.PaginaAnterior(idEmpresa); // Ir a la página anterior
            ActualizarInterfaz(ofertas);
        }

        private void lbNombreTrabajo_Click_1(object sender, EventArgs e)
        {

        }
    }
}
