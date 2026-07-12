using System;
using System.Web.UI.WebControls;
using Comercio_Web.Helpers;
using negocio;

namespace Comercio_Web
{
    public partial class VentasLista : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                cargarClientes();
                cargarUsuarios();
                TamanioPagina = int.Parse(ddlTamanioPagina.SelectedValue);
                PaginaActual = 1;
                cargarGrilla();
            }
        }

        private void cargarClientes()
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            ddlCliente.DataSource = clienteNegocio.Listar();
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "IdCliente";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem("TODOS", "0"));
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

        private void cargarGrilla(string busquedaVenta = null, string busquedaFactura = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            VentasNegocio n = new VentasNegocio();
            int idCliente = int.Parse(ddlCliente.SelectedValue);
            int idUsuario = int.Parse(ddlUsuario.SelectedValue);

            int totalRegistros = n.Contar(busquedaVenta, busquedaFactura, idCliente, idUsuario, fechaDesde, fechaHasta);
            EstadoPaginacion paginacion = PaginacionHelper.Crear(PaginaActual, TamanioPagina, totalRegistros);

            PaginaActual = paginacion.PaginaActual;
            dgvVentas.DataSource = n.BuscarPaginado(busquedaVenta, busquedaFactura, idCliente, idUsuario, fechaDesde, fechaHasta, PaginaActual, TamanioPagina);
            dgvVentas.DataBind();

            lblPaginacion.Text = "Mostrando " + paginacion.Desde + "-" + paginacion.Hasta + " de " + paginacion.TotalRegistros + " ventas";
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
            cargarGrilla(txtBuscar.Text.Trim(), txtBuscarFactura.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscarFactura.Text = string.Empty;
            ddlCliente.SelectedValue = "0";
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
            cargarGrilla(txtBuscar.Text.Trim(), txtBuscarFactura.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 1)
                PaginaActual--;

            DateTime fechaDesde;
            DateTime fechaHasta;
            DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
            DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;
            cargarGrilla(txtBuscar.Text.Trim(), txtBuscarFactura.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            PaginaActual++;

            DateTime fechaDesde;
            DateTime fechaHasta;
            DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
            DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;
            cargarGrilla(txtBuscar.Text.Trim(), txtBuscarFactura.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
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
                cargarGrilla(txtBuscar.Text.Trim(), txtBuscarFactura.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
            }
        }
    }
}
