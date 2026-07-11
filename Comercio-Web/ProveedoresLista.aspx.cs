using negocio;
using System;
using System.Web.UI.WebControls;

namespace Comercio_Web
{
    public partial class ProveedoresLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAccesoAdmin(Session["usuario"]))
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla(string busqueda = null)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            bool valorEstado;
            bool? estado = bool.TryParse(ddlEstado.SelectedValue, out valorEstado) ? valorEstado : (bool?)null;
            dgvProveedores.DataSource = negocio.Buscar(busqueda, estado);
            dgvProveedores.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarGrilla(txtBuscar.Text.Trim());
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;
            cargarGrilla();
        }

        protected void dgvProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvProveedores.SelectedDataKey.Value.ToString();
            Response.Redirect("ProveedoresFormulario.aspx?id=" + id);
        }
        protected void chkAccion_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkEstadoProveedor = (CheckBox)sender;
            GridViewRow fila = (GridViewRow)chkEstadoProveedor.NamingContainer;
            ProveedorNegocio negocio = new ProveedorNegocio();
            int idProveedor = Convert.ToInt32(dgvProveedores.DataKeys[fila.RowIndex].Value);
            bool activo = chkEstadoProveedor.Checked;
            negocio.cambiarEstadoProveedor(idProveedor, activo);
            cargarGrilla(txtBuscar.Text.Trim());
        }   
    }
}
