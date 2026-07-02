using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdCliente, Dni, Nombre, Email, Activo FROM Clientes");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {  
                    Cliente c = new Cliente();
                    c.IdCliente = (int)datos.Lector["IdCliente"];
                    c.Dni = (string)datos.Lector["Dni"];
                    c.Nombre = (string)datos.Lector["Nombre"];
                    c.Email = datos.Lector["Email"] is DBNull ? "" : (string)datos.Lector["Email"];
                    c.Activo = Convert.ToBoolean(datos.Lector["Activo"]);
                    lista.Add(c);
                }
                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cliente BuscarPorId(int id)
        {
            Cliente cliente = null;
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdCliente, Dni, Nombre, Email FROM Clientes WHERE IdCliente = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    cliente = new Cliente();
                    cliente.IdCliente = (int)datos.Lector["IdCliente"];
                    cliente.Dni = (string)datos.Lector["Dni"];
                    cliente.Nombre = (string)datos.Lector["Nombre"];
                    cliente.Email = datos.Lector["Email"] is DBNull ? "" : (string)datos.Lector["Email"];
                }
                datos.cerrarConexion();
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Cliente cambiarEstadoCliente(int idCliente, bool Activo)
        {
            Cliente cliente = null;
           
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Clientes SET Activo = @activo WHERE IdCliente = @id");
                datos.setearParametro("@activo", Activo);
                datos.setearParametro("@id", idCliente);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cliente;
        }

        public bool leerEstadoCliente(int idCliente)
        {
            bool estado = false;
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT Activo FROM Clientes WHERE IdCliente = @id");
                datos.setearParametro("@id", idCliente);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    estado = (bool)datos.Lector["Activo"];
                }
                datos.cerrarConexion();
                return estado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Agregar(Cliente cliente)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("INSERT INTO Clientes (Dni, Nombre, Email) VALUES (@dni, @nombre, @email)");
                datos.setearParametro("@dni", cliente.Dni);
                datos.setearParametro("@nombre", cliente.Nombre);
                datos.setearParametro("@email", cliente.Email);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Cliente cliente)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Clientes SET Dni = @dni, Nombre = @nombre, Email = @email WHERE IdCliente = @id");
                datos.setearParametro("@dni", cliente.Dni);
                datos.setearParametro("@nombre", cliente.Nombre);
                datos.setearParametro("@email", cliente.Email);
                datos.setearParametro("@id", cliente.IdCliente);
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
                datos.setearConsulta("DELETE FROM Clientes WHERE IdCliente = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
