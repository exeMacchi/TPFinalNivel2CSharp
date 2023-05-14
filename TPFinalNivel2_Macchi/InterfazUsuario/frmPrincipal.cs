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
        private int indiceModificacionPendiente;
        
        // Constructor
        public frmPrincipal()
        {
            InitializeComponent();
            StartUp();
            ReiniciarPlantillaNuevoArticulo();
        }

        // --------------------- Métodos ------------------------ //
        private void StartUp()
        {
            nuevoArticuloPendiente = false;
            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;
            panelFiltroAvanzado.Visible = false;
            panelModificarArticulo.Visible = false;
            panelAgregarArticulo.Visible = false;
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
            CargarImagen(articuloSeleccionado.Imagen);
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
        private void CargarImagen(string img)
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

        private void ReiniciarPlantillaNuevoArticulo()
        {
            pboxImagenAA.Image = Properties.Resources.placeholder;
            txbxCargarImagenAA.Visible = false;
            txbxCargarImagenAA.Text = "";
            txbxCodigoAA.Text = "";
            txbxNombreAA.Text = "";
            txbxDescripcionAA.Text = "";
            comboxMarcaAA.SelectedIndex = -1;
            comboxCategoriaAA.SelectedIndex = -1;
            txbxPrecioAA.Text = "";
            btnReiniciarAA.Visible = false;
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
                FormatearDGV();
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
                    MostrarInfoArticulo();
                    panelAgregarArticulo.Visible = false;
                    panelModificarArticulo.Visible = false;
                }
                else if (panelModificarArticulo.Visible) // Verifica si la capa de modificación está abierta.
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
            // El botón "Nuevo artículo" se hace visible, sea el caso que sea.
            btnNuevoArticulo.Visible = true;
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
            btnNuevoArticulo.Visible = true;
            nuevoArticuloPendiente = false;
            ReiniciarPlantillaNuevoArticulo();
        }

        private void btnCancelarAgregacion_Click(object sender, EventArgs e)
        {
            // Aviso: articulo descartado
            // Focusear el indice del nuevo articulo para que el usuario vea la info.
                // Cuando se focuseé, esto ya no va a ser necesario.
                panelAgregarArticulo.Visible = false;
                panelModificarArticulo.Visible = false;
            btnNuevoArticulo.Visible = true;
            nuevoArticuloPendiente = false;
            ReiniciarPlantillaNuevoArticulo();
        }

        private void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            // Verificar si hay un nuevo artículo pendiente para saber si reiniciar o no 
            // la pantilla nuevo artículo.
            if (!nuevoArticuloPendiente)
            {
                ReiniciarPlantillaNuevoArticulo();
                nuevoArticuloPendiente = true;
            }
            // Si hay un artículo pendiente, y mínimo un dato en alguno de los campos,
            // se le permite al usuario que reinicie la plantilla si quiere ingresar otra información.
            else if (txbxCodigoAA.Text != "" || txbxNombreAA.Text != "" || 
                     txbxDescripcionAA.Text != "" || comboxMarcaAA.SelectedIndex != -1 || 
                     comboxCategoriaAA.SelectedIndex != -1 || txbxPrecioAA.Text != "")
            {
                btnReiniciarAA.Visible = true;
            }

            // El botón "Nuevo artículo" se hace invisible hasta que se agregue, cancele o
            // se cambie la selección de artículo.
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


        private void dgvArticulos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Verificar si hay mínimo un dato en el DGV para mostrarlo.
            if (!dgvArticulos.Visible)
            {
                dgvArticulos.Visible = true;
                FormatearDGV();
            }
        }

        private void btnReiniciarAA_Click(object sender, EventArgs e)
        {
            ReiniciarPlantillaNuevoArticulo();
        }
    }
}
