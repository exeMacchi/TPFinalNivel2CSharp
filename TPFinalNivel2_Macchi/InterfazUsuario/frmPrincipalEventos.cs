using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;

namespace InterfazUsuario
{
    public partial class frmPrincipal
    {

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
                    DialogResult r = MessageBox.Show("Hay información de un nuevo artículo no agregado, " +
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
        // ------------------------- Agregar artículo ----------------------- //
        // ================================================================== //


        /// <summary>
        /// Hacer visible la plantilla "Agregar artículo".
        /// </summary>
        private void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            // Verificar si hay un nuevo artículo pendiente para saber si reiniciar o no 
            // la pantilla nuevo artículo.
            if (!nuevoArticuloPendiente)
            {
                ReiniciarPlantillaNuevoArticulo();
            }

            // El botón "Nuevo artículo" o "Nuevo artículo pendiente" se hace invisible
            // hasta que se agregue, cancele o se cambie la selección de artículo.
            btnNuevoArticulo.Visible = false;

            // Verificar si se apretó este botón desde la plantilla "Modificar artículo"
            // para habilitar el botón de "Modificación pendiente".
            if (modificacionPendiente)
            {
                btnModificacionPendiente.Visible = true;
            }

            panelModificarArticulo.Visible = true;
            panelAgregarArticulo.Visible = true;
        }


        /// <summary>
        /// Crear un nuevo artículo, agregarlo a la base de datos y mostrar su información
        /// en la plantilla "Detalles artículos".
        /// </summary>
        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            // Verificar a través del botón Cancelar si hay o no registros en el DGV. Si hay
            // mínimo un registro, se habilita el botón; si no lo hay, no se permite cancelar.
            if (!btnCancelarAgregacion.Enabled)
                btnCancelarAgregacion.Enabled = true;

            Articulo nuevoArticulo = new Articulo();
            nuevoArticulo.Codigo = txbxCodigoAA.Text;
            nuevoArticulo.Nombre = txbxNombreAA.Text;
            nuevoArticulo.Descripcion = txbxDescripcionAA.Text;
            nuevoArticulo.Marca = (Marca) comboxMarcaAA.SelectedItem;
            nuevoArticulo.Categoria = (Categoria)comboxCategoriaAA.SelectedItem;

            if (decimal.TryParse(txbxPrecioAA.Text, out decimal d))
            {
                nuevoArticulo.Precio = d;
            }
            else
            {
                MessageBox.Show("El valor del precio introducido no es válido. " +
                                "Verifique que la información sea correcta.",
                                "Error de conversión",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!CargarImagenAA(txbxCargarImagenAA.Text))
            {
                nuevoArticulo.Imagen = "";
            }
            else
            {
                nuevoArticulo.Imagen = txbxCargarImagenAA.Text;
            }

            AgregarNuevoArticulo(nuevoArticulo);
            ActualizarDGV();
            try
            {
                FocusearUltimoElemento();
                dgvArticulos_SelectionChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al focusear el nuevo artículo.");
            }
            btnNuevoArticulo.Visible = true;
            ReiniciarPlantillaNuevoArticulo();
        }


