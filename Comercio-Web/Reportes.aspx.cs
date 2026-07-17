using System;
using negocio;

namespace Comercio_Web
{
    public partial class Reportes : System.Web.UI.Page
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
                VentasNegocio ventasNegocio = new VentasNegocio();

                decimal facturacionDia = ventasNegocio.ObtenerTotalFacturadoHoyGeneral();
                decimal facturacionMes = ventasNegocio.ObtenerTotalFacturadoMesGeneral();
                int cantidadVentasMes = ventasNegocio.ObtenerCantidadVentasMesGeneral();

                lblFacturacionDia.Text = facturacionDia.ToString("N2");
                lblFacturacionMes.Text = facturacionMes.ToString("N2");
                lblVentasMes.Text = cantidadVentasMes.ToString();

                CargarTopVendedores();
            }
        }

        protected void ddlPeriodoTopVendedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTopVendedores();
        }

        private void CargarTopVendedores()
        {
            VentasNegocio ventasNegocio = new VentasNegocio();
            dgvTopVendedores.DataSource = ventasNegocio.ObtenerTopVendedores(ddlPeriodoTopVendedores.SelectedValue);
            dgvTopVendedores.DataBind();
        }
    }
}
