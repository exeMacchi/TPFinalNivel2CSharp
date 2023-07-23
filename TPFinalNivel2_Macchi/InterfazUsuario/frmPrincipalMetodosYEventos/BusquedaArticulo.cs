using System;
using System.Collections.Generic;
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
        /// Búsqueda de artículos en la lista <see cref="articulos"/> según un filtro 
        /// basado en el nombre de estos.
        /// </summary>
        private void BusquedaBasica()
        {
            string filtro = txbxBuscar.Text;

            try
            {
                if (articulos != null)
                {
                    List<Articulo> articulosFiltrados = articulos.FindAll(art => art.Nombre.ToUpper().Contains(filtro.ToUpper()));

                    if (articulosFiltrados != null)
                    {
                        dgvArticulos.DataSource = null;
                        dgvArticulos.DataSource = articulosFiltrados;
                        FormatearDGV();
                        ActualizarResultados();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if (campo == "Marca" || campo == "Categoria")
            {
                filtro = criterio;
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
                    condicion = $"M.Descripcion LIKE '{criterio}'";
                    break;

                case "Categoría":
                    condicion = $"C.Descripcion LIKE '{criterio}'";
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
        // ------------------------------ Eventos --------------------------- //
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
                lbBusqueda.Text = "Búsqueda rápida";
                btnFiltroAvanzado.Text = "AVANZADA";
            }    
            else if (!panelFiltroAvanzado.Visible)
            {
                comboxCampoBusqueda.SelectedIndex = 0;
                panelFiltroAvanzado.Visible = true;
                lbBusqueda.Text = "Búsqueda avanzada";
                btnFiltroAvanzado.Text = "RÁPIDA";
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
            else if (opcion == "Marca")
            {
                comboxCriterioBusqueda.Items.Clear();
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                List<Marca> marcas = marcaNegocio.CargarMarcas();
                foreach(Marca marca in marcas)
                {
                    comboxCriterioBusqueda.Items.Add(marca.Descripcion);
                }

                comboxCriterioBusqueda.SelectedIndex = 0;
            }
            else if (opcion == "Categoría")
            {
                comboxCriterioBusqueda.Items.Clear();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<Categoria> categorias = categoriaNegocio.CargarCategorias();
                foreach(Categoria categoria in categorias)
                {
                    comboxCriterioBusqueda.Items.Add(categoria.Descripcion);
                }

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
                if (e.KeyCode == Keys.Enter && articulos != null)
                {
                    btnBuscar_Click(sender, e);
                }
            }
        }

    }
}
