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
        private int idEmpresaSeleccionada = 1; // ID de la empresa que quieres filtrar (esto puedes cambiarlo dinámicamente)

       

        public verOfertasEmpleos()
        {
            InitializeComponent();
            // Instanciamos la conexión con los parámetros del servidor y base de datos
            crud = new verOfertaEmpleoCRUD(@"(Localdb)\rodolfo server", "Empleo");
            crud.VerificarConexion(); // Verificamos la conexión
            CargarOfertas(); // Cargar las primeras ofertas al iniciar
        }

        private void CargarOfertas()
        {
            var ofertas = crud.ObtenerOfertasPorEmpresa(idEmpresaSeleccionada, 0);
            ActualizarInterfaz(ofertas);
        }

        private void ActualizarInterfaz(List<(string titulo, DateTime fechaPublicacion)> ofertas)
        {
            // Actualizamos los títulos de los trabajos
            lbNombreTrabajo.Text = ofertas.Count > 0 ? ofertas[0].titulo : "Sin oferta";
            lbNombreTrabajo2.Text = ofertas.Count > 1 ? ofertas[1].titulo : "Sin oferta";
            lbNombreTrabajo3.Text = ofertas.Count > 2 ? ofertas[2].titulo : "Sin oferta";
            lbNombreTrabajo4.Text = ofertas.Count > 3 ? ofertas[3].titulo : "Sin oferta";

            // Actualizamos las fechas de publicación
            lbFechaOferta.Text = ofertas.Count > 0 ? ofertas[0].fechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaOferta2.Text = ofertas.Count > 1 ? ofertas[1].fechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaOferta3.Text = ofertas.Count > 2 ? ofertas[2].fechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
            lbFechaOferta4.Text = ofertas.Count > 3 ? ofertas[3].fechaPublicacion.ToString("dd/MM/yyyy") : "N/A";
        }

        private void verOfertasEmpleos_Load(object sender, EventArgs e)
        {

        }

        private void lbFechaPostulacion3_Click(object sender, EventArgs e)
        {

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            var ofertas = crud.SiguientePagina(idEmpresaSeleccionada);
            ActualizarInterfaz(ofertas);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            var ofertas = crud.PaginaAnterior(idEmpresaSeleccionada);
            ActualizarInterfaz(ofertas);
        }
    }
}
