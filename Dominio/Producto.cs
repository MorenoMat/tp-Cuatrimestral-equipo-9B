using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal UltimoPrecio { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public string Descripcion { get; set; }
        public int IDMarca { get; set; }
        public int IDCategoria { get; set; }
        public List<Proveedor> Proveedores { get; set; }

    }
}
