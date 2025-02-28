using SistemaEmpleos.Entidades;
using SistemaEmpleos.ModuloConexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpleos.Datos
{
    public class DOfertaEmpleo
    {
		private Conexion con;
        public DOfertaEmpleo(string servidor, string baseDatos) 
		{
            con = new Conexion(servidor, baseDatos);
        }

        public DataTable BuscarOferta(int id_oferta_empleo)
        {
            try
            {
                SqlDataReader reader;
                DataTable tabla = new DataTable();
                if (con.AbrirConexion())
                {
                    string query = "Select id_pais, id_provincia, titulo, descripcion, vacantes, salario, horario, duracion_contrato, fecha_publicacion from OfertaEmpleo where id_oferta_empleo = @id_oferta_empleo";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("id_oferta_empleo", SqlDbType.Int).Value = id_oferta_empleo;

                    reader = cmd.ExecuteReader();
                    tabla.Load(reader);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.CerrarConexion();
            }
        }

        public string Agregar(EOfertaEmpleo oferta)
        {
			string Rsp = "OK";
            try
			{
				if (con.AbrirConexion())
				{
					string query = "Insert into OfertaEmpleo(id_pais, id_provincia, id_empresa, titulo, descripcion, vacantes, salario, horario, duracion_contrato, fecha_publicacion, estado)\r\nValues(@id_pais,@id_provincia,@id_empresa,@titulo,@descripcion,@vacantes,@salario,@horario,@duracion_contrato,@fecha_publicacion,'P')";

                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = oferta.id_pais;
                    cmd.Parameters.Add("@id_provincia", SqlDbType.Int).Value = oferta.id_provincia;
                    cmd.Parameters.Add("@id_empresa", SqlDbType.Int).Value = oferta.id_empresa;
					cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = oferta.titulo;
                    cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = oferta.descripcion;
                    cmd.Parameters.Add("@vacantes", SqlDbType.Int).Value = oferta.vacantes;
                    cmd.Parameters.Add("@salario", SqlDbType.Float).Value = oferta.salario;
                    cmd.Parameters.Add("@horario", SqlDbType.VarChar).Value = oferta.horario;
                    cmd.Parameters.Add("@duracion_contrato", SqlDbType.VarChar).Value = oferta.duracion_contrato;
                    cmd.Parameters.Add("@fecha_publicacion", SqlDbType.Date).Value = oferta.fecha_publicacion;

                    Rsp = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo guardar la oferta de empleo";
                }
                else
                {
                    Rsp = "Ocurrio un error";
                }
			}
			catch (Exception ex)
			{
				Rsp = ex.Message;
			}
			finally
			{
				con.CerrarConexion();
			}

			return Rsp;
        }

        public DataTable ListarPais()
        {
            try
            {
                SqlDataReader reader;
                DataTable tabla = new DataTable();
                if (con.AbrirConexion())
                {
                    string query = "Select id_pais, nombre_pais from Pais";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    
                    reader = cmd.ExecuteReader();
                    tabla.Load(reader);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.CerrarConexion();
            }
        }
        public DataTable BuscarProvincia(int id_pais)
        {
            try
            {
                SqlDataReader reader;
                DataTable tabla = new DataTable();
                if (con.AbrirConexion())
                {
                    string query = "Select id_provincia, id_pais, nombre_provincia from Provincia where id_pais = @id_pais";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;

                    reader = cmd.ExecuteReader();
                    tabla.Load(reader);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.CerrarConexion();
            }
        }

        public DataTable ListarCategorias()
        {
            try
            {
                SqlDataReader reader;
                DataTable tabla = new DataTable();
                if (con.AbrirConexion())
                {
                    string query = "Select id_categoria_profesional, nombre_categoria_profesional from CategoriaProfesional";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;

                    reader = cmd.ExecuteReader();
                    tabla.Load(reader);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.CerrarConexion();
            }
        }
        public DataTable BuscarSubCategoria(int id_categoria_profesional)
        {
            try
            {
                SqlDataReader reader;
                DataTable tabla = new DataTable();
                if (con.AbrirConexion())
                {
                    string query = "Select id_subcategoria_profesional, id_categoria_profesional, nombre_subcategoria_profesional from SubcategoriaProfesional where id_categoria_profesional = @id_categoria_profesional";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_categoria_profesional", SqlDbType.Int).Value = id_categoria_profesional;

                    reader = cmd.ExecuteReader();
                    tabla.Load(reader);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.CerrarConexion();
            }
        }
        public int ObtenerIDOferta()
        {
            int Rsp = 0;
            try
            {
                if (con.AbrirConexion())
                {
                    string query = "Select MAX(id_oferta_empleo) from OfertaEmpleo;";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;

                    Rsp = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.CerrarConexion();
            }

            return Rsp;
        }
        public string GuardarCategoria(int id_oferta_empleo, int id_categoria_profesional, int id_subcategoria_profesional)
        {
            string Rsp = "";
            try
            {
                if (con.AbrirConexion())
                {
                    string query = "Insert into OfertaCategoria(id_oferta_empleo, id_categoria_profesional, id_subcategoria_profesional)\r\nValues(@id_oferta_empleo, @id_categoria_profesional, @id_subcategoria_profesional)";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_oferta_empleo", SqlDbType.Int).Value = id_oferta_empleo;
                    cmd.Parameters.Add("@id_categoria_profesional", SqlDbType.Int).Value = id_categoria_profesional;
                    cmd.Parameters.Add("@id_subcategoria_profesional", SqlDbType.Int).Value = id_subcategoria_profesional;

                    Rsp = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo guardar las categorias del empleo";
                }
                else
                {
                    Rsp = "Ocurrio un error";
                }
            }
            catch (Exception ex)
            {
                Rsp = ex.Message;
            }
            finally
            {
                con.CerrarConexion();
            }

            return Rsp;
        }
        public DataTable BuscarCategoriaOferta(int id_oferta_empleo)
        {
            try
            {
                SqlDataReader reader;
                DataTable tabla = new DataTable();
                if (con.AbrirConexion())
                {
                    string query = "Select cap.id_categoria_profesional, cap.nombre_categoria_profesional, scp.id_subcategoria_profesional, scp.nombre_subcategoria_profesional from OfertaCategoria oc\r\ninner join CategoriaProfesional cap\r\non oc.id_categoria_profesional = cap.id_categoria_profesional\r\ninner join SubcategoriaProfesional scp\r\non oc.id_subcategoria_profesional = scp.id_subcategoria_profesional\r\nwhere oc.id_oferta_empleo = @id_oferta_empleo";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_oferta_empleo", SqlDbType.Int).Value = id_oferta_empleo;

                    reader = cmd.ExecuteReader();
                    tabla.Load(reader);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.CerrarConexion();
            }
        }

        public string Modificar(EOfertaEmpleo oferta)
        {
            string Rsp = "OK";
            try
            {
                if (con.AbrirConexion())
                {
                    string query = "Update OfertaEmpleo\r\nSet id_pais = @id_pais,\r\nid_provincia = @id_provincia,\r\ntitulo = @titulo,\r\ndescripcion = @descripcion,\r\nvacantes = @vacantes,\r\nsalario = @salario,\r\nhorario = @horario,\r\nduracion_contrato = @duracion_contrato\r\nWhere id_oferta_empleo = @id_oferta_empleo";

                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_oferta_empleo", SqlDbType.Int).Value = oferta.id_oferta_empleo;
                    cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = oferta.id_pais;
                    cmd.Parameters.Add("@id_provincia", SqlDbType.Int).Value = oferta.id_provincia;
                    cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = oferta.titulo;
                    cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = oferta.descripcion;
                    cmd.Parameters.Add("@vacantes", SqlDbType.Int).Value = oferta.vacantes;
                    cmd.Parameters.Add("@salario", SqlDbType.Float).Value = oferta.salario;
                    cmd.Parameters.Add("@horario", SqlDbType.VarChar).Value = oferta.horario;
                    cmd.Parameters.Add("@duracion_contrato", SqlDbType.VarChar).Value = oferta.duracion_contrato;

                    Rsp = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo modificar la oferta de empleo";
                }
                else
                {
                    Rsp = "Ocurrio un error";
                }
            }
            catch (Exception ex)
            {
                Rsp = ex.Message;
            }
            finally
            {
                con.CerrarConexion();
            }

            return Rsp;
        }
        public int ExisteOfertaCategoriaAgregada(int id_oferta_empleo, int id_categoria_profesional, int id_subcategoria_profesional)
        {
            int Rsp = 0;
            try
            {
                if (con.AbrirConexion())
                {
                    string query = "if exists(Select * from OfertaCategoria where id_oferta_empleo = @id_oferta_empleo and id_categoria_profesional = @id_categoria_profesional\r\nand id_subcategoria_profesional = @id_subcategoria_profesional)\r\nBegin\r\n\tSelect 1\r\nEnd";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_oferta_empleo", SqlDbType.Int).Value = id_oferta_empleo;
                    cmd.Parameters.Add("@id_categoria_profesional", SqlDbType.Int).Value = id_categoria_profesional;
                    cmd.Parameters.Add("@id_subcategoria_profesional", SqlDbType.Int).Value = id_subcategoria_profesional;

                    object resultado = cmd.ExecuteScalar();
                    if(resultado != null)
                    {
                        Rsp = Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.CerrarConexion();
            }

            return Rsp;
        }
        public string ModificarOfertaCategoria(int id_oferta_empleo, int id_categoria_profesional, int id_subcategoria_profesional)
        {
            string Rsp = "";
            try
            {
                if (con.AbrirConexion())
                {
                    string query = "Insert into OfertaCategoria(id_oferta_empleo, id_categoria_profesional, id_subcategoria_profesional)\r\nValues(@id_oferta_empleo, @id_categoria_profesional, @id_subcategoria_profesional)";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_oferta_empleo", SqlDbType.Int).Value = id_oferta_empleo;
                    cmd.Parameters.Add("@id_categoria_profesional", SqlDbType.Int).Value = id_categoria_profesional;
                    cmd.Parameters.Add("@id_subcategoria_profesional", SqlDbType.Int).Value = id_subcategoria_profesional;

                    Rsp = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo guardar las categorias del empleo";
                }
                else
                {
                    Rsp = "Ocurrio un error";
                }
            }
            catch (Exception ex)
            {
                Rsp = ex.Message;
            }
            finally
            {
                con.CerrarConexion();
            }

            return Rsp;
        }
        public string EliminarOfertaCategoria(int id_oferta_empleo, int id_categoria_profesional, int id_subcategoria_profesional)
        {
            string Rsp = "";
            try
            {
                if (con.AbrirConexion())
                {
                    string query = "Delete from OfertaCategoria where id_oferta_empleo = @id_oferta_empleo and id_categoria_profesional = @id_categoria_profesional\r\nand id_subcategoria_profesional = @id_subcategoria_profesional";
                    SqlCommand cmd = new SqlCommand(query, con.Conexion_);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_oferta_empleo", SqlDbType.Int).Value = id_oferta_empleo;
                    cmd.Parameters.Add("@id_categoria_profesional", SqlDbType.Int).Value = id_categoria_profesional;
                    cmd.Parameters.Add("@id_subcategoria_profesional", SqlDbType.Int).Value = id_subcategoria_profesional;

                    Rsp = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.CerrarConexion();
            }

            return Rsp;
        }
    }
}
