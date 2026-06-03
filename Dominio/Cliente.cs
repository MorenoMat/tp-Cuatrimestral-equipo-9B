using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente // se asigna un cliente a la venta 
    {
        public int ID { get; set; }
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }
}