        /// <summary>
        /// Cancelar la agregación de un nuevo artículo a la base de datos. Se reinicia la
        /// plantilla a su configuración básica.
        /// </summary>
        private void btnCancelarAgregacion_Click(object sender, EventArgs e)
        {
            if (nuevoArticuloPendiente)
            {
                MessageBox.Show("Información del nuevo artículo descartada.", "Información descartada",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            panelAgregarArticulo.Visible = false;
            panelModificarArticulo.Visible = false;
            btnNuevoArticulo.Visible = true;
            ReiniciarPlantillaNuevoArticulo();
        }


        /// <summary>
        /// Reiniciar la plantilla "Agregar artículo" a su configuración básica.
        /// </summary>
        private void btnReiniciarAA_Click(object sender, EventArgs e)
        {
            ReiniciarPlantillaNuevoArticulo();
        }


        /// <summary>
        /// Cargar <see cref="pboxImagenAA"/> con una imagen local.
        /// </summary>
        private void btnCargarImagenLocalAA_Click(object sender, EventArgs e)
        {
            lbAvisoImagenAA.Visible = false;
            pboxImagenAA.Image = Properties.Resources.placeholder;
            txbxCargarImagenAA.Visible = false;
            txbxCargarImagenAA.Text = "";
            btnCargarImagenUrlAA.Text = "IMAGEN URL";

            ofdImagen.Filter = "Archivos de imagen|*.jpg;*.png";
            if (ofdImagen.ShowDialog() == DialogResult.OK)
            {
                imagenLocal = true;
                txbxCargarImagenAA.Text = ofdImagen.FileName;
            }
        }


        /// <summary>
        /// Habilitar <see cref="txbxCargarImagenAA"/> para escribir una URL.
        /// </summary>
        private void btnCargarImagenUrlAA_Click(object sender, EventArgs e)
        {
            if (!txbxCargarImagenAA.Visible)
            {
                lbAvisoImagenAA.Visible = false;
                pboxImagenAA.Image = Properties.Resources.placeholder;
                txbxCargarImagenAA.Text = "https://...";
                txbxCargarImagenAA.Visible = true;
                btnCargarImagenUrlAA.Text = "BORRAR URL";
            }
            else
            {
                pboxImagenAA.Image = Properties.Resources.placeholder;
                txbxCargarImagenAA.Text = "";
                txbxCargarImagenAA.Focus();
            }
        }


        /// <summary>
        /// Cargar <see cref="pboxImagenAA"/> con el <see cref="txbxCargarImagenAA"/> si el
        /// texto de este último es diferente a los dos posibles valores por defecto (vacío o
        /// "https://...")
        /// </summary>
        private void txbxCargarImagenAA_TextChanged(object sender, EventArgs e)
        {
            if (txbxCargarImagenAA.Text != "" && txbxCargarImagenAA.Text != "https://...")
            {
                CargarImagenAA(txbxCargarImagenAA.Text);
            }
            else
            {
                lbAvisoImagenAA.Visible = false;
            }
            HabilitarBtnReiniciarAA();
        }


        /// <summary>
        /// Borrar el valor por defecto "https://..." cuando se hace focus en <see cref="txbxCargarImagenAA"/>
        /// </summary>
        private void txbxCargarImagenAA_Enter(object sender, EventArgs e)
        {
            if (txbxCargarImagenAA.Text == "https://...")
            {
                txbxCargarImagenAA.Text = "";
            }
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarAA"/> y <see cref="btnAgregarArticulo"/>
        /// </summary>
        private void txbxCodigoAA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarAA();
            HabilitarBtnAgregar();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarAA"/> y <see cref="btnAgregarArticulo"/>
        /// </summary>
        private void txbxNombreAA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarAA();
            HabilitarBtnAgregar();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarAA"/>
        /// </summary>
        private void txbxDescripcionAA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarAA();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarAA"/> y <see cref="btnAgregarArticulo"/>
        /// </summary>
        private void comboxMarcaAA_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarAA();
            HabilitarBtnAgregar();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarAA"/> y <see cref="btnAgregarArticulo"/>
        /// </summary>
        private void comboxCategoriaAA_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarAA();
            HabilitarBtnAgregar();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarAA"/> y <see cref="btnAgregarArticulo"/>
        /// </summary>
        private void txbxPrecioAA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarAA();
            HabilitarBtnAgregar();
        }


        /// <summary>
        /// Permitir solo escribir números, comas y puntos en el <see cref="txbxPrecioAA"/>
        /// </summary>
        private void txbxPrecioAA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 &&  
                 e.KeyChar != 44 && e.KeyChar != 46)
            {
                  e.Handled = true;
            }
        }


