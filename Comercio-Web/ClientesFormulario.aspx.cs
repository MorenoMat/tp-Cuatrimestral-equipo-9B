using System;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class ClientesFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    ClienteNegocio negocio = new ClienteNegocio();
                    Cliente cliente = negocio.BuscarPorId(id);
                    txtDni.Text = cliente.Dni.ToString();
                    txtNombre.Text = cliente.Nombre;
                    txtEmail.Text = cliente.Email;
                    lblTitulo.Text = "Editar Cliente";
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteNegocio negocio = new ClienteNegocio();
                Cliente cliente = new Cliente();
                cliente.Dni = int.Parse(txtDni.Text);
                cliente.Nombre = txtNombre.Text;
                cliente.Email = txtEmail.Text;

                if (Request.QueryString["id"] != null)
                {
                    cliente.IdCliente = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(cliente);
                }
                else
                {
                    negocio.Agregar(cliente);
                }
                Response.Redirect("ClientesLista.aspx");
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
                ClienteNegocio negocio = new ClienteNegocio();
                negocio.Eliminar(id);
                Response.Redirect("ClientesLista.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
