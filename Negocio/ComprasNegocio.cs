using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class ComprasNegocio
    {
        public List<Compra> Listar()
        {
            return Buscar(null, 0);
        }

        public List<Compra> Buscar(string busqueda)
        {
            return Buscar(busqueda, 0);
        }

        public List<Compra> Buscar(string busqueda, int idProveedor)
        {
            List<Compra> lista = new List<Compra>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = @"SELECT c.IdCompra, c.FechaCompra, 
                                              p.IdProveedor, p.Nombre AS ProveedorNombre,
                                              u.IdUsuario, u.Nombre AS UsuarioNombre
                                       FROM Compras c
                                       INNER JOIN Proveedores p ON c.IdProveedor = p.IdProveedor
                                       INNER JOIN Usuarios u ON c.IdUsuario = u.IdUsuario
                                       WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    consulta += " AND CAST(c.IdCompra AS VARCHAR(20)) LIKE @busqueda";
                }

                if (idProveedor > 0)
                {
                    consulta += " AND c.IdProveedor = @idProveedor";
                }

                consulta += " ORDER BY c.FechaCompra DESC";

                datos.setearConsulta(consulta);
                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    datos.setearParametro("@busqueda", "%" + busqueda + "%");
                }

                if (idProveedor > 0)
                {
                    datos.setearParametro("@idProveedor", idProveedor);
                }

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Compra c = new Compra();
                    c.IdCompra = (int)datos.Lector["IdCompra"];
                    c.FechaCompra = (DateTime)datos.Lector["FechaCompra"];
                    c.Proveedor = new Proveedor();
                    c.Proveedor.IdProveedor = (int)datos.Lector["IdProveedor"];
                    c.Proveedor.Nombre = (string)datos.Lector["ProveedorNombre"];
                    c.Usuario = new Usuario();
                    c.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    c.Usuario.Nombre = (string)datos.Lector["UsuarioNombre"];
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

        public void Registrar(Compra compra)
        {
            try 
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(@"INSERT INTO Compras (FechaCompra, IdProveedor, IdUsuario) 
                                       VALUES (@fecha,  @idProveedor, @idUsuario); 
                                       SELECT SCOPE_IDENTITY();"); //@estado,
                datos.setearParametro("@fecha", compra.FechaCompra);
                datos.setearParametro("@idProveedor", compra.Proveedor.IdProveedor);
                datos.setearParametro("@idUsuario", compra.Usuario.IdUsuario);
                int idCompra = datos.ejecutarAccionScalar();

                foreach (DetalleCompra detalle in compra.DetalleCompras)
                {
                    AccesoDatos datosDetalle = new AccesoDatos();
                    datosDetalle.setearConsulta(@"INSERT INTO DetalleCompras (Cantidad, PrecioUnitario, IdCompra, IdProducto)
                                                  VALUES (@cantidad, @precio, @idCompra, @idProducto)"); 
                    datosDetalle.setearParametro("@cantidad", detalle.Cantidad);
                    datosDetalle.setearParametro("@precio", detalle.PrecioUnitario);
                    datosDetalle.setearParametro("@idCompra", idCompra);
                    datosDetalle.setearParametro("@idProducto", detalle.IdProducto);
                    datosDetalle.ejecutarAccion();

                    AccesoDatos datosStock = new AccesoDatos();
                    datosStock.setearConsulta(@"UPDATE Productos 
                                                SET StockActual = StockActual + @cantidad,
                                                    UltimoPrecioCompra = @precio
                                                WHERE IdProducto = @idProducto");
                    datosStock.setearParametro("@cantidad", detalle.Cantidad);
                    datosStock.setearParametro("@precio", detalle.PrecioUnitario);
                    datosStock.setearParametro("@idProducto", detalle.IdProducto);
                    datosStock.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
