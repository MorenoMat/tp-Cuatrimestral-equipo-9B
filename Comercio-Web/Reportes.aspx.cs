using System;
using negocio;

namespace Comercio_Web
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAccesoAdmin(Session["usuario"]))
            {
                Response.Redirect("Default.aspx", false);
                return;
            }
        }
    }
}
