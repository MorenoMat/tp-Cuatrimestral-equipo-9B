using System;
using System.Web.UI.WebControls;
using Comercio_Web.Helpers;
using negocio;

namespace Comercio_Web
{
    public partial class ProductosLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAccesoAdmin(Session["usuario"]))
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            AlertasStockHelper.Cargar(pnlAlertas);

            if (!IsPostBack)
            {
                cargarMarcas();
                cargarCategorias();
                cargarGrilla();
            }
        }

        private void cargarMarcas()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            ddlMarca.DataSource = marcaNegocio.Listar();
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "IdMarca";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("TODAS", "0"));
        }

        private void cargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            ddlCategoria.DataSource = categoriaNegocio.Listar();
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("TODAS", "0"));
        }

        private void cargarGrilla(string busqueda = null)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            int idMarca = int.Parse(ddlMarca.SelectedValue);
            int idCategoria = int.Parse(ddlCategoria.SelectedValue);
            dgvProductos.DataSource = negocio.Buscar(busqueda, idMarca, idCategoria);
            dgvProductos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarGrilla(txtBuscar.Text.Trim());
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            ddlMarca.SelectedValue = "0";
            ddlCategoria.SelectedValue = "0";
            cargarGrilla();
        }

        protected void dgvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvProductos.SelectedDataKey.Value.ToString();
            Response.Redirect("ProductosFormulario.aspx?id=" + id);
        }
    }
}
