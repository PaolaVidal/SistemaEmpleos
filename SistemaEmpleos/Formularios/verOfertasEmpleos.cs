using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaEmpleos.LogicaVerOfertasEmpleo;
using SistemaEmpleos.LogicaVerPostulante; // Importamos la clase

namespace SistemaEmpleos.Formularios
{
    public partial class verOfertasEmpleos : Form
    {
        private verOfertaEmpleoCRUD crud; // Instancia de verOfertaEmpleoCRUD
        private int idCandidato = 4; // ID del candidato (puede cambiarse dinámicamente)



        public verOfertasEmpleos()
        {
            InitializeComponent();
            // Instanciamos la conexión con los parámetros del servidor y base de datos
            crud = new verOfertaEmpleoCRUD(@"VALERIAV\MSSQLSERVER01", "Empleo2");
            //crud.VerificarConexion(); // Verificamos la conexión
            CargarPostulaciones(); // Cargar las primeras ofertas al iniciar
        }

        private void CargarPostulaciones()
        {
            var postulaciones = crud.ObtenerOfertasPorCandidato(idCandidato, 0);
            ActualizarInterfaz(postulaciones);
        }

        private void ActualizarInterfaz(List<OfertaEmpleo> ofertas)
        {
            // Actualizamos los títulos de los trabajos
            lbNombreTrabajo.Text = ofertas.Count > 0 ? ofertas[0].Titulo : "Sin oferta";
            lbNombreTrabajo2.Text = ofertas.Count > 1 ? ofertas[1].Titulo : "Sin oferta";
            lbNombreTrabajo3.Text = ofertas.Count > 2 ? ofertas[2].Titulo : "Sin oferta";
            lbNombreTrabajo4.Text = ofertas.Count > 3 ? ofertas[3].Titulo : "Sin oferta";

            // Actualizamos las fechas de publicación
            lbFechaOferta.Text = ofertas.Count > 0 ? ofertas[0].FechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaOferta2.Text = ofertas.Count > 1 ? ofertas[1].FechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaOferta3.Text = ofertas.Count > 2 ? ofertas[2].FechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaOferta4.Text = ofertas.Count > 3 ? ofertas[3].FechaPublicacion.ToString("dd/MM/yyyy") : "N/A";

            // Actualizamos las fechas de postulación
            lbFechaOferta.Text = ofertas.Count > 0 ? ofertas[0].FechaPostulacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaOferta2.Text = ofertas.Count > 1 ? ofertas[1].FechaPostulacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaOferta3.Text = ofertas.Count > 2 ? ofertas[2].FechaPostulacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaOferta4.Text = ofertas.Count > 3 ? ofertas[3].FechaPostulacion.ToString("dd/MM/yyyy") : "N/A";

            // Actualizamos los estados de las postulaciones (utilizando ObtenerEstadoAmigable)
            lbEstado.Text = ofertas.Count > 0 ? ofertas[0].ObtenerEstadoAmigable() : "N/A";
            lbEstado2.Text = ofertas.Count > 1 ? ofertas[1].ObtenerEstadoAmigable() : "N/A";
            lbEstado3.Text = ofertas.Count > 2 ? ofertas[2].ObtenerEstadoAmigable() : "N/A";
            lbEstado4.Text = ofertas.Count > 3 ? ofertas[3].ObtenerEstadoAmigable() : "N/A";

            // Cambiar el color del texto según el estado
            CambiarColorEstado(lbEstado, ofertas.Count > 0 ? ofertas[0].Estado : "");
            CambiarColorEstado(lbEstado2, ofertas.Count > 1 ? ofertas[1].Estado : "");
            CambiarColorEstado(lbEstado3, ofertas.Count > 2 ? ofertas[2].Estado : "");
            CambiarColorEstado(lbEstado4, ofertas.Count > 3 ? ofertas[3].Estado : "");
        }

        // Método para cambiar el color del texto según el estado
        private void CambiarColorEstado(Label label, string estado)
        {
            switch (estado.ToLower())
            {
                case "postulado":
                    label.ForeColor = Color.Blue; // Azul para "Postulado"
                    break;
                case "aceptado":
                    label.ForeColor = Color.Green; // Verde para "Aceptado"
                    break;
                case "no disponible":
                    label.ForeColor = Color.Gray; // Gris para "No disponible"
                    break;
                default:
                    label.ForeColor = Color.Black; // Negro para estados desconocidos
                    break;
            }
        }


        private void verOfertasEmpleos_Load(object sender, EventArgs e)
        {

        }

        private void lbFechaPostulacion3_Click(object sender, EventArgs e)
        {

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            var ofertas = crud.SiguientePagina(idCandidato);
            ActualizarInterfaz(ofertas);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            var ofertas = crud.PaginaAnterior(idCandidato);
            ActualizarInterfaz(ofertas);
        }

        private void lbNombreTrabajo_Click(object sender, EventArgs e)
        {

        }
    }
}
