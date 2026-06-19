using System;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class UsuariosFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    Usuario usuario = negocio.BuscarPorId(id);
                    txtNombre.Text = usuario.Nombre;
                    txtUsuarioLogin.Text = usuario.UsuarioLogin;
                    chkAdmin.Checked = usuario.Admin;
                    lblTitulo.Text = "Editar Usuario";
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = new Usuario();
                usuario.Nombre = txtNombre.Text;
                usuario.UsuarioLogin = txtUsuarioLogin.Text;
                usuario.Contraseña = txtContraseña.Text;
                usuario.Admin = chkAdmin.Checked;

                if (Request.QueryString["id"] != null)
                {
                    usuario.IdUsuario = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(usuario);
                }
                else
                {
                    negocio.Agregar(usuario);
                }
                Response.Redirect("UsuariosLista.aspx");
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
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Eliminar(id);
                Response.Redirect("UsuariosLista.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
