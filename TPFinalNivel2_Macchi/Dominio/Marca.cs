using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Marca
    {
        // Atributos
        private int id;
        private string descripcion;

        // Propiedades
        public int ID { get { return id; } set { id = value; } }

        public string Descripcion { get {  return descripcion; } set { descripcion = value; } }

        // Métodos
        public override string ToString()
        {
            return descripcion;
        }
    }
}
