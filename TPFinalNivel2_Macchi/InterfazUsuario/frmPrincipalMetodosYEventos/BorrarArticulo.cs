using System;
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
        // ------------------------------ Eventos --------------------------- //
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

    }
}
