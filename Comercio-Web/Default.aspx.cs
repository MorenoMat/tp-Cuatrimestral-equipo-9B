using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio_Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAlertasStock();
            }
        }

        private void CargarAlertasStock()
        {
            try
            {
                negocio.ProductoNegocio productoNegocio = new negocio.ProductoNegocio();
                System.Collections.Generic.List<Dominio.Producto> productos = productoNegocio.Listar();

                var bajoStock = productos.FindAll(p => p.StockActual < p.StockMinimo);

                if (bajoStock.Count == 0)
                {
                    Literal sinAlertas = new Literal();
                    sinAlertas.Text = "<div class=\"alert alert-success mb-2\">Todos los productos tienen stock suficiente.</div>";
                    pnlAlertas.Controls.Add(sinAlertas);
                }
                else
                {
                    foreach (Dominio.Producto p in bajoStock)
                    {
                        Literal alerta = new Literal();
                        alerta.Text = string.Format(
                            "<div class=\"alert alert-danger mb-2\"><strong>{0}</strong> tiene poco stock ({1} unidades)</div>",
                            System.Web.HttpUtility.HtmlEncode(p.Nombre),
                            p.StockActual);
                        pnlAlertas.Controls.Add(alerta);
                    }
                }
            }
            catch (Exception ex)
            {
                Literal error = new Literal();
                error.Text = string.Format(
                    "<div class=\"alert alert-warning\">Error al cargar alertas: {0}</div>",
                    System.Web.HttpUtility.HtmlEncode(ex.Message));
                pnlAlertas.Controls.Add(error);
            }
        }
    }
}