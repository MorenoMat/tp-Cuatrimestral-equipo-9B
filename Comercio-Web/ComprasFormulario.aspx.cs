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
            if (!IsPostBack)
            {
                Session.Remove(SESSION_LINEAS);
                cargarDropDowns();
                cargarPrecio();
            }
            cargarGrillaLineas();

        }

        private void cargarDropDowns()
        {
            ProveedorNegocio provNeg = new ProveedorNegocio();
            ddlProveedor.DataSource = provNeg.Listar();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "IdProveedor";
            ddlProveedor.DataBind();

            UsuarioNegocio usuNeg = new UsuarioNegocio();
            ddlUsuario.DataSource = usuNeg.Listar();
            ddlUsuario.DataTextField = "Nombre";
            ddlUsuario.DataValueField = "IdUsuario";
            ddlUsuario.DataBind();

            ProductoNegocio prodN = new ProductoNegocio();
            ddlProducto.DataSource = prodN.Listar();
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataValueField = "IdProducto";
            ddlProducto.DataBind();

        }
        private void cargarPrecio()
        {
            int idProducto = int.Parse(ddlProducto.SelectedValue);
            ProductoNegocio prodN = new ProductoNegocio();
            Producto producto = prodN.BuscarPorId(idProducto);
            txtPrecio.Text = producto.UltimoPrecio.ToString("F2"); //  f2 = lo paso a 2 decimales
            Session["PrecioActual"] = producto.UltimoPrecio.ToString("F2");
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
            cargarGrillaLineas();
        }

        protected void dgvLineas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                int idProducto = (int)dgvLineas.DataKeys[index].Value;
                List<DetalleCompra> lineas = ObtenerLineas();
                lineas.RemoveAll(l => l.IdProducto == idProducto);
                Session[SESSION_LINEAS] = lineas;
                cargarGrillaLineas();
            }
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
            compra.FechaCompra = DateTime.Now;
            compra.Proveedor = new Proveedor { IdProveedor = int.Parse(ddlProveedor.SelectedValue) };
            compra.Usuario = new Usuario { IdUsuario = int.Parse(ddlUsuario.SelectedValue) };
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
