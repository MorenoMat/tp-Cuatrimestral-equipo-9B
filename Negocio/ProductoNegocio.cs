using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = @"SELECT p.IdProducto, p.Nombre, p.Descripcion, 
                                        p.UltimoPrecioCompra, p.PorcentajeGanancia, 
                                        p.StockActual, p.StockMinimo,
                                        m.IdMarca, m.Descripcion AS MarcaDesc,
                                        c.IdCategoria, c.Descripcion AS CategoriaDesc
                                       FROM Productos p
                                       INNER JOIN Marcas m ON p.IdMarca = m.IdMarca
                                       INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
                                       ORDER BY p.Nombre, p.IdProducto";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto p = new Producto();
                    p.IdProducto = (int)datos.Lector["IdProducto"];
                    p.Nombre = (string)datos.Lector["Nombre"];
                    p.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                    p.UltimoPrecio = (decimal)datos.Lector["UltimoPrecioCompra"];
                    p.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    p.StockActual = (int)datos.Lector["StockActual"];
                    p.StockMinimo = (int)datos.Lector["StockMinimo"];
                    p.Marca = new Marca();
                    p.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    p.Marca.Descripcion = (string)datos.Lector["MarcaDesc"];
                    p.Categoria = new Categoria();
                    p.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    p.Categoria.Descripcion = (string)datos.Lector["CategoriaDesc"];
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

        public List<Producto> BuscarPaginado(string busqueda, int idMarca, int idCategoria, int numeroPagina, int tamanioPagina)
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                int offset = (numeroPagina - 1) * tamanioPagina;
                string consulta = @"SELECT p.IdProducto, p.Nombre, p.Descripcion, 
                                        p.UltimoPrecioCompra, p.PorcentajeGanancia, 
                                        p.StockActual, p.StockMinimo,
                                        m.IdMarca, m.Descripcion AS MarcaDesc,
                                        c.IdCategoria, c.Descripcion AS CategoriaDesc
                                       FROM Productos p
                                       INNER JOIN Marcas m ON p.IdMarca = m.IdMarca
                                       INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
                                       WHERE 1=1";

                consulta += ObtenerWhereFiltros(busqueda, idMarca, idCategoria);
                consulta += " ORDER BY p.Nombre, p.IdProducto OFFSET @offset ROWS FETCH NEXT @tamanioPagina ROWS ONLY";

                datos.setearConsulta(consulta);
                CargarParametrosFiltros(datos, busqueda, idMarca, idCategoria);
                datos.setearParametro("@offset", offset);
                datos.setearParametro("@tamanioPagina", tamanioPagina);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto p = new Producto();
                    p.IdProducto = (int)datos.Lector["IdProducto"];
                    p.Nombre = (string)datos.Lector["Nombre"];
                    p.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                    p.UltimoPrecio = (decimal)datos.Lector["UltimoPrecioCompra"];
                    p.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    p.StockActual = (int)datos.Lector["StockActual"];
                    p.StockMinimo = (int)datos.Lector["StockMinimo"];
                    p.Marca = new Marca();
                    p.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    p.Marca.Descripcion = (string)datos.Lector["MarcaDesc"];
                    p.Categoria = new Categoria();
                    p.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    p.Categoria.Descripcion = (string)datos.Lector["CategoriaDesc"];
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

        public int Contar(string busqueda, int idMarca, int idCategoria)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = "SELECT COUNT(*) FROM Productos p WHERE 1=1";

                consulta += ObtenerWhereFiltros(busqueda, idMarca, idCategoria);

                datos.setearConsulta(consulta);
                CargarParametrosFiltros(datos, busqueda, idMarca, idCategoria);

                int total = datos.ejecutarAccionScalar();
                datos.cerrarConexion();
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ObtenerWhereFiltros(string busqueda, int idMarca, int idCategoria)
        {
            string where = string.Empty;

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                where += " AND p.Nombre LIKE @busqueda";
            }

            if (idMarca > 0)
            {
                where += " AND p.IdMarca = @idMarca";
            }

            if (idCategoria > 0)
            {
                where += " AND p.IdCategoria = @idCategoria";
            }

            return where;
        }

        private void CargarParametrosFiltros(AccesoDatos datos, string busqueda, int idMarca, int idCategoria)
        {
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                datos.setearParametro("@busqueda", "%" + busqueda + "%");
            }

            if (idMarca > 0)
            {
                datos.setearParametro("@idMarca", idMarca);
            }

            if (idCategoria > 0)
            {
                datos.setearParametro("@idCategoria", idCategoria);
            }
        }

        public Producto BuscarPorId(int id)
        {
            Producto producto = null;
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(@"SELECT p.IdProducto, p.Nombre, p.Descripcion,
                                        p.UltimoPrecioCompra, p.PorcentajeGanancia,
                                        p.StockActual, p.StockMinimo,
                                        m.IdMarca, m.Descripcion AS MarcaDesc,
                                        c.IdCategoria, c.Descripcion AS CategoriaDesc
                                       FROM Productos p
                                       INNER JOIN Marcas m ON p.IdMarca = m.IdMarca
                                       INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
                                       WHERE p.IdProducto = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    producto = new Producto();
                    producto.IdProducto = (int)datos.Lector["IdProducto"];
                    producto.Nombre = (string)datos.Lector["Nombre"];
                    producto.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                    producto.UltimoPrecio = (decimal)datos.Lector["UltimoPrecioCompra"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.Marca = new Marca();
                    producto.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    producto.Marca.Descripcion = (string)datos.Lector["MarcaDesc"];
                    producto.Categoria = new Categoria();
                    producto.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    producto.Categoria.Descripcion = (string)datos.Lector["CategoriaDesc"];
                }
                datos.cerrarConexion();

                if (producto != null)
                    producto.Proveedores = ListarProveedoresDeProducto(id);

                return producto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Proveedor> ListarProveedoresDeProducto(int idProducto)
        {
            List<Proveedor> lista = new List<Proveedor>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(@"SELECT prov.IdProveedor, prov.Nombre
                                       FROM Proveedores prov
                                       INNER JOIN Producto_Proveedor pp ON prov.IdProveedor = pp.IdProveedor
                                       WHERE pp.IdProducto = @id");
                datos.setearParametro("@id", idProducto);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Proveedor p = new Proveedor();
                    p.IdProveedor = (int)datos.Lector["IdProveedor"];
                    p.Nombre = (string)datos.Lector["Nombre"];
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

        public void Agregar(Producto producto)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(@"INSERT INTO Productos (Nombre, Descripcion, UltimoPrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, IdMarca, IdCategoria)
                                       VALUES (@nombre, @descripcion, @precio, @ganancia, @stockActual, @stockMinimo, @idMarca, @idCategoria);
                                       SELECT SCOPE_IDENTITY();");
                datos.setearParametro("@nombre", producto.Nombre);
                datos.setearParametro("@descripcion", producto.Descripcion ?? "");
                datos.setearParametro("@precio", producto.UltimoPrecio);
                datos.setearParametro("@ganancia", producto.PorcentajeGanancia);
                datos.setearParametro("@stockActual", producto.StockActual);
                datos.setearParametro("@stockMinimo", producto.StockMinimo);
                datos.setearParametro("@idMarca", producto.Marca.IdMarca);
                datos.setearParametro("@idCategoria", producto.Categoria.IdCategoria);
                int nuevoId = datos.ejecutarAccionScalar();

                if (producto.Proveedores != null)
                {
                    foreach (Proveedor prov in producto.Proveedores)
                    {
                        AccesoDatos datosRel = new AccesoDatos();
                        datosRel.setearConsulta("INSERT INTO Producto_Proveedor (IdProducto, IdProveedor) VALUES (@idProducto, @idProveedor)");
                        datosRel.setearParametro("@idProducto", nuevoId);
                        datosRel.setearParametro("@idProveedor", prov.IdProveedor);
                        datosRel.ejecutarAccion();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Producto producto)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(@"UPDATE Productos SET Nombre = @nombre, Descripcion = @descripcion,
                                        UltimoPrecioCompra = @precio, PorcentajeGanancia = @ganancia,
                                        StockActual = @stockActual, StockMinimo = @stockMinimo,
                                        IdMarca = @idMarca, IdCategoria = @idCategoria
                                       WHERE IdProducto = @id");
                datos.setearParametro("@nombre", producto.Nombre);
                datos.setearParametro("@descripcion", producto.Descripcion ?? "");
                datos.setearParametro("@precio", producto.UltimoPrecio);
                datos.setearParametro("@ganancia", producto.PorcentajeGanancia);
                datos.setearParametro("@stockActual", producto.StockActual);
                datos.setearParametro("@stockMinimo", producto.StockMinimo);
                datos.setearParametro("@idMarca", producto.Marca.IdMarca);
                datos.setearParametro("@idCategoria", producto.Categoria.IdCategoria);
                datos.setearParametro("@id", producto.IdProducto);
                datos.ejecutarAccion();

                AccesoDatos datosDel = new AccesoDatos();
                datosDel.setearConsulta("DELETE FROM Producto_Proveedor WHERE IdProducto = @id");
                datosDel.setearParametro("@id", producto.IdProducto);
                datosDel.ejecutarAccion();

                if (producto.Proveedores != null)
                {
                    foreach (Proveedor prov in producto.Proveedores)
                    {
                        AccesoDatos datosRel = new AccesoDatos();
                        datosRel.setearConsulta("INSERT INTO Producto_Proveedor (IdProducto, IdProveedor) VALUES (@idProducto, @idProveedor)");
                        datosRel.setearParametro("@idProducto", producto.IdProducto);
                        datosRel.setearParametro("@idProveedor", prov.IdProveedor);
                        datosRel.ejecutarAccion();
                    }
                }
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
                AccesoDatos datosDel = new AccesoDatos();
                datosDel.setearConsulta("DELETE FROM Producto_Proveedor WHERE IdProducto = @id");
                datosDel.setearParametro("@id", id);
                datosDel.ejecutarAccion();

                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM Productos WHERE IdProducto = @id");
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
