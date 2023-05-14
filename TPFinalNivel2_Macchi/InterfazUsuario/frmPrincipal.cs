using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using LogicaNegocio;

namespace InterfazUsuario
{
    public partial class frmPrincipal : Form
    {
        // Atributos
        private List<Articulo> articulos = null;
        private bool modificacionPendiente;
        private int indiceModificacionPendiente;
        
        // Constructor
        public frmPrincipal()
        {
            InitializeComponent();
            StartUp();
            // Reiniciar panelAgregarArticulo (por las dudas);
        }

        // --------------------- Métodos ------------------------ //
        private void StartUp()
        {
            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;
            panelFiltroAvanzado.Visible = false;
            panelModificarArticulo.Visible = false;
            panelAgregarArticulo.Visible = false;
        }
        private void NingunRegistro()
        {
            dgvArticulos.Visible = false;
            panelModificarArticulo.Visible = true;
            panelAgregarArticulo.Visible = true;
            btnCancelarAgregacion.Enabled = false;
        }
        private void MostrarInfoArticulo()
        {
            Articulo articuloSeleccionado = (Articulo) dgvArticulos.CurrentRow.DataBoundItem;
            txbxCodigoDA.Text = articuloSeleccionado.Codigo;
            txbxNombreDA.Text = articuloSeleccionado.Nombre;
            txbxDescripcionDA.Text = articuloSeleccionado.Descripcion;
            txbxMarcaDA.Text = articuloSeleccionado.Marca.Descripcion;
            txbxCategoriaDA.Text = articuloSeleccionado.Categoria.Descripcion;
            // CargarImagen();
            txbxPrecioDA.Text = "$" + articuloSeleccionado.Precio.ToString("N2");
        }

        // --------------------- Eventos ------------------------ //
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ArticuloNegocio artNegocio = new ArticuloNegocio();
            try
            {
                articulos = artNegocio.CargarArticulos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error: cargar articulos");
            }

            if (articulos != null && articulos.Count > 0)
            {
                dgvArticulos.DataSource = articulos;
            }
            else
            {
                NingunRegistro();
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay articulos disponibles. Si no los hay, activar
            // la pestaña de Agregar artículo.
            if (dgvArticulos.CurrentRow != null)
            {
                if (panelAgregarArticulo.Visible) // Verifica si la capa de articulos esta abierta
                {
                    panelAgregarArticulo.Visible = false;
                    panelModificarArticulo.Visible = false;
                    // Mostrar info articulo seleccionado
                    MostrarInfoArticulo();
                }
                else if (panelModificarArticulo.Visible) // Verifica si la capa de modificación está abierta.
                {
                    if (modificacionPendiente)
                    {
                        btnModificacionPendiente.Visible = true;
                    }
                    panelModificarArticulo.Visible = false;
                    // Mostrar info articulo seleccionado
                    MostrarInfoArticulo();
                }
                else
                {
                    // Mostrar info articulo seleccionado
                    MostrarInfoArticulo();
                }
            }
            else
            {
                NingunRegistro();
            }
        }




        // Filtro avanzado
        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            if (panelFiltroAvanzado.Visible)
            {
                panelFiltroAvanzado.Visible = false;
            }    
            else if (!panelFiltroAvanzado.Visible)
            {
                panelFiltroAvanzado.Visible = true;
            }
        }

        // Detalles artículos
        private void btnModificarArticulo_Click(object sender, EventArgs e)
        {
            // Verifico si hay una modificacion pendiente y, si la hay, le pregunto al
            // usuario si quiere descartar los cambios y modificar el item seleccionado
            if (modificacionPendiente) 
            {
                DialogResult r = MessageBox.Show("Hay una modifición pendiente, ¿quiere descartarla?", 
                                 "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (r == DialogResult.Yes)
                {
                    btnModificacionPendiente.Visible = false;
                    // Aviso: cambios descartados.
                    // Nueva modificacion()
                        panelModificarArticulo.Visible = true;
                        modificacionPendiente = true;
                        // indiceModificacionPendiente = dgvArticulos...
                }
            }
            else
            {
                // Nueva modificacion()
                    panelModificarArticulo.Visible = true;
                    modificacionPendiente = true;
                    // indiceModificacionPendiente = dgvArticulos...
            }
        }

        private void btnEliminarArticulo_Click(object sender, EventArgs e)
        {
            // Funcionalidad de eliminacion fisica.
        }


        // Modificar artículo
        private void btnConfirmarModificacion_Click(object sender, EventArgs e)
        {
            // Realizar cambios
            // Aviso: cambios realizados.
            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;
            panelModificarArticulo.Visible = false;
            // Focusear articulo modificado
        }

        private void btnCancelarModificacion_Click(object sender, EventArgs e)
        {
            // Aviso: cambios descarto.s
            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;
            panelModificarArticulo.Visible = false;
            // Focusear articulo modificado
        }


        // Agregar artículo
        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            if (!btnCancelarAgregacion.Enabled)
                btnCancelarAgregacion.Enabled = true;

            // Agregar
            // Aviso: articulo agregado con exito!
            // Focusear el indice del nuevo articulo para que el usuario vea la info.
                // Cuando se focuseé, esto ya no va a ser necesario.
                panelAgregarArticulo.Visible = false;
                panelModificarArticulo.Visible = false;
            // Resetear panelArticuloAgregar()
        }

        private void btnCancelarAgregacion_Click(object sender, EventArgs e)
        {
            // Aviso: articulo descartado
            // Focusear el indice del nuevo articulo para que el usuario vea la info.
                // Cuando se focuseé, esto ya no va a ser necesario.
                panelAgregarArticulo.Visible = false;
                panelModificarArticulo.Visible = false;
            // Resetear panelArticuloAgregar()
        }

        private void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            if (modificacionPendiente)
            {
                btnModificacionPendiente.Visible = true;
            }
            panelModificarArticulo.Visible = true;
            panelAgregarArticulo.Visible = true;
        }

        private void btnModificacionPendiente_Click(object sender, EventArgs e)
        {
            if (panelAgregarArticulo.Visible)
            {
                panelAgregarArticulo.Visible = false;
            }
            panelModificarArticulo.Visible = true;
        }


        private void dgvArticulos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Verificar si hay mínimo un dato en el DGV
            if (!dgvArticulos.Visible)
            {
                dgvArticulos.Visible = true;
            }
        }
    }
}
