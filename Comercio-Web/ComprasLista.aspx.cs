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
                cargarUsuarios();
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

        private void cargarUsuarios()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            ddlUsuario.DataSource = usuarioNegocio.Listar();
            ddlUsuario.DataTextField = "Nombre";
            ddlUsuario.DataValueField = "IdUsuario";
            ddlUsuario.DataBind();
            ddlUsuario.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        private void cargarGrilla(string busqueda = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            ComprasNegocio n = new ComprasNegocio();
            int idProveedor = int.Parse(ddlProveedor.SelectedValue);
            int idUsuario = int.Parse(ddlUsuario.SelectedValue);
            List<Compra> lista = n.Buscar(busqueda, idProveedor, idUsuario, fechaDesde, fechaHasta);
            dgvCompras.DataSource = lista;
            dgvCompras.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde;
            DateTime fechaHasta;
            DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
            DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;

            cargarGrilla(txtBuscar.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            ddlProveedor.SelectedValue = "0";
            ddlUsuario.SelectedValue = "0";
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;
            cargarGrilla();
        }
    }
}
