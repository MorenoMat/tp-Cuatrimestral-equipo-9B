using Dominio;
using negocio;
using System;
using System.Data.SqlClient;

namespace Comercio_Web
{
    public partial class UsuariosFormulario : System.Web.UI.Page
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
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    Usuario usuario = negocio.BuscarPorId(id);
                    txtNombre.Text = usuario.Nombre;
                    txtUsuarioLogin.Text = usuario.UsuarioLogin;
                    chkAdmin.Checked = usuario.esAdmin;
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
                usuario.esAdmin = chkAdmin.Checked;

                if (Request.QueryString["id"] != null) // edicion
                 {
                   usuario.IdUsuario = int.Parse(Request.QueryString["id"]);
                    if (!string.IsNullOrEmpty(txtContraseña.Text))
                        usuario.Contraseña = Seguridad.HashearContraseña(txtContraseña.Text);
                     else
                     {
                        Usuario existente = negocio.BuscarPorId(usuario.IdUsuario);
                        usuario.Contraseña = existente.Contraseña;
                     }

                    negocio.Modificar(usuario);
                 }

               else //agregar
                {
                    if(negocio.BuscarPorUsuario(usuario.UsuarioLogin) != null)
                    {
                        Response.Write("El nombre de usuario ya existe.");
                        return;
                    }
                    usuario.Contraseña = Seguridad.HashearContraseña(txtContraseña.Text);
                    negocio.Agregar(usuario);
                }
               Response.Redirect("UsuariosLista.aspx");
            }
            catch (SqlException ex)
            {
                Response.Write("No se ha podido guardar el usuario por ERROR SQL: " + ex.Message);

            }
            catch (Exception ex)
            {
                Response.Write("No se ha podido guardar el usuario por " + ex.Message);
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
