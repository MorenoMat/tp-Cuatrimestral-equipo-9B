using System.Web.UI.WebControls;

namespace Comercio_Web.Helpers
{
    public static class TablaSinResultadosHelper
    {
        public static void Aplicar(GridView grid, bool hayFiltros)
        {
            string mensaje = hayFiltros
                ? "No hay datos para la búsqueda aplicada."
                : "No hay datos para mostrar.";

            grid.EmptyDataText = "<div class='tabla-empty-state'>"
                + "<div class='tabla-empty-title'>Sin resultados</div>"
                + "<div class='tabla-empty-message'>" + mensaje + "</div>"
                + "<div class='tabla-empty-skeleton'>"
                + "<span></span><span></span><span></span>"
                + "</div>"
                + "</div>";
        }
    }
}
