using System;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class MarcasFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    MarcaNegocio negocio = new MarcaNegocio();
                    Marca marca = negocio.BuscarPorId(id);
                    txtDescripcion.Text = marca.Descripcion;
                    lblTitulo.Text = "Editar Marca";
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                Marca marca = new Marca();
                marca.Descripcion = txtDescripcion.Text;

                if (Request.QueryString["id"] != null)
                {
                    marca.IdMarca = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(marca);
                }
                else
                {
                    negocio.Agregar(marca);
                }
                Response.Redirect("MarcasLista.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                MarcaNegocio negocio = new MarcaNegocio();
                negocio.Eliminar(id);
                Response.Redirect("MarcasLista.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
