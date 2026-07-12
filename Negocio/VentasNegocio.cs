using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class VentasNegocio
    {
        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = @"SELECT v.IdVenta, v.NumeroFactura, v.FechaVenta, v.Total,
                                              c.IdCliente, c.Nombre AS ClienteNombre,
                                              u.IdUsuario, u.Nombre AS UsuarioNombre
                                       FROM Ventas v
                                       INNER JOIN Clientes c ON v.IdCliente = c.IdCliente
                                       INNER JOIN Usuarios u ON v.IdUsuario = u.IdUsuario
                                       ORDER BY v.IdVenta DESC";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Venta v = new Venta();
                    v.IdVenta = (int)datos.Lector["IdVenta"];
                    v.NumeroFactura = (int)datos.Lector["NumeroFactura"];
                    v.FechaVenta = (DateTime)datos.Lector["FechaVenta"];
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

        public List<Venta> BuscarPaginado(string busquedaVenta, string busquedaFactura, int idCliente, int idUsuario, DateTime? fechaDesde, DateTime? fechaHasta, int numeroPagina, int tamanioPagina)
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                int offset = (numeroPagina - 1) * tamanioPagina;
                string consulta = @"SELECT v.IdVenta, v.NumeroFactura, v.FechaVenta, v.Total,
                                              c.IdCliente, c.Nombre AS ClienteNombre,
                                              u.IdUsuario, u.Nombre AS UsuarioNombre
                                       FROM Ventas v
                                       INNER JOIN Clientes c ON v.IdCliente = c.IdCliente
                                       INNER JOIN Usuarios u ON v.IdUsuario = u.IdUsuario
                                       WHERE 1=1";

                consulta += ObtenerWhereFiltros(busquedaVenta, busquedaFactura, idCliente, idUsuario, fechaDesde, fechaHasta);
                consulta += " ORDER BY v.IdVenta DESC OFFSET @offset ROWS FETCH NEXT @tamanioPagina ROWS ONLY";

                datos.setearConsulta(consulta);
                CargarParametrosFiltros(datos, busquedaVenta, busquedaFactura, idCliente, idUsuario, fechaDesde, fechaHasta);
                datos.setearParametro("@offset", offset);
                datos.setearParametro("@tamanioPagina", tamanioPagina);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Venta v = new Venta();
                    v.IdVenta = (int)datos.Lector["IdVenta"];
                    v.NumeroFactura = (int)datos.Lector["NumeroFactura"];
                    v.FechaVenta = (DateTime)datos.Lector["FechaVenta"];
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

        public int Contar(string busquedaVenta, string busquedaFactura, int idCliente, int idUsuario, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = "SELECT COUNT(*) FROM Ventas v WHERE 1=1";

                consulta += ObtenerWhereFiltros(busquedaVenta, busquedaFactura, idCliente, idUsuario, fechaDesde, fechaHasta);

                datos.setearConsulta(consulta);
                CargarParametrosFiltros(datos, busquedaVenta, busquedaFactura, idCliente, idUsuario, fechaDesde, fechaHasta);

                int total = datos.ejecutarAccionScalar();
                datos.cerrarConexion();
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ObtenerWhereFiltros(string busquedaVenta, string busquedaFactura, int idCliente, int idUsuario, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            string where = string.Empty;

            if (!string.IsNullOrWhiteSpace(busquedaVenta))
            {
                where += " AND CAST(v.IdVenta AS VARCHAR(20)) LIKE @busquedaVenta";
            }

            if (!string.IsNullOrWhiteSpace(busquedaFactura))
            {
                where += " AND CAST(v.NumeroFactura AS VARCHAR(20)) LIKE @busquedaFactura";
            }

            if (idCliente > 0)
            {
                where += " AND v.IdCliente = @idCliente";
            }

            if (idUsuario > 0)
            {
                where += " AND v.IdUsuario = @idUsuario";
            }

            if (fechaDesde.HasValue)
            {
                where += " AND v.fechaVenta >= @fechaDesde";
            }

            if (fechaHasta.HasValue)
            {
                where += " AND v.fechaVenta < @fechaHasta";
            }

            return where;
        }

        private void CargarParametrosFiltros(AccesoDatos datos, string busquedaVenta, string busquedaFactura, int idCliente, int idUsuario, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            if (!string.IsNullOrWhiteSpace(busquedaVenta))
            {
                datos.setearParametro("@busquedaVenta", "%" + busquedaVenta + "%");
            }

            if (!string.IsNullOrWhiteSpace(busquedaFactura))
            {
                datos.setearParametro("@busquedaFactura", "%" + busquedaFactura + "%");
            }

            if (idCliente > 0)
            {
                datos.setearParametro("@idCliente", idCliente);
            }

            if (idUsuario > 0)
            {
                datos.setearParametro("@idUsuario", idUsuario);
            }

            if (fechaDesde.HasValue)
            {
                datos.setearParametro("@fechaDesde", fechaDesde.Value.Date);
            }

            if (fechaHasta.HasValue)
            {
                datos.setearParametro("@fechaHasta", fechaHasta.Value.Date.AddDays(1));
            }
        }

        public List<DetalleVenta> ListarDetalleVenta(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(@"SELECT dv.IdDetalleVenta, dv.Cantidad, dv.PrecioUnitario, dv.IdVenta, dv.IdProducto, p.Nombre AS ProductoNombre
                                       FROM DetalleVentas dv
                                       INNER JOIN Productos p ON dv.IdProducto = p.IdProducto
                                       WHERE dv.IdVenta = @idVenta
                                       ORDER BY dv.IdDetalleVenta");
                datos.setearParametro("@idVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta detalle = new DetalleVenta();
                    detalle.Id = (int)datos.Lector["IdDetalleVenta"];
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalle.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    detalle.IdVenta = (int)datos.Lector["IdVenta"];
                    detalle.IdProducto = (int)datos.Lector["IdProducto"];
                    detalle.ProductoNombre = (string)datos.Lector["ProductoNombre"];
                    lista.Add(detalle);
                }

                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Venta ObtenerPorId(int idVenta)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(@"SELECT v.IdVenta, v.Total
                                      FROM Ventas v
                                      WHERE v.IdVenta = @idVenta");
                datos.setearParametro("@idVenta", idVenta);
                datos.ejecutarLectura();

                Venta venta = null;
                if (datos.Lector.Read())
                {
                    venta = new Venta();
                    venta.IdVenta = (int)datos.Lector["IdVenta"];
                    venta.Total = (decimal)datos.Lector["Total"];
                }

                datos.cerrarConexion();
                return venta;
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
