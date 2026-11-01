namespace proyecto_integrador.pantallas
{
    partial class MenuDeComidaForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMenusCreados = new System.Windows.Forms.DataGridView();
            this.dgvID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCostoAdulto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCostoNino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gBoxMenus = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminarMenu = new System.Windows.Forms.Button();
            this.btnModificarMenu = new System.Windows.Forms.Button();
            this.btnCrearMenu = new System.Windows.Forms.Button();
            this.lblCostoNino = new System.Windows.Forms.Label();
            this.txtBoxCostoNino = new System.Windows.Forms.TextBox();
            this.lblCostoAdulto = new System.Windows.Forms.Label();
            this.txtBoxCostoAdulto = new System.Windows.Forms.TextBox();
            this.lblNombreMenu = new System.Windows.Forms.Label();
            this.txtBoxNombre = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenusCreados)).BeginInit();
            this.gBoxMenus.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMenusCreados
            // 
            this.dgvMenusCreados.AllowUserToAddRows = false;
            this.dgvMenusCreados.AllowUserToDeleteRows = false;
            this.dgvMenusCreados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMenusCreados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMenusCreados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenusCreados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvID,
            this.dgvNombre,
            this.dgvCostoAdulto,
            this.dgvCostoNino});
            this.dgvMenusCreados.Location = new System.Drawing.Point(340, 20);
            this.dgvMenusCreados.MultiSelect = false;
            this.dgvMenusCreados.Name = "dgvMenusCreados";
            this.dgvMenusCreados.ReadOnly = true;
            this.dgvMenusCreados.RowHeadersVisible = false;
            this.dgvMenusCreados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMenusCreados.Size = new System.Drawing.Size(420, 380);
            this.dgvMenusCreados.TabIndex = 0;
            this.dgvMenusCreados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMenusCreados_CellClick);
            // 
            // dgvID
            // 
            this.dgvID.DataPropertyName = "Id";
            this.dgvID.FillWeight = 20F;
            this.dgvID.HeaderText = "ID";
            this.dgvID.Name = "dgvID";
            this.dgvID.ReadOnly = true;
            // 
            // dgvNombre
            // 
            this.dgvNombre.DataPropertyName = "Nombre";
            this.dgvNombre.FillWeight = 80F;
            this.dgvNombre.HeaderText = "Nombre";
            this.dgvNombre.Name = "dgvNombre";
            this.dgvNombre.ReadOnly = true;
            // 
            // dgvCostoAdulto
            // 
            this.dgvCostoAdulto.DataPropertyName = "CostoPorAdulto";
            dataGridViewCellStyle1.Format = "C2";
            this.dgvCostoAdulto.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCostoAdulto.FillWeight = 60F;
            this.dgvCostoAdulto.HeaderText = "Costo por adulto";
            this.dgvCostoAdulto.Name = "dgvCostoAdulto";
            this.dgvCostoAdulto.ReadOnly = true;
            // 
            // dgvCostoNino
            // 
            this.dgvCostoNino.DataPropertyName = "CostoPorNiño";
            dataGridViewCellStyle2.Format = "C2";
            this.dgvCostoNino.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCostoNino.FillWeight = 60F;
            this.dgvCostoNino.HeaderText = "Costo por niño";
            this.dgvCostoNino.Name = "dgvCostoNino";
            this.dgvCostoNino.ReadOnly = true;
            // 
            // gBoxMenus
            // 
            this.gBoxMenus.Controls.Add(this.btnLimpiar);
            this.gBoxMenus.Controls.Add(this.btnEliminarMenu);
            this.gBoxMenus.Controls.Add(this.btnModificarMenu);
            this.gBoxMenus.Controls.Add(this.btnCrearMenu);
            this.gBoxMenus.Controls.Add(this.lblCostoNino);
            this.gBoxMenus.Controls.Add(this.txtBoxCostoNino);
            this.gBoxMenus.Controls.Add(this.lblCostoAdulto);
            this.gBoxMenus.Controls.Add(this.txtBoxCostoAdulto);
            this.gBoxMenus.Controls.Add(this.lblNombreMenu);
            this.gBoxMenus.Controls.Add(this.txtBoxNombre);
            this.gBoxMenus.Location = new System.Drawing.Point(21, 20);
            this.gBoxMenus.Name = "gBoxMenus";
            this.gBoxMenus.Size = new System.Drawing.Size(300, 230);
            this.gBoxMenus.TabIndex = 1;
            this.gBoxMenus.TabStop = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(155, 180);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(125, 32);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminarMenu
            // 
            this.btnEliminarMenu.Location = new System.Drawing.Point(15, 180);
            this.btnEliminarMenu.Name = "btnEliminarMenu";
            this.btnEliminarMenu.Size = new System.Drawing.Size(125, 32);
            this.btnEliminarMenu.TabIndex = 8;
            this.btnEliminarMenu.Text = "Eliminar Menu";
            this.btnEliminarMenu.UseVisualStyleBackColor = true;
            this.btnEliminarMenu.Click += new System.EventHandler(this.btnEliminarMenu_Click);
            // 
            // btnModificarMenu
            // 
            this.btnModificarMenu.Location = new System.Drawing.Point(155, 135);
            this.btnModificarMenu.Name = "btnModificarMenu";
            this.btnModificarMenu.Size = new System.Drawing.Size(125, 32);
            this.btnModificarMenu.TabIndex = 7;
            this.btnModificarMenu.Text = "Modificar Menu";
            this.btnModificarMenu.UseVisualStyleBackColor = true;
            this.btnModificarMenu.Click += new System.EventHandler(this.btnModificarMenu_Click);
            // 
            // btnCrearMenu
            // 
            this.btnCrearMenu.Location = new System.Drawing.Point(15, 135);
            this.btnCrearMenu.Name = "btnCrearMenu";
            this.btnCrearMenu.Size = new System.Drawing.Size(125, 32);
            this.btnCrearMenu.TabIndex = 6;
            this.btnCrearMenu.Text = "Crear Menu";
            this.btnCrearMenu.UseVisualStyleBackColor = true;
            this.btnCrearMenu.Click += new System.EventHandler(this.btnCrearMenu_Click);
            // 
            // lblCostoNino
            // 
            this.lblCostoNino.AutoSize = true;
            this.lblCostoNino.Location = new System.Drawing.Point(15, 97);
            this.lblCostoNino.Name = "lblCostoNino";
            this.lblCostoNino.Size = new System.Drawing.Size(86, 13);
            this.lblCostoNino.TabIndex = 5;
            this.lblCostoNino.Text = "Costo por niño:";
            // 
            // txtBoxCostoNino
            // 
            this.txtBoxCostoNino.Location = new System.Drawing.Point(125, 94);
            this.txtBoxCostoNino.Name = "txtBoxCostoNino";
            this.txtBoxCostoNino.Size = new System.Drawing.Size(155, 20);
            this.txtBoxCostoNino.TabIndex = 2;
            // 
            // lblCostoAdulto
            // 
            this.lblCostoAdulto.AutoSize = true;
            this.lblCostoAdulto.Location = new System.Drawing.Point(15, 60);
            this.lblCostoAdulto.Name = "lblCostoAdulto";
            this.lblCostoAdulto.Size = new System.Drawing.Size(93, 13);
            this.lblCostoAdulto.TabIndex = 4;
            this.lblCostoAdulto.Text = "Costo por adulto:";
            // 
            // txtBoxCostoAdulto
            // 
            this.txtBoxCostoAdulto.Location = new System.Drawing.Point(125, 57);
            this.txtBoxCostoAdulto.Name = "txtBoxCostoAdulto";
            this.txtBoxCostoAdulto.Size = new System.Drawing.Size(155, 20);
            this.txtBoxCostoAdulto.TabIndex = 1;
            // 
            // lblNombreMenu
            // 
            this.lblNombreMenu.AutoSize = true;
            this.lblNombreMenu.Location = new System.Drawing.Point(15, 23);
            this.lblNombreMenu.Name = "lblNombreMenu";
            this.lblNombreMenu.Size = new System.Drawing.Size(85, 13);
            this.lblNombreMenu.TabIndex = 3;
            this.lblNombreMenu.Text = "Ingrese nombre:";
            // 
            // txtBoxNombre
            // 
            this.txtBoxNombre.Location = new System.Drawing.Point(125, 20);
            this.txtBoxNombre.Name = "txtBoxNombre";
            this.txtBoxNombre.Size = new System.Drawing.Size(155, 20);
            this.txtBoxNombre.TabIndex = 0;
            // 
            // MenuDeComidaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.gBoxMenus);
            this.Controls.Add(this.dgvMenusCreados);
            this.Name = "MenuDeComidaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menus";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenusCreados)).EndInit();
            this.gBoxMenus.ResumeLayout(false);
            this.gBoxMenus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMenusCreados;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCostoAdulto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCostoNino;
        private System.Windows.Forms.GroupBox gBoxMenus;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminarMenu;
        private System.Windows.Forms.Button btnModificarMenu;
        private System.Windows.Forms.Button btnCrearMenu;
        private System.Windows.Forms.Label lblCostoNino;
        private System.Windows.Forms.TextBox txtBoxCostoNino;
        private System.Windows.Forms.Label lblCostoAdulto;
        private System.Windows.Forms.TextBox txtBoxCostoAdulto;
        private System.Windows.Forms.Label lblNombreMenu;
        private System.Windows.Forms.TextBox txtBoxNombre;
    }
}