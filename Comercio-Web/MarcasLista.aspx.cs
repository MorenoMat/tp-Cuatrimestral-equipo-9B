using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace Comercio_Web
{
    public partial class MarcasLista : System.Web.UI.Page
    {
        private class PaginaItem
        {
            public int Numero { get; set; }
            public bool Actual { get; set; }
        }

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
            MarcaNegocio negocio = new MarcaNegocio();
            int totalRegistros = negocio.Contar(busqueda);
            int totalPaginas = totalRegistros == 0 ? 1 : (int)Math.Ceiling((double)totalRegistros / TamanioPagina);

            if (PaginaActual > totalPaginas)
                PaginaActual = totalPaginas;
            if (PaginaActual < 1)
                PaginaActual = 1;

            dgvMarcas.DataSource = negocio.BuscarPaginado(busqueda, PaginaActual, TamanioPagina);
            dgvMarcas.DataBind();

            int desde = totalRegistros == 0 ? 0 : ((PaginaActual - 1) * TamanioPagina) + 1;
            int hasta = totalRegistros == 0 ? 0 : Math.Min(PaginaActual * TamanioPagina, totalRegistros);

            lblPaginacion.Text = "Mostrando " + desde + "-" + hasta + " de " + totalRegistros + " marcas";
            btnAnterior.Enabled = PaginaActual > 1;
            btnSiguiente.Enabled = PaginaActual < totalPaginas;
            btnAnterior.CssClass = btnAnterior.Enabled ? "btn btn-outline-secondary btn-sm" : "btn btn-outline-secondary btn-sm disabled";
            btnSiguiente.CssClass = btnSiguiente.Enabled ? "btn btn-outline-secondary btn-sm" : "btn btn-outline-secondary btn-sm disabled";

            ddlTamanioPagina.SelectedValue = TamanioPagina.ToString();
            cargarPaginas(totalPaginas);
        }

        private void cargarPaginas(int totalPaginas)
        {
            List<PaginaItem> paginas = new List<PaginaItem>();

            for (int i = 1; i <= totalPaginas; i++)
            {
                paginas.Add(new PaginaItem
                {
                    Numero = i,
                    Actual = i == PaginaActual
                });
            }

            rptPaginas.DataSource = paginas;
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

        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvMarcas.SelectedDataKey.Value.ToString();
            Response.Redirect("MarcasFormulario.aspx?id=" + id); // redirecciona a MarcasFormulario.aspx
        }
    }
}
