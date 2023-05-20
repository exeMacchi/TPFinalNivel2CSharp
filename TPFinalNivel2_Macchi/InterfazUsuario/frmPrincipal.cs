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
using LogicaNegocio;

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

        // ================================================================== //
        // ---------------------------- Métodos ----------------------------- //
        // ================================================================== //
        /// <summary>
        /// Configuración básica de la aplicación.
        /// </summary>
        private void StartUp()
        {
            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;

            panelFiltroAvanzado.Visible = false;

            panelModificarArticulo.Visible = false;
            panelAgregarArticulo.Visible = false;

            CargarComboBoxes();

            toolTip.SetToolTip(lbBusqueda, "Búsqueda predeterminada: filtro según el nombre del artículo.\n" +
                                           "Búsqueda avanzada: filtro según campos y criterios.");
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
        /// formatea cómo se ve la columna Precio, Codigo y Nombre; en cambio, si no tiene
        /// registros, solo oculta columnas.
        /// </summary>
        private void FormatearDGV()
        {
            if (dgvArticulos.CurrentRow != null)
            {
                OcultarColumnas();
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "N2";
                dgvArticulos.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvArticulos.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            else
            {
                OcultarColumnas();
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
            else if (articulos.Count <= 0)
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

        // ------------------------- Agregar artículo ----------------------- //
        /// <summary>
        /// Configuración básica de la plantilla "Nuevo artículo".
        /// </summary>
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

        // ------------------------- Borrar artículo ------------------------ //
        /// <summary>
        /// Eliminar de la base de datos un artículo seleccionado por el usuario.
        /// </summary>
        /// <param name="articuloSeleccionado">Artículo seleccionado en el DGV.</param>
        /// <exception cref="Exception">Se lanza si no se pudo borrar el artículo seleccionado.
        /// </exception>
        private void EliminarArticulo(Articulo articuloSeleccionado)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                articuloNegocio.BorrarArticulo(articuloSeleccionado);
                MessageBox.Show("¡Artículo eliminado con éxito!", "Artículo eliminado",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarDGV();
            }
            catch (Exception)
            {
                MessageBox.Show("El artículo seleccionado no se pudo borrar.", "Error: borrar artículo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------- Modificar artículo --------------------- //
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
                    btnImagenUrlMA.Text = "Borrar URL";
                }
                else
                {
                    txbxImagenMA.Visible = false;
                    btnImagenUrlMA.Text = "Imagen URL";
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
            btnImagenUrlMA.Text = "Imagen URL";
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
                btnConfirmarModificacion.Enabled = true;
                lbImpresindibleMA6.Visible = false;
                lbAvisoModificarMA.Visible = false;
            }
            else
            {
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

        // ----------------------- Búsqueda de artículos -------------------- //
        /// <summary>
        /// Búsqueda de artículos en la lista <see cref="articulos"/> según un filtro 
        /// basado en el nombre de estos.
        /// </summary>
        private void BusquedaPredeterminada()
        {
            string filtro = txbxBuscar.Text;

            List<Articulo> articulosFiltrados = articulos.FindAll(art => art.Nombre.ToUpper().Contains(filtro.ToUpper()));

            if (articulosFiltrados != null)
            {
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = articulosFiltrados;
                FormatearDGV();
                ActualizarResultados();
            }
        }

        /// <summary>
        /// Búsqueda de artículos en la base de datos según un campo, un criterio y un filtro.
        /// </summary>
        private void BusquedaAvanzada()
        {
            string campo = comboxCampoBusqueda.SelectedItem.ToString();
            string criterio = comboxCriterioBusqueda.SelectedItem.ToString();
            string filtro;
            if (campo == "Precio")
            {
                if (decimal.TryParse(txbxBuscar.Text, out decimal d))
                {
                    filtro = txbxBuscar.Text;
                }
                else
                {
                    MessageBox.Show("El valor del precio introducido no es válido. " +
                                    "Verifique que la información sea correcta.",
                                    "Error de conversión",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                filtro = txbxBuscar.Text;
            }

            string condicion = CrearClausulaWhere(campo, criterio, filtro);

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> articulosFiltrados = articuloNegocio.Buscar(condicion);

            if (articulosFiltrados != null)
            {

                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = articulosFiltrados;
                FormatearDGV();
                ActualizarResultados();
            }
        }

        /// <summary>
        /// Crear la cláusula WHERE que se utilizará como condición en la consulta SQL.
        /// </summary>
        /// <param name="campo">Campo seleccionado en el <see cref="comboxCampoBusqueda"/>.</param>
        /// <param name="criterio">Criterio seleccionado en el <see cref="comboxCriterioBusqueda"/>.</param>
        /// <param name="filtro">Filtro escrito en el <see cref="txbxBuscar"/>.</param>
        /// <returns>Cláusula WHERE completa.</returns>
        private string CrearClausulaWhere(string campo, string criterio, string filtro)
        {
            string condicion = null;
            switch(campo)
            {
                case "Código":
                    condicion = CondicionCampoTexto("A.Codigo", criterio, filtro);
                    break;

                case "Nombre":
                    condicion = CondicionCampoTexto("A.Nombre", criterio, filtro);
                    break;

                case "Marca":
                    condicion = CondicionCampoTexto("M.Descripcion", criterio, filtro);
                    break;

                case "Categoría":
                    condicion = CondicionCampoTexto("C.Descripcion", criterio, filtro);
                    break;

                case "Precio":
                    condicion = CondicionCampoNumero("A.Precio", criterio, filtro);
                    break;
            }

            return $"WHERE {condicion};";
        }

        /// <summary>
        /// Devolver la condición después del WHERE con el operador de comparación textual (LIKE).
        /// </summary>
        /// <param name="campo">Campo correspondiente en la base de datos.</param>
        /// <param name="criterio">Criterio seleccionado en el <see cref="comboxCriterioBusqueda"/>.</param>
        /// <param name="filtro">Filtro escrito en el <see cref="txbxBuscar"/>.</param>
        /// <returns></returns>
        private string CondicionCampoTexto(string campo, string criterio, string filtro)
        {
            switch (criterio)
            {
                case "Comience con...":
                    return $"{campo} LIKE '{filtro}%'";

                case "Termine con...":
                    return $"{campo} LIKE '%{filtro}'";

                default:
                    return $"{campo} LIKE '%{filtro}%'";
            }
        }

        /// <summary>
        /// Devolver la condición después del WHERE con el operador de comparación numérico.
        /// </summary>
        /// <param name="campo">Campo correspondiente en la base de datos.</param>
        /// <param name="criterio">Criterio seleccioando en el <see cref="comboxCriterioBusqueda"/>.</param>
        /// <param name="filtro">Filtro escrito en el <see cref="txbxBuscar"/>.</param>
        /// <returns></returns>
        private string CondicionCampoNumero(string campo, string criterio, string filtro)
        {
            switch (criterio)
            {
                case "Mayor a...":
                    return $"{campo} > {filtro}";

                case "Menor a...":
                    return $"{campo} < {filtro}";

                default:
                    return $"{campo} = {filtro}";
            }
        }

        // ================================================================== //
        // ----------------------------- Eventos ---------------------------- //
        // ================================================================== //

        /// <summary>
        /// Cargar al iniciar la aplicación los artículos al DGV.
        /// </summary>
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ActualizarDGV();
        }


        // ------------------------- Detalles artículo ----------------------- //
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


        // ------------------------- Agregar artículo ----------------------- //
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
            btnCargarImagenUrlAA.Text = "Imagen URL";

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
                btnCargarImagenUrlAA.Text = "Borrar URL";
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


        // ------------------------- Borrar artículo ------------------------ //
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



        // ------------------------- Modificar artículo ----------------------- //
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
            btnImagenUrlMA.Text = "Imagen URL";

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
                btnImagenUrlMA.Text = "Borrar URL";
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


        // ----------------------- Búsqueda por criterios ------------------- //
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
                lbBusqueda.Text = "Búsqueda predeterminada";
            }    
            else if (!panelFiltroAvanzado.Visible)
            {
                comboxCampoBusqueda.SelectedIndex = 0;
                panelFiltroAvanzado.Visible = true;
                lbBusqueda.Text = "Búsqueda avanzada";
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
                BusquedaPredeterminada();
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


        // ------------------------------------------------------------------ //
        /// <summary>
        /// Verificar antes de que se cierre la aplicación si hay una modificación pendiente
        /// y/o información de un nuevo artículo no agregado. Si lo hubiera, se le pregunta
        /// al usuario si quiere descartar dicha modificación y/o agregación.
        /// </summary>
        private void btnCerrarFormulario_Click(object sender, EventArgs e)
        {
            if (modificacionPendiente)
            {
                DialogResult r = MessageBox.Show("Hay una modificación pendiente, ¿quiere descartarla?", 
                                                 "Modificación pendiente", MessageBoxButtons.YesNo, 
                                                 MessageBoxIcon.Warning);

                if (r == DialogResult.No)
                {
                    return;
                }
            }

            if (nuevoArticuloPendiente)
            {
                DialogResult r = MessageBox.Show("Hay información de un nuevo artículo no agregado, " +
                                                 "¿quiere descartarla?", "Nuevo artículo pendiente",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (r == DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }
    }
}
