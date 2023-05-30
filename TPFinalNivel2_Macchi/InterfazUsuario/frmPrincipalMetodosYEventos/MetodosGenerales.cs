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
        // ---------------------------- Generales --------------------------- //
        // ================================================================== //

        /// <summary>
        /// Configuración básica de la aplicación.
        /// </summary>
        private void StartUp()
        {
            this.Icon = Properties.Resources.Catalogo;

            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;

            panelFiltroAvanzado.Visible = false;

            panelModificarArticulo.Visible = false;
            panelAgregarArticulo.Visible = false;

            CargarComboBoxes();

            toolTip.SetToolTip(lbBusqueda, "Búsqueda básica: filtro según el nombre del artículo.\n" +
                                           "Búsqueda avanzada: filtro según campo y criterio.");
        }


        /// <summary>
        /// Configuración que ocurre cuando no hay ningún registro en la lista "artículos",
        /// es decir, no hay ningún artículo registrado en la base de datos. Principalmente,
        /// este método obliga a agregar un artículo nuevo para poder seguir utilizando la
        /// aplicación.
        /// </summary>
        private void NingunRegistro()
        {
            dgvArticulos.Visible = false;
            ReiniciarPlantillaNuevoArticulo();
            panelModificarArticulo.Visible = true;
            panelAgregarArticulo.Visible = true;
            btnCancelarAgregacion.Enabled = false;
            btnNuevoArticulo.Visible = false;
            btnBuscar.Enabled = false;
        }


        /// <summary>
        /// Mostrar en la plantilla "Detalles artículos" la información del artículo 
        /// seleccionado en DGV.
        /// </summary>
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


        /// <summary>
        /// Ocultar columnas que no son importantes de mostrar en el DGV.
        /// </summary>
        private void OcultarColumnas()
        {
            dgvArticulos.Columns["ID"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["Imagen"].Visible = false;
        }


        /// <summary>
        /// Estilizar el formato del DGV. Si este tiene registros, además de ocultar columnas,
        /// formatea cómo se ve la columna Precio, Codigo y Nombre, ajusta el tamaño de las filas
        /// y acopla los bordes de la grilla a los bordes del componente. En cambio, si no tiene
        /// registros, solo oculta y acopla columnas.
        /// </summary>
        private void FormatearDGV()
        {
            if (dgvArticulos.CurrentRow != null)
            {
                OcultarColumnas();
                // Formato moneda en la columna Precio.
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "C";

                AjustarSizeFilas();
                AcoplarColumnasDGV();
            }
            else
            {
                OcultarColumnas();
                AcoplarColumnasDGV();
            }
        }


        /// <summary>
        /// Estilizar el formato de un ComboBox cuando se hace focus en este para tener un 
        /// aspecto acorde al programa.
        /// </summary>
        /// <param name="combox">ComboBox a formatear</param>
        /// <param name="e">Evento DrawItemEventArgs</param>
        private void FormatearComboBox(ComboBox combox, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Color ColorSelector = Color.FromArgb(173, 149, 116);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(ColorSelector), e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(combox.BackColor), e.Bounds);
                }

                e.Graphics.DrawString(combox.Items[e.Index].ToString(), e.Font, Brushes.White, e.Bounds);

                e.DrawFocusRectangle();
            }
        }


        /// <summary>
        /// Cargar la imagen del PictureBox de la plantilla "Detalles artículos" con una
        /// ruta de archivo local o URL.
        /// </summary>
        /// <param name="img">Ruta de archivo local o URL</param>
        /// <exception cref="Exception">Si no se carga la imagen en el PictureBox con la
        /// ruta o URL dada, o si tampoco carga la imagen por defecto</exception>
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


        /// <summary>
        /// Cargar todos los ComboBoxes de la aplicación excepto los criterios de búsqueda,
        /// ya que se cargan luego dependiendo del campo seleccionado por el usuario.
        /// </summary>
        private void CargarComboBoxes()
        {
            // Marcas
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            // Plantilla "Agregar artículo"
            comboxMarcaAA.DataSource = marcaNegocio.CargarMarcas();
            comboxMarcaAA.DisplayMember = "Descripcion";
            comboxMarcaAA.ValueMember = "ID";
            // Plantilla "Modificar artículo"
            comboxMarcaMA.DataSource = marcaNegocio.CargarMarcas();
            comboxMarcaMA.DisplayMember = "Descripcion";
            comboxMarcaMA.ValueMember = "ID";

            // Categorías
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            // Plantilla "Agregar artículo"
            comboxCategoriaAA.DataSource = categoriaNegocio.CargarCategorias();
            comboxCategoriaAA.DisplayMember = "Descripcion";
            comboxCategoriaAA.ValueMember = "ID";
            // Plantilla "Modificar artículo"
            comboxCategoriaMA.DataSource = categoriaNegocio.CargarCategorias();
            comboxCategoriaMA.DisplayMember = "Descripcion";
            comboxCategoriaMA.ValueMember = "ID";

            // Campos de búsqueda avanzada
            comboxCampoBusqueda.Items.Add("Código");
            comboxCampoBusqueda.Items.Add("Nombre");
            comboxCampoBusqueda.Items.Add("Marca");
            comboxCampoBusqueda.Items.Add("Categoría");
            comboxCampoBusqueda.Items.Add("Precio");
        }


        /// <summary>
        /// Refrescar y formatear el DGV con todos los artículos que se lean de la base de
        /// datos. Si no hay ningún registro, se lanza <see cref="NingunRegistro"/>
        /// </summary>
        private void ActualizarDGV()
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
                ActualizarResultados();
            }
            else if (articulos != null && articulos.Count <= 0)
            {
                NingunRegistro();
            }
            else
            {
                NingunRegistro();
            }
        }


        /// <summary>
        /// Devolver el artículo seleccionado por el usuario en el DGV.
        /// </summary>
        /// <returns>Artículo asociado a la fila seleccionada en el DGV.</returns>
        /// <exception cref="NullReferenceException">Se lanza si no se puede devolver el
        /// artículo enlazado a la fila seleccionada.</exception>
        /// <exception cref="Exception">Se lanza si tampoco se puede devolver el primer
        /// artículo del DGV.</exception>
        private Articulo ArticuloSeleccionado()
        {
            Articulo artSeleccionado = new Articulo();
            try
            {
                return (Articulo) dgvArticulos.CurrentRow.DataBoundItem;
            }
            catch(NullReferenceException)
            {
                try
                {
                    return (Articulo) dgvArticulos.Rows[0].DataBoundItem;
                }
                catch(Exception)
                {
                    MessageBox.Show("Ningún artículo fue seleccionado", "Error", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return artSeleccionado;
                }
            }
        }


        /// <summary>
        /// Actualiza el recuento de registros (filas) que almacena el DGV y muestra dicha 
        /// cantidad en la interfaz de usuario.
        /// </summary>
        private void ActualizarResultados()
        {
            cantArticulos = dgvArticulos.RowCount;
            lbResultadosBusqueda.Text = $"Resultados: {cantArticulos}";
        }

        
        /// <summary>
        /// Método para aumentar el tamaño de las filas.
        /// </summary>
        private void AjustarSizeFilas()
        {
            for (int i = 0; i < dgvArticulos.Rows.Count; i++)
            {
                dgvArticulos.Rows[i].Height = 40;
            }

        }


        /// <summary>
        /// Método para acoplar las columnas al borde del DGV.
        /// </summary>
        private void AcoplarColumnasDGV()
        {
            foreach (DataGridViewColumn column in dgvArticulos.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

    }
}
