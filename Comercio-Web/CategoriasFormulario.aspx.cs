using System;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class CategoriasFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    Categoria categoria = negocio.BuscarPorId(id);
                    txtDescripcion.Text = categoria.Descripcion;
                    lblTitulo.Text = "Editar Categoría";
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                Categoria categoria = new Categoria();
                categoria.Descripcion = txtDescripcion.Text;

                if (Request.QueryString["id"] != null)
                {
                    categoria.IdCategoria = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(categoria);
                }
                else
                {
                    negocio.Agregar(categoria);
                }
                Response.Redirect("CategoriasLista.aspx");
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
                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.Eliminar(id);
                Response.Redirect("CategoriasLista.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
