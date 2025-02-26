using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpleos.LogicaVerPostulante
{
    internal class OfertaEmpleo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaPostulacion { get; set; }

        // Constructor sin parámetros para compatibilidad con ORMs y serialización
        public OfertaEmpleo() { }

        public OfertaEmpleo(int id, string titulo, DateTime fechaPublicacion, string estado, DateTime fechaPostulacion)
        {
            Id = id;
            Titulo = titulo;
            FechaPublicacion = fechaPublicacion;
            Estado = estado;
            FechaPostulacion = fechaPostulacion;
        }

        public void ActualizarDatos(string titulo, DateTime fechaPublicacion, string estado, DateTime fechaPostulacion)
        {
            Titulo = titulo;
            FechaPublicacion = fechaPublicacion;
            Estado = estado;
            FechaPostulacion = fechaPostulacion;
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
            return $"{Titulo} - Publicado el: {FechaPublicacion.ToShortDateString()} - Estado: {Estado} - Postulado el: {FechaPostulacion.ToShortDateString()}";
        }
    }
}
