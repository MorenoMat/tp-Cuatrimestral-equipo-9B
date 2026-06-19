using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente // se asigna un cliente a la venta 
    {
        public int IdCliente { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }
}
