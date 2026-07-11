using System;
using negocio;

namespace Comercio_Web
{
    public partial class CategoriasLista : System.Web.UI.Page
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
            CategoriaNegocio negocio = new CategoriaNegocio();
            dgvCategorias.DataSource = negocio.Buscar(busqueda);
            dgvCategorias.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarGrilla(txtBuscar.Text.Trim());
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            cargarGrilla();
        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvCategorias.SelectedDataKey.Value.ToString();
            Response.Redirect("CategoriasFormulario.aspx?id=" + id);
        }
    }
}
