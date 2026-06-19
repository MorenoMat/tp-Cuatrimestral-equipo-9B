using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdProveedor, Nombre, Telefono, Email, Cuit FROM Proveedores");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Proveedor p = new Proveedor();
                    p.IdProveedor = (int)datos.Lector["IdProveedor"];
                    p.Nombre = (string)datos.Lector["Nombre"];
                    p.Telefono = datos.Lector["Telefono"] is DBNull ? "" : (string)datos.Lector["Telefono"];
                    p.Email = datos.Lector["Email"] is DBNull ? "" : (string)datos.Lector["Email"];
                    p.Cuit = (string)datos.Lector["Cuit"];
                    lista.Add(p);
                }
                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Proveedor BuscarPorId(int id)
        {
            Proveedor proveedor = null;
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdProveedor, Nombre, Telefono, Email,Cuit FROM Proveedores WHERE IdProveedor = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    proveedor = new Proveedor();
                    proveedor.IdProveedor = (int)datos.Lector["IdProveedor"];
                    proveedor.Nombre = (string)datos.Lector["Nombre"];
                    proveedor.Telefono = datos.Lector["Telefono"] is DBNull ? "" : (string)datos.Lector["Telefono"];
                    proveedor.Email = datos.Lector["Email"] is DBNull ? "" : (string)datos.Lector["Email"];
                    proveedor.Cuit = (string)datos.Lector["Cuit"];
                }
                datos.cerrarConexion();
                return proveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Agregar(Proveedor proveedor)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("INSERT INTO Proveedores (Nombre, Telefono, Email,Cuit) VALUES (@nombre, @telefono, @email, @Cuit)");
                datos.setearParametro("@nombre", proveedor.Nombre);
                datos.setearParametro("@telefono", proveedor.Telefono);
                datos.setearParametro("@email", proveedor.Email);
                datos.setearParametro("@cuit", proveedor.Cuit);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Proveedor proveedor)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Proveedores SET Nombre = @nombre, Telefono = @telefono, Email = @email, Cuit = @cuit WHERE IdProveedor = @id");
                datos.setearParametro("@nombre", proveedor.Nombre);
                datos.setearParametro("@telefono", proveedor.Telefono);
                datos.setearParametro("@email", proveedor.Email);
                datos.setearParametro("@id", proveedor.IdProveedor);
                datos.setearParametro("@cuit", proveedor.Cuit);
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
                datos.setearConsulta("DELETE FROM Proveedores WHERE IdProveedor = @id");
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
