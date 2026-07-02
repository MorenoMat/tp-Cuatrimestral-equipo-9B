using System;
using System.Data.SqlClient;
using System.Web.Services.Description;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class ProveedoresFormulario : System.Web.UI.Page
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
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    ProveedorNegocio negocio = new ProveedorNegocio();
                    Proveedor proveedor = negocio.BuscarPorId(id);
                    txtNombre.Text = proveedor.Nombre;
                    txtTelefono.Text = proveedor.Telefono;
                    txtEmail.Text = proveedor.Email;
                    txtCuit.Text = proveedor.Cuit;  
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
                proveedor.Cuit = txtCuit.Text;

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
            catch (SqlException ex)
            {
                Response.Write("No se ha podido guardar el proveedor por ERROR SQL" + ex.Message);
                
            }
            catch (Exception ex)
            {
                Response.Write("No se ha podido guardar el proveedor por " + ex.Message);
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
