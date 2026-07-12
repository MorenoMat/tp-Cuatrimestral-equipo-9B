using Dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Comercio_Web
{
    public partial class VentasFormulario : System.Web.UI.Page
    {
        private const string SESSION_LINEAS = "lineasVenta";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove(SESSION_LINEAS);
                cargarDropDowns();
                cargarPrecio();
            cargarGrillaLineas();
            }
            actualizarTotal();
        }

        private void cargarDropDowns()
        {
            ClienteNegocio cn = new ClienteNegocio();
            ddlCliente.DataSource = cn.Listar();
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "IdCliente";
            ddlCliente.DataBind();

            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            lblUsuarioVenta.Text = usuarioLogueado.Nombre;

            ProductoNegocio pn = new ProductoNegocio();
            ddlProducto.DataSource = pn.Listar();
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataValueField = "IdProducto";
            ddlProducto.DataBind();
        }
        private void cargarPrecio()
        {
            int idProducto = int.Parse(ddlProducto.SelectedValue);
            ProductoNegocio prodN = new ProductoNegocio();
            Producto producto = prodN.BuscarPorId(idProducto);

            decimal precioVenta = producto.UltimoPrecio + (producto.UltimoPrecio * (producto.PorcentajeGanancia / 100m));
            txtPrecio.Text = precioVenta.ToString("F2");
            Session["PrecioActual"] = precioVenta.ToString("F2");
        }
        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarPrecio();
        }
        private List<DetalleVenta> ObtenerLineas()
        {
            if (Session[SESSION_LINEAS] == null)
                Session[SESSION_LINEAS] = new List<DetalleVenta>();
            return (List<DetalleVenta>)Session[SESSION_LINEAS];
        }

        private void cargarGrillaLineas()
        {
            dgvLineas.DataSource = ObtenerLineas();
            dgvLineas.DataBind();
        }

        private void actualizarTotal()
        {
            decimal total = 0;
            foreach (DetalleVenta d in ObtenerLineas())
                total += d.Cantidad * d.PrecioUnitario;
            lblTotal.Text = total.ToString("N2");
        }

        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            int idProducto = int.Parse(ddlProducto.SelectedValue);
            string nombreProducto = ddlProducto.SelectedItem.Text;
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
                    PrecioUnitario = precio,  
                });
            }

            Session[SESSION_LINEAS] = lineas;
            lblMensaje.Text = "";
            actualizarTotal();
            cargarGrillaLineas();
        }

        protected void dgvLineas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
            actualizarTotal();
            cargarGrillaLineas();
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
                lblMensaje.Text = "Ingrese un númerooooooooooooooo de factura válido.";
                return;
            }





            decimal total = 0;
            foreach (DetalleVenta d in lineas)
                total += d.Cantidad * d.PrecioUnitario;

            Venta venta = new Venta();
            venta.NumeroFactura = nroFactura; 
            venta.Total = total;
            venta.Cliente = new Cliente { IdCliente = int.Parse(ddlCliente.SelectedValue) };
            venta.Usuario = (Usuario)Session["usuario"];
            venta.detalleVentas = lineas;

            try
            {
                VentasNegocio vn = new VentasNegocio();
                vn.Registrar(venta);
                Session.Remove(SESSION_LINEAS);
                Response.Redirect("VentasLista.aspx");

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }
    }
}
