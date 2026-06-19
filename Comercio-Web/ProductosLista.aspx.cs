using System;
using negocio;

namespace Comercio_Web
{
    public partial class ProductosLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            dgvProductos.DataSource = negocio.Listar();
            dgvProductos.DataBind();
        }

        protected void dgvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvProductos.SelectedDataKey.Value.ToString();
            Response.Redirect("ProductosFormulario.aspx?id=" + id);
        }
    }
}