        // ================================================================== //
        // ------------------------- Borrar artículo ------------------------ //
        // ================================================================== //


        /// <summary>
        /// Eliminar el artículo seleccionado en el DGV.
        /// </summary>
        private void btnEliminarArticulo_Click(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado = ArticuloSeleccionado();


            DialogResult r = MessageBox.Show("¿Desea eliminar el siguiente artículo?\n" +
                                             $"{articuloSeleccionado}", "Aviso: eliminar artículo",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                EliminarArticulo(articuloSeleccionado);
            }
        }


        // ================================================================== //
        // ------------------------- Modificar artículo --------------------- //
        // ================================================================== //


        /// <summary>
        /// Modificar un artículo seleccionado en el DGV.
        /// </summary>
        private void btnModificarArticulo_Click(object sender, EventArgs e)
        {
            // Verificar si hay una modificacion pendiente y, si la hay, preguntar al usuario
            // si quiere descartar los cambios y modificar el nuevo artículo seleccionado.
            if (modificacionPendiente) 
            {
                DialogResult r = MessageBox.Show("Hay una modificación pendiente, ¿quiere descartarla?", 
                                                 "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (r == DialogResult.Yes)
                {
                    ReiniciarPlantillaModificacion();
                    btnModificacionPendiente.Visible = false;
                    MessageBox.Show("Cambios descartados.", "Aviso", MessageBoxButtons.OK, 
                                    MessageBoxIcon.Information);
                    PrepararModificacion();
                    panelModificarArticulo.Visible = true;
                    modificacionPendiente = true;
                }
            }
            else
            {
                PrepararModificacion();
                panelModificarArticulo.Visible = true;
                modificacionPendiente = true;
            }
        }


        /// <summary>
        /// Confirmar la modificación del artículo seleccionado, la cual se verá reflejada 
        /// en la base de datos, y mostrar la nueva información en la plantilla "Detalles artículos".
        /// </summary>
        private void btnConfirmarModificacion_Click(object sender, EventArgs e)
        {
            // Crear el artículo con las modificaciones.
            Articulo articuloModificado = new Articulo();
            articuloModificado.ID = idArticuloModificacionPendiente;
            articuloModificado.Codigo = txbxCodigoMA.Text;
            articuloModificado.Nombre = txbxNombreMA.Text;
            articuloModificado.Descripcion = txbxDescripcionMA.Text;
            articuloModificado.Marca = (Marca) comboxMarcaMA.SelectedItem;
            articuloModificado.Categoria = (Categoria) comboxCategoriaMA.SelectedItem;

            if (decimal.TryParse(txbxPrecioMA.Text, out decimal d))
            {
                articuloModificado.Precio = d;
            }
            else
            {
                MessageBox.Show("El valor del precio introducido no es válido. " +
                                "Verifique que la información sea correcta.",
                                "Error de conversión",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!CargarImagenAA(txbxImagenMA.Text))
            {
                articuloModificado.Imagen = "";
            }
            else
            {
                articuloModificado.Imagen = txbxImagenMA.Text;
            }

            // Realizar la modificacion en la base de datos.
            ModificarArticulo(articuloModificado);
            modificacionPendiente = false;

            // Mostrar la modificación al usuario.
            ActualizarDGV();
            try
            {
                FocusearElementoModificado();
                dgvArticulos_SelectionChanged(sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo focusear el elemento modificado.", "Error");
            }

            ReiniciarPlantillaModificacion();
        }


        /// <summary>
        /// Cancelar la modificación del artículo seleccionado.
        /// </summary>
        private void btnCancelarModificacion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cambios descartados.", "Aviso", MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;
            panelModificarArticulo.Visible = false;
            ReiniciarPlantillaModificacion();
        }


        /// <summary>
        /// Reiniciar la plantilla "Modificar artículo" a su configuración básica.
        /// </summary>
        private void btnReiniciarMA_Click(object sender, EventArgs e)
        {
            ReiniciarPlantillaModificacion();
        }


        /// <summary>
        /// Volver a la plantilla "Modificar artículo" con la información del artículo
        /// previamente seleccionado.
        /// </summary>
        private void btnModificacionPendiente_Click(object sender, EventArgs e)
        {
            btnModificacionPendiente.Visible = false;

            if (panelAgregarArticulo.Visible)
            {
                panelAgregarArticulo.Visible = false;
            }

            // Habilitar el botón Nuevo Artículo
            btnNuevoArticulo.Visible = true;

            panelModificarArticulo.Visible = true;
        }


        /// <summary>
        /// Cargar <see cref="pboxImagenMA"/> con el <see cref="txbxImagenMA"/> si el texto 
        /// de este último es diferente a los dos posibles valores por defecto (vacío o "https://...")
        /// </summary>
        private void txbxImagenMA_TextChanged(object sender, EventArgs e)
        {
            if (txbxImagenMA.Text != "" && txbxImagenMA.Text != "https://...")
            {
                CargarImagenMA(txbxImagenMA.Text);
            }
            else
            {
                lbAvisoImagenMA.Visible = false;
            }
            HabilitarBtnReiniciarMA();
        }


        /// <summary>
        /// Cargar <see cref="pboxImagenMA"/> con una imagen local.
        /// </summary>
        private void btnImagenLocalMA_Click(object sender, EventArgs e)
        {
            lbAvisoImagenMA.Visible = false;
            pboxImagenMA.Image = Properties.Resources.placeholder;
            txbxImagenMA.Visible = false;
            txbxImagenMA.Text = "";
            btnImagenUrlMA.Text = "IMAGEN URL";

            ofdImagen.Filter = "Archivos de imagen|*.jpg;*.png";
            if (ofdImagen.ShowDialog() == DialogResult.OK)
            {
                imagenLocal = true;
                txbxImagenMA.Text = ofdImagen.FileName;
            }
        }


        /// <summary>
        /// Habilitar <see cref="txbxImagenMA"/> para escribir una URL.
        /// </summary>
        private void btnImagenUrlMA_Click(object sender, EventArgs e)
        {
            if (!txbxImagenMA.Visible)
            {
                lbAvisoImagenMA.Visible = false;
                pboxImagenMA.Image = Properties.Resources.placeholder;
                txbxImagenMA.Text = "https://...";
                txbxImagenMA.Visible = true;
                btnImagenUrlMA.Text = "BORRAR URL";
            }
            else
            {
                pboxImagenMA.Image = Properties.Resources.placeholder;
                txbxImagenMA.Text = "";
                txbxImagenMA.Focus();
            }
        }


        /// <summary>
        /// Borrar el valor por defecto "https://..." cuando se hace focus en <see cref="txbxImagenMA"/>
        /// </summary>
        private void txbxImagenMA_Enter(object sender, EventArgs e)
        {
            if (txbxImagenMA.Text == "https://...")
            {
                txbxImagenMA.Text = "";
            }
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarMA"/> y <see cref="btnConfirmarModificacion"/>
        /// </summary>
        private void txbxCodigoMA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarMA();
            HabilitarBtnConfirmarModificacion();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarMA"/> y <see cref="btnConfirmarModificacion"/>
        /// </summary>
        private void txbxNombreMA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarMA();
            HabilitarBtnConfirmarModificacion();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarMA"/>
        /// </summary>
        private void txbxDescripcionMA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarMA();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarMA"/> y <see cref="btnConfirmarModificacion"/>
        /// </summary>
        private void comboxMarcaMA_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarMA();
            HabilitarBtnConfirmarModificacion();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarMA"/> y <see cref="btnConfirmarModificacion"/>
        /// </summary>
        private void comboxCategoriaMA_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarMA();
            HabilitarBtnConfirmarModificacion();
        }


        /// <summary>
        /// Validación <see cref="btnReiniciarMA"/> y <see cref="btnConfirmarModificacion"/>
        /// </summary>
        private void txbxPrecioMA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciarMA();
            HabilitarBtnConfirmarModificacion();
        }


        /// <summary>
        /// Permitir solo escribir números, comas y puntos en <see cref="txbxPrecioMA"/>
        /// </summary>
        private void txbxPrecioMA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 &&  
                 e.KeyChar != 44 && e.KeyChar != 46)
            {
                  e.Handled = true;
            }
        }


