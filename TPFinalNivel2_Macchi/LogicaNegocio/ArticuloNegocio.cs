﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;

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
    }
}