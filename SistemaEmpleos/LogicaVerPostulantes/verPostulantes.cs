using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpleos.LogicaVerPostulantes
{
    internal class verPostulantes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string EstadoPostulacion { get; set; }

        // Constructor sin parámetros para compatibilidad con ORMs y serialización
        public verPostulantes() { }

        // Constructor con parámetros
        public verPostulantes(int id, string nombre, string apellido, string email, string telefono, string estadoPostulacion)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Telefono = telefono;
            EstadoPostulacion = estadoPostulacion;
        }

        public void ActualizarDatos(string nombre, string apellido, string email, string telefono, string estadoPostulacion)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Telefono = telefono;
            EstadoPostulacion = estadoPostulacion;
        }

        // Método para obtener un estado amigable
        public string ObtenerEstadoAmigable()
        {
            switch (EstadoPostulacion)
            {
                case "P":
                    return "Postulado";
                case "E":
                    return "En revisión";
                case "A":
                    return "Aceptado";
                case "R":
                    return "Rechazado";
                default:
                    return "Estado desconocido";
            }
        }

        public override string ToString()
        {
            return $"{Nombre} {Apellido} - Email: {Email} - Teléfono: {Telefono} - Estado: {ObtenerEstadoAmigable()}";
        }
    }
}
