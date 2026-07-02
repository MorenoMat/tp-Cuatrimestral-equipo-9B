using System;
using System.Web;
using System.Web.UI;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page is Login))
            {
                if (!Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    lblUsuario.Text = usuario.Nombre;

                    bool esAdministrador = Seguridad.esAdmin(Session["usuario"]);
                    navMarcas.Visible = esAdministrador;
                    navCategorias.Visible = esAdministrador;
                    navProveedores.Visible = esAdministrador;
                    navUsuarios.Visible = esAdministrador;
                    navProductos.Visible = esAdministrador;
                    navCompras.Visible = esAdministrador;
                }
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
