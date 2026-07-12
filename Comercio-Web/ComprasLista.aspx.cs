using System;
using System.Web.UI.WebControls;
using Comercio_Web.Helpers;
using negocio;

namespace Comercio_Web
{
    public partial class ComprasLista : System.Web.UI.Page
    {
        private int PaginaActual
        {
            get { return ViewState["PaginaActual"] != null ? (int)ViewState["PaginaActual"] : 1; }
            set { ViewState["PaginaActual"] = value; }
        }

        private int TamanioPagina
        {
            get { return ViewState["TamanioPagina"] != null ? (int)ViewState["TamanioPagina"] : 10; }
            set { ViewState["TamanioPagina"] = value; }
        }

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

                int tamanioPaginaUrl;
                if (int.TryParse(Request.QueryString["tamanio"], out tamanioPaginaUrl) && ddlTamanioPagina.Items.FindByValue(tamanioPaginaUrl.ToString()) != null)
                {
                    TamanioPagina = tamanioPaginaUrl;
                }
                else
                {
                    TamanioPagina = int.Parse(ddlTamanioPagina.SelectedValue);
                }

                ddlTamanioPagina.SelectedValue = TamanioPagina.ToString();

                int paginaUrl;
                PaginaActual = int.TryParse(Request.QueryString["pagina"], out paginaUrl) && paginaUrl > 0 ? paginaUrl : 1;

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

            int totalRegistros = n.Contar(busqueda, idProveedor, idUsuario, fechaDesde, fechaHasta);
            EstadoPaginacion paginacion = PaginacionHelper.Crear(PaginaActual, TamanioPagina, totalRegistros);

            PaginaActual = paginacion.PaginaActual;
            dgvCompras.DataSource = n.BuscarPaginado(busqueda, idProveedor, idUsuario, fechaDesde, fechaHasta, PaginaActual, TamanioPagina);
            dgvCompras.DataBind();

            lblPaginacion.Text = "Mostrando " + paginacion.Desde + "-" + paginacion.Hasta + " de " + paginacion.TotalRegistros + " compras";
            btnAnterior.Enabled = paginacion.PuedeIrAnterior;
            btnSiguiente.Enabled = paginacion.PuedeIrSiguiente;
            btnAnterior.CssClass = btnAnterior.Enabled ? "btn btn-outline-secondary btn-sm" : "btn btn-outline-secondary btn-sm disabled";
            btnSiguiente.CssClass = btnSiguiente.Enabled ? "btn btn-outline-secondary btn-sm" : "btn btn-outline-secondary btn-sm disabled";

            ddlTamanioPagina.SelectedValue = TamanioPagina.ToString();
            rptPaginas.DataSource = paginacion.Paginas;
            rptPaginas.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde;
            DateTime fechaHasta;
            DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
            DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;

            PaginaActual = 1;
            cargarGrilla(txtBuscar.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            ddlProveedor.SelectedValue = "0";
            ddlUsuario.SelectedValue = "0";
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;
            PaginaActual = 1;
            cargarGrilla();
        }

        protected void ddlTamanioPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime fechaDesde;
            DateTime fechaHasta;
            DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
            DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;

            TamanioPagina = int.Parse(ddlTamanioPagina.SelectedValue);
            PaginaActual = 1;
            cargarGrilla(txtBuscar.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 1)
                PaginaActual--;

            DateTime fechaDesde;
            DateTime fechaHasta;
            DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
            DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;
            cargarGrilla(txtBuscar.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            PaginaActual++;

            DateTime fechaDesde;
            DateTime fechaHasta;
            DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
            DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;
            cargarGrilla(txtBuscar.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void rptPaginas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "IrAPagina")
            {
                PaginaActual = int.Parse(e.CommandArgument.ToString());

                DateTime fechaDesde;
                DateTime fechaHasta;
                DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
                DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;
                cargarGrilla(txtBuscar.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
            }
        }

        protected string ObtenerUrlDetalle(object idCompra)
        {
            return "ComprasDetalle.aspx?id=" + idCompra + "&pagina=" + PaginaActual + "&tamanio=" + TamanioPagina;
        }

    }
}
