using System;
using Comercio_Web.Helpers;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                lblSaludo.Text = "HOLA " + usuario.Nombre;

                VentasNegocio ventasNegocio = new VentasNegocio();
                decimal totalFacturadoHoy = ventasNegocio.ObtenerTotalFacturadoHoyPorUsuario(usuario.IdUsuario);
                decimal totalFacturadoMes = ventasNegocio.ObtenerTotalFacturadoMesPorUsuario(usuario.IdUsuario);

                lblVentasDia.Text = totalFacturadoHoy.ToString("N2");
                lblVentasMes.Text = totalFacturadoMes.ToString("N2");

                int cantidadBajoStock = AlertasStockHelper.ObtenerCantidadProductosBajoStock();
                lblAlertasStock.Text = cantidadBajoStock == 1
                    ? "1 producto bajo"
                    : cantidadBajoStock + " productos bajos";
                lblAlertasStock.CssClass = cantidadBajoStock == 0 ? "text-success" : "text-danger";
            }
        }
    }
}
