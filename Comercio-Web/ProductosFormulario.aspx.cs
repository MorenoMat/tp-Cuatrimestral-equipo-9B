using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class ProductosFormulario : System.Web.UI.Page
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
                cargarDropDowns();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    ProductoNegocio negocio = new ProductoNegocio();
                    Producto producto = negocio.BuscarPorId(id);

                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    txtStockActual.Text = producto.StockActual.ToString();
                    txtStockMinimo.Text = producto.StockMinimo.ToString();
                    txtPrecio.Text = producto.UltimoPrecio.ToString();
                    txtGanancia.Text = producto.PorcentajeGanancia.ToString();
                    ddlMarca.SelectedValue = producto.Marca.IdMarca.ToString();
                    ddlCategoria.SelectedValue = producto.Categoria.IdCategoria.ToString();

                    foreach (ListItem item in cblProveedores.Items)
                    {
                        if (producto.Proveedores != null)
                        {
                            item.Selected = producto.Proveedores.Exists(p => p.IdProveedor.ToString() == item.Value);
                        }
                    }

                    lblTitulo.Text = "Editar Producto";
                    btnEliminar.Visible = true;
                }
            }
        }

        private void cargarDropDowns()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            ddlMarca.DataSource = marcaNegocio.Listar();
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "IdMarca";
            ddlMarca.DataBind();

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            ddlCategoria.DataSource = categoriaNegocio.Listar();
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataBind();

            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            cblProveedores.DataSource = proveedorNegocio.Listar();
            cblProveedores.DataTextField = "Nombre";
            cblProveedores.DataValueField = "IdProveedor";
            cblProveedores.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = new Producto();
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.StockActual = int.Parse(txtStockActual.Text);
                producto.StockMinimo = int.Parse(txtStockMinimo.Text);
                producto.UltimoPrecio = decimal.Parse(txtPrecio.Text);
                producto.PorcentajeGanancia = decimal.Parse(txtGanancia.Text);
                producto.Marca = new Marca { IdMarca = int.Parse(ddlMarca.SelectedValue) };
                producto.Categoria = new Categoria { IdCategoria = int.Parse(ddlCategoria.SelectedValue) };

                producto.Proveedores = new List<Proveedor>();
                foreach (ListItem item in cblProveedores.Items)
                {
                    if (item.Selected)
                        producto.Proveedores.Add(new Proveedor { IdProveedor = int.Parse(item.Value) });
                }

                if (Request.QueryString["id"] != null)
                {
                    producto.IdProducto = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(producto);
                }
                else
                {
                    negocio.Agregar(producto);
                }
                Response.Redirect("ProductosLista.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                ProductoNegocio negocio = new ProductoNegocio();
                negocio.Eliminar(id);
                Response.Redirect("ProductosLista.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
