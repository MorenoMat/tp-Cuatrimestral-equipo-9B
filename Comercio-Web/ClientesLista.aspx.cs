using negocio;
using System;
using System.Web.UI.WebControls;

namespace Comercio_Web
{
    public partial class ClientesLista : System.Web.UI.Page
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
            ClienteNegocio negocio = new ClienteNegocio();
            dgvClientes.DataSource = negocio.Buscar(busqueda);
            dgvClientes.DataBind();
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

        protected void dgvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvClientes.SelectedDataKey.Value.ToString();
            Response.Redirect("ClientesFormulario.aspx?id=" + id);
        }
        protected void chkAccion_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkEstadoCliente = (CheckBox)sender;
            GridViewRow fila = (GridViewRow)chkEstadoCliente.NamingContainer;
            ClienteNegocio negocio = new ClienteNegocio();
            int idCliente = Convert.ToInt32(dgvClientes.DataKeys[fila.RowIndex].Value);
            bool activo = chkEstadoCliente.Checked;
            negocio.cambiarEstadoCliente(idCliente, activo);
            cargarGrilla(txtBuscar.Text.Trim());
        }
    }
}
