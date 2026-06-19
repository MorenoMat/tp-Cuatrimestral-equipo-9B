using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string UsuarioLogin { get; set; }
        public string Contraseña { get; set; }
        public bool esAdmin { get; set; }
    }
}
