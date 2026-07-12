using System;
using System.Collections.Generic;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class ComprasDetalle : System.Web.UI.Page
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
                int idCompra;
                if (!int.TryParse(Request.QueryString["id"], out idCompra))
                {
                    Response.Redirect("ComprasLista.aspx", false);
                    return;
                }

                cargarDetalle(idCompra);
            }
        }

        private void cargarDetalle(int idCompra)
        {
            ComprasNegocio negocio = new ComprasNegocio();
            List<DetalleCompra> detalles = negocio.ListarDetalleCompra(idCompra);

            lblDetalleCompraTitulo.Text = "Compra N° " + idCompra;
            dgvDetalleCompra.DataSource = detalles;
            dgvDetalleCompra.DataBind();
        }
    }
}
