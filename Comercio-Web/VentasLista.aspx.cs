using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
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
                cargarClientes();
                cargarUsuarios();
                cargarGrilla();
            }
        }

        private void cargarClientes()
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            ddlCliente.DataSource = clienteNegocio.Listar();
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "IdCliente";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        private void cargarUsuarios()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            ddlUsuario.DataSource = usuarioNegocio.Listar();
            ddlUsuario.DataTextField = "Nombre";
            ddlUsuario.DataValueField = "IdUsuario";
            ddlUsuario.DataBind();
            ddlUsuario.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        private void cargarGrilla(string busquedaVenta = null, string busquedaFactura = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            VentasNegocio n = new VentasNegocio();
            int idCliente = int.Parse(ddlCliente.SelectedValue);
            int idUsuario = int.Parse(ddlUsuario.SelectedValue);
            List<Venta> lista = n.Buscar(busquedaVenta, busquedaFactura, idCliente, idUsuario, fechaDesde, fechaHasta);
            dgvVentas.DataSource = lista;
            dgvVentas.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde;
            DateTime fechaHasta;
            DateTime? fechaDesdeFiltro = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) ? fechaDesde : (DateTime?)null;
            DateTime? fechaHastaFiltro = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta) ? fechaHasta : (DateTime?)null;

            cargarGrilla(txtBuscar.Text.Trim(), txtBuscarFactura.Text.Trim(), fechaDesdeFiltro, fechaHastaFiltro);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscarFactura.Text = string.Empty;
            ddlCliente.SelectedValue = "0";
            ddlUsuario.SelectedValue = "0";
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;
            cargarGrilla();
        }
    }
}
