using System;
using System.Collections.Generic;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class VentasLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla(string busquedaVenta = null, string busquedaFactura = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            VentasNegocio n = new VentasNegocio();
            List<Venta> lista = n.Buscar(busquedaVenta, busquedaFactura, fechaDesde, fechaHasta);
            dgvVentas.DataSource = lista;
            dgvVentas.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde;
            DateTime fechaHasta;
            DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
            DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;

            cargarGrilla(txtBuscar.Text.Trim(), txtBuscarFactura.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscarFactura.Text = string.Empty;
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;
            cargarGrilla();
        }
    }
}
