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
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelMetodosVentana = new System.Windows.Forms.Panel();
            this.panelFiltroRapido = new System.Windows.Forms.Panel();
            this.btnFiltroAvanzado = new System.Windows.Forms.Button();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.panelSubPrincipalIzquierda = new System.Windows.Forms.Panel();
            this.panelGridDataView = new System.Windows.Forms.Panel();
            this.btnSeleccionarArticulo = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelFiltroAvanzado = new System.Windows.Forms.Panel();
            this.panelDetallesArticulos = new System.Windows.Forms.Panel();
            this.panelModificarArticulo = new System.Windows.Forms.Panel();
            this.panelAgregarArticulo = new System.Windows.Forms.Panel();
            this.lbDetallesArticulos = new System.Windows.Forms.Label();
            this.lbImagen = new System.Windows.Forms.Label();
            this.btnModificarArticulo = new System.Windows.Forms.Button();
            this.btnEliminarArticulo = new System.Windows.Forms.Button();
            this.lbModificarArticulo = new System.Windows.Forms.Label();
            this.btnConfirmarModificacion = new System.Windows.Forms.Button();
            this.btnCancelarModificacion = new System.Windows.Forms.Button();
            this.btnAgregarArticulo = new System.Windows.Forms.Button();
            this.btnCancelarAgregacion = new System.Windows.Forms.Button();
            this.lbAgregarArticulo = new System.Windows.Forms.Label();
            this.btnNuevoArticulo = new System.Windows.Forms.Button();
            this.btnModificacionPendiente = new System.Windows.Forms.Button();
            this.btnCerrarFormulario = new System.Windows.Forms.Button();
            this.btnMaximizarFormulario = new System.Windows.Forms.Button();
            this.btnMinimizarFormulario = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            this.panelMetodosVentana.SuspendLayout();
            this.panelFiltroRapido.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            this.panelSubPrincipalIzquierda.SuspendLayout();
            this.panelGridDataView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelDetallesArticulos.SuspendLayout();
            this.panelModificarArticulo.SuspendLayout();
            this.panelAgregarArticulo.SuspendLayout();
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
            this.panelSuperior.Size = new System.Drawing.Size(1339, 100);
            this.panelSuperior.TabIndex = 0;
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
            this.panelMetodosVentana.Location = new System.Drawing.Point(846, 0);
            this.panelMetodosVentana.Name = "panelMetodosVentana";
            this.panelMetodosVentana.Size = new System.Drawing.Size(493, 100);
            this.panelMetodosVentana.TabIndex = 1;
            // 
            // panelFiltroRapido
            // 
            this.panelFiltroRapido.BackColor = System.Drawing.Color.Chocolate;
            this.panelFiltroRapido.Controls.Add(this.btnFiltroAvanzado);
            this.panelFiltroRapido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltroRapido.Location = new System.Drawing.Point(0, 0);
            this.panelFiltroRapido.Name = "panelFiltroRapido";
            this.panelFiltroRapido.Size = new System.Drawing.Size(846, 100);
            this.panelFiltroRapido.TabIndex = 0;
            // 
            // btnFiltroAvanzado
            // 
            this.btnFiltroAvanzado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFiltroAvanzado.Location = new System.Drawing.Point(675, 37);
            this.btnFiltroAvanzado.Name = "btnFiltroAvanzado";
            this.btnFiltroAvanzado.Size = new System.Drawing.Size(159, 45);
            this.btnFiltroAvanzado.TabIndex = 0;
            this.btnFiltroAvanzado.Text = "Expandir / Ocultar";
            this.btnFiltroAvanzado.UseVisualStyleBackColor = true;
            this.btnFiltroAvanzado.Click += new System.EventHandler(this.btnFiltroAvanzado_Click);
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelPrincipal.Controls.Add(this.panelSubPrincipalIzquierda);
            this.panelPrincipal.Controls.Add(this.panelDetallesArticulos);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 100);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(1339, 482);
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
            this.panelSubPrincipalIzquierda.Size = new System.Drawing.Size(846, 482);
            this.panelSubPrincipalIzquierda.TabIndex = 1;
            // 
            // panelGridDataView
            // 
            this.panelGridDataView.BackColor = System.Drawing.Color.Navy;
            this.panelGridDataView.Controls.Add(this.btnSeleccionarArticulo);
            this.panelGridDataView.Controls.Add(this.dataGridView1);
            this.panelGridDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridDataView.Location = new System.Drawing.Point(0, 100);
            this.panelGridDataView.Name = "panelGridDataView";
            this.panelGridDataView.Size = new System.Drawing.Size(846, 382);
            this.panelGridDataView.TabIndex = 1;
            // 
            // btnSeleccionarArticulo
            // 
            this.btnSeleccionarArticulo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSeleccionarArticulo.BackColor = System.Drawing.Color.Silver;
            this.btnSeleccionarArticulo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarArticulo.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionarArticulo.Location = new System.Drawing.Point(311, 329);
            this.btnSeleccionarArticulo.Name = "btnSeleccionarArticulo";
            this.btnSeleccionarArticulo.Size = new System.Drawing.Size(208, 42);
            this.btnSeleccionarArticulo.TabIndex = 1;
            this.btnSeleccionarArticulo.Text = "Seleccion articulo";
            this.btnSeleccionarArticulo.UseVisualStyleBackColor = false;
            this.btnSeleccionarArticulo.Click += new System.EventHandler(this.btnSeleccionarArticulo_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(822, 315);
            this.dataGridView1.TabIndex = 0;
            // 
            // panelFiltroAvanzado
            // 
            this.panelFiltroAvanzado.BackColor = System.Drawing.Color.Aquamarine;
            this.panelFiltroAvanzado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltroAvanzado.Location = new System.Drawing.Point(0, 0);
            this.panelFiltroAvanzado.Name = "panelFiltroAvanzado";
            this.panelFiltroAvanzado.Size = new System.Drawing.Size(846, 100);
            this.panelFiltroAvanzado.TabIndex = 0;
            // 
            // panelDetallesArticulos
            // 
            this.panelDetallesArticulos.BackColor = System.Drawing.Color.Moccasin;
            this.panelDetallesArticulos.Controls.Add(this.panelModificarArticulo);
            this.panelDetallesArticulos.Controls.Add(this.lbImagen);
            this.panelDetallesArticulos.Controls.Add(this.lbDetallesArticulos);
            this.panelDetallesArticulos.Controls.Add(this.btnEliminarArticulo);
            this.panelDetallesArticulos.Controls.Add(this.btnModificarArticulo);
            this.panelDetallesArticulos.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDetallesArticulos.Location = new System.Drawing.Point(846, 0);
            this.panelDetallesArticulos.Name = "panelDetallesArticulos";
            this.panelDetallesArticulos.Size = new System.Drawing.Size(493, 482);
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
            this.panelModificarArticulo.Size = new System.Drawing.Size(493, 482);
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
            this.panelAgregarArticulo.Size = new System.Drawing.Size(493, 482);
            this.panelAgregarArticulo.TabIndex = 0;
            // 
            // lbDetallesArticulos
            // 
            this.lbDetallesArticulos.AutoSize = true;
            this.lbDetallesArticulos.Location = new System.Drawing.Point(156, 27);
            this.lbDetallesArticulos.Name = "lbDetallesArticulos";
            this.lbDetallesArticulos.Size = new System.Drawing.Size(110, 16);
            this.lbDetallesArticulos.TabIndex = 0;
            this.lbDetallesArticulos.Text = "Detalles artículos";
            // 
            // lbImagen
            // 
            this.lbImagen.AutoSize = true;
            this.lbImagen.Location = new System.Drawing.Point(181, 100);
            this.lbImagen.Name = "lbImagen";
            this.lbImagen.Size = new System.Drawing.Size(52, 16);
            this.lbImagen.TabIndex = 1;
            this.lbImagen.Text = "Imagen";
            // 
            // btnModificarArticulo
            // 
            this.btnModificarArticulo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModificarArticulo.Location = new System.Drawing.Point(99, 429);
            this.btnModificarArticulo.Name = "btnModificarArticulo";
            this.btnModificarArticulo.Size = new System.Drawing.Size(145, 41);
            this.btnModificarArticulo.TabIndex = 2;
            this.btnModificarArticulo.Text = "MODIFICAR";
            this.btnModificarArticulo.UseVisualStyleBackColor = true;
            this.btnModificarArticulo.Click += new System.EventHandler(this.btnModificarArticulo_Click);
            // 
            // btnEliminarArticulo
            // 
            this.btnEliminarArticulo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminarArticulo.Location = new System.Drawing.Point(303, 429);
            this.btnEliminarArticulo.Name = "btnEliminarArticulo";
            this.btnEliminarArticulo.Size = new System.Drawing.Size(145, 41);
            this.btnEliminarArticulo.TabIndex = 3;
            this.btnEliminarArticulo.Text = "ELIMINAR";
            this.btnEliminarArticulo.UseVisualStyleBackColor = true;
            this.btnEliminarArticulo.Click += new System.EventHandler(this.btnEliminarArticulo_Click);
            // 
            // lbModificarArticulo
            // 
            this.lbModificarArticulo.AutoSize = true;
            this.lbModificarArticulo.Location = new System.Drawing.Point(156, 43);
            this.lbModificarArticulo.Name = "lbModificarArticulo";
            this.lbModificarArticulo.Size = new System.Drawing.Size(127, 16);
            this.lbModificarArticulo.TabIndex = 0;
            this.lbModificarArticulo.Text = "Modificando artículo";
            // 
            // btnConfirmarModificacion
            // 
            this.btnConfirmarModificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarModificacion.Location = new System.Drawing.Point(99, 412);
            this.btnConfirmarModificacion.Name = "btnConfirmarModificacion";
            this.btnConfirmarModificacion.Size = new System.Drawing.Size(145, 41);
            this.btnConfirmarModificacion.TabIndex = 3;
            this.btnConfirmarModificacion.Text = "CONFIRMAR";
            this.btnConfirmarModificacion.UseVisualStyleBackColor = true;
            this.btnConfirmarModificacion.Click += new System.EventHandler(this.btnConfirmarModificacion_Click);
            // 
            // btnCancelarModificacion
            // 
            this.btnCancelarModificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarModificacion.Location = new System.Drawing.Point(303, 412);
            this.btnCancelarModificacion.Name = "btnCancelarModificacion";
            this.btnCancelarModificacion.Size = new System.Drawing.Size(145, 41);
            this.btnCancelarModificacion.TabIndex = 4;
            this.btnCancelarModificacion.Text = "CANCELAR";
            this.btnCancelarModificacion.UseVisualStyleBackColor = true;
            this.btnCancelarModificacion.Click += new System.EventHandler(this.btnCancelarModificacion_Click);
            // 
            // btnAgregarArticulo
            // 
            this.btnAgregarArticulo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarArticulo.Location = new System.Drawing.Point(99, 412);
            this.btnAgregarArticulo.Name = "btnAgregarArticulo";
            this.btnAgregarArticulo.Size = new System.Drawing.Size(145, 41);
            this.btnAgregarArticulo.TabIndex = 4;
            this.btnAgregarArticulo.Text = "AGREGAR";
            this.btnAgregarArticulo.UseVisualStyleBackColor = true;
            this.btnAgregarArticulo.Click += new System.EventHandler(this.btnAgregarArticulo_Click);
            // 
            // btnCancelarAgregacion
            // 
            this.btnCancelarAgregacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarAgregacion.Location = new System.Drawing.Point(303, 412);
            this.btnCancelarAgregacion.Name = "btnCancelarAgregacion";
            this.btnCancelarAgregacion.Size = new System.Drawing.Size(145, 41);
            this.btnCancelarAgregacion.TabIndex = 5;
            this.btnCancelarAgregacion.Text = "CANCELAR";
            this.btnCancelarAgregacion.UseVisualStyleBackColor = true;
            this.btnCancelarAgregacion.Click += new System.EventHandler(this.btnCancelarAgregacion_Click);
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
            // btnNuevoArticulo
            // 
            this.btnNuevoArticulo.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoArticulo.Location = new System.Drawing.Point(6, 37);
            this.btnNuevoArticulo.Name = "btnNuevoArticulo";
            this.btnNuevoArticulo.Size = new System.Drawing.Size(93, 45);
            this.btnNuevoArticulo.TabIndex = 0;
            this.btnNuevoArticulo.Text = "Nuevo artículo";
            this.btnNuevoArticulo.UseVisualStyleBackColor = true;
            this.btnNuevoArticulo.Click += new System.EventHandler(this.btnNuevoArticulo_Click);
            // 
            // btnModificacionPendiente
            // 
            this.btnModificacionPendiente.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificacionPendiente.Location = new System.Drawing.Point(116, 37);
            this.btnModificacionPendiente.Name = "btnModificacionPendiente";
            this.btnModificacionPendiente.Size = new System.Drawing.Size(141, 45);
            this.btnModificacionPendiente.TabIndex = 1;
            this.btnModificacionPendiente.Text = "Modificación pendiente";
            this.btnModificacionPendiente.UseVisualStyleBackColor = true;
            this.btnModificacionPendiente.Click += new System.EventHandler(this.btnModificacionPendiente_Click);
            // 
            // btnCerrarFormulario
            // 
            this.btnCerrarFormulario.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarFormulario.Location = new System.Drawing.Point(437, 12);
            this.btnCerrarFormulario.Name = "btnCerrarFormulario";
            this.btnCerrarFormulario.Size = new System.Drawing.Size(44, 36);
            this.btnCerrarFormulario.TabIndex = 2;
            this.btnCerrarFormulario.Text = "X";
            this.btnCerrarFormulario.UseVisualStyleBackColor = true;
            // 
            // btnMaximizarFormulario
            // 
            this.btnMaximizarFormulario.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaximizarFormulario.Location = new System.Drawing.Point(387, 12);
            this.btnMaximizarFormulario.Name = "btnMaximizarFormulario";
            this.btnMaximizarFormulario.Size = new System.Drawing.Size(44, 36);
            this.btnMaximizarFormulario.TabIndex = 3;
            this.btnMaximizarFormulario.Text = "■";
            this.btnMaximizarFormulario.UseVisualStyleBackColor = true;
            // 
            // btnMinimizarFormulario
            // 
            this.btnMinimizarFormulario.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimizarFormulario.Location = new System.Drawing.Point(337, 12);
            this.btnMinimizarFormulario.Name = "btnMinimizarFormulario";
            this.btnMinimizarFormulario.Size = new System.Drawing.Size(44, 36);
            this.btnMinimizarFormulario.TabIndex = 4;
            this.btnMinimizarFormulario.Text = "-";
            this.btnMinimizarFormulario.UseVisualStyleBackColor = true;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 582);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panelSuperior);
            this.Name = "frmPrincipal";
            this.Text = "Form1";
            this.panelSuperior.ResumeLayout(false);
            this.panelMetodosVentana.ResumeLayout(false);
            this.panelFiltroRapido.ResumeLayout(false);
            this.panelPrincipal.ResumeLayout(false);
            this.panelSubPrincipalIzquierda.ResumeLayout(false);
            this.panelGridDataView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelDetallesArticulos.ResumeLayout(false);
            this.panelDetallesArticulos.PerformLayout();
            this.panelModificarArticulo.ResumeLayout(false);
            this.panelModificarArticulo.PerformLayout();
            this.panelAgregarArticulo.ResumeLayout(false);
            this.panelAgregarArticulo.PerformLayout();
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSeleccionarArticulo;
        private System.Windows.Forms.Panel panelModificarArticulo;
        private System.Windows.Forms.Panel panelAgregarArticulo;
        private System.Windows.Forms.Button btnEliminarArticulo;
        private System.Windows.Forms.Button btnModificarArticulo;
        private System.Windows.Forms.Label lbImagen;
        private System.Windows.Forms.Label lbDetallesArticulos;
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
    }
}

