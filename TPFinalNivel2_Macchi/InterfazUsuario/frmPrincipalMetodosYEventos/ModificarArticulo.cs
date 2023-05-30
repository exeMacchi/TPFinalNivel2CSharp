using System;
using System.Drawing;
using System.Windows.Forms;
using Dominio;
using LogicaNegocio;

namespace InterfazUsuario
{
    public partial class frmPrincipal
    {
        // ================================================================== //
        // ------------------------------- Métodos -------------------------- //
        // ================================================================== //

        /// <summary>
        /// Preparar la plantilla "Modificar artículo" con la información del artículo
        /// seleccionado por el usuario en el DGV.
        /// </summary>
        private void PrepararModificacion()
        {
            Articulo artSeleccionado = ArticuloSeleccionado();
            idArticuloModificacionPendiente = artSeleccionado.ID;

            if (CargarImagenMA(artSeleccionado.Imagen))
            {
                if (artSeleccionado.Imagen.Contains("https://") || artSeleccionado.Imagen.Contains("http://"))
                {
                    txbxImagenMA.Visible = true;
                    btnImagenUrlMA.Text = "BORRAR URL";
                }
                else
                {
                    txbxImagenMA.Visible = false;
                    btnImagenUrlMA.Text = "IMAGEN URL";
                }
                txbxImagenMA.Text = artSeleccionado.Imagen;
            }
            txbxCodigoMA.Text = artSeleccionado.Codigo;
            txbxNombreMA.Text = artSeleccionado.Nombre;
            txbxDescripcionMA.Text = artSeleccionado.Descripcion;
            comboxMarcaMA.SelectedValue = artSeleccionado.Marca.ID;
            comboxCategoriaMA.SelectedValue = artSeleccionado.Categoria.ID;
            txbxPrecioMA.Text = artSeleccionado.Precio.ToString("N2");
        }


        /// <summary>
        /// Cargar la imagen del PictureBox de la plantilla "Modificar artículo" con una
        /// ruta de archivo local o URL.
        /// </summary>
        /// <param name="img">Ruta de archivo local o URL.</param>
        /// <returns>Booleano que representa si cargó o no la imagen.</returns>
        /// <exception cref="Exception">Si no se carga la imagen en el PictureBox con la
        /// ruta o URL dada, o si tampoco carga la imagen por defecto</exception>
        private bool CargarImagenMA(string img)
        {
            try
            {
                pboxImagenMA.Load(img);
                lbAvisoImagenMA.Visible = false;
                imagenLocal = false;
                return true;
            }
            catch (Exception)
            {
                try
                {
                    pboxImagenMA.Image = Properties.Resources.placeholder;
                    if (imagenLocal)
                    {
                        txbxImagenMA.Text = "";
                        imagenLocal = false;
                    }
                    lbAvisoImagenMA.Visible = true;
                    return false;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al cargar el placeholder");
                    if (imagenLocal)
                    {
                        txbxImagenMA.Text = "";
                        imagenLocal = false;
                    }
                    lbAvisoImagenMA.Visible = true;
                    return false;
                }
            }
        }

        
        /// <summary>
        /// Configuración básica de la plantilla "Modificar artículo".
        /// </summary>
        private void ReiniciarPlantillaModificacion()
        {
            pboxImagenMA.Image = Properties.Resources.placeholder;
            btnImagenUrlMA.Text = "IMAGEN URL";
            imagenLocal = false;
            lbAvisoImagenMA.Visible = false;
            txbxImagenMA.Visible = false;

            txbxImagenMA.Text = "";
            txbxCodigoMA.Text = "";
            txbxNombreMA.Text = "";
            txbxDescripcionMA.Text = "";
            comboxMarcaMA.SelectedIndex = -1;
            comboxCategoriaMA.SelectedIndex = -1;
            txbxPrecioMA.Text = "";

            btnReiniciarMA.Visible = false;
            btnConfirmarModificacion.Enabled = false;
            lbImpresindibleMA6.Visible = true;
            lbAvisoModificarMA.Visible = true;
        }


        /// <summary>
        /// Permitir al usuario apretar el botón <see cref="btnReiniciarMA"/> si se modificó
        /// alguno de los controles de la plantilla "Modificar artículo" con el fin de que esta
        /// vuelva a su forma básica.
        /// </summary>
        private void HabilitarBtnReiniciarMA()
        {
            if ((txbxImagenMA.Text != "" && txbxImagenMA.Text != "https://...") || 
                txbxCodigoMA.Text != "" || txbxNombreMA.Text != "" || txbxDescripcionMA.Text != "" ||
                comboxMarcaMA.SelectedIndex != -1 || comboxCategoriaMA.SelectedIndex != -1 ||
                txbxPrecioMA.Text != "")
            {
                btnReiniciarMA.Visible = true;
            }
            else
            {
                btnReiniciarMA.Visible = false;
            }
        }


        /// <summary>
        /// Permitir al usuario apretar el botón <see cref="btnConfirmarModificacion"/> si 
        /// se rellenan los campos impresindibles.
        /// </summary>
        private void HabilitarBtnConfirmarModificacion()
        {
            if (txbxCodigoMA.Text != "" && txbxNombreMA.Text != "" && 
                comboxMarcaMA.SelectedIndex != -1 && comboxCategoriaMA.SelectedIndex != -1 &&
                txbxPrecioMA.Text != "")
            {
                btnConfirmarModificacion.BackColor = Color.FromArgb(173, 149, 116);
                btnConfirmarModificacion.Enabled = true;
                lbImpresindibleMA6.Visible = false;
                lbAvisoModificarMA.Visible = false;
            }
            else
            {
                btnConfirmarModificacion.BackColor = Color.FromArgb(85, 98, 115);
                btnConfirmarModificacion.Enabled = false;
                lbImpresindibleMA6.Visible = true;
                lbAvisoModificarMA.Visible = true;
            }
        }


        /// <summary>
        /// Modificar en la base de datos un artículo seleccionado.
        /// </summary>
        /// <param name="articuloModificado">Artículo modificado en la plantilla "Modificar
        /// artículo".</param>
        private void ModificarArticulo(Articulo articuloModificado)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                articuloNegocio.ModificarArticulo(articuloModificado);
                MessageBox.Show("¡Artículo modificado con éxito!", "Nuevo artículo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "Error: modificar artículo");
            }
        }


        // ================================================================== //
        // ------------------------------ Eventos --------------------------- //
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
                lbAvisoImagenMA.Visible = false;
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

    }
}
