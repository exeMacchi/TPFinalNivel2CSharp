using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfazUsuario
{
    public partial class frmPrincipal : Form
    {
        private bool modificacionPendiente;
        private int indiceModificacionPendiente;
        public frmPrincipal()
        {
            InitializeComponent();
            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;
            panelFiltroAvanzado.Visible = false;
            panelDetallesArticulos.Visible = true;
            panelModificarArticulo.Visible = false;
            panelAgregarArticulo.Visible = false;
            // Reiniciar panelAgregarArticulo (por las dudas);
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

        // Activar PanelSubPrincipalDerecha
        private void btnSeleccionarArticulo_Click(object sender, EventArgs e)
        {
            if (panelDetallesArticulos.Visible)
            {
                if (panelAgregarArticulo.Visible) // Verifica si la capa de articulos esta abierta
                {
                    panelAgregarArticulo.Visible = false;
                    panelModificarArticulo.Visible = false;
                    // Mostrar info articulo seleccionado
                }
                else if (panelModificarArticulo.Visible) // Verifica si la capa de modificación está abierta.
                {
                    if (modificacionPendiente)
                    {
                        btnModificacionPendiente.Visible = true;
                    }
                    panelModificarArticulo.Visible = false;
                    // Mostrar info articulo seleccionado
                }
                else
                {
                    MessageBox.Show("Cambio de índice");
                    // Mostrar info articulo seleccionado
                }
            }
            else if (!panelDetallesArticulos.Visible)
            {
                panelDetallesArticulos.Visible = true;
                MessageBox.Show("Cambio de índice");
                // Mostrar info articulo seleccionado
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
            panelAgregarArticulo.Visible = false;
            panelModificarArticulo.Visible = false;

            // Funcionalidad
                // Aviso: articulo agregado con exito!
                // Resetear panelArticuloAgregar()
                // Focusear el indice del nuevo articulo para que el usuario vea la info.
        }

        private void btnCancelarAgregacion_Click(object sender, EventArgs e)
        {
            panelAgregarArticulo.Visible = false;
            panelModificarArticulo.Visible = false;

            // Funcionalidad
                // Aviso: articulo descartado
                // Resetear panelArticuloAgregar()
                // Focusear el indice del nuevo articulo para que el usuario vea la info.
        }

        private void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            if (!panelDetallesArticulos.Visible)
            {
                panelDetallesArticulos.Visible = true;
            }
            if (modificacionPendiente)
            {
                btnModificacionPendiente.Visible = true;
            }
            panelModificarArticulo.Visible = true;
            panelAgregarArticulo.Visible = true;
        }

        private void btnModificacionPendiente_Click(object sender, EventArgs e)
        {
            if (!panelDetallesArticulos.Visible)
            {
                panelDetallesArticulos.Visible = true;
            }
            if (panelAgregarArticulo.Visible)
            {
                panelAgregarArticulo.Visible = false;
            }
            panelModificarArticulo.Visible = true;
        }
    }
}
