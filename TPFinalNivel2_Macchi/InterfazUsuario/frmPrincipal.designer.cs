namespace InterfazUsuario
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelFiltroRapido = new System.Windows.Forms.Panel();
            this.btnFiltroAvanzado = new System.Windows.Forms.Button();
            this.panelMetodosVentana = new System.Windows.Forms.Panel();
            this.btnMinimizarFormulario = new System.Windows.Forms.Button();
            this.btnMaximizarFormulario = new System.Windows.Forms.Button();
            this.btnCerrarFormulario = new System.Windows.Forms.Button();
            this.btnModificacionPendiente = new System.Windows.Forms.Button();
            this.btnNuevoArticulo = new System.Windows.Forms.Button();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.panelSubPrincipalIzquierda = new System.Windows.Forms.Panel();
            this.panelGridDataView = new System.Windows.Forms.Panel();
            this.dgvArticulos = new System.Windows.Forms.DataGridView();
            this.lbDataGridViewVacio = new System.Windows.Forms.Label();
            this.panelFiltroAvanzado = new System.Windows.Forms.Panel();
            this.panelDetallesArticulos = new System.Windows.Forms.Panel();
            this.panelModificarArticulo = new System.Windows.Forms.Panel();
            this.panelAgregarArticulo = new System.Windows.Forms.Panel();
            this.lbAgregarArticulo = new System.Windows.Forms.Label();
            this.btnCancelarAgregacion = new System.Windows.Forms.Button();
            this.btnAgregarArticulo = new System.Windows.Forms.Button();
            this.btnCancelarModificacion = new System.Windows.Forms.Button();
            this.btnConfirmarModificacion = new System.Windows.Forms.Button();
            this.lbModificarArticulo = new System.Windows.Forms.Label();
            this.btnEliminarArticuloDA = new System.Windows.Forms.Button();
            this.btnModificarArticuloDA = new System.Windows.Forms.Button();
            this.pboxImagenDA = new System.Windows.Forms.PictureBox();
            this.lbCodigoDA = new System.Windows.Forms.Label();
            this.lbNombreDA = new System.Windows.Forms.Label();
            this.lbDescripcionDA = new System.Windows.Forms.Label();
            this.lbMarcaDA = new System.Windows.Forms.Label();
            this.lbCategoriaDA = new System.Windows.Forms.Label();
            this.lbPrecioDA = new System.Windows.Forms.Label();
            this.txbxCodigoDA = new System.Windows.Forms.TextBox();
            this.txbxNombreDA = new System.Windows.Forms.TextBox();
            this.txbxDescripcionDA = new System.Windows.Forms.TextBox();
            this.txbxMarcaDA = new System.Windows.Forms.TextBox();
            this.txbxCategoriaDA = new System.Windows.Forms.TextBox();
            this.txbxPrecioDA = new System.Windows.Forms.TextBox();
            this.panelSuperior.SuspendLayout();
            this.panelFiltroRapido.SuspendLayout();
            this.panelMetodosVentana.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            this.panelSubPrincipalIzquierda.SuspendLayout();
            this.panelGridDataView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).BeginInit();
            this.panelDetallesArticulos.SuspendLayout();
            this.panelModificarArticulo.SuspendLayout();
            this.panelAgregarArticulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImagenDA)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkSalmon;
            this.panelSuperior.Controls.Add(this.panelFiltroRapido);
            this.panelSuperior.Controls.Add(this.panelMetodosVentana);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1351, 54);
            this.panelSuperior.TabIndex = 0;
            // 
            // panelFiltroRapido
            // 
            this.panelFiltroRapido.BackColor = System.Drawing.Color.Chocolate;
            this.panelFiltroRapido.Controls.Add(this.btnFiltroAvanzado);
            this.panelFiltroRapido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltroRapido.Location = new System.Drawing.Point(0, 0);
            this.panelFiltroRapido.Name = "panelFiltroRapido";
            this.panelFiltroRapido.Size = new System.Drawing.Size(778, 54);
            this.panelFiltroRapido.TabIndex = 0;
            // 
            // btnFiltroAvanzado
            // 
            this.btnFiltroAvanzado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFiltroAvanzado.Location = new System.Drawing.Point(613, 3);
            this.btnFiltroAvanzado.Name = "btnFiltroAvanzado";
            this.btnFiltroAvanzado.Size = new System.Drawing.Size(159, 45);
            this.btnFiltroAvanzado.TabIndex = 0;
            this.btnFiltroAvanzado.Text = "Expandir / Ocultar";
            this.btnFiltroAvanzado.UseVisualStyleBackColor = true;
            this.btnFiltroAvanzado.Click += new System.EventHandler(this.btnFiltroAvanzado_Click);
            // 
            // panelMetodosVentana
            // 
            this.panelMetodosVentana.BackColor = System.Drawing.Color.ForestGreen;
            this.panelMetodosVentana.Controls.Add(this.btnMinimizarFormulario);
            this.panelMetodosVentana.Controls.Add(this.btnMaximizarFormulario);
            this.panelMetodosVentana.Controls.Add(this.btnCerrarFormulario);
            this.panelMetodosVentana.Controls.Add(this.btnModificacionPendiente);
            this.panelMetodosVentana.Controls.Add(this.btnNuevoArticulo);
            this.panelMetodosVentana.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelMetodosVentana.Location = new System.Drawing.Point(778, 0);
            this.panelMetodosVentana.Name = "panelMetodosVentana";
            this.panelMetodosVentana.Size = new System.Drawing.Size(573, 54);
            this.panelMetodosVentana.TabIndex = 1;
            // 
            // btnMinimizarFormulario
            // 
            this.btnMinimizarFormulario.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimizarFormulario.Location = new System.Drawing.Point(426, 3);
            this.btnMinimizarFormulario.Name = "btnMinimizarFormulario";
            this.btnMinimizarFormulario.Size = new System.Drawing.Size(44, 36);
            this.btnMinimizarFormulario.TabIndex = 4;
            this.btnMinimizarFormulario.Text = "-";
            this.btnMinimizarFormulario.UseVisualStyleBackColor = true;
            // 
            // btnMaximizarFormulario
            // 
            this.btnMaximizarFormulario.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaximizarFormulario.Location = new System.Drawing.Point(476, 3);
            this.btnMaximizarFormulario.Name = "btnMaximizarFormulario";
            this.btnMaximizarFormulario.Size = new System.Drawing.Size(44, 36);
            this.btnMaximizarFormulario.TabIndex = 3;
            this.btnMaximizarFormulario.Text = "■";
            this.btnMaximizarFormulario.UseVisualStyleBackColor = true;
            // 
            // btnCerrarFormulario
            // 
            this.btnCerrarFormulario.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarFormulario.Location = new System.Drawing.Point(526, 3);
            this.btnCerrarFormulario.Name = "btnCerrarFormulario";
            this.btnCerrarFormulario.Size = new System.Drawing.Size(44, 36);
            this.btnCerrarFormulario.TabIndex = 2;
            this.btnCerrarFormulario.Text = "X";
            this.btnCerrarFormulario.UseVisualStyleBackColor = true;
            // 
            // btnModificacionPendiente
            // 
            this.btnModificacionPendiente.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificacionPendiente.Location = new System.Drawing.Point(191, 3);
            this.btnModificacionPendiente.Name = "btnModificacionPendiente";
            this.btnModificacionPendiente.Size = new System.Drawing.Size(190, 45);
            this.btnModificacionPendiente.TabIndex = 1;
            this.btnModificacionPendiente.Text = "Modificación pendiente";
            this.btnModificacionPendiente.UseVisualStyleBackColor = true;
            this.btnModificacionPendiente.Click += new System.EventHandler(this.btnModificacionPendiente_Click);
            // 
            // btnNuevoArticulo
            // 
            this.btnNuevoArticulo.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoArticulo.Location = new System.Drawing.Point(7, 3);
            this.btnNuevoArticulo.Name = "btnNuevoArticulo";
            this.btnNuevoArticulo.Size = new System.Drawing.Size(178, 45);
            this.btnNuevoArticulo.TabIndex = 0;
            this.btnNuevoArticulo.Text = "Nuevo artículo";
            this.btnNuevoArticulo.UseVisualStyleBackColor = true;
            this.btnNuevoArticulo.Click += new System.EventHandler(this.btnNuevoArticulo_Click);
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelPrincipal.Controls.Add(this.panelSubPrincipalIzquierda);
            this.panelPrincipal.Controls.Add(this.panelDetallesArticulos);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 54);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(1351, 644);
            this.panelPrincipal.TabIndex = 1;
            // 
            // panelSubPrincipalIzquierda
            // 
            this.panelSubPrincipalIzquierda.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.panelSubPrincipalIzquierda.Controls.Add(this.panelGridDataView);
            this.panelSubPrincipalIzquierda.Controls.Add(this.panelFiltroAvanzado);
            this.panelSubPrincipalIzquierda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSubPrincipalIzquierda.Location = new System.Drawing.Point(0, 0);
            this.panelSubPrincipalIzquierda.Name = "panelSubPrincipalIzquierda";
            this.panelSubPrincipalIzquierda.Size = new System.Drawing.Size(778, 644);
            this.panelSubPrincipalIzquierda.TabIndex = 1;
            // 
            // panelGridDataView
            // 
            this.panelGridDataView.BackColor = System.Drawing.Color.Navy;
            this.panelGridDataView.Controls.Add(this.dgvArticulos);
            this.panelGridDataView.Controls.Add(this.lbDataGridViewVacio);
            this.panelGridDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridDataView.Location = new System.Drawing.Point(0, 57);
            this.panelGridDataView.Name = "panelGridDataView";
            this.panelGridDataView.Size = new System.Drawing.Size(778, 587);
            this.panelGridDataView.TabIndex = 1;
            // 
            // dgvArticulos
            // 
            this.dgvArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArticulos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvArticulos.Location = new System.Drawing.Point(3, 7);
            this.dgvArticulos.MultiSelect = false;
            this.dgvArticulos.Name = "dgvArticulos";
            this.dgvArticulos.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvArticulos.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvArticulos.RowHeadersWidth = 51;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvArticulos.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvArticulos.RowTemplate.Height = 24;
            this.dgvArticulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArticulos.Size = new System.Drawing.Size(769, 568);
            this.dgvArticulos.TabIndex = 0;
            this.dgvArticulos.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvArticulos_RowsAdded);
            this.dgvArticulos.SelectionChanged += new System.EventHandler(this.dgvArticulos_SelectionChanged);
            // 
            // lbDataGridViewVacio
            // 
            this.lbDataGridViewVacio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbDataGridViewVacio.AutoSize = true;
            this.lbDataGridViewVacio.Font = new System.Drawing.Font("Century Gothic", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDataGridViewVacio.ForeColor = System.Drawing.Color.White;
            this.lbDataGridViewVacio.Location = new System.Drawing.Point(39, 250);
            this.lbDataGridViewVacio.Name = "lbDataGridViewVacio";
            this.lbDataGridViewVacio.Size = new System.Drawing.Size(675, 44);
            this.lbDataGridViewVacio.TabIndex = 2;
            this.lbDataGridViewVacio.Text = "No hay registros en la base de datos.";
            // 
            // panelFiltroAvanzado
            // 
            this.panelFiltroAvanzado.BackColor = System.Drawing.Color.Aquamarine;
            this.panelFiltroAvanzado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltroAvanzado.Location = new System.Drawing.Point(0, 0);
            this.panelFiltroAvanzado.Name = "panelFiltroAvanzado";
            this.panelFiltroAvanzado.Size = new System.Drawing.Size(778, 57);
            this.panelFiltroAvanzado.TabIndex = 0;
            // 
            // panelDetallesArticulos
            // 
            this.panelDetallesArticulos.BackColor = System.Drawing.Color.Moccasin;
            this.panelDetallesArticulos.Controls.Add(this.panelModificarArticulo);
            this.panelDetallesArticulos.Controls.Add(this.txbxPrecioDA);
            this.panelDetallesArticulos.Controls.Add(this.txbxCategoriaDA);
            this.panelDetallesArticulos.Controls.Add(this.txbxMarcaDA);
            this.panelDetallesArticulos.Controls.Add(this.txbxDescripcionDA);
            this.panelDetallesArticulos.Controls.Add(this.txbxNombreDA);
            this.panelDetallesArticulos.Controls.Add(this.txbxCodigoDA);
            this.panelDetallesArticulos.Controls.Add(this.lbPrecioDA);
            this.panelDetallesArticulos.Controls.Add(this.lbCategoriaDA);
            this.panelDetallesArticulos.Controls.Add(this.lbMarcaDA);
            this.panelDetallesArticulos.Controls.Add(this.lbDescripcionDA);
            this.panelDetallesArticulos.Controls.Add(this.lbNombreDA);
            this.panelDetallesArticulos.Controls.Add(this.lbCodigoDA);
            this.panelDetallesArticulos.Controls.Add(this.btnEliminarArticuloDA);
            this.panelDetallesArticulos.Controls.Add(this.btnModificarArticuloDA);
            this.panelDetallesArticulos.Controls.Add(this.pboxImagenDA);
            this.panelDetallesArticulos.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDetallesArticulos.Location = new System.Drawing.Point(778, 0);
            this.panelDetallesArticulos.Name = "panelDetallesArticulos";
            this.panelDetallesArticulos.Size = new System.Drawing.Size(573, 644);
            this.panelDetallesArticulos.TabIndex = 0;
            // 
            // panelModificarArticulo
            // 
            this.panelModificarArticulo.BackColor = System.Drawing.Color.SandyBrown;
            this.panelModificarArticulo.Controls.Add(this.panelAgregarArticulo);
            this.panelModificarArticulo.Controls.Add(this.btnCancelarModificacion);
            this.panelModificarArticulo.Controls.Add(this.btnConfirmarModificacion);
            this.panelModificarArticulo.Controls.Add(this.lbModificarArticulo);
            this.panelModificarArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModificarArticulo.Location = new System.Drawing.Point(0, 0);
            this.panelModificarArticulo.Name = "panelModificarArticulo";
            this.panelModificarArticulo.Size = new System.Drawing.Size(573, 644);
            this.panelModificarArticulo.TabIndex = 0;
            // 
            // panelAgregarArticulo
            // 
            this.panelAgregarArticulo.BackColor = System.Drawing.Color.SaddleBrown;
            this.panelAgregarArticulo.Controls.Add(this.lbAgregarArticulo);
            this.panelAgregarArticulo.Controls.Add(this.btnCancelarAgregacion);
            this.panelAgregarArticulo.Controls.Add(this.btnAgregarArticulo);
            this.panelAgregarArticulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAgregarArticulo.Location = new System.Drawing.Point(0, 0);
            this.panelAgregarArticulo.Name = "panelAgregarArticulo";
            this.panelAgregarArticulo.Size = new System.Drawing.Size(573, 644);
            this.panelAgregarArticulo.TabIndex = 0;
            // 
            // lbAgregarArticulo
            // 
            this.lbAgregarArticulo.AutoSize = true;
            this.lbAgregarArticulo.Location = new System.Drawing.Point(166, 43);
            this.lbAgregarArticulo.Name = "lbAgregarArticulo";
            this.lbAgregarArticulo.Size = new System.Drawing.Size(91, 16);
            this.lbAgregarArticulo.TabIndex = 6;
            this.lbAgregarArticulo.Text = "Articulo nuevo";
            // 
            // btnCancelarAgregacion
            // 
            this.btnCancelarAgregacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarAgregacion.Location = new System.Drawing.Point(383, 574);
            this.btnCancelarAgregacion.Name = "btnCancelarAgregacion";
            this.btnCancelarAgregacion.Size = new System.Drawing.Size(145, 41);
            this.btnCancelarAgregacion.TabIndex = 5;
            this.btnCancelarAgregacion.Text = "CANCELAR";
            this.btnCancelarAgregacion.UseVisualStyleBackColor = true;
            this.btnCancelarAgregacion.Click += new System.EventHandler(this.btnCancelarAgregacion_Click);
            // 
            // btnAgregarArticulo
            // 
            this.btnAgregarArticulo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarArticulo.Location = new System.Drawing.Point(179, 574);
            this.btnAgregarArticulo.Name = "btnAgregarArticulo";
            this.btnAgregarArticulo.Size = new System.Drawing.Size(145, 41);
            this.btnAgregarArticulo.TabIndex = 4;
            this.btnAgregarArticulo.Text = "AGREGAR";
            this.btnAgregarArticulo.UseVisualStyleBackColor = true;
            this.btnAgregarArticulo.Click += new System.EventHandler(this.btnAgregarArticulo_Click);
            // 
            // btnCancelarModificacion
            // 
            this.btnCancelarModificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarModificacion.Location = new System.Drawing.Point(358, 574);
            this.btnCancelarModificacion.Name = "btnCancelarModificacion";
            this.btnCancelarModificacion.Size = new System.Drawing.Size(145, 41);
            this.btnCancelarModificacion.TabIndex = 4;
            this.btnCancelarModificacion.Text = "CANCELAR";
            this.btnCancelarModificacion.UseVisualStyleBackColor = true;
            this.btnCancelarModificacion.Click += new System.EventHandler(this.btnCancelarModificacion_Click);
            // 
            // btnConfirmarModificacion
            // 
            this.btnConfirmarModificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarModificacion.Location = new System.Drawing.Point(56, 574);
            this.btnConfirmarModificacion.Name = "btnConfirmarModificacion";
            this.btnConfirmarModificacion.Size = new System.Drawing.Size(145, 41);
            this.btnConfirmarModificacion.TabIndex = 3;
            this.btnConfirmarModificacion.Text = "CONFIRMAR";
            this.btnConfirmarModificacion.UseVisualStyleBackColor = true;
            this.btnConfirmarModificacion.Click += new System.EventHandler(this.btnConfirmarModificacion_Click);
            // 
            // lbModificarArticulo
            // 
            this.lbModificarArticulo.AutoSize = true;
            this.lbModificarArticulo.Location = new System.Drawing.Point(238, 145);
            this.lbModificarArticulo.Name = "lbModificarArticulo";
            this.lbModificarArticulo.Size = new System.Drawing.Size(127, 16);
            this.lbModificarArticulo.TabIndex = 0;
            this.lbModificarArticulo.Text = "Modificando artículo";
            // 
            // btnEliminarArticuloDA
            // 
            this.btnEliminarArticuloDA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminarArticuloDA.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarArticuloDA.Location = new System.Drawing.Point(325, 579);
            this.btnEliminarArticuloDA.Name = "btnEliminarArticuloDA";
            this.btnEliminarArticuloDA.Size = new System.Drawing.Size(145, 41);
            this.btnEliminarArticuloDA.TabIndex = 3;
            this.btnEliminarArticuloDA.Text = "ELIMINAR";
            this.btnEliminarArticuloDA.UseVisualStyleBackColor = true;
            this.btnEliminarArticuloDA.Click += new System.EventHandler(this.btnEliminarArticulo_Click);
            // 
            // btnModificarArticuloDA
            // 
            this.btnModificarArticuloDA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModificarArticuloDA.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarArticuloDA.Location = new System.Drawing.Point(93, 579);
            this.btnModificarArticuloDA.Name = "btnModificarArticuloDA";
            this.btnModificarArticuloDA.Size = new System.Drawing.Size(145, 41);
            this.btnModificarArticuloDA.TabIndex = 2;
            this.btnModificarArticuloDA.Text = "MODIFICAR";
            this.btnModificarArticuloDA.UseVisualStyleBackColor = true;
            this.btnModificarArticuloDA.Click += new System.EventHandler(this.btnModificarArticulo_Click);
            // 
            // pboxImagenDA
            // 
            this.pboxImagenDA.BackColor = System.Drawing.Color.Snow;
            this.pboxImagenDA.Location = new System.Drawing.Point(161, 20);
            this.pboxImagenDA.Name = "pboxImagenDA";
            this.pboxImagenDA.Size = new System.Drawing.Size(256, 256);
            this.pboxImagenDA.TabIndex = 7;
            this.pboxImagenDA.TabStop = false;
            // 
            // lbCodigoDA
            // 
            this.lbCodigoDA.AutoSize = true;
            this.lbCodigoDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCodigoDA.Location = new System.Drawing.Point(93, 298);
            this.lbCodigoDA.Name = "lbCodigoDA";
            this.lbCodigoDA.Size = new System.Drawing.Size(62, 17);
            this.lbCodigoDA.TabIndex = 8;
            this.lbCodigoDA.Text = "Código:";
            // 
            // lbNombreDA
            // 
            this.lbNombreDA.AutoSize = true;
            this.lbNombreDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombreDA.Location = new System.Drawing.Point(90, 334);
            this.lbNombreDA.Name = "lbNombreDA";
            this.lbNombreDA.Size = new System.Drawing.Size(65, 17);
            this.lbNombreDA.TabIndex = 9;
            this.lbNombreDA.Text = "Nombre:";
            // 
            // lbDescripcionDA
            // 
            this.lbDescripcionDA.AutoSize = true;
            this.lbDescripcionDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescripcionDA.Location = new System.Drawing.Point(68, 371);
            this.lbDescripcionDA.Name = "lbDescripcionDA";
            this.lbDescripcionDA.Size = new System.Drawing.Size(87, 17);
            this.lbDescripcionDA.TabIndex = 10;
            this.lbDescripcionDA.Text = "Descripción:";
            // 
            // lbMarcaDA
            // 
            this.lbMarcaDA.AutoSize = true;
            this.lbMarcaDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMarcaDA.Location = new System.Drawing.Point(102, 447);
            this.lbMarcaDA.Name = "lbMarcaDA";
            this.lbMarcaDA.Size = new System.Drawing.Size(53, 17);
            this.lbMarcaDA.TabIndex = 11;
            this.lbMarcaDA.Text = "Marca:";
            // 
            // lbCategoriaDA
            // 
            this.lbCategoriaDA.AutoSize = true;
            this.lbCategoriaDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCategoriaDA.Location = new System.Drawing.Point(76, 486);
            this.lbCategoriaDA.Name = "lbCategoriaDA";
            this.lbCategoriaDA.Size = new System.Drawing.Size(79, 17);
            this.lbCategoriaDA.TabIndex = 12;
            this.lbCategoriaDA.Text = "Categoría:";
            // 
            // lbPrecioDA
            // 
            this.lbPrecioDA.AutoSize = true;
            this.lbPrecioDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecioDA.Location = new System.Drawing.Point(103, 525);
            this.lbPrecioDA.Name = "lbPrecioDA";
            this.lbPrecioDA.Size = new System.Drawing.Size(52, 17);
            this.lbPrecioDA.TabIndex = 13;
            this.lbPrecioDA.Text = "Precio:";
            // 
            // txbxCodigoDA
            // 
            this.txbxCodigoDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbxCodigoDA.Location = new System.Drawing.Point(161, 295);
            this.txbxCodigoDA.Name = "txbxCodigoDA";
            this.txbxCodigoDA.Size = new System.Drawing.Size(256, 23);
            this.txbxCodigoDA.TabIndex = 14;
            // 
            // txbxNombreDA
            // 
            this.txbxNombreDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbxNombreDA.Location = new System.Drawing.Point(161, 331);
            this.txbxNombreDA.Name = "txbxNombreDA";
            this.txbxNombreDA.Size = new System.Drawing.Size(256, 23);
            this.txbxNombreDA.TabIndex = 15;
            // 
            // txbxDescripcionDA
            // 
            this.txbxDescripcionDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbxDescripcionDA.Location = new System.Drawing.Point(161, 368);
            this.txbxDescripcionDA.Multiline = true;
            this.txbxDescripcionDA.Name = "txbxDescripcionDA";
            this.txbxDescripcionDA.Size = new System.Drawing.Size(256, 60);
            this.txbxDescripcionDA.TabIndex = 16;
            // 
            // txbxMarcaDA
            // 
            this.txbxMarcaDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbxMarcaDA.Location = new System.Drawing.Point(161, 444);
            this.txbxMarcaDA.Name = "txbxMarcaDA";
            this.txbxMarcaDA.Size = new System.Drawing.Size(256, 23);
            this.txbxMarcaDA.TabIndex = 17;
            // 
            // txbxCategoriaDA
            // 
            this.txbxCategoriaDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbxCategoriaDA.Location = new System.Drawing.Point(161, 483);
            this.txbxCategoriaDA.Name = "txbxCategoriaDA";
            this.txbxCategoriaDA.Size = new System.Drawing.Size(256, 23);
            this.txbxCategoriaDA.TabIndex = 18;
            // 
            // txbxPrecioDA
            // 
            this.txbxPrecioDA.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbxPrecioDA.Location = new System.Drawing.Point(161, 522);
            this.txbxPrecioDA.Name = "txbxPrecioDA";
            this.txbxPrecioDA.Size = new System.Drawing.Size(256, 23);
            this.txbxPrecioDA.TabIndex = 19;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 698);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panelSuperior);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo de artículos";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelFiltroRapido.ResumeLayout(false);
            this.panelMetodosVentana.ResumeLayout(false);
            this.panelPrincipal.ResumeLayout(false);
            this.panelSubPrincipalIzquierda.ResumeLayout(false);
            this.panelGridDataView.ResumeLayout(false);
            this.panelGridDataView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).EndInit();
            this.panelDetallesArticulos.ResumeLayout(false);
            this.panelDetallesArticulos.PerformLayout();
            this.panelModificarArticulo.ResumeLayout(false);
            this.panelModificarArticulo.PerformLayout();
            this.panelAgregarArticulo.ResumeLayout(false);
            this.panelAgregarArticulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImagenDA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Panel panelMetodosVentana;
        private System.Windows.Forms.Panel panelFiltroRapido;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Panel panelSubPrincipalIzquierda;
        private System.Windows.Forms.Panel panelGridDataView;
        private System.Windows.Forms.Panel panelFiltroAvanzado;
        private System.Windows.Forms.Panel panelDetallesArticulos;
        private System.Windows.Forms.Button btnFiltroAvanzado;
        private System.Windows.Forms.DataGridView dgvArticulos;
        private System.Windows.Forms.Panel panelModificarArticulo;
        private System.Windows.Forms.Panel panelAgregarArticulo;
        private System.Windows.Forms.Button btnEliminarArticuloDA;
        private System.Windows.Forms.Button btnModificarArticuloDA;
        private System.Windows.Forms.Label lbAgregarArticulo;
        private System.Windows.Forms.Button btnCancelarAgregacion;
        private System.Windows.Forms.Button btnAgregarArticulo;
        private System.Windows.Forms.Button btnCancelarModificacion;
        private System.Windows.Forms.Button btnConfirmarModificacion;
        private System.Windows.Forms.Label lbModificarArticulo;
        private System.Windows.Forms.Button btnNuevoArticulo;
        private System.Windows.Forms.Button btnMaximizarFormulario;
        private System.Windows.Forms.Button btnCerrarFormulario;
        private System.Windows.Forms.Button btnModificacionPendiente;
        private System.Windows.Forms.Button btnMinimizarFormulario;
        private System.Windows.Forms.PictureBox pboxImagenDA;
        private System.Windows.Forms.Label lbDataGridViewVacio;
        private System.Windows.Forms.Label lbNombreDA;
        private System.Windows.Forms.Label lbCodigoDA;
        private System.Windows.Forms.Label lbDescripcionDA;
        private System.Windows.Forms.Label lbCategoriaDA;
        private System.Windows.Forms.Label lbMarcaDA;
        private System.Windows.Forms.Label lbPrecioDA;
        private System.Windows.Forms.TextBox txbxPrecioDA;
        private System.Windows.Forms.TextBox txbxCategoriaDA;
        private System.Windows.Forms.TextBox txbxMarcaDA;
        private System.Windows.Forms.TextBox txbxDescripcionDA;
        private System.Windows.Forms.TextBox txbxNombreDA;
        private System.Windows.Forms.TextBox txbxCodigoDA;
    }
}

