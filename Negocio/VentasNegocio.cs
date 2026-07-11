using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class VentasNegocio
    {
        public List<Venta> Listar()
        {
            return Buscar(null, null);
        }

        public List<Venta> Buscar(string busqueda)
        {
            return Buscar(busqueda, null);
        }

        public List<Venta> Buscar(string busquedaVenta, string busquedaFactura)
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = @"SELECT v.IdVenta, v.NumeroFactura, v.Total,
                                              c.IdCliente, c.Nombre AS ClienteNombre,
                                              u.IdUsuario, u.Nombre AS UsuarioNombre
                                       FROM Ventas v
                                       INNER JOIN Clientes c ON v.IdCliente = c.IdCliente
                                       INNER JOIN Usuarios u ON v.IdUsuario = u.IdUsuario
                                       WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(busquedaVenta))
                {
                    consulta += " AND CAST(v.IdVenta AS VARCHAR(20)) LIKE @busquedaVenta";
                }

                if (!string.IsNullOrWhiteSpace(busquedaFactura))
                {
                    consulta += " AND CAST(v.NumeroFactura AS VARCHAR(20)) LIKE @busquedaFactura";
                }

                consulta += " ORDER BY v.IdVenta DESC";

                datos.setearConsulta(consulta);
                if (!string.IsNullOrWhiteSpace(busquedaVenta))
                {
                    datos.setearParametro("@busquedaVenta", "%" + busquedaVenta + "%");
                }

                if (!string.IsNullOrWhiteSpace(busquedaFactura))
                {
                    datos.setearParametro("@busquedaFactura", "%" + busquedaFactura + "%");
                }

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Venta v = new Venta();
                    v.IdVenta = (int)datos.Lector["IdVenta"];
                    v.NumeroFactura = (int)datos.Lector["NumeroFactura"];
                    v.Total = (decimal)datos.Lector["Total"];
                    v.Cliente = new Cliente();
                    v.Cliente.IdCliente = (int)datos.Lector["IdCliente"];
                    v.Cliente.Nombre = (string)datos.Lector["ClienteNombre"];
                    v.Usuario = new Usuario();
                    v.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    v.Usuario.Nombre = (string)datos.Lector["UsuarioNombre"];
                    lista.Add(v);
                }
                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Registrar(Venta venta)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(@"INSERT INTO Ventas (NumeroFactura, Total, IdCliente, IdUsuario)
                                       VALUES (@nroFactura, @total, @idCliente, @idUsuario);
                                       SELECT SCOPE_IDENTITY();");
                datos.setearParametro("@nroFactura", venta.NumeroFactura);
                datos.setearParametro("@total", venta.Total);
                datos.setearParametro("@idCliente", venta.Cliente.IdCliente);
                datos.setearParametro("@idUsuario", venta.Usuario.IdUsuario);
                int idVenta = datos.ejecutarAccionScalar();

                foreach (DetalleVenta detalle in venta.detalleVentas)
                {
                    AccesoDatos datosDetalle = new AccesoDatos();
                    datosDetalle.setearConsulta(@"INSERT INTO DetalleVentas (Cantidad, PrecioUnitario, IdVenta, IdProducto)
                                                  VALUES (@cantidad, @precio, @idVenta, @idProducto)");
                    datosDetalle.setearParametro("@cantidad", detalle.Cantidad);
                    datosDetalle.setearParametro("@precio", detalle.PrecioUnitario);
                    datosDetalle.setearParametro("@idVenta", idVenta);
                    datosDetalle.setearParametro("@idProducto", detalle.IdProducto);
                    datosDetalle.ejecutarAccion();

                    AccesoDatos datosStock = new AccesoDatos();
                    datosStock.setearConsulta(@"UPDATE Productos 
                                                SET StockActual = StockActual - @cantidad
                                                WHERE IdProducto = @idProducto");
                    datosStock.setearParametro("@cantidad", detalle.Cantidad);
                    datosStock.setearParametro("@idProducto", detalle.IdProducto);
                    datosStock.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool nroFacturaExiste(int nroFactura) {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT COUNT(*) FROM Ventas WHERE numeroFactura = @nroFactura");

                datos.setearParametro("@nroFactura", nroFactura);


                int cantidad = Convert.ToInt32(datos.ejecutarAccionScalar());
               
                return cantidad > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    } 
}
