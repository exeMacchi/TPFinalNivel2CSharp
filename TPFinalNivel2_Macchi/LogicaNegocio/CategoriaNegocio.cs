using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;

namespace LogicaNegocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> CargarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            Datos db = new Datos();

            try
            {
                db.SetQuery("SELECT C.Id AS ID, " +
	                        "       C.Descripcion AS Descripcion " +
                            "FROM CATEGORIAS AS C " +
                            "ORDER BY C.Descripcion ASC;");
                db.ExecuteRead();

                while (db.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.ID = (int) db.Lector["ID"];
                    categoria.Descripcion = (string) db.Lector["Descripcion"];

                    categorias.Add(categoria);
                }

                return categorias;
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
