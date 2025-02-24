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

        public OfertaEmpleo(int id, string titulo, DateTime fechaPublicacion)
        {
            Id = id;
            Titulo = titulo;
            FechaPublicacion = fechaPublicacion;
        }

        public override string ToString()
        {
            return $"{Titulo} - Publicado el: {FechaPublicacion.ToShortDateString()}";
        }
    }
}
