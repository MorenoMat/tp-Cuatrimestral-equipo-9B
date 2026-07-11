using System;
using System.Web.UI;
using negocio;

namespace Comercio_Web
{
    public partial class MarcasLista : System.Web.UI.Page
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

        private void cargarGrilla(string busqueda = null)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            dgvMarcas.DataSource = negocio.Buscar(busqueda);
            dgvMarcas.DataBind();
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

        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvMarcas.SelectedDataKey.Value.ToString();
            Response.Redirect("MarcasFormulario.aspx?id=" + id); // redirecciona a MarcasFormulario.aspx
        }
    }
}