        // ================================================================== //
        // ----------------------- Búsqueda por criterios ------------------- //
        // ================================================================== //


        /// <summary>
        /// Habilitar la búsqueda predeterminada por nombre o la búsqueda avanzada por campo
        /// y criterio.
        /// </summary>
        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            txbxBuscar.Text = "";

            if (panelFiltroAvanzado.Visible)
            {
                panelFiltroAvanzado.Visible = false;
                lbBusqueda.Text = "Búsqueda básica (?)";
                btnFiltroAvanzado.Text = "AVANZADA";
            }    
            else if (!panelFiltroAvanzado.Visible)
            {
                comboxCampoBusqueda.SelectedIndex = 0;
                panelFiltroAvanzado.Visible = true;
                lbBusqueda.Text = "Búsqueda avanzada (?)";
                btnFiltroAvanzado.Text = "BÁSICA";
            }
            ActualizarDGV();
        }


        /// <summary>
        /// Realizar la búsqueda.
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (panelFiltroAvanzado.Visible)
            {
                BusquedaAvanzada();
            }
            else
            {
                BusquedaBasica();
            }
        }


        /// <summary>
        /// Evento que solo ocurre cuando está establecido la búsqueda predeterminada.
        /// </summary>
        private void txbxBuscar_TextChanged(object sender, EventArgs e)
        {
            if (!panelFiltroAvanzado.Visible)
            {
                btnBuscar_Click(sender, e);
            }
        }


        /// <summary>
        /// Cargar <see cref="comboxCriterioBusqueda"/> según el campo que el usuario seleccione.
        /// </summary>
        private void comboxCampoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = comboxCampoBusqueda.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                comboxCriterioBusqueda.Items.Clear();
                comboxCriterioBusqueda.Items.Add("Mayor a...");
                comboxCriterioBusqueda.Items.Add("Menor a...");
                comboxCriterioBusqueda.Items.Add("Igual a...");
                comboxCriterioBusqueda.SelectedIndex = 0;
            }
            else
            {
                comboxCriterioBusqueda.Items.Clear();
                comboxCriterioBusqueda.Items.Add("Comience con...");
                comboxCriterioBusqueda.Items.Add("Termine con...");
                comboxCriterioBusqueda.Items.Add("Contenga...");
                comboxCriterioBusqueda.SelectedIndex = 0;
            }

            // Se reinicia el TextBox Buscar para evitar posibles errores al seleccionar
            // campos con criterios diferentes.
            txbxBuscar.Text = "";
        }


        /// <summary>
        /// Permitir solo escribir números, comas y puntos cuando la búsqueda avanzada esté
        /// activada y el campo Precio esté seleccionado.
        /// </summary>
        private void txbxBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (panelFiltroAvanzado.Visible && comboxCampoBusqueda.SelectedItem.ToString() == "Precio")
            {
                if ( (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 &&  
                     e.KeyChar != 44 && e.KeyChar != 46)
                {
                      e.Handled = true;
                }
            }
        }


        /// <summary>
        /// Lanzar el evento <see cref="btnBuscar_Click(object, EventArgs)"/> con la tecla
        /// "Enter" si la búsqueda avanzada está activada.
        /// </summary>
        private void txbxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (panelFiltroAvanzado.Visible)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnBuscar_Click(sender, e);
                }
            }
        }


        // ================================================================== //
        // ------------------------------- Otros ---------------------------- //
        // ================================================================== //

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
