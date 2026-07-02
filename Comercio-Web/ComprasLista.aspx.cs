using System;
using System.Collections.Generic;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class ComprasLista : System.Web.UI.Page
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
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            ComprasNegocio n = new ComprasNegocio();
            List<Compra> lista = n.Listar();
            dgvCompras.DataSource = lista;
            dgvCompras.DataBind();
        }
    }
}
