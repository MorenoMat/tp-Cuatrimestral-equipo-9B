using System;
using System.Security.Cryptography;
using System.Text;
using Dominio;

namespace negocio
{
    public static class Seguridad
    {
        public static string HashearContraseña(string contraseña)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public static bool sesionActiva(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            return usuario != null && usuario.IdUsuario != 0;
        }

        public static bool esAdmin(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            return usuario != null && usuario.esAdmin;
        }

        public static bool ValidarAccesoAdmin(object user)
        {
            if (!esAdmin(user))
            {
                return false;
            }
            return true;
        }
    }
}
