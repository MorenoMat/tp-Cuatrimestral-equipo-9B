using System;
using System.Collections.Generic;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class ComprasFormulario : System.Web.UI.Page
    {
        private const string SESSION_LINEAS = "lineasCompra";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAccesoAdmin(Session["usuario"]))
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                Session.Remove(SESSION_LINEAS);
                cargarDropDowns();
                cargarGrillaLineas();
            }
        }

        private void cargarDropDowns()
        {
            ProveedorNegocio provNeg = new ProveedorNegocio();
            ddlProveedor.DataSource = provNeg.Listar().FindAll(p => p.Activo);
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "IdProveedor";
            ddlProveedor.DataBind();

            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            lblUsuarioCompra.Text = usuarioLogueado != null ? usuarioLogueado.Nombre : string.Empty;

            cargarProductosPorProveedor();
        }

        private void cargarProductosPorProveedor()
        {
            int idProveedor = int.Parse(ddlProveedor.SelectedValue);
            ProductoNegocio prodN = new ProductoNegocio();
            ddlProducto.DataSource = prodN.ListarPorProveedor(idProveedor);
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataValueField = "IdProducto";
            ddlProducto.DataBind();

            if (ddlProducto.Items.Count > 0)
            {
                cargarPrecio();
            }
            else
            {
                txtPrecio.Text = "0,00";
                lblStock.Text = "0";
                Session["PrecioActual"] = "0,00";
            }
        }

        private void cargarPrecio()
        {
            if (ddlProducto.Items.Count == 0)
                return;

            int idProducto = int.Parse(ddlProducto.SelectedValue);
            ProductoNegocio prodN = new ProductoNegocio();
            Producto producto = prodN.BuscarPorId(idProducto);
            txtPrecio.Text = producto.UltimoPrecio.ToString("F2");
            lblStock.Text = producto.StockActual.ToString();
            Session["PrecioActual"] = producto.UltimoPrecio.ToString("F2");
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProductosPorProveedor();
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarPrecio();
        }

        private List<DetalleCompra> ObtenerLineas()
        {
            if (Session[SESSION_LINEAS] == null)
                Session[SESSION_LINEAS] = new List<DetalleCompra>();
            return (List<DetalleCompra>)Session[SESSION_LINEAS];
        }

        private void cargarGrillaLineas()
        {
            dgvLineas.DataSource = ObtenerLineas();
            dgvLineas.DataBind();
        }
        private void actualizarTotal()
        {
            decimal total = 0;
            foreach (DetalleCompra d in ObtenerLineas())
                total += d.Cantidad * d.PrecioUnitario;
            lblTotal.Text = total.ToString("N2");   
        }

        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            if (ddlProducto.Items.Count == 0)
            {
                lblMensaje.Text = "El proveedor seleccionado no tiene productos disponibles.";
                return;
            }

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

            List<DetalleCompra> lineas = ObtenerLineas();

            DetalleCompra linea = lineas.Find(l => l.IdProducto == idProducto);
            if (linea != null)
            {
                linea.Cantidad += cantidad;
                linea.PrecioUnitario = precio;
            }
            else
            {
                lineas.Add(new DetalleCompra
                {
                    IdProducto = idProducto,
                    ProductoNombre = nombreProducto,
                    Cantidad = cantidad,
                    PrecioUnitario = precio
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
            List<DetalleCompra> lineas = ObtenerLineas(); // carga la lista de  detallesCompra
            DetalleCompra linea = lineas.Find(l => l.IdProducto == idProducto); // carga en el detalleCompra la q coincida con el id
            if (e.CommandName == "Quitar")
            { 
                lineas.RemoveAll(l => l.IdProducto == idProducto);
            }
            else if(e.CommandName == "Restar")
               { if(linea.Cantidad > 1) 
                    linea.Cantidad --;
                    else 
                     lineas.RemoveAll(l => l.IdProducto == idProducto);
            }
            else if(e.CommandName == "Sumar")
            {
                linea.Cantidad++;
            }   

            Session[SESSION_LINEAS] = lineas;
            actualizarTotal();
            cargarGrillaLineas();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            List<DetalleCompra> lineas = ObtenerLineas();
            if (lineas.Count == 0)
            {
                lblMensaje.Text = "Debe agregar al menos un producto.";
                return;
            }

            Compra compra = new Compra();
            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            if (usuarioLogueado == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            compra.FechaCompra = DateTime.Now;
            compra.Proveedor = new Proveedor { IdProveedor = int.Parse(ddlProveedor.SelectedValue) };
            compra.Usuario = usuarioLogueado;
            compra.DetalleCompras = lineas;

            try
            {
                ComprasNegocio cn = new ComprasNegocio();
                cn.Registrar(compra);
                Session.Remove(SESSION_LINEAS);
                Response.Redirect("ComprasLista.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }
    }
}
