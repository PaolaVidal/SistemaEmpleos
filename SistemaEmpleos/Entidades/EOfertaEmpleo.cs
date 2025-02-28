using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpleos.Entidades
{
    public class EOfertaEmpleo
    {
        public int id_oferta_empleo {  get; set; }
        public int id_pais { get; set; }
        public int id_provincia { get; set; }
        public int id_empresa { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int vacantes { get; set; }
        public double salario { get; set; }
        public string horario { get; set; }
        public string duracion_contrato { get; set; }
        public DateTime fecha_publicacion { get; set; }
        public char estado { get; set; }
    }
}
