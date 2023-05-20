using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;

namespace LogicaNegocio
{
    public class MarcaNegocio
    {
        /// <summary>
        /// Leer y devolver de la base de datos los registros de la entidad "MARCAS".
        /// </summary>
        /// <returns>Lista con objetos de tipo clase Marca</returns>
        public List<Marca> CargarMarcas()
        {
            List<Marca> marcas = new List<Marca>();
            Datos db = new Datos();

            try
            {
                db.SetQuery("SELECT M.Id AS ID, " +
	                        "       M.Descripcion AS Descripcion " +
                            "FROM MARCAS AS M " +
                            "ORDER BY M.Descripcion ASC;");
                db.ExecuteRead();

                while (db.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.ID = (int) db.Lector["ID"];
                    marca.Descripcion = (string) db.Lector["Descripcion"];

                    marcas.Add(marca);
                }

                return marcas;
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
