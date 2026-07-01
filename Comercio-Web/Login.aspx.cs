using System;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["usuario"] != null)
                Response.Redirect("Default.aspx");
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.UsuarioLogin = txtUsuarioLogin.Text.Trim();
                usuario.Contraseña = txtContrasena.Text;

                UsuarioNegocio negocio = new UsuarioNegocio();
                if (negocio.Login(usuario))
                {
                    Session["usuario"] = usuario;
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al iniciar sesión: " + ex.Message;
                lblError.Visible = true;
            }
        }
    }
}
