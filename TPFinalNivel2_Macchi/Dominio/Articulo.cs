using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        // Atributos
        private int id;
        private string codigo;
        private string nombre;
        private string descripcion;
        private Marca marca;
        private Categoria categoria;
        private string imagen;
        private decimal precio;

        // Propiedades
        public int ID { get { return id; } set { id = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public Marca Marca { get { return marca; } set { marca = value; } }
        public Categoria Categoria { get { return categoria; } set { categoria = value; } }
        public string Imagen { get { return imagen; } set { imagen = value; } }
        public decimal Precio { get { return precio; } set { precio = value; } }

        // Constructor
        public Articulo()
        {
            marca = new Marca();
            categoria = new Categoria();
        }

        // Métodos
        public override string ToString()
        {
            return $"Código: {codigo}\n" +
                   $"Nombre: {nombre}\n" +
                   $"Descripcion: {descripcion}\n" +
                   $"Marca: {marca.Descripcion}\n" +
                   $"Categoria: {categoria.Descripcion}\n" +
                   $"Imagen: {imagen}\n" +
                   $"Precio: ${precio}";
        }
    }
}
