using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class Datos
    {
        // Atributos
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        
        // Propiedades
        public SqlDataReader Lector { get { return lector; } }

        // Constructor
        public Datos()
        {
            conexion = new SqlConnection("server=DESKTOP-FUV4AD1;" + 
                                         "database=CATALOGO_DB;" + 
                                         "integrated security=true;");
            comando = new SqlCommand();
        }

        // Métodos
        public void SetQuery(string query)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
        }

        public void SetParametro(string param, object value)
        {
            comando.Parameters.AddWithValue(param, value);
        }

        public void ExecuteRead()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ExecuteNonQuery()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseConnection()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }
    }
}
