using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Dominio;
using negocio;

namespace Comercio_Web.Helpers
{
    public static class AlertasStockHelper
    {
        public static void Cargar(Panel panelAlertas)
        {
            try
            {
                panelAlertas.Controls.Clear();

                List<Producto> bajoStock = ObtenerProductosBajoStock();

                if (bajoStock.Count == 0)
                {
                    Literal sinAlertas = new Literal();
                    sinAlertas.Text = "<div class=\"alert alert-success mb-2\">Todos los productos tienen stock suficiente.</div>";
                    panelAlertas.Controls.Add(sinAlertas);
                }
                else
                {
                    foreach (Producto producto in bajoStock)
                    {
                        Literal alerta = new Literal();
                        alerta.Text = string.Format(
                            "<div class=\"alert-stock\"><strong>{0}</strong> tiene poco stock ({1} unidades)</div>",
                            HttpUtility.HtmlEncode(producto.Nombre),
                            producto.StockActual);
                        panelAlertas.Controls.Add(alerta);
                    }
                }
            }
            catch (Exception ex)
            {
                Literal error = new Literal();
                error.Text = string.Format(
                    "<div class=\"alert alert-warning\">Error al cargar alertas: {0}</div>",
                    HttpUtility.HtmlEncode(ex.Message));
                panelAlertas.Controls.Add(error);
            }
        }

        public static int ObtenerCantidadProductosBajoStock()
        {
            return ObtenerProductosBajoStock().Count;
        }

        private static List<Producto> ObtenerProductosBajoStock()
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Producto> productos = productoNegocio.Listar();
            return productos.FindAll(p => p.StockActual < p.StockMinimo);
        }
    }
}
