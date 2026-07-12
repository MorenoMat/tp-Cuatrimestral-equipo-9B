using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdCategoria, Descripcion FROM Categorias ORDER BY Descripcion, IdCategoria");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria c = new Categoria();
                    c.IdCategoria = (int)datos.Lector["IdCategoria"];
                    c.Descripcion = (string)datos.Lector["Descripcion"];
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

        public List<Categoria> BuscarPaginado(string busqueda, int numeroPagina, int tamanioPagina)
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                int offset = (numeroPagina - 1) * tamanioPagina;
                string consulta = "SELECT IdCategoria, Descripcion FROM Categorias WHERE 1=1";

                consulta += ObtenerWhereFiltros(busqueda);
                consulta += " ORDER BY Descripcion, IdCategoria OFFSET @offset ROWS FETCH NEXT @tamanioPagina ROWS ONLY";

                datos.setearConsulta(consulta);
                CargarParametrosFiltros(datos, busqueda);
                datos.setearParametro("@offset", offset);
                datos.setearParametro("@tamanioPagina", tamanioPagina);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria c = new Categoria();
                    c.IdCategoria = (int)datos.Lector["IdCategoria"];
                    c.Descripcion = (string)datos.Lector["Descripcion"];
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

        public int Contar(string busqueda)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                string consulta = "SELECT COUNT(*) FROM Categorias WHERE 1=1";

                consulta += ObtenerWhereFiltros(busqueda);

                datos.setearConsulta(consulta);
                CargarParametrosFiltros(datos, busqueda);

                int total = datos.ejecutarAccionScalar();
                datos.cerrarConexion();
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ObtenerWhereFiltros(string busqueda)
        {
            string where = string.Empty;

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                where += " AND Descripcion LIKE @busqueda";
            }

            return where;
        }

        private void CargarParametrosFiltros(AccesoDatos datos, string busqueda)
        {
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                datos.setearParametro("@busqueda", "%" + busqueda + "%");
            }
        }

        public Categoria BuscarPorId(int id)
        {
            Categoria categoria = null;
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdCategoria, Descripcion FROM Categorias WHERE IdCategoria = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    categoria = new Categoria();
                    categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    categoria.Descripcion = (string)datos.Lector["Descripcion"];
                }
                datos.cerrarConexion();
                return categoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Agregar(Categoria categoria)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("INSERT INTO Categorias (Descripcion) VALUES (@descripcion)");
                datos.setearParametro("@descripcion", categoria.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Categoria categoria)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Categorias SET Descripcion = @descripcion WHERE IdCategoria = @id");
                datos.setearParametro("@descripcion", categoria.Descripcion);
                datos.setearParametro("@id", categoria.IdCategoria);
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
                datos.setearConsulta("DELETE FROM Categorias WHERE IdCategoria = @id");
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
