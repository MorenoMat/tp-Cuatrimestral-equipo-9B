using System;
using System.Collections.Generic;
using Dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdMarca, Descripcion FROM Marcas ORDER BY Descripcion, IdMarca");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Marca m = new Marca();
                    m.IdMarca = (int)datos.Lector["IdMarca"];
                    m.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(m);
                }
                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Marca> BuscarPaginado(string busqueda, int numeroPagina, int tamanioPagina)
        {
            List<Marca> lista = new List<Marca>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                int offset = (numeroPagina - 1) * tamanioPagina;
                string consulta = "SELECT IdMarca, Descripcion FROM Marcas WHERE 1=1";

                consulta += ObtenerWhereFiltros(busqueda);
                consulta += " ORDER BY Descripcion, IdMarca OFFSET @offset ROWS FETCH NEXT @tamanioPagina ROWS ONLY";

                datos.setearConsulta(consulta);
                CargarParametrosFiltros(datos, busqueda);
                datos.setearParametro("@offset", offset);
                datos.setearParametro("@tamanioPagina", tamanioPagina);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Marca m = new Marca();
                    m.IdMarca = (int)datos.Lector["IdMarca"];
                    m.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(m);
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
                string consulta = "SELECT COUNT(*) FROM Marcas WHERE 1=1";

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

        public Marca BuscarPorId(int id)
        {
            Marca marca = null;
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IdMarca, Descripcion FROM Marcas WHERE IdMarca = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    marca = new Marca();
                    marca.IdMarca = (int)datos.Lector["IdMarca"];
                    marca.Descripcion = (string)datos.Lector["Descripcion"];
                }
                datos.cerrarConexion();
                return marca;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Agregar(Marca marca)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("INSERT INTO Marcas (Descripcion) VALUES (@descripcion)");
                datos.setearParametro("@descripcion", marca.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Marca marca)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Marcas SET Descripcion = @descripcion WHERE IdMarca = @id");
                datos.setearParametro("@descripcion", marca.Descripcion);
                datos.setearParametro("@id", marca.IdMarca);
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
                datos.setearConsulta("DELETE FROM Marcas WHERE IdMarca = @id");
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
