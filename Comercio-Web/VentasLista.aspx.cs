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

        private void cargarGrilla(string busqueda = null)
        {
            VentasNegocio n = new VentasNegocio();
            List<Venta> lista = n.Buscar(busqueda);
            dgvVentas.DataSource = lista;
            dgvVentas.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarGrilla(txtBuscar.Text.Trim());
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            cargarGrilla();
        }
    }
}
