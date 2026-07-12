using System;
using Dominio;

namespace Comercio_Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                lblSaludo.Text = "HOLA " + usuario.Nombre;
            }
        }
    }
}
