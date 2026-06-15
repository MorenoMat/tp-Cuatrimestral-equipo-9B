using System;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class ProveedoresFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    ProveedorNegocio negocio = new ProveedorNegocio();
                    Proveedor proveedor = negocio.BuscarPorId(id);
                    txtNombre.Text = proveedor.Nombre;
                    txtTelefono.Text = proveedor.Telefono;
                    txtEmail.Text = proveedor.Email;
                    lblTitulo.Text = "Editar Proveedor";
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ProveedorNegocio negocio = new ProveedorNegocio();
                Proveedor proveedor = new Proveedor();
                proveedor.Nombre = txtNombre.Text;
                proveedor.Telefono = txtTelefono.Text;
                proveedor.Email = txtEmail.Text;

                if (Request.QueryString["id"] != null)
                {
                    proveedor.IdProveedor = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(proveedor);
                }
                else
                {
                    negocio.Agregar(proveedor);
                }
                Response.Redirect("ProveedoresLista.aspx");
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
                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.Eliminar(id);
                Response.Redirect("ProveedoresLista.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
