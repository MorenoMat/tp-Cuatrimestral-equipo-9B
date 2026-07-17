using System;
using System.Collections.Generic;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class VentasDetalle : System.Web.UI.Page
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
                int idVenta;
                if (!int.TryParse(Request.QueryString["id"], out idVenta))
                {
                    Response.Redirect("VentasLista.aspx", false);
                    return;
                }

                configurarVolver();
                cargarDetalle(idVenta);
            }
        }

        private void cargarDetalle(int idVenta)
        {
            VentasNegocio negocio = new VentasNegocio();
            Venta venta = negocio.ObtenerPorId(idVenta);
            if (venta == null)
            {
                Response.Redirect("VentasLista.aspx", false);
                return;
            }

            List<DetalleVenta> detalles = negocio.ListarDetalleVenta(idVenta);

            lblDetalleVentaTitulo.Text = "Venta N° " + idVenta;
            lblTotalValor.Text = venta.Total.ToString("N2");
            lblCliente.Text = venta.Cliente.Nombre;
            lblUsuario.Text = venta.Usuario.Nombre;

            dgvDetalleVenta.DataSource = detalles;
            dgvDetalleVenta.DataBind();
        }

        private void configurarVolver()
        {
            int pagina;
            int tamanio;

            bool paginaValida = int.TryParse(Request.QueryString["pagina"], out pagina) && pagina > 0;
            bool tamanioValido = int.TryParse(Request.QueryString["tamanio"], out tamanio) && tamanio > 0;

            string url = "VentasLista.aspx";
            if (paginaValida || tamanioValido)
            {
                url += "?";
                if (paginaValida)
                    url += "pagina=" + pagina;

                if (tamanioValido)
                    url += (paginaValida ? "&" : string.Empty) + "tamanio=" + tamanio;
            }

            lnkVolver.NavigateUrl = url;
        }
    }
}
