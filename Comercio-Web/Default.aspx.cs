using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class Default : System.Web.UI.Page
    {
        private const string SESSION_LINEAS = "lineasVentaHome";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            CargarAlertasStock();
            if (!IsPostBack)
            {
                CargarDropDowns();
                CargarPrecio();
            CargarGrillaLineas();
            }
            ActualizarTotal();
        }

        // --- Alertas de stock -----------------------------------------------

        private void CargarAlertasStock()
        {
            try
            {
                ProductoNegocio productoNegocio = new ProductoNegocio();
                List<Producto> productos = productoNegocio.Listar();
                List<Producto> bajoStock = productos.FindAll(p => p.StockActual < p.StockMinimo);

                if (bajoStock.Count == 0)
                {
                    Literal sinAlertas = new Literal();
                    sinAlertas.Text = "<div class=\"alert alert-success mb-2\">Todos los productos tienen stock suficiente.</div>";
                    pnlAlertas.Controls.Add(sinAlertas);
                }
                else
                {
                    foreach (Producto p in bajoStock)
                    {
                        Literal alerta = new Literal();
                        alerta.Text = string.Format(
                            "<div class=\"alert-stock\"><strong>{0}</strong> tiene poco stock ({1} unidades)</div>",
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

        // --- Nueva Venta ----------------------------------------------------

        private void CargarDropDowns()
        {
            ClienteNegocio cn = new ClienteNegocio();
            ddlCliente.DataSource = cn.Listar().FindAll(c => c.Activo);
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "IdCliente";
            ddlCliente.DataBind();

            ProductoNegocio pn = new ProductoNegocio();
            ddlProducto.DataSource = pn.Listar();
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataValueField = "IdProducto";
            ddlProducto.DataBind();

            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            lblUsuarioVenta.Text = usuarioLogueado.Nombre;
        }

        private void CargarPrecio()
        {
            if (ddlProducto.Items.Count == 0) return;
            int idProducto = int.Parse(ddlProducto.SelectedValue);
            ProductoNegocio prodN = new ProductoNegocio();
            Producto producto = prodN.BuscarPorId(idProducto);
            txtPrecio.Text = producto.UltimoPrecio.ToString("F2");
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPrecio();
        }

        private List<DetalleVenta> ObtenerLineas()
        {
            if (Session[SESSION_LINEAS] == null)
                Session[SESSION_LINEAS] = new List<DetalleVenta>();
            return (List<DetalleVenta>)Session[SESSION_LINEAS];
        }

        private void CargarGrillaLineas()
        {
            dgvLineas.DataSource = ObtenerLineas();
            dgvLineas.DataBind();
        }

        private void ActualizarTotal()
        {
            decimal total = 0;
            foreach (DetalleVenta d in ObtenerLineas())
                total += d.Cantidad * d.PrecioUnitario;
            lblTotal.Text = total.ToString("N2");
        }

        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            int cantidad;
            decimal precio;

            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                lblMensaje.Text = "La cantidad debe ser un número mayor a 0.";
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text, out precio) || precio < 0)
            {
                lblMensaje.Text = "El precio debe ser un número válido.";
                return;
            }

            int idProducto = int.Parse(ddlProducto.SelectedValue);
            string nombreProducto = ddlProducto.SelectedItem.Text;
            List<DetalleVenta> lineas = ObtenerLineas();

            DetalleVenta linea = lineas.Find(l => l.IdProducto == idProducto);
            if (linea != null)
            {
                linea.Cantidad += cantidad;
                linea.PrecioUnitario = precio;
            }
            else
            {
               
                lineas.Add(new DetalleVenta
                {
                    IdProducto = idProducto,
                    ProductoNombre = nombreProducto,
                    Cantidad = cantidad,
                    PrecioUnitario = precio
                });
            }

            Session[SESSION_LINEAS] = lineas;
            lblMensaje.Text = "";
            ActualizarTotal();
            CargarGrillaLineas();
        }

        protected void dgvLineas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
  
            int idProducto = int.Parse(e.CommandArgument.ToString());
            List<DetalleVenta> lineas = ObtenerLineas(); // carga la lista de  detallesCompra
            DetalleVenta linea = lineas.Find(l => l.IdProducto == idProducto); // carga en el detalleCompra la q coincida con el id
            if (e.CommandName == "Quitar")
            {
                lineas.RemoveAll(l => l.IdProducto == idProducto);
            }
            else if (e.CommandName == "Restar")
            {
                if (linea.Cantidad > 1)
                    linea.Cantidad--;
                else
                    lineas.RemoveAll(l => l.IdProducto == idProducto);
            }
            else if (e.CommandName == "Sumar")
            {
                linea.Cantidad++;
            }
            Session[SESSION_LINEAS] = lineas;
            ActualizarTotal();
            CargarGrillaLineas();
        
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {


            List<DetalleVenta> lineas = ObtenerLineas();
            if (lineas.Count == 0)
            {
                lblMensaje.Text = "Debe agregar al menos un producto.";
                return;
            }

            int nroFactura;
            if (!int.TryParse(txtNumeroFactura.Text, out nroFactura) || nroFactura <= 0)
            {
                lblMensaje.Text = "Ingrese un número de factura válido.";
                return;
            }
            VentasNegocio vn = new VentasNegocio();
            if (vn.nroFacturaExiste(nroFactura))
            {
                lblMensaje.Text = "El número de factura ya existe.";
                return;
            }
            decimal total = 0;
            ProductoNegocio prodN = new ProductoNegocio();
            foreach (DetalleVenta d in lineas)
            {
                total += d.Cantidad * d.PrecioUnitario;
                Producto producto = prodN.BuscarPorId(d.IdProducto);
                if (d.Cantidad > producto.StockActual)
                {
                    lblMensaje.Text = $"No hay stock suficiente para {producto.Nombre}. " +
                                      $"Stock disponible: {producto.StockActual}.";
                    return;
                }
            }

                Venta venta = new Venta();
            venta.NumeroFactura = nroFactura;
            venta.Total = total;
            venta.Cliente = new Cliente { IdCliente = int.Parse(ddlCliente.SelectedValue) };
            venta.Usuario = (Usuario)Session["usuario"];
            venta.detalleVentas = lineas;

            try
            {
               // VentasNegocio vn = new VentasNegocio();
                vn.Registrar(venta);
                Session.Remove(SESSION_LINEAS);
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(SESSION_LINEAS);
            Response.Redirect("Default.aspx");
        }
    }
}
