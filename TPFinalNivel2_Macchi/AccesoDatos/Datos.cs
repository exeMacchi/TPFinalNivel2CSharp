﻿using System;
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
        /// <summary>
        /// Establecer la consulta SQL al SqlCommand
        /// </summary>
        /// <param name="query">Consulta SQL.</param>
        public void SetQuery(string query)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
        }


        /// <summary>
        /// Configurar los parámetros de comando.
        /// </summary>
        /// <param name="param">@Parámetro.</param>
        /// <param name="value">Valor del parámetro.</param>
        public void SetParametro(string param, object value)
        {
            comando.Parameters.AddWithValue(param, value);
        }


        /// <summary>
        /// Ejecutar la consulta SQL para una operación SELECT.
        /// </summary>
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


        /// <summary>
        /// Ejecutar la consulta SQL para una operación INSERT, UPDATE o DELETE. 
        /// </summary>
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


        /// <summary>
        /// Cerrar la conexión a la base de datos y posible DataReader.
        /// </summary>
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
