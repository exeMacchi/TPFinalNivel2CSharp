using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            modificacionPendiente = false;
            btnModificacionPendiente.Visible = false;

            panelFiltroAvanzado.Visible = false;

            panelModificarArticulo.Visible = false;
            panelAgregarArticulo.Visible = false;

            CargarComboBoxes();

            toolTip.SetToolTip(lbBusqueda, "Búsqueda predeterminada: filtro según el nombre del artículo.\n" +
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


        // ================================================================== //
        // ------------------------- Agregar artículo ----------------------- //
        // ================================================================== //


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


        // ================================================================== //
        // ------------------------- Borrar artículo ------------------------ //
        // ================================================================== //


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


        // ================================================================== //
        // ------------------------ Modificar artículo ---------------------- //
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


        // ================================================================== //
        // ----------------------- Busqueda de artículo --------------------- //
        // ================================================================== //


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
    }
}
