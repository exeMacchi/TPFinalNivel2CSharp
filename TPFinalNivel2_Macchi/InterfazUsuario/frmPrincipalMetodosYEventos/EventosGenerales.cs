using System;
using System.Windows.Forms;

namespace InterfazUsuario
{
    public partial class frmPrincipal
    {
        // ================================================================== //
        // ------------------------- Detalles artículos --------------------- //
        // ================================================================== //

        /// <summary>
        /// Mostrar la información del artículo seleccionado en la plantilla "Detalles artículos"
        /// </summary>
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            // El botón "Nuevo artículo" o "Nuevo artículo pendiente" se hace visible,
            // sea el caso que sea.
            btnNuevoArticulo.Visible = true;

            // Verificar si hay articulos disponibles. Si no los hay, activar método NingunRegistro()
            if (dgvArticulos.CurrentRow != null)
            {
                // Verificar si la capa "Agregar articulo" está abierta; si lo está, la oculta.
                if (panelAgregarArticulo.Visible) 
                {
                    MostrarInfoArticulo();
                    panelAgregarArticulo.Visible = false;
                    panelModificarArticulo.Visible = false;
                }
                // Verificar si la capa "Modificar artículo" está abierta; si lo está, la oculta.
                else if (panelModificarArticulo.Visible) 
                {
                    MostrarInfoArticulo();
                    if (modificacionPendiente)
                    {
                        btnModificacionPendiente.Visible = true;
                    }
                    panelModificarArticulo.Visible = false;
                }
                else
                {
                    MostrarInfoArticulo();
                }
            }
            else if (articulos.Count <= 0)
            {
                NingunRegistro();
            }
        }


        /// <summary>
        /// Verificar que haya mínimo un registro en el DGV para mostrarlo y habilitar la
        /// búsqueda
        /// </summary>
        private void dgvArticulos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!dgvArticulos.Visible)
            {
                dgvArticulos.Visible = true;
                FormatearDGV();
                btnBuscar.Enabled = true;
            }
        }


        // ================================================================== //
        // ---------------------------- Generales --------------------------- //
        // ================================================================== //

        /// <summary>
        /// Cargar al iniciar la aplicación los artículos al DGV.
        /// </summary>
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ActualizarDGV();
        }


        /// <summary>
        /// Verificar antes de que se cierre la aplicación si hay una modificación pendiente
        /// y/o información de un nuevo artículo no agregado. Si lo hubiera, se le pregunta
        /// al usuario si quiere descartar dicha modificación y/o agregación.
        /// </summary>
        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool noSalida = false;

            if (modificacionPendiente)
            {
                DialogResult r = MessageBox.Show("Hay una modificación pendiente, ¿quiere descartarla?", 
                                                 "Modificación pendiente", MessageBoxButtons.YesNo, 
                                                 MessageBoxIcon.Warning);

                if (r == DialogResult.No)
                {
                    noSalida = true;
                }
                else
                {
                    noSalida = false;
                }
            }

            if (!noSalida)
            {
                if (nuevoArticuloPendiente)
                {
                    DialogResult r = MessageBox.Show("Hay información de un nuevo artículo pendiente, " +
                                                     "¿quiere descartarla?", "Nuevo artículo pendiente",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (r == DialogResult.No)
                    {
                       noSalida = true;
                    }
                    else
                    {
                        noSalida = false;
                    }
                }
            }

            e.Cancel = noSalida;
        }


        // ----------------- Cambio de cursores en botones ------------------ //
        // Plantilla "Detalles artículos"
        private void btnModificarArticuloDA_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnModificarArticuloDA_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnEliminarArticuloDA_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnEliminarArticuloDA_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }


        // Plantilla "Agregar artículo"
        private void btnNuevoArticulo_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnNuevoArticulo_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnAgregarArticulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (btnAgregarArticulo.Enabled)
            {
                Cursor = Cursors.Hand;
            }
        }
        private void btnAgregarArticulo_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnCancelarAgregacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (btnCancelarAgregacion.Enabled)
            {
                Cursor = Cursors.Hand;
            }
        }
        private void btnCancelarAgregacion_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnReiniciarAA_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnReiniciarAA_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnCargarImagenLocalAA_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnCargarImagenLocalAA_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnCargarImagenUrlAA_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnCargarImagenUrlAA_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }


        // Plantilla "Modificar artículo"
        private void btnModificacionPendiente_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnModificacionPendiente_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnConfirmarModificacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (btnConfirmarModificacion.Enabled)
            {
                Cursor = Cursors.Hand;
            }
        }
        private void btnConfirmarModificacion_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnCancelarModificacion_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnCancelarModificacion_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnReiniciarMA_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnReiniciarMA_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnImagenLocalMA_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnImagenLocalMA_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnImagenUrlMA_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnImagenUrlMA_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }


        // Plantilla "Búsqueda de artículos"
        private void btnBuscar_MouseMove(object sender, MouseEventArgs e)
        {
            if (btnBuscar.Enabled)
            {
                Cursor = Cursors.Hand;
            }
        }
        private void btnBuscar_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnFiltroAvanzado_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void btnFiltroAvanzado_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }


        // ------------------ Estilización de los ComboBox ------------------ //
        private void comboxCriterioBusqueda_DrawItem(object sender, DrawItemEventArgs e)
        {
            FormatearComboBox((ComboBox)sender, e);
        }

        private void comboxCampoBusqueda_DrawItem(object sender, DrawItemEventArgs e)
        {
            FormatearComboBox((ComboBox)sender, e);
        }

        private void comboxMarcaAA_DrawItem(object sender, DrawItemEventArgs e)
        {
            FormatearComboBox((ComboBox)sender, e);
        }

        private void comboxCategoriaAA_DrawItem(object sender, DrawItemEventArgs e)
        {
            FormatearComboBox((ComboBox)sender, e);
        }

        private void comboxMarcaMA_DrawItem(object sender, DrawItemEventArgs e)
        {
            FormatearComboBox((ComboBox)sender, e);
        }

        private void comboxCategoriaMA_DrawItem(object sender, DrawItemEventArgs e)
        {
            FormatearComboBox((ComboBox)sender, e);
        }
    }
}
