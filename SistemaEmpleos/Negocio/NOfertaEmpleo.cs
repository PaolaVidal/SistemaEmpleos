using SistemaEmpleos.LogicaVerPostulante;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpleos.Negocio
{
    public class NOfertaEmpleo
    {
        public static string Agregar(int id_pais, int id_provincia, int id_empresa, string titulo, string descripcion, int vacantes, double salario, string horario, string duracion_contrato, DateTime fecha_publicacion)
        {
            string Rsp = "";
            try
            {
                EOfertaEmpleo obj = new EOfertaEmpleo();
                DOfertaEmpleo datos = new DOfertaEmpleo("localhost", "Empleo");

                obj.id_pais = id_pais;
                obj.id_provincia = id_provincia;
                obj.id_empresa = id_empresa;
                obj.titulo = titulo;
                obj.descripcion = descripcion;
                obj.vacantes = vacantes;
                obj.salario = salario;
                obj.horario = horario;
                obj.duracion_contrato = duracion_contrato;
                obj.fecha_publicacion = fecha_publicacion;

                Rsp = datos.Agregar(obj);

            }
            catch (Exception ex)
            {
                Rsp = ex.Message;
            }

            return Rsp;
        }

        public static DataTable ListarPais()
        {
            DataTable tabla = new DataTable();
            DOfertaEmpleo Datos = new DOfertaEmpleo("localhost", "Empleo");
            tabla = Datos.ListarPais();
            return tabla;
        }

        public static DataTable BuscarProvincia(int id_pais)
        {
            DataTable tabla = new DataTable();
            DOfertaEmpleo Datos = new DOfertaEmpleo("localhost", "Empleo");
            tabla = Datos.BuscarProvincia(id_pais);
            return tabla;
        }

        public static DataTable ListarCategorias()
        {
            DataTable tabla = new DataTable();
            DOfertaEmpleo Datos = new DOfertaEmpleo("localhost", "Empleo");
            tabla = Datos.ListarCategorias();
            return tabla;
        }

        public static DataTable BuscarSubCategoria(int id_categoria_profesional)
        {
            DataTable tabla = new DataTable();
            DOfertaEmpleo Datos = new DOfertaEmpleo("localhost", "Empleo");
            tabla = Datos.BuscarSubCategoria(id_categoria_profesional);
            return tabla;
        }
        public static int ObtenerIDOferta()
        {
            int Rsp = 0;
            try
            {
                DOfertaEmpleo datos = new DOfertaEmpleo("localhost", "Empleo");
                Rsp = datos.ObtenerIDOferta();
            }
            catch (Exception)
            {
                throw;
            }

            return Rsp;
        }
        public static string GuardarCategoria(int id_oferta_empleo, int id_categoria_profesional, int id_subcategoria_profesional)
        {
            string Rsp = "";
            try
            {
                DOfertaEmpleo datos = new DOfertaEmpleo("localhost", "Empleo");
                Rsp = datos.GuardarCategoria(id_oferta_empleo, id_categoria_profesional, id_subcategoria_profesional);
            }
            catch (Exception ex)
            {
                Rsp = ex.Message;
            }

            return Rsp;
        }

        public static DataTable BuscarOferta(int id_oferta_empleo)
        {
            DataTable tabla = new DataTable();
            DOfertaEmpleo Datos = new DOfertaEmpleo("localhost", "Empleo");
            tabla = Datos.BuscarOferta(id_oferta_empleo);
            return tabla;
        }

        public static DataTable BuscarCategoriaOferta(int id_oferta_empleo)
        {
            DataTable tabla = new DataTable();
            DOfertaEmpleo Datos = new DOfertaEmpleo("localhost", "Empleo");
            tabla = Datos.BuscarCategoriaOferta(id_oferta_empleo);
            return tabla;
        }

        public static string Modificar(int id_oferta_empleo, int id_pais, int id_provincia, string titulo, string descripcion, int vacantes, double salario, string horario, string duracion_contrato)
        {
            string Rsp = "";
            try
            {
                EOfertaEmpleo obj = new EOfertaEmpleo();
                DOfertaEmpleo datos = new DOfertaEmpleo("localhost", "Empleo");

                obj.id_oferta_empleo = id_oferta_empleo;
                obj.id_pais = id_pais;
                obj.id_provincia = id_provincia;
                obj.titulo = titulo;
                obj.descripcion = descripcion;
                obj.vacantes = vacantes;
                obj.salario = salario;
                obj.horario = horario;
                obj.duracion_contrato = duracion_contrato;

                Rsp = datos.Modificar(obj);

            }
            catch (Exception ex)
            {
                Rsp = ex.Message;
            }

            return Rsp;
        }
        public static int ExisteOfertaCategoriaAgregada(int id_oferta_empleo, int id_categoria_profesional, int id_subcategoria_profesional)
        {
            int Rsp = 0;
            try
            {
                DOfertaEmpleo datos = new DOfertaEmpleo("localhost", "Empleo");
                Rsp = datos.ExisteOfertaCategoriaAgregada(id_oferta_empleo, id_categoria_profesional, id_subcategoria_profesional);
            }
            catch (Exception ex)
            {
                throw;
            }

            return Rsp;
        }
        public static string ModificarOfertaCategoria(int id_oferta_empleo, int id_categoria_profesional, int id_subcategoria_profesional)
        {
            string Rsp = "";
            try
            {
                if (ExisteOfertaCategoriaAgregada(id_oferta_empleo, id_categoria_profesional, id_subcategoria_profesional) == 0)
                {
                    DOfertaEmpleo datos = new DOfertaEmpleo("localhost", "Empleo");
                    Rsp = datos.ModificarOfertaCategoria(id_oferta_empleo, id_categoria_profesional, id_subcategoria_profesional);
                }
            }
            catch (Exception ex)
            {
                Rsp = ex.Message;
            }

            return Rsp;
        }
        public static string EliminarOfertaCategoria(int id_oferta_empleo, int id_categoria_profesional, int id_subcategoria_profesional)
        {
            string Rsp = "";
            try
            {
                if (ExisteOfertaCategoriaAgregada(id_oferta_empleo, id_categoria_profesional, id_subcategoria_profesional) == 1)
                {
                    DOfertaEmpleo datos = new DOfertaEmpleo("localhost", "Empleo");
                    Rsp = datos.EliminarOfertaCategoria(id_oferta_empleo, id_categoria_profesional, id_subcategoria_profesional);
                }
                else
                {
                    Rsp = "La categoria no existe";
                }
            }
            catch (Exception ex)
            {
                Rsp = ex.Message;
            }

            return Rsp;
        }
    }
}
}
