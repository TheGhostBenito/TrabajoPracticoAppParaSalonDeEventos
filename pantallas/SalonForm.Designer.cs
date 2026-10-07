namespace proyecto_integrador.pantallas
{
    partial class SalonForm
    {
        /// <summary>
        
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSalonesCreados = new System.Windows.Forms.DataGridView();
            this.dgvID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvUbicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gBoxSalones = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminarSalon = new System.Windows.Forms.Button();
            this.btnModificarSalon = new System.Windows.Forms.Button();
            this.btnCrearSalon = new System.Windows.Forms.Button();
            this.lblMontoSalon = new System.Windows.Forms.Label();
            this.txtBoxMonto = new System.Windows.Forms.TextBox();
            this.lblUbicacionSalon = new System.Windows.Forms.Label();
            this.txtBoxUbicacion = new System.Windows.Forms.TextBox();
            this.lblNombreSalon = new System.Windows.Forms.Label();
            this.txtBoxNombre = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalonesCreados)).BeginInit();
            this.gBoxSalones.SuspendLayout();
            this.SuspendLayout();
            
            // dgvSalonesCreados
            
            this.dgvSalonesCreados.AllowUserToAddRows = false;
            this.dgvSalonesCreados.AllowUserToDeleteRows = false;
            this.dgvSalonesCreados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSalonesCreados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSalonesCreados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalonesCreados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvID,
            this.dgvNombre,
            this.dgvUbicacion,
            this.dgvMonto});
            this.dgvSalonesCreados.Location = new System.Drawing.Point(340, 20);
            this.dgvSalonesCreados.MultiSelect = false;
            this.dgvSalonesCreados.Name = "dgvSalonesCreados";
            this.dgvSalonesCreados.ReadOnly = true;
            this.dgvSalonesCreados.RowHeadersVisible = false;
            this.dgvSalonesCreados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSalonesCreados.Size = new System.Drawing.Size(420, 380);
            this.dgvSalonesCreados.TabIndex = 0;
            this.dgvSalonesCreados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSalonesCreados_CellClick);
            
            // dgvID
            
            this.dgvID.DataPropertyName = "Id";
            this.dgvID.FillWeight = 20F;
            this.dgvID.HeaderText = "ID";
            this.dgvID.Name = "dgvID";
            this.dgvID.ReadOnly = true;
            
            // dgvNombre
            
            this.dgvNombre.DataPropertyName = "Nombre";
            this.dgvNombre.FillWeight = 80F;
            this.dgvNombre.HeaderText = "Nombre";
            this.dgvNombre.Name = "dgvNombre";
            this.dgvNombre.ReadOnly = true;
            // 
            // dgvUbicacion
            // 
            this.dgvUbicacion.DataPropertyName = "Ubicacion";
            this.dgvUbicacion.FillWeight = 80F;
            this.dgvUbicacion.HeaderText = "Ubicacion";
            this.dgvUbicacion.Name = "dgvUbicacion";
            this.dgvUbicacion.ReadOnly = true;
            
            // dgvMonto
             
            this.dgvMonto.DataPropertyName = "CostoBase";
            dataGridViewCellStyle2.Format = "C2";
            this.dgvMonto.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMonto.FillWeight = 60F;
            this.dgvMonto.HeaderText = "Monto";
            this.dgvMonto.Name = "dgvMonto";
            this.dgvMonto.ReadOnly = true;
            // 
            // gBoxSalones
            // 
            this.gBoxSalones.Controls.Add(this.btnLimpiar);
            this.gBoxSalones.Controls.Add(this.btnEliminarSalon);
            this.gBoxSalones.Controls.Add(this.btnModificarSalon);
            this.gBoxSalones.Controls.Add(this.btnCrearSalon);
            this.gBoxSalones.Controls.Add(this.lblMontoSalon);
            this.gBoxSalones.Controls.Add(this.txtBoxMonto);
            this.gBoxSalones.Controls.Add(this.lblUbicacionSalon);
            this.gBoxSalones.Controls.Add(this.txtBoxUbicacion);
            this.gBoxSalones.Controls.Add(this.lblNombreSalon);
            this.gBoxSalones.Controls.Add(this.txtBoxNombre);
            this.gBoxSalones.Location = new System.Drawing.Point(21, 20);
            this.gBoxSalones.Name = "gBoxSalones";
            this.gBoxSalones.Size = new System.Drawing.Size(300, 230);
            this.gBoxSalones.TabIndex = 1;
            this.gBoxSalones.TabStop = false;
            
            // btnLimpiar
            
            this.btnLimpiar.Location = new System.Drawing.Point(155, 180);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(125, 32);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminarSalon
            // 
            this.btnEliminarSalon.Location = new System.Drawing.Point(15, 180);
            this.btnEliminarSalon.Name = "btnEliminarSalon";
            this.btnEliminarSalon.Size = new System.Drawing.Size(125, 32);
            this.btnEliminarSalon.TabIndex = 8;
            this.btnEliminarSalon.Text = "Eliminar Salon";
            this.btnEliminarSalon.UseVisualStyleBackColor = true;
            this.btnEliminarSalon.Click += new System.EventHandler(this.btnEliminarSalon_Click);
            
            // btnModificarSalon
            
            this.btnModificarSalon.Location = new System.Drawing.Point(155, 135);
            this.btnModificarSalon.Name = "btnModificarSalon";
            this.btnModificarSalon.Size = new System.Drawing.Size(125, 32);
            this.btnModificarSalon.TabIndex = 7;
            this.btnModificarSalon.Text = "Modificar Salon";
            this.btnModificarSalon.UseVisualStyleBackColor = true;
            this.btnModificarSalon.Click += new System.EventHandler(this.btnModificarSalon_Click);
            // 
            // btnCrearSalon
            // 
            this.btnCrearSalon.Location = new System.Drawing.Point(15, 135);
            this.btnCrearSalon.Name = "btnCrearSalon";
            this.btnCrearSalon.Size = new System.Drawing.Size(125, 32);
            this.btnCrearSalon.TabIndex = 6;
            this.btnCrearSalon.Text = "Crear Salon";
            this.btnCrearSalon.UseVisualStyleBackColor = true;
            this.btnCrearSalon.Click += new System.EventHandler(this.btnCrearSalon_Click);
            
            // lblMontoSalon
            
            this.lblMontoSalon.AutoSize = true;
            this.lblMontoSalon.Location = new System.Drawing.Point(15, 97);
            this.lblMontoSalon.Name = "lblMontoSalon";
            this.lblMontoSalon.Size = new System.Drawing.Size(77, 13);
            this.lblMontoSalon.TabIndex = 5;
            this.lblMontoSalon.Text = "Ingrese monto:";
            // 
            // txtBoxMonto
            // 
            this.txtBoxMonto.Location = new System.Drawing.Point(125, 94);
            this.txtBoxMonto.Name = "txtBoxMonto";
            this.txtBoxMonto.Size = new System.Drawing.Size(155, 20);
            this.txtBoxMonto.TabIndex = 2;
            
            // lblUbicacionSalon
            
            this.lblUbicacionSalon.AutoSize = true;
            this.lblUbicacionSalon.Location = new System.Drawing.Point(15, 60);
            this.lblUbicacionSalon.Name = "lblUbicacionSalon";
            this.lblUbicacionSalon.Size = new System.Drawing.Size(94, 13);
            this.lblUbicacionSalon.TabIndex = 4;
            this.lblUbicacionSalon.Text = "Ingrese ubicacion:";
            
            // txtBoxUbicacion
            
            this.txtBoxUbicacion.Location = new System.Drawing.Point(125, 57);
            this.txtBoxUbicacion.Name = "txtBoxUbicacion";
            this.txtBoxUbicacion.Size = new System.Drawing.Size(155, 20);
            this.txtBoxUbicacion.TabIndex = 1;
            
            // lblNombreSalon
            
            this.lblNombreSalon.AutoSize = true;
            this.lblNombreSalon.Location = new System.Drawing.Point(15, 23);
            this.lblNombreSalon.Name = "lblNombreSalon";
            this.lblNombreSalon.Size = new System.Drawing.Size(83, 13);
            this.lblNombreSalon.TabIndex = 3;
            this.lblNombreSalon.Text = "Ingrese nombre:";
            
            // txtBoxNombre
            
            this.txtBoxNombre.Location = new System.Drawing.Point(125, 20);
            this.txtBoxNombre.Name = "txtBoxNombre";
            this.txtBoxNombre.Size = new System.Drawing.Size(155, 20);
            this.txtBoxNombre.TabIndex = 0;
            
            // SalonForm
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.gBoxSalones);
            this.Controls.Add(this.dgvSalonesCreados);
            this.Name = "SalonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Salones";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalonesCreados)).EndInit();
            this.gBoxSalones.ResumeLayout(false);
            this.gBoxSalones.PerformLayout();
            this.ResumeLayout(false);

        }

        

        private System.Windows.Forms.DataGridView dgvSalonesCreados;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvUbicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMonto;
        private System.Windows.Forms.GroupBox gBoxSalones;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminarSalon;
        private System.Windows.Forms.Button btnModificarSalon;
        private System.Windows.Forms.Button btnCrearSalon;
        private System.Windows.Forms.Label lblMontoSalon;
        private System.Windows.Forms.TextBox txtBoxMonto;
        private System.Windows.Forms.Label lblUbicacionSalon;
        private System.Windows.Forms.TextBox txtBoxUbicacion;
        private System.Windows.Forms.Label lblNombreSalon;
        private System.Windows.Forms.TextBox txtBoxNombre;
    }
}