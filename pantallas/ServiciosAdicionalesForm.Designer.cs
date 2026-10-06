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
            this.gBoxServiciosAdicionales = new System.Windows.Forms.GroupBox();
            this.txtBoxDescripcion = new System.Windows.Forms.TextBox();
            this.descripcionServiciosAdicionales = new System.Windows.Forms.Label();
            this.txtBoxMonto = new System.Windows.Forms.TextBox();
            this.lblMontoServicios = new System.Windows.Forms.Label();
            this.btnCrearServicio = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiciosCreados)).BeginInit();
            this.gBoxServiciosAdicionales.SuspendLayout();
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
            // gBoxServiciosAdicionales
            // 
            this.gBoxServiciosAdicionales.Controls.Add(this.btnCrearServicio);
            this.gBoxServiciosAdicionales.Controls.Add(this.lblMontoServicios);
            this.gBoxServiciosAdicionales.Controls.Add(this.txtBoxMonto);
            this.gBoxServiciosAdicionales.Controls.Add(this.descripcionServiciosAdicionales);
            this.gBoxServiciosAdicionales.Controls.Add(this.txtBoxDescripcion);
            this.gBoxServiciosAdicionales.Location = new System.Drawing.Point(21, 56);
            this.gBoxServiciosAdicionales.Name = "gBoxServiciosAdicionales";
            this.gBoxServiciosAdicionales.Size = new System.Drawing.Size(234, 218);
            this.gBoxServiciosAdicionales.TabIndex = 1;
            this.gBoxServiciosAdicionales.TabStop = false;
            // 
            // txtBoxDescripcion
            // 
            this.txtBoxDescripcion.Location = new System.Drawing.Point(128, 19);
            this.txtBoxDescripcion.Name = "txtBoxDescripcion";
            this.txtBoxDescripcion.Size = new System.Drawing.Size(100, 20);
            this.txtBoxDescripcion.TabIndex = 0;
            // 
            // descripcionServiciosAdicionales
            // 
            this.descripcionServiciosAdicionales.AutoSize = true;
            this.descripcionServiciosAdicionales.Location = new System.Drawing.Point(20, 22);
            this.descripcionServiciosAdicionales.Name = "descripcionServiciosAdicionales";
            this.descripcionServiciosAdicionales.Size = new System.Drawing.Size(102, 13);
            this.descripcionServiciosAdicionales.TabIndex = 1;
            this.descripcionServiciosAdicionales.Text = "Ingrese descripcion:";
            // 
            // txtBoxMonto
            // 
            this.txtBoxMonto.Location = new System.Drawing.Point(128, 59);
            this.txtBoxMonto.Name = "txtBoxMonto";
            this.txtBoxMonto.Size = new System.Drawing.Size(100, 20);
            this.txtBoxMonto.TabIndex = 2;
            // 
            // lblMontoServicios
            // 
            this.lblMontoServicios.AutoSize = true;
            this.lblMontoServicios.Location = new System.Drawing.Point(45, 62);
            this.lblMontoServicios.Name = "lblMontoServicios";
            this.lblMontoServicios.Size = new System.Drawing.Size(77, 13);
            this.lblMontoServicios.TabIndex = 3;
            this.lblMontoServicios.Text = "Ingrese monto:";
            // 
            // btnCrearServicio
            // 
            this.btnCrearServicio.Location = new System.Drawing.Point(128, 110);
            this.btnCrearServicio.Name = "btnCrearServicio";
            this.btnCrearServicio.Size = new System.Drawing.Size(99, 33);
            this.btnCrearServicio.TabIndex = 4;
            this.btnCrearServicio.Text = "Crear Servicio";
            this.btnCrearServicio.UseVisualStyleBackColor = true;
            // 
            // ServiciosAdicionalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.gBoxServiciosAdicionales);
            this.Controls.Add(this.dgvServiciosCreados);
            this.Name = "ServiciosAdicionalesForm";
            this.Text = "form";
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiciosCreados)).EndInit();
            this.gBoxServiciosAdicionales.ResumeLayout(false);
            this.gBoxServiciosAdicionales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvServiciosCreados;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gdvDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMonto;
        private System.Windows.Forms.GroupBox gBoxServiciosAdicionales;
        private System.Windows.Forms.TextBox txtBoxMonto;
        private System.Windows.Forms.Label descripcionServiciosAdicionales;
        private System.Windows.Forms.TextBox txtBoxDescripcion;
        private System.Windows.Forms.Button btnCrearServicio;
        private System.Windows.Forms.Label lblMontoServicios;
    }
}