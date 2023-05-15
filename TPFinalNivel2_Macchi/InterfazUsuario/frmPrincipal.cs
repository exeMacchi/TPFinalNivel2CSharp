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
        private bool nuevoArticuloPendiente;
        private bool modificacionPendiente;
        private bool imagenLocal;
        private int indiceModificacionPendiente;
        
        // Constructor
        public frmPrincipal()
        {
            InitializeComponent();
            StartUp();
            ReiniciarPlantillaNuevoArticulo();
        }

        // ====================================================== //
        // --------------------- Métodos ------------------------ //
        // ====================================================== //

        private void StartUp()
        {
            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;

            panelFiltroAvanzado.Visible = false;

            panelModificarArticulo.Visible = false;
            panelAgregarArticulo.Visible = false;

            CargarComboBoxes();
        }
        private void NingunRegistro()
        {
            dgvArticulos.Visible = false;
            ReiniciarPlantillaNuevoArticulo();
            panelModificarArticulo.Visible = true;
            panelAgregarArticulo.Visible = true;
            btnCancelarAgregacion.Enabled = false;
            btnNuevoArticulo.Visible = false;
        }
        private void MostrarInfoArticulo()
        {
            Articulo articuloSeleccionado = (Articulo) dgvArticulos.CurrentRow.DataBoundItem;
            txbxCodigoDA.Text = articuloSeleccionado.Codigo;
            txbxNombreDA.Text = articuloSeleccionado.Nombre;
            txbxDescripcionDA.Text = articuloSeleccionado.Descripcion;
            txbxMarcaDA.Text = articuloSeleccionado.Marca.Descripcion;
            txbxCategoriaDA.Text = articuloSeleccionado.Categoria.Descripcion;
            CargarImagenDA(articuloSeleccionado.Imagen);
            txbxPrecioDA.Text = "$" + articuloSeleccionado.Precio.ToString("N2");
        }
        private void OcultarColumnas()
        {
            dgvArticulos.Columns["ID"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["Imagen"].Visible = false;
        }
        private void FormatearDGV()
        {
            OcultarColumnas();
            dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "N2";
            dgvArticulos.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvArticulos.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void CargarImagenDA(string img)
        {
            try
            {
                pboxImagenDA.Load(img);
            }
            catch (Exception)
            {
                try
                {
                    pboxImagenDA.Image = Properties.Resources.placeholder;
                }
                catch(Exception)
                {
                    MessageBox.Show("Error al cargar el placeholder");
                }
            }
        }

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
                    return false;
                }
            }
        }

        private void CargarComboBoxes()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            comboxMarcaAA.DataSource = marcaNegocio.CargarMarcas();
            // ComboBox marca modificacion

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            comboxCategoriaAA.DataSource = categoriaNegocio.CargarCategorias();
            // ComboBox categoria modificacion
        }

        private void ReiniciarPlantillaNuevoArticulo()
        {
            btnNuevoArticulo.Text = "Nuevo artículo";
            pboxImagenAA.Image = Properties.Resources.placeholder;

            btnCargarImagenUrlAA.Text = "Imagen URL";
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
            lbImpresindibleAA6.Visible = true;
            lbAvisoAgregarAA.Visible = true;
        }

        private void HabilitarBtnReiniciar()
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

        private void HabilitarBtnAgregar()
        {
            if (txbxCodigoAA.Text != "" && txbxNombreAA.Text != "" && 
                comboxMarcaAA.SelectedIndex != -1 && comboxCategoriaAA.SelectedIndex != -1 &&
                txbxPrecioAA.Text != "")
            {
                btnAgregarArticulo.Enabled = true;
                lbImpresindibleAA6.Visible = false;
                lbAvisoAgregarAA.Visible = false;
            }
            else
            {
                btnAgregarArticulo.Enabled = false;
                lbImpresindibleAA6.Visible = true;
                lbAvisoAgregarAA.Visible = true;
            }
        }

        // ====================================================== //
        // ----------------------- Eventos ---------------------- //
        // ====================================================== //
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
                FormatearDGV();
            }
            else
            {
                NingunRegistro();
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            // El botón "Nuevo artículo" o "Nuevo artículo pendiete" se hace visible,
            // sea el caso que sea.
            btnNuevoArticulo.Visible = true;

            // Verificar si hay articulos disponibles. Si no los hay, activar
            // la pestaña de Agregar artículo.
            if (dgvArticulos.CurrentRow != null)
            {
                // Verifica si la capa de articulos está abierta; si lo está, la oculta.
                if (panelAgregarArticulo.Visible) 
                {
                    MostrarInfoArticulo();
                    panelAgregarArticulo.Visible = false;
                    panelModificarArticulo.Visible = false;
                }
                // Verifica si la capa de modificación está abierta; si lo está, la oculta.
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
            else
            {
                NingunRegistro();
            }
        }

        private void dgvArticulos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Verificar si hay mínimo un dato en el DGV para mostrarlo.
            if (!dgvArticulos.Visible)
            {
                dgvArticulos.Visible = true;
                FormatearDGV();
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
            // Aviso: cambios descartados
            modificacionPendiente = false;
            // Focusear articulo modificado
            btnModificacionPendiente.Visible = false;
            panelModificarArticulo.Visible = false;
        }
        private void btnModificacionPendiente_Click(object sender, EventArgs e)
        {
            btnModificacionPendiente.Visible = false;

            // Volver a la planilla modificar artículo con la información del artículo
            // previamente seleccionado.
            if (panelAgregarArticulo.Visible)
            {
                panelAgregarArticulo.Visible = false;
            }

            // Habilitar el botón Nuevo Artículo
            btnNuevoArticulo.Visible = true;

            panelModificarArticulo.Visible = true;
        }


        // ----------------------- Agregar artículo ---------------------- //
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

            // Verificar si se apretó el botón desde la plantilla de modificación de un
            // artículo para habilitar el botón de "Modificación pendiente".
            if (modificacionPendiente)
            {
                btnModificacionPendiente.Visible = true;
            }

            // Hacer visible la plantilla nuevo artículo.
            panelModificarArticulo.Visible = true;
            panelAgregarArticulo.Visible = true;
        }

        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            // Se habilita el botón de cancelar porque hay mínimo un registro en la DB.
            if (!btnCancelarAgregacion.Enabled)
                btnCancelarAgregacion.Enabled = true;

            // Verificar si la ruta/url es válida antes de subirla a la DB.
            if (!CargarImagenAA(txbxCargarImagenAA.Text))
            {
                txbxCargarImagenAA.Text = "";
            }

            // Agregar
            // Aviso: articulo agregado con exito!
            // Focusear el indice del nuevo articulo para que el usuario vea la info.
                // Cuando se focuseé, esto ya no va a ser necesario.
                panelAgregarArticulo.Visible = false;
                panelModificarArticulo.Visible = false;
            btnNuevoArticulo.Visible = true;
            ReiniciarPlantillaNuevoArticulo();
        }

        private void btnCancelarAgregacion_Click(object sender, EventArgs e)
        {
            // Aviso: articulo descartado
            panelAgregarArticulo.Visible = false;
            panelModificarArticulo.Visible = false;
            btnNuevoArticulo.Visible = true;
            ReiniciarPlantillaNuevoArticulo();
        }

        private void btnReiniciarAA_Click(object sender, EventArgs e)
        {
            ReiniciarPlantillaNuevoArticulo();
        }

        private void btnCargarImagenLocalAA_Click(object sender, EventArgs e)
        {
            lbAvisoImagenAA.Visible = false;
            pboxImagenAA.Image = Properties.Resources.placeholder;
            txbxCargarImagenAA.Text = "";
            txbxCargarImagenAA.Visible = false;
            btnCargarImagenUrlAA.Text = "Imagen URL";

            ofdImagen.Filter = "Archivos de imagen|*.jpg;*.png";
            if (ofdImagen.ShowDialog() == DialogResult.OK)
            {
                imagenLocal = true;
                txbxCargarImagenAA.Text = ofdImagen.FileName;
            }
        }

        private void btnCargarImagenUrlAA_Click(object sender, EventArgs e)
        {
            if (!txbxCargarImagenAA.Visible)
            {
                lbAvisoImagenAA.Visible = false;
                pboxImagenAA.Image = Properties.Resources.placeholder;
                txbxCargarImagenAA.Text = "https://...";
                txbxCargarImagenAA.Visible = true;
                btnCargarImagenUrlAA.Text = "Borrar URL";
            }
            else
            {
                txbxCargarImagenAA.Text = "";
                txbxCargarImagenAA.Focus();
            }
        }

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
            HabilitarBtnReiniciar();
        }

        private void txbxCargarImagenAA_Enter(object sender, EventArgs e)
        {
            if (txbxCargarImagenAA.Text == "https://...")
            {
                txbxCargarImagenAA.Text = "";
            }
        }

        private void txbxCodigoAA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciar();
            HabilitarBtnAgregar();
        }

        private void txbxNombreAA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciar();
            HabilitarBtnAgregar();
        }

        private void txbxDescripcionAA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciar();
        }

        private void comboxMarcaAA_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciar();
            HabilitarBtnAgregar();
        }

        private void comboxCategoriaAA_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciar();
            HabilitarBtnAgregar();
        }

        private void txbxPrecioAA_TextChanged(object sender, EventArgs e)
        {
            HabilitarBtnReiniciar();
            HabilitarBtnAgregar();
        }

        private void txbxPrecioAA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                  e.Handled = true;
            }
        }

        // --------------------------------------------------------------- //
        private void btnCerrarFormulario_Click(object sender, EventArgs e)
        {
            if (modificacionPendiente)
            {
                // Preguntar si quiere descartar los cambios
            }
            if (nuevoArticuloPendiente)
            {
                if (nuevoArticuloPendiente)
                {
                    DialogResult r = MessageBox.Show("Hay información de un nuevo artículo no agregado, " +
                                                     "¿desea descartarla?", "Nuevo artículo pendiente",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (r == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            this.Close();
        }

    }
}
