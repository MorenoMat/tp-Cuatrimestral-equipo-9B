using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class ComprasLista : System.Web.UI.Page
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
                cargarProveedores();
                cargarGrilla();
            }
        }

        private void cargarProveedores()
        {
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            ddlProveedor.DataSource = proveedorNegocio.Listar();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "IdProveedor";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        private void cargarGrilla(string busqueda = null)
        {
            ComprasNegocio n = new ComprasNegocio();
            int idProveedor = int.Parse(ddlProveedor.SelectedValue);
            List<Compra> lista = n.Buscar(busqueda, idProveedor);
            dgvCompras.DataSource = lista;
            dgvCompras.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarGrilla(txtBuscar.Text.Trim());
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            ddlProveedor.SelectedValue = "0";
            cargarGrilla();
        }
    }
}
