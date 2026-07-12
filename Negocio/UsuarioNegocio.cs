using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdUsuario, Nombre, UsuarioLogin, EsAdmin FROM Usuarios ORDER BY Nombre, IdUsuario");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario u = new Usuario();
                    u.IdUsuario = (int)datos.Lector["IdUsuario"];
                    u.Nombre = (string)datos.Lector["Nombre"];
                    u.UsuarioLogin = (string)datos.Lector["UsuarioLogin"];
                    u.esAdmin = (bool)datos.Lector["EsAdmin"];
                    lista.Add(u);
                }
                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> BuscarPaginado(string busqueda, int numeroPagina, int tamanioPagina)
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                int offset = (numeroPagina - 1) * tamanioPagina;
                string consulta = "SELECT IdUsuario, Nombre, UsuarioLogin, EsAdmin FROM Usuarios WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    consulta += " AND (UsuarioLogin LIKE @busqueda OR Nombre LIKE @busqueda)";
                }

                consulta += " ORDER BY Nombre, IdUsuario OFFSET @offset ROWS FETCH NEXT @tamanioPagina ROWS ONLY";

                datos.setearConsulta(consulta);
                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    datos.setearParametro("@busqueda", "%" + busqueda + "%");
                }
                datos.setearParametro("@offset", offset);
                datos.setearParametro("@tamanioPagina", tamanioPagina);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario u = new Usuario();
                    u.IdUsuario = (int)datos.Lector["IdUsuario"];
                    u.Nombre = (string)datos.Lector["Nombre"];
                    u.UsuarioLogin = (string)datos.Lector["UsuarioLogin"];
                    u.esAdmin = (bool)datos.Lector["EsAdmin"];
                    lista.Add(u);
                }
                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Contar(string busqueda)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = "SELECT COUNT(*) FROM Usuarios WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    consulta += " AND (UsuarioLogin LIKE @busqueda OR Nombre LIKE @busqueda)";
                }

                datos.setearConsulta(consulta);
                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    datos.setearParametro("@busqueda", "%" + busqueda + "%");
                }

                int total = datos.ejecutarAccionScalar();
                datos.cerrarConexion();
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario BuscarPorId(int id)
        {
            Usuario usuario = null;
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdUsuario, Nombre, UsuarioLogin, Contraseña, EsAdmin FROM Usuarios WHERE IdUsuario = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.UsuarioLogin = (string)datos.Lector["UsuarioLogin"];
                    usuario.Contraseña = (string)datos.Lector["Contraseña"];
                    usuario.esAdmin = (bool)datos.Lector["EsAdmin"];
                }
                datos.cerrarConexion();
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario BuscarPorUsuario(string UsuarioLogin)
        {
            Usuario usuario = null;
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdUsuario, Nombre, UsuarioLogin, Contraseña, EsAdmin FROM Usuarios WHERE UsuarioLogin = @UsuarioLogin");
                datos.setearParametro("@UsuarioLogin", UsuarioLogin);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.UsuarioLogin = (string)datos.Lector["UsuarioLogin"];
                    usuario.Contraseña = (string)datos.Lector["Contraseña"];
                    usuario.esAdmin = (bool)datos.Lector["EsAdmin"];
                }
                datos.cerrarConexion();
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Agregar(Usuario usuario)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("INSERT INTO Usuarios (Nombre, UsuarioLogin, Contraseña, EsAdmin) VALUES (@nombre, @login, @pass, @admin)");
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@login", usuario.UsuarioLogin);
                datos.setearParametro("@pass", usuario.Contraseña);
                datos.setearParametro("@admin", usuario.esAdmin);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Usuario usuario)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Usuarios SET Nombre = @nombre, UsuarioLogin = @login, Contraseña = @pass, EsAdmin = @admin WHERE IdUsuario = @id");
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@login", usuario.UsuarioLogin);
                datos.setearParametro("@pass", usuario.Contraseña);
                datos.setearParametro("@admin", usuario.esAdmin);
                datos.setearParametro("@id", usuario.IdUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM Usuarios WHERE IdUsuario = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Login(Usuario usuario)
        {
            try
            {
                string hashPass = Seguridad.HashearContraseña(usuario.Contraseña);
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdUsuario, Nombre, UsuarioLogin, EsAdmin FROM Usuarios WHERE UsuarioLogin = @login AND Contraseña = @pass");
                datos.setearParametro("@login", usuario.UsuarioLogin);
                datos.setearParametro("@pass", hashPass);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.esAdmin = (bool)datos.Lector["EsAdmin"];
                    datos.cerrarConexion();
                    return true;
                }
                datos.cerrarConexion();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
