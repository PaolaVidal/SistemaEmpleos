using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpleos.LogicaVerPostulante
{
    internal class verOfertasEmpleoEmpresa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Estado { get; set; }
        public int IdEmpresa { get; set; }  // Agregado para identificar a la empresa que publicó la oferta

        // Constructor sin parámetros para compatibilidad con ORMs y serialización
        public verOfertasEmpleoEmpresa() { }

        // Constructor actualizado
        public verOfertasEmpleoEmpresa(int id, string titulo, DateTime fechaPublicacion, string estado, int idEmpresa)
        {
            Id = id;
            Titulo = titulo;
            FechaPublicacion = fechaPublicacion;
            Estado = estado;
            IdEmpresa = idEmpresa;  // Inicializamos el Id de la empresa
        }

        public void ActualizarDatos(string titulo, DateTime fechaPublicacion, string estado, int idEmpresa)
        {
            Titulo = titulo;
            FechaPublicacion = fechaPublicacion;
            Estado = estado;
            IdEmpresa = idEmpresa;  // Actualizamos el Id de la empresa
        }

        // Método que convierte el estado en una cadena más entendible
        public string ObtenerEstadoAmigable()
        {
            switch (Estado)
            {
                case "A":
                    return "Disponible";
                case "C":
                    return "Cerrada";
                case "P":
                    return "Postulada";
                default:
                    return "Estado desconocido";
            }
        }

        public override string ToString()
        {
            return $"{Titulo} - Publicado el: {FechaPublicacion.ToShortDateString()} - Estado: {ObtenerEstadoAmigable()} - Empresa ID: {IdEmpresa}";
        }
    }
}
