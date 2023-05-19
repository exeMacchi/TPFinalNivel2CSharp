using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;
using System.Runtime.Serialization;

namespace LogicaNegocio
{
    public class ArticuloNegocio
    { 
        public List<Articulo> CargarArticulos()
        {
            List<Articulo> articulos = new List<Articulo>();
            Datos db = new Datos();
            try
            {
                db.SetQuery("SELECT A.Id AS ID, " +
	                        "       A.Codigo AS CODIGO, " +
	                        "       A.Nombre AS NOMBRE, " +
	                        "       A.Descripcion AS DESCRIPCION, " +
	                        "       A.IdMarca AS IDMARCA, " +
	                        "       M.Descripcion AS MARCA, " +
	                        "       A.IdCategoria AS IDCATEGORIA, " +
	                        "       C.Descripcion AS CATEGORIA, " +
	                        "       A.ImagenUrl AS IMAGEN, " +
	                        "       A.Precio AS PRECIO " +
                            "FROM ARTICULOS AS A " +
                            "INNER JOIN MARCAS AS M ON A.IdMarca = M.Id " +
                            "INNER JOIN CATEGORIAS AS C ON A.IdCategoria = C.Id;");
                db.ExecuteRead();

                while (db.Lector.Read())
                {
                    Articulo art = new Articulo();

                    art.ID = (int)db.Lector["ID"];

                    if (!(db.Lector["CODIGO"] is DBNull))
                        art.Codigo = (string)db.Lector["CODIGO"];

                    if (!(db.Lector["NOMBRE"] is DBNull))
                        art.Nombre = (string)db.Lector["NOMBRE"];

                    if (!(db.Lector["DESCRIPCION"] is DBNull))
                        art.Descripcion = (string)db.Lector["DESCRIPCION"];

                    art.Marca.ID = (int)db.Lector["IDMARCA"];

                    if (!(db.Lector["MARCA"] is DBNull))
                        art.Marca.Descripcion = (string)db.Lector["MARCA"];

                    art.Categoria.ID = (int)db.Lector["IDCATEGORIA"];

                    if (!(db.Lector["CATEGORIA"] is DBNull)) 
                        art.Categoria.Descripcion = (string)db.Lector["CATEGORIA"];

                    if (!(db.Lector["IMAGEN"] is DBNull))
                        art.Imagen = (string)db.Lector["IMAGEN"];

                    if (!(db.Lector["PRECIO"] is DBNull))
                        art.Precio = (decimal)db.Lector["PRECIO"];

                    articulos.Add(art);
                }

                return articulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void AgregarNuevoArticulo(Articulo nuevoArticulo)
        {
            Datos db = new Datos();

            try
            {
                db.SetQuery("INSERT INTO ARTICULOS " +
                            "VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, " +
                            "        @Imagen, @Precio);");
                db.SetParametro("@Codigo", nuevoArticulo.Codigo);
                db.SetParametro("@Nombre", nuevoArticulo.Nombre);
                db.SetParametro("@Descripcion", nuevoArticulo.Descripcion);
                db.SetParametro("@IdMarca", nuevoArticulo.Marca.ID);
                db.SetParametro("@IdCategoria", nuevoArticulo.Categoria.ID);
                db.SetParametro("@Imagen", nuevoArticulo.Imagen);
                db.SetParametro("@Precio", nuevoArticulo.Precio);
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void BorrarArticulo(Articulo articuloSeleccionado)
        {
            Datos db = new Datos();

            try
            {
                db.SetQuery("DELETE FROM ARTICULOS WHERE ARTICULOS.Id = @ID;");
                db.SetParametro("@ID", articuloSeleccionado.ID);
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void ModificarArticulo(Articulo articuloModificado)
        {
            Datos db = new Datos();

            try
            {
                db.SetQuery("UPDATE ARTICULOS " +
                            "SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, " +
                            "    IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @Imagen, " + 
                            "    Precio = @Precio " +
                            "WHERE ARTICULOS.Id = @ID;");
                db.SetParametro("@Codigo", articuloModificado.Codigo);
                db.SetParametro("@Nombre", articuloModificado.Nombre);
                db.SetParametro("@Descripcion", articuloModificado.Descripcion);
                db.SetParametro("@IdMarca", articuloModificado.Marca.ID);
                db.SetParametro("@IdCategoria", articuloModificado.Categoria.ID);
                db.SetParametro("@Imagen", articuloModificado.Imagen);
                db.SetParametro("@Precio", articuloModificado.Precio);
                db.SetParametro("@ID", articuloModificado.ID);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public List<Articulo> Buscar(string condicion)
        {
            List<Articulo> articulosEncontrados = new List<Articulo>();
            Datos db = new Datos();

            try
            {
                db.SetQuery("SELECT A.Id AS ID, " +
                            "       A.Codigo AS CODIGO, " +
                            "       A.Nombre AS NOMBRE, " +
                            "       A.Descripcion AS DESCRIPCION, " +
                            "       A.IdMarca AS IDMARCA, " +
                            "       M.Descripcion AS MARCA, " +
                            "       A.IdCategoria AS IDCATEGORIA, " +
                            "       C.Descripcion AS CATEGORIA, " +
                            "       A.ImagenUrl AS IMAGEN, " +
                            "       A.Precio AS PRECIO " +
                            "FROM ARTICULOS AS A " +
                            "INNER JOIN MARCAS AS M ON A.IdMarca = M.Id " +
                            "INNER JOIN CATEGORIAS AS C ON A.IdCategoria = C.Id " +
                            condicion);
                db.ExecuteRead();

                while (db.Lector.Read())
                {
                    Articulo art = new Articulo();

                    art.ID = (int)db.Lector["ID"];

                    if (!(db.Lector["CODIGO"] is DBNull))
                        art.Codigo = (string)db.Lector["CODIGO"];

                    if (!(db.Lector["NOMBRE"] is DBNull))
                        art.Nombre = (string)db.Lector["NOMBRE"];

                    if (!(db.Lector["DESCRIPCION"] is DBNull))
                        art.Descripcion = (string)db.Lector["DESCRIPCION"];

                    art.Marca.ID = (int)db.Lector["IDMARCA"];

                    if (!(db.Lector["MARCA"] is DBNull))
                        art.Marca.Descripcion = (string)db.Lector["MARCA"];

                    art.Categoria.ID = (int)db.Lector["IDCATEGORIA"];

                    if (!(db.Lector["CATEGORIA"] is DBNull))
                        art.Categoria.Descripcion = (string)db.Lector["CATEGORIA"];

                    if (!(db.Lector["IMAGEN"] is DBNull))
                        art.Imagen = (string)db.Lector["IMAGEN"];

                    if (!(db.Lector["PRECIO"] is DBNull))
                        art.Precio = (decimal)db.Lector["PRECIO"];

                    articulosEncontrados.Add(art);
                }

                return articulosEncontrados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.CloseConnection();
            }
        }
    }
}
