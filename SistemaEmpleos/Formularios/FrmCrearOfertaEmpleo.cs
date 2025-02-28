using SistemaEmpleos.Clases;
using SistemaEmpleos.Negocio;
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
    public partial class FrmCrearOfertaEmpleo : Form
    {
        private DataTable TblCategoria;
        public bool OfertaModificar;
        public int IDOfertaModificar;
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje,"Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void FormatTblCategoria()
        {
            this.TblCategoria = new DataTable();
            TblCategoria.Columns.Add("id_categoria_profesional");
            TblCategoria.Columns.Add("Categoria");
            TblCategoria.Columns.Add("id_subcategoria_profesional");
            TblCategoria.Columns.Add("SubCategoria");

            this.dgvCategoria.DataSource = this.TblCategoria;

            dgvCategoria.Columns[0].Visible = false;
            dgvCategoria.Columns[1].Width = 130;
            dgvCategoria.Columns[2].Visible = false;
            dgvCategoria.Columns[3].Width = 130;
        }
        private void Limpiar()
        {
            errorPro.Clear();
            this.TblCategoria.Rows.Clear();
            txtTitulo.Text = "";
            cboPais.SelectedIndex = 0;
            cboProvincia.DataSource = null;
            nudSalario.Value = 1;
            nudVacantes.Value = 1;
            nudAnios.Value = 0;
            nudMeses.Value = 0;
            txtDescripcion.Text = "";
            cboCategoria.SelectedIndex = 0;
            cboSubCategoria.DataSource = null;
            FormatDatePicker();
        }
        private bool VerificarDatosCompletos()
        {
            bool Rsp = false;
            try
            {
                if (txtDescripcion.Text.Trim() == string.Empty)
                {
                    errorPro.SetError(txtDescripcion,"No se ha agregado un titulo a la oferta");
                }
                if (cboProvincia.Text == string.Empty)
                {
                    errorPro.SetError(cboProvincia,"No se ha seleccionado una provincia");
                }
                if (nudAnios.Value == 0 && nudMeses.Value == 0)
                {
                    errorPro.SetError(nudAnios,"Año o mes su valor debe ser por lo menos 1");
                    errorPro.SetError(nudMeses, "Año o mes su valor debe ser por lo menos 1");
                }
                if(txtDescripcion.Text == string.Empty)
                {
                    errorPro.SetError(txtDescripcion, "No se ha agregado una descripcion del trabajo");
                }
                if(this.TblCategoria.Rows.Count == 0)
                {
                    errorPro.SetError(cboCategoria, "No se ha agregado ninguna categoria");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Rsp;
        }
        public FrmCrearOfertaEmpleo()
        {
            InitializeComponent();
            CargarPaises();
            CargarCategorias();
            FormatTblCategoria();
            if (OfertaModificar)
            {
                ObtenerDatosOferta();
            }
            else
            {
                FormatDatePicker();
            }
        }
        private void FormatDatePicker()
        {
            try
            {
                for (int i = 1; i <= 28; i++)
                {
                    string dtpNombre = "dtpHor" + i;

                    DateTimePicker dtp = this.Controls.Find(dtpNombre, true).FirstOrDefault() as DateTimePicker;

                    if (dtp != null)
                    {
                        DateTime hora = DateTime.ParseExact("00:00", "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                        dtp.Value = hora;
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void CargarPaises()
        {
            try
            {
                cboPais.DataSource = NOfertaEmpleo.ListarPais();
                cboPais.ValueMember = "id_pais";
                cboPais.DisplayMember = "nombre_pais";
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void BuscarProvincia()
        {
            try
            {
                cboProvincia.DataSource = null;
                cboProvincia.DataSource = NOfertaEmpleo.BuscarProvincia(Convert.ToInt32(cboPais.SelectedValue));
                cboProvincia.ValueMember = "id_provincia";
                cboProvincia.DisplayMember = "nombre_provincia";
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void AccionesPnlCate()
        {
            try
            {
                if (pnlCate.Visible)
                {
                    pnlCate.Visible = false;
                }
                else
                {
                    pnlCate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void CargarCategorias()
        {
            try
            {
                cboCategoria.DataSource = NOfertaEmpleo.ListarCategorias();
                cboCategoria.ValueMember = "id_categoria_profesional";
                cboCategoria.DisplayMember = "nombre_categoria_profesional";
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void BuscarSubCategoria()
        {
            try
            {
                cboSubCategoria.DataSource = null;
                cboSubCategoria.DataSource = NOfertaEmpleo.BuscarSubCategoria(Convert.ToInt32(cboCategoria.SelectedValue));
                cboSubCategoria.ValueMember = "id_subcategoria_profesional";
                cboSubCategoria.DisplayMember = "nombre_subcategoria_profesional";
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void GuardarCategoria()
        {
            try
            {
                if (Convert.ToInt32(cboSubCategoria.ValueMember) > 0)
                {
                    this.TblCategoria.Rows.Add(cboCategoria.SelectedValue, cboCategoria.Text, cboSubCategoria.SelectedValue, cboSubCategoria.Text);
                }
                else
                {
                    errorPro.SetError(cboSubCategoria,"Seleccione una categoria y luego una subcategoria");
                    MensajeError("No se ha seleccionado una subcategoria");
                }
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private string GenerarContratoTiempo()
        {
            string Rsp = "";
            if (nudAnios.Value > 0)
            {
                Rsp = nudAnios.Value.ToString();
            }
            if(nudAnios.Value > 0)
            {
                Rsp += ";" + nudMeses.Value.ToString();
            }

            return Rsp;
        }
        private string GenerarHorario()
        {
            string horario = "";
            for (int i = 1; i <= 28; i++)
            {
                string dtpNombre = "dtpHor" + i;

                DateTimePicker dtp = this.Controls.Find(dtpNombre, true).FirstOrDefault() as DateTimePicker;

                if (dtp != null)
                {
                    DateTime hora = dtp.Value;
                    horario += hora.ToString("HH:mm") + ";";
                }
            }
            
            return horario;
        }
        private int ObtenerIDOferta()
        {
            int ID = 0;
            try
            {
                ID = NOfertaEmpleo.ObtenerIDOferta();
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
            return ID;
        }
        private void GuardarCategoriasProfesionales()
        {
            try
            {
                for(int i = 0; i < dgvCategoria.Rows.Count; i++)
                {
                    NOfertaEmpleo.GuardarCategoria(ObtenerIDOferta(), Convert.ToInt32(dgvCategoria.Rows[i].Cells["id_categoria_profesional"].Value), Convert.ToInt32(dgvCategoria.Rows[i].Cells["id_subcategoria_profesional"].Value));
                }
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void Guardar()
        {
            string Rsp = "", contrato = "", horario = "";
            try
            {
                if (txtTitulo.Text == string.Empty)
                {

                }
                else
                {
                    contrato = GenerarContratoTiempo();
                    horario = GenerarHorario();
                    Rsp = NOfertaEmpleo.Agregar(Convert.ToInt32(cboPais.SelectedValue),Convert.ToInt32(cboProvincia.SelectedValue), Obj_Usuario.id_empresa,txtTitulo.Text.Trim(),txtDescripcion.Text.Trim(),Convert.ToInt32(nudVacantes.Value),Convert.ToDouble(nudSalario.Value),horario,contrato,DateTime.Now);
                    GuardarCategoriasProfesionales();
                    if(Rsp == "OK")
                    {
                        MensajeOk("Se guardo correctamente");
                    }
                    else
                    {
                        MensajeError(Rsp);
                    }
                }

            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void QuitarCategoria()
        {
            try
            {
                int row = dgvCategoria.CurrentRow.Index;
                this.TblCategoria.Rows.RemoveAt(row);
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        #region ModificarOferta
        private void MostrarHorarioMod(string horario)
        {
            try
            {
                string[] partsHorario = horario.Split(';');
                string hora = "";
                for(int i = 0; i <= 28; i++)
                {
                    string dtpNombre = "dtpHor" + (i+1);
                    hora = partsHorario[i];
                    DateTimePicker dtp = this.Controls.Find(dtpNombre, true).FirstOrDefault() as DateTimePicker;

                    if (dtp != null)
                    {
                        DateTime horaMod = DateTime.ParseExact(hora, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                        dtp.Value = horaMod;
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void MostrarContratoTiempo(string contrato)
        {
            try
            {
                string[] tiempo = contrato.Split(';');
                nudAnios.Value = Convert.ToInt32(tiempo[0]);
                nudMeses.Value = Convert.ToInt32(tiempo[1]);
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void ObtenerDatosOferta()
        {
            try
            {
                DataTable Tabla = NOfertaEmpleo.BuscarOferta(this.IDOfertaModificar);
                cboPais.SelectedValue = Convert.ToInt32(Tabla.Rows[0]["id_pais"]);
                BuscarProvincia();
                cboProvincia.SelectedValue = Convert.ToInt32(Tabla.Rows[0]["id_provincia"]);
                txtTitulo.Text = Convert.ToString(Tabla.Rows[0]["titulo"]);
                txtDescripcion.Text = Convert.ToString(Tabla.Rows[0]["descripcion"]);
                nudVacantes.Value = Convert.ToInt32(Tabla.Rows[0]["vacantes"]);
                nudSalario.Value = Convert.ToDecimal(Tabla.Rows[0]["salario"]);
                MostrarHorarioMod(Convert.ToString(Tabla.Rows[0]["horario"]));
                MostrarContratoTiempo(Convert.ToString(Tabla.Rows[0]["duracion_contrato"]));
                ObtenerCategoriasOferta();
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void ObtenerCategoriasOferta()
        {
            try
            {
                DataTable Tabla = NOfertaEmpleo.BuscarCategoriaOferta(this.IDOfertaModificar);
                for (int i = 0; i < Tabla.Rows.Count; i++)
                {
                    this.TblCategoria.Rows.Add(Tabla.Rows[i]["id_categoria_profesional"], Tabla.Rows[i]["nombre_categoria_profesional"], Tabla.Rows[i]["id_subcategoria_profesional"], Tabla.Rows[i]["nombre_subcategoria_profesional"]);
                }
                dgvCategoria.Columns[0].Visible = false;
                dgvCategoria.Columns[1].Width = 130;
                dgvCategoria.Columns[2].Visible = false;
                dgvCategoria.Columns[3].Width = 130;
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void EliminarCategoriasProfesionales()
        {
            try
            {
                string Rsp = "";
                int row = 0;
                DialogResult opc = MessageBox.Show("¿Esta seguro que desea eliminar la categoria permanentemente?","Eliminar",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(opc == DialogResult.OK)
                {
                    Rsp = NOfertaEmpleo.EliminarOfertaCategoria(this.IDOfertaModificar, Convert.ToInt32(dgvCategoria.CurrentRow.Cells["id_categoria_profesional"].Value), Convert.ToInt32(dgvCategoria.CurrentRow.Cells["id_subcategoria_profesional"].Value));
                    if(Rsp == "OK")
                    {
                        row = dgvCategoria.CurrentRow.Index;
                        this.TblCategoria.Rows.RemoveAt(row);
                    }
                    else
                    {
                        MensajeError(Rsp);
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void ModificarCategoriasProfesionales()
        {
            try
            {
                for (int i = 0; i < dgvCategoria.Rows.Count; i++)
                {
                    NOfertaEmpleo.ModificarOfertaCategoria(this.IDOfertaModificar, Convert.ToInt32(dgvCategoria.Rows[i].Cells["id_categoria_profesional"].Value), Convert.ToInt32(dgvCategoria.Rows[i].Cells["id_subcategoria_profesional"].Value));
                }
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        private void Modificar()
        {
            string Rsp = "", contrato = "", horario = "";
            try
            {
                if (txtTitulo.Text == string.Empty)
                {

                }
                else
                {
                    contrato = GenerarContratoTiempo();
                    horario = GenerarHorario();
                    Rsp = NOfertaEmpleo.Modificar(this.IDOfertaModificar, Convert.ToInt32(cboPais.SelectedValue), Convert.ToInt32(cboProvincia.SelectedValue), txtTitulo.Text.Trim(), txtDescripcion.Text.Trim(), Convert.ToInt32(nudVacantes.Value), Convert.ToDouble(nudSalario.Value), horario, contrato);
                    ModificarCategoriasProfesionales();
                    if(Rsp == "OK")
                    {
                        MensajeOk("Se modifico correctamente");
                    }
                    else
                    {
                        MensajeError(Rsp);
                    }
                }

            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }
        #endregion ModificarOferta

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!this.OfertaModificar)
            {
                Guardar();
            }
            else
            {
                Modificar();
            }
        }

        private void cboPais_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BuscarProvincia();
        }

        private void cboCategoria_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BuscarSubCategoria();
        }

        private void btnAgrCate_Click(object sender, EventArgs e)
        {
            AccionesPnlCate();
        }

        private void btnCerrarCate_Click(object sender, EventArgs e)
        {
            AccionesPnlCate();
        }

        private void btnGuardarCate_Click(object sender, EventArgs e)
        {
            GuardarCategoria();
        }

        private void dgvCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (!this.OfertaModificar)
                {
                    QuitarCategoria();
                }
                else
                {
                    EliminarCategoriasProfesionales();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!OfertaModificar)
            {
                Limpiar();
            }
            else
            {
                this.Close();
            }
        }
    }
}
