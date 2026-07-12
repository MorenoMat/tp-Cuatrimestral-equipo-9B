using System;
using System.Collections.Generic;

namespace Comercio_Web.Helpers
{
    public class PaginaItem
    {
        public int Numero { get; set; }
        public bool Actual { get; set; }
    }

    public class EstadoPaginacion
    {
        public int PaginaActual { get; set; }
        public int TamanioPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public int Desde { get; set; }
        public int Hasta { get; set; }
        public bool PuedeIrAnterior { get; set; }
        public bool PuedeIrSiguiente { get; set; }
        public List<PaginaItem> Paginas { get; set; }
    }

    public static class PaginacionHelper
    {
        public static EstadoPaginacion Crear(int paginaActual, int tamanioPagina, int totalRegistros)
        {
            int totalPaginas = totalRegistros == 0 ? 1 : (int)Math.Ceiling((double)totalRegistros / tamanioPagina);

            if (paginaActual > totalPaginas)
                paginaActual = totalPaginas;
            if (paginaActual < 1)
                paginaActual = 1;

            int desde = totalRegistros == 0 ? 0 : ((paginaActual - 1) * tamanioPagina) + 1;
            int hasta = totalRegistros == 0 ? 0 : Math.Min(paginaActual * tamanioPagina, totalRegistros);

            List<PaginaItem> paginas = new List<PaginaItem>();
            for (int i = 1; i <= totalPaginas; i++)
            {
                paginas.Add(new PaginaItem
                {
                    Numero = i,
                    Actual = i == paginaActual
                });
            }

            return new EstadoPaginacion
            {
                PaginaActual = paginaActual,
                TamanioPagina = tamanioPagina,
                TotalRegistros = totalRegistros,
                TotalPaginas = totalPaginas,
                Desde = desde,
                Hasta = hasta,
                PuedeIrAnterior = paginaActual > 1,
                PuedeIrSiguiente = paginaActual < totalPaginas,
                Paginas = paginas
            };
        }
    }
}
