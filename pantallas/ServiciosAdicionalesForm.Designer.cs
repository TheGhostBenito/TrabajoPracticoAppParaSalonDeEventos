namespace proyecto_integrador.pantallas
{
    partial class ServiciosAdicionalesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvServiciosCreados = new System.Windows.Forms.DataGridView();
            this.dgvID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gdvDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiciosCreados)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvServiciosCreados
            // 
            this.dgvServiciosCreados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiciosCreados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvID,
            this.gdvDescripcion,
            this.dgvMonto});
            this.dgvServiciosCreados.Location = new System.Drawing.Point(518, 56);
            this.dgvServiciosCreados.Name = "dgvServiciosCreados";
            this.dgvServiciosCreados.Size = new System.Drawing.Size(414, 433);
            this.dgvServiciosCreados.TabIndex = 0;
            // 
            // dgvID
            // 
            this.dgvID.HeaderText = "ID";
            this.dgvID.Name = "dgvID";
            // 
            // gdvDescripcion
            // 
            this.gdvDescripcion.HeaderText = "Descripcion";
            this.gdvDescripcion.Name = "gdvDescripcion";
            // 
            // dgvMonto
            // 
            this.dgvMonto.HeaderText = "Monto";
            this.dgvMonto.Name = "dgvMonto";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(27, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // ServiciosAdicionalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvServiciosCreados);
            this.Name = "ServiciosAdicionalesForm";
            this.Text = "form";
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiciosCreados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvServiciosCreados;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gdvDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMonto;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}