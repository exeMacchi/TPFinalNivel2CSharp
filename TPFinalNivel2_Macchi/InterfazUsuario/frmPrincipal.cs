using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;

namespace InterfazUsuario
{
    public partial class frmPrincipal : Form
    {
        // ================================================================== //
        // ---------------------------- Atributos --------------------------- //
        // ================================================================== //
        /// <summary>
        /// DataSource fundamental del DGV.
        /// </summary>
        private List<Articulo> articulos = null;
        
        /// <summary>
        /// Atributo bandera que me permite verificar si hay un nuevo artículo pendiente.
        /// </summary>
        private bool nuevoArticuloPendiente;

        /// <summary>
        /// Atributo bandera que me permite verificar si hay una modificación pendiente.
        /// </summary>
        private bool modificacionPendiente;

        /// <summary>
        /// Boolean que me ayuda a controlar la validación del botón <see cref="btnReiniciarAA"/>
        /// y la del botón <see cref="btnReiniciarMA"/> cuando no carga la imagen local tanto 
        /// en la plantilla "Agregar artículo" como en la plantilla "Modificar artículo".
        /// </summary>
        private bool imagenLocal;

        /// <summary>
        /// ID del artículo que se está modificando. Sirve para realizar la modificación
        /// en la base de datos y para focusear el artículo en el DGV con el fin de mostrar
        /// al usuario su información modificada.
        /// </summary>
        private int idArticuloModificacionPendiente;

        /// <summary>
        /// Entero que almacena la cantidad de artículos presentes en el DGV.
        /// </summary>
        private int cantArticulos;
        
        // ================================================================== //
        // ---------------------------- Constructor ------------------------- //
        // ================================================================== //
        public frmPrincipal()
        {
            InitializeComponent();
            StartUp();
            ReiniciarPlantillaNuevoArticulo();
            ReiniciarPlantillaModificacion();
        }
    }
}
