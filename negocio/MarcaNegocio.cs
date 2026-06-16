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
                datos.setearConsulta("SELECT IdMarca, Descripcion FROM Marcas");
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
