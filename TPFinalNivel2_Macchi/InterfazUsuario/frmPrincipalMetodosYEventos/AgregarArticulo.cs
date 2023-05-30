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
        /// Configuración básica de la plantilla "Nuevo artículo".
        /// </summary>
        private void ReiniciarPlantillaNuevoArticulo()
        {
            btnNuevoArticulo.Text = "Nuevo artículo";
            pboxImagenAA.Image = Properties.Resources.placeholder;

            btnCargarImagenUrlAA.Text = "IMAGEN URL";
            imagenLocal = false;
            lbAvisoImagenAA.Visible = false;
            txbxCargarImagenAA.Visible = false;

            txbxCargarImagenAA.Text = "";
            txbxCodigoAA.Text = "";
            txbxNombreAA.Text = "";
            txbxDescripcionAA.Text = "";
            comboxMarcaAA.SelectedIndex = -1;
            comboxCategoriaAA.SelectedIndex = -1;
            txbxPrecioAA.Text = "";

            nuevoArticuloPendiente = false;
            btnReiniciarAA.Visible = false;

            btnAgregarArticulo.Enabled = false;
            btnAgregarArticulo.BackColor = Color.FromArgb(85, 98, 115); 
            lbImpresindibleAA6.Visible = true;
            lbAvisoAgregarAA.Visible = true;
        }


        /// <summary>
        /// Agregar un nuevo artículo en la base de datos.
        /// </summary>
        /// <param name="nuevoArticulo">Artículo con la nueva información</param>
        /// <exception cref="Exception">Se lanza cuando no se pudo agregar el nuevo artículo
        /// en la base de datos.</exception>
        private void AgregarNuevoArticulo(Articulo nuevoArticulo)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                articuloNegocio.AgregarNuevoArticulo(nuevoArticulo);
                MessageBox.Show("¡Nuevo artículo agregado con éxito!", "Nuevo artículo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error: agregar nuevo artículo");
            }
        }


        /// <summary>
        /// Cargar la imagen del PictureBox de la plantilla "Agregar artículo" con una
        /// ruta de archivo local o URL.
        /// </summary>
        /// <param name="img">Ruta de archivo local o URL.</param>
        /// <returns>Booleano que representa si cargó o no la imagen.</returns>
        /// <exception cref="Exception">Si no se carga la imagen en el PictureBox con la
        /// ruta o URL dada, o si tampoco carga la imagen por defecto</exception>
        private bool CargarImagenAA(string img)
        {
            try
            {
                pboxImagenAA.Load(img);
                lbAvisoImagenAA.Visible = false;
                imagenLocal = false;
                return true;
            }
            catch (Exception)
            {
                try
                {
                    pboxImagenAA.Image = Properties.Resources.placeholder;
                    if (imagenLocal)
                    {
                        txbxCargarImagenAA.Text = "";
                        imagenLocal = false;
                    }
                    lbAvisoImagenAA.Visible = true;
                    return false;
                }
                catch(Exception)
                {
                    MessageBox.Show("Error al cargar el placeholder");
                    if (imagenLocal)
                    {
                        txbxCargarImagenAA.Text = "";
                        imagenLocal = false;
                    }
                    lbAvisoImagenAA.Visible = true;
                    return false;
                }
            }
        }


        /// <summary>
        /// Permitir al usuario apretar el botón <see cref="btnReiniciarAA"/> si se modificó
        /// alguno de los controles de la plantilla "Agregar artículo" con el fin de que esta
        /// vuelva a su forma básica.
        /// </summary>
        private void HabilitarBtnReiniciarAA()
        {
            if ((txbxCargarImagenAA.Text != "" && txbxCargarImagenAA.Text != "https://...") ||
                txbxCodigoAA.Text != "" || txbxNombreAA.Text != "" || txbxDescripcionAA.Text != "" ||
                comboxMarcaAA.SelectedIndex != -1 || comboxCategoriaAA.SelectedIndex != -1 ||
                txbxPrecioAA.Text != "")
            {
                btnNuevoArticulo.Text = "Nuevo artículo pendiente";
                btnReiniciarAA.Visible = true;
                nuevoArticuloPendiente = true;
            }
            else
            {
                btnNuevoArticulo.Text = "Nuevo artículo";
                btnReiniciarAA.Visible = false;
                nuevoArticuloPendiente = false;
            }
        }


        /// <summary>
        /// Permitir al usuario apretar el botón <see cref="btnAgregarArticulo"/> si se
        /// rellenan los campos impresindibles.
        /// </summary>
        private void HabilitarBtnAgregar()
        {
            if (txbxCodigoAA.Text != "" && txbxNombreAA.Text != "" && 
                comboxMarcaAA.SelectedIndex != -1 && comboxCategoriaAA.SelectedIndex != -1 &&
                txbxPrecioAA.Text != "")
            {
                btnAgregarArticulo.BackColor = Color.FromArgb(173, 149, 116);
                btnAgregarArticulo.Enabled = true;
                lbImpresindibleAA6.Visible = false;
                lbAvisoAgregarAA.Visible = false;
            }
            else
            {
                btnAgregarArticulo.BackColor = Color.FromArgb(85, 98, 115); 
                btnAgregarArticulo.Enabled = false;
                lbImpresindibleAA6.Visible = true;
                lbAvisoAgregarAA.Visible = true;
            }
        }


        /// <summary>
        /// Focusear el índice del nuevo artículo agregado en la base de datos para que su
        /// información se muestre en la plantilla "Detalles artículos".
        /// </summary>
        private void FocusearUltimoElemento()
        {
            int ultimoIndice = dgvArticulos.Rows.Count - 1;
            dgvArticulos.CurrentCell = dgvArticulos.Rows[ultimoIndice].Cells[1]; 

            if (dgvArticulos.Rows.Count > 0)
            {
                dgvArticulos.FirstDisplayedScrollingRowIndex = dgvArticulos.Rows.Count - 1;
                dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Selected = true;
            }
        }


        /// <summary>
        /// Focusear el índice del artículo modificado para mostrar su información en la
        /// plantilla "Detalles artículos".
        /// </summary>
        private void FocusearElementoModificado()
        {
            int indiceArticuloModificado = articulos.FindIndex(articulo => articulo.ID == idArticuloModificacionPendiente);

            if (indiceArticuloModificado >= 0)
            {
                dgvArticulos.CurrentCell = dgvArticulos.Rows[indiceArticuloModificado].Cells[1];
                dgvArticulos.Rows[indiceArticuloModificado].Selected = true;
            }
        }


        // ================================================================== //
        // ------------------------------ Eventos --------------------------- //
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
                lbAvisoImagenAA.Visible = false;
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

    }
}
