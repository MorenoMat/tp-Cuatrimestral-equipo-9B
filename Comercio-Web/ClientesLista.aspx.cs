using Comercio_Web.Helpers;
using negocio;
using System;
using System.Web.UI.WebControls;

namespace Comercio_Web
{
    public partial class ClientesLista : System.Web.UI.Page
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
                TamanioPagina = int.Parse(ddlTamanioPagina.SelectedValue);
                PaginaActual = 1;
                cargarGrilla();
            }
        }

        private void cargarGrilla(string busqueda = null)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            bool valorEstado;
            bool? estado = bool.TryParse(ddlEstado.SelectedValue, out valorEstado) ? valorEstado : (bool?)null;

            int totalRegistros = negocio.Contar(busqueda, estado);
            EstadoPaginacion paginacion = PaginacionHelper.Crear(PaginaActual, TamanioPagina, totalRegistros);

            PaginaActual = paginacion.PaginaActual;
            bool hayFiltros = !string.IsNullOrWhiteSpace(busqueda) || estado.HasValue;
            TablaSinResultadosHelper.Aplicar(dgvClientes, hayFiltros);
            dgvClientes.DataSource = negocio.BuscarPaginado(busqueda, estado, PaginaActual, TamanioPagina);
            dgvClientes.DataBind();

            lblPaginacion.Text = "Mostrando " + paginacion.Desde + "-" + paginacion.Hasta + " de " + paginacion.TotalRegistros + " clientes";
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
            PaginaActual = 1;
            cargarGrilla(txtBuscar.Text.Trim());
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;
            PaginaActual = 1;
            cargarGrilla();
        }

        protected void ddlTamanioPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            TamanioPagina = int.Parse(ddlTamanioPagina.SelectedValue);
            PaginaActual = 1;
            cargarGrilla(txtBuscar.Text.Trim());
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 1)
                PaginaActual--;

            cargarGrilla(txtBuscar.Text.Trim());
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            PaginaActual++;
            cargarGrilla(txtBuscar.Text.Trim());
        }

        protected void rptPaginas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "IrAPagina")
            {
                PaginaActual = int.Parse(e.CommandArgument.ToString());
                cargarGrilla(txtBuscar.Text.Trim());
            }
        }

        protected void dgvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvClientes.SelectedDataKey.Value.ToString();
            Response.Redirect("ClientesFormulario.aspx?id=" + id);
        }

        protected void chkAccion_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkEstadoCliente = (CheckBox)sender;
            GridViewRow fila = (GridViewRow)chkEstadoCliente.NamingContainer;
            ClienteNegocio negocio = new ClienteNegocio();
            int idCliente = Convert.ToInt32(dgvClientes.DataKeys[fila.RowIndex].Value);
            bool activo = chkEstadoCliente.Checked;
            negocio.cambiarEstadoCliente(idCliente, activo);
            cargarGrilla(txtBuscar.Text.Trim());
        }
    }
}
