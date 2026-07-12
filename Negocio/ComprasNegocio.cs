using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class ComprasNegocio
    {
        public List<Compra> Listar()
        {
            List<Compra> lista = new List<Compra>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = @"SELECT c.IdCompra, c.FechaCompra, 
                                              p.IdProveedor, p.Nombre AS ProveedorNombre,
                                              u.IdUsuario, u.Nombre AS UsuarioNombre,
                                              ISNULL(SUM(dc.Cantidad * dc.PrecioUnitario), 0) AS Total
                                       FROM Compras c
                                       INNER JOIN Proveedores p ON c.IdProveedor = p.IdProveedor
                                       INNER JOIN Usuarios u ON c.IdUsuario = u.IdUsuario
                                       LEFT JOIN DetalleCompras dc ON c.IdCompra = dc.IdCompra
                                       GROUP BY c.IdCompra, c.FechaCompra, p.IdProveedor, p.Nombre, u.IdUsuario, u.Nombre
                                       ORDER BY c.FechaCompra DESC, c.IdCompra DESC";

                datos.setearConsulta(consulta);
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
                    c.Total = Convert.ToDecimal(datos.Lector["Total"]);
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

        public List<Compra> BuscarPaginado(string busqueda, int idProveedor, int idUsuario, DateTime? fechaDesde, DateTime? fechaHasta, int numeroPagina, int tamanioPagina)
        {
            List<Compra> lista = new List<Compra>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                int offset = (numeroPagina - 1) * tamanioPagina;
                string consulta = @"SELECT c.IdCompra, c.FechaCompra, 
                                              p.IdProveedor, p.Nombre AS ProveedorNombre,
                                              u.IdUsuario, u.Nombre AS UsuarioNombre,
                                              ISNULL(SUM(dc.Cantidad * dc.PrecioUnitario), 0) AS Total
                                       FROM Compras c
                                       INNER JOIN Proveedores p ON c.IdProveedor = p.IdProveedor
                                       INNER JOIN Usuarios u ON c.IdUsuario = u.IdUsuario
                                       LEFT JOIN DetalleCompras dc ON c.IdCompra = dc.IdCompra
                                       WHERE 1=1";

                consulta += ObtenerWhereFiltros(busqueda, idProveedor, idUsuario, fechaDesde, fechaHasta);
                consulta += " GROUP BY c.IdCompra, c.FechaCompra, p.IdProveedor, p.Nombre, u.IdUsuario, u.Nombre";
                consulta += " ORDER BY c.FechaCompra DESC, c.IdCompra DESC OFFSET @offset ROWS FETCH NEXT @tamanioPagina ROWS ONLY";

                datos.setearConsulta(consulta);
                CargarParametrosFiltros(datos, busqueda, idProveedor, idUsuario, fechaDesde, fechaHasta);
                datos.setearParametro("@offset", offset);
                datos.setearParametro("@tamanioPagina", tamanioPagina);

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
                    c.Total = Convert.ToDecimal(datos.Lector["Total"]);
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

        public int Contar(string busqueda, int idProveedor, int idUsuario, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = "SELECT COUNT(*) FROM Compras c WHERE 1=1";

                consulta += ObtenerWhereFiltros(busqueda, idProveedor, idUsuario, fechaDesde, fechaHasta);

                datos.setearConsulta(consulta);
                CargarParametrosFiltros(datos, busqueda, idProveedor, idUsuario, fechaDesde, fechaHasta);

                int total = datos.ejecutarAccionScalar();
                datos.cerrarConexion();
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ObtenerWhereFiltros(string busqueda, int idProveedor, int idUsuario, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            string where = string.Empty;

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                where += " AND CAST(c.IdCompra AS VARCHAR(20)) LIKE @busqueda";
            }

            if (idProveedor > 0)
            {
                where += " AND c.IdProveedor = @idProveedor";
            }

            if (idUsuario > 0)
            {
                where += " AND c.IdUsuario = @idUsuario";
            }

            if (fechaDesde.HasValue)
            {
                where += " AND c.FechaCompra >= @fechaDesde";
            }

            if (fechaHasta.HasValue)
            {
                where += " AND c.FechaCompra < @fechaHasta";
            }

            return where;
        }

        private void CargarParametrosFiltros(AccesoDatos datos, string busqueda, int idProveedor, int idUsuario, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                datos.setearParametro("@busqueda", "%" + busqueda + "%");
            }

            if (idProveedor > 0)
            {
                datos.setearParametro("@idProveedor", idProveedor);
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
