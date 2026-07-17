using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Dominio;
using negocio;

namespace Comercio_Web
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page is Login))
            {
                if (!Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    lblUsuario.Text = usuario.Nombre;

                    bool esAdministrador = Seguridad.esAdmin(Session["usuario"]);
                    navInventario.Visible = esAdministrador;
                    navUsuarios.Visible = esAdministrador;

                    string paginaActual = VirtualPathUtility.GetFileName(Request.Path);
                    bool paginaInventario = EsPagina(paginaActual, "MarcasLista.aspx", "MarcasFormulario.aspx")
                        || EsPagina(paginaActual, "CategoriasLista.aspx", "CategoriasFormulario.aspx")
                        || EsPagina(paginaActual, "ProveedoresLista.aspx", "ProveedoresFormulario.aspx")
                        || EsPagina(paginaActual, "ProductosLista.aspx", "ProductosFormulario.aspx")
                        || EsPagina(paginaActual, "ComprasLista.aspx", "ComprasFormulario.aspx");

                    bool mostrarSidebarInventario = esAdministrador && paginaInventario;
                    pnlInventarioSidebar.Visible = mostrarSidebarInventario;
                    contenedorContenido.Attributes["class"] = mostrarSidebarInventario ? "w-100 ps-lg-3" : "w-100";
                    contenedorContenido.Style["marginLeft"] = mostrarSidebarInventario ? "250px" : "0";

                    MarcarSidebarActivo(paginaActual);
                }
            }
        }

        private static bool EsPagina(string paginaActual, params string[] paginas)
        {
            foreach (string pagina in paginas)
            {
                if (string.Equals(paginaActual, pagina, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private void MarcarSidebarActivo(string paginaActual)
        {
            const string claseBase = "list-group-item list-group-item-action py-3 fw-semibold fs-6";

            lnkSidebarMarcas.Attributes["class"] = claseBase;
            lnkSidebarCategorias.Attributes["class"] = claseBase;
            lnkSidebarProveedores.Attributes["class"] = claseBase;
            lnkSidebarProductos.Attributes["class"] = claseBase;
            lnkSidebarCompras.Attributes["class"] = claseBase;

            HtmlAnchor linkActivo = null;

            if (EsPagina(paginaActual, "MarcasLista.aspx", "MarcasFormulario.aspx"))
                linkActivo = lnkSidebarMarcas;
            else if (EsPagina(paginaActual, "CategoriasLista.aspx", "CategoriasFormulario.aspx"))
                linkActivo = lnkSidebarCategorias;
            else if (EsPagina(paginaActual, "ProveedoresLista.aspx", "ProveedoresFormulario.aspx"))
                linkActivo = lnkSidebarProveedores;
            else if (EsPagina(paginaActual, "ProductosLista.aspx", "ProductosFormulario.aspx"))
                linkActivo = lnkSidebarProductos;
            else if (EsPagina(paginaActual, "ComprasLista.aspx", "ComprasFormulario.aspx"))
                linkActivo = lnkSidebarCompras;

            if (linkActivo != null)
            {
                linkActivo.Attributes["class"] = claseBase + " active";
                linkActivo.Attributes["aria-current"] = "page";
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
