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

        private void cargarGrilla()
        {
            ClienteNegocio negocio = new ClienteNegocio();
            dgvClientes.DataSource = negocio.Listar();
            dgvClientes.DataBind();
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
            cargarGrilla();
        }
    }
}
