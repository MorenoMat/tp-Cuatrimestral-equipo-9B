using System;
using negocio;

namespace Comercio_Web
{
    public partial class UsuariosLista : System.Web.UI.Page
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
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvUsuarios.DataSource = negocio.Buscar(busqueda);
            dgvUsuarios.DataBind();
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

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvUsuarios.SelectedDataKey.Value.ToString();
            Response.Redirect("UsuariosFormulario.aspx?id=" + id);
        }
    }
}
