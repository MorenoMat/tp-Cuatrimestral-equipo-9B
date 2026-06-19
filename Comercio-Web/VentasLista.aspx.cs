using System;
using System.Collections.Generic;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class VentasLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            VentasNegocio n = new VentasNegocio();
            List<Venta> lista = n.Listar();
            dgvVentas.DataSource = lista;
            dgvVentas.DataBind();
        }
    }
}
