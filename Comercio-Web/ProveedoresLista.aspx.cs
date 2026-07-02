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

        private void cargarGrilla()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            dgvProveedores.DataSource = negocio.Listar();
            dgvProveedores.DataBind();
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
            cargarGrilla();
        }   
    }
}
