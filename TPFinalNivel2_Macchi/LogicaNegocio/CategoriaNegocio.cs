using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LogicaNegocio
{
    public class CategoriaNegocio
    {
        /// <summary>
        /// Leer y devolver de la base de datos los registros de la entidad "CATEGORIAS".
        /// </summary>
        /// <returns>Lista con objetos de tipo clase Categoria</returns>
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
            catch (SqlException)
            {
                MessageBox.Show("No se pudo cargar las categorías por un error en la conexión " + 
                                "a la base de datos.", "Error: categoría negocio", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return categorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return categorias;
            }
            finally
            {
                db.CloseConnection();
            }
        }
    }
}
