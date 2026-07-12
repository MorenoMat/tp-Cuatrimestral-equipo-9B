using System;
using System.Web.UI.WebControls;
using Comercio_Web.Helpers;
using negocio;

namespace Comercio_Web
{
    public partial class CategoriasLista : System.Web.UI.Page
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
                TamanioPagina = int.Parse(ddlTamanioPagina.SelectedValue);
                PaginaActual = 1;
                cargarGrilla();
            }
        }

        private void cargarGrilla(string busqueda = null)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            int totalRegistros = negocio.Contar(busqueda);
            EstadoPaginacion paginacion = PaginacionHelper.Crear(PaginaActual, TamanioPagina, totalRegistros);

            PaginaActual = paginacion.PaginaActual;
            dgvCategorias.DataSource = negocio.BuscarPaginado(busqueda, PaginaActual, TamanioPagina);
            dgvCategorias.DataBind();

            lblPaginacion.Text = "Mostrando " + paginacion.Desde + "-" + paginacion.Hasta + " de " + paginacion.TotalRegistros + " categorías";
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

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvCategorias.SelectedDataKey.Value.ToString();
            Response.Redirect("CategoriasFormulario.aspx?id=" + id);
        }
    }
}
