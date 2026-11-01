namespace proyecto_integrador.pantallas
{
    partial class SolapaIzquierdaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Panel panelMarca;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Label lblNombreAplicacion;
        private System.Windows.Forms.Label lblSubtituloAplicacion;
        private System.Windows.Forms.Label lblTituloSeccion;
        private System.Windows.Forms.Label lblDescripcionSeccion;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.Button btnSalon;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnServiciosAdicionales;

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
            this.panelLateral = new System.Windows.Forms.Panel();
            this.btnServiciosAdicionales = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnSalon = new System.Windows.Forms.Button();
            this.btnInicio = new System.Windows.Forms.Button();
            this.panelMarca = new System.Windows.Forms.Panel();
            this.lblSubtituloAplicacion = new System.Windows.Forms.Label();
            this.lblNombreAplicacion = new System.Windows.Forms.Label();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblDescripcionSeccion = new System.Windows.Forms.Label();
            this.lblTituloSeccion = new System.Windows.Forms.Label();
            this.panelLateral.SuspendLayout();
            this.panelMarca.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            this.panelEncabezado.SuspendLayout();
            this.SuspendLayout();
            //
            // panelLateral
            //
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(25, 35, 52);
            this.panelLateral.Controls.Add(this.btnServiciosAdicionales);
            this.panelLateral.Controls.Add(this.btnMenu);
            this.panelLateral.Controls.Add(this.btnSalon);
            this.panelLateral.Controls.Add(this.btnInicio);
            this.panelLateral.Controls.Add(this.panelMarca);
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(230, 760);
            this.panelLateral.TabIndex = 0;
            //
            // btnServiciosAdicionales
            //
            this.btnServiciosAdicionales.BackColor = System.Drawing.Color.FromArgb(25, 35, 52);
            this.btnServiciosAdicionales.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnServiciosAdicionales.FlatAppearance.BorderSize = 0;
            this.btnServiciosAdicionales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServiciosAdicionales.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnServiciosAdicionales.ForeColor = System.Drawing.Color.FromArgb(225, 231, 239);
            this.btnServiciosAdicionales.Location = new System.Drawing.Point(0, 284);
            this.btnServiciosAdicionales.Name = "btnServiciosAdicionales";
            this.btnServiciosAdicionales.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnServiciosAdicionales.Size = new System.Drawing.Size(230, 54);
            this.btnServiciosAdicionales.TabIndex = 4;
            this.btnServiciosAdicionales.Text = "Servicios adicionales";
            this.btnServiciosAdicionales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnServiciosAdicionales.UseVisualStyleBackColor = false;
            this.btnServiciosAdicionales.Click += new System.EventHandler(this.btnServiciosAdicionales_Click);
            //
            // btnMenu
            //
            this.btnMenu.BackColor = System.Drawing.Color.FromArgb(25, 35, 52);
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnMenu.ForeColor = System.Drawing.Color.FromArgb(225, 231, 239);
            this.btnMenu.Location = new System.Drawing.Point(0, 230);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnMenu.Size = new System.Drawing.Size(230, 54);
            this.btnMenu.TabIndex = 3;
            this.btnMenu.Text = "Menú";
            this.btnMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenu.UseVisualStyleBackColor = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            //
            // btnSalon
            //
            this.btnSalon.BackColor = System.Drawing.Color.FromArgb(25, 35, 52);
            this.btnSalon.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSalon.FlatAppearance.BorderSize = 0;
            this.btnSalon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSalon.ForeColor = System.Drawing.Color.FromArgb(225, 231, 239);
            this.btnSalon.Location = new System.Drawing.Point(0, 176);
            this.btnSalon.Name = "btnSalon";
            this.btnSalon.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnSalon.Size = new System.Drawing.Size(230, 54);
            this.btnSalon.TabIndex = 2;
            this.btnSalon.Text = "Salón";
            this.btnSalon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalon.UseVisualStyleBackColor = false;
            this.btnSalon.Click += new System.EventHandler(this.btnSalon_Click);
            //
            // btnInicio
            //
            this.btnInicio.BackColor = System.Drawing.Color.FromArgb(25, 35, 52);
            this.btnInicio.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInicio.FlatAppearance.BorderSize = 0;
            this.btnInicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInicio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnInicio.ForeColor = System.Drawing.Color.FromArgb(225, 231, 239);
            this.btnInicio.Location = new System.Drawing.Point(0, 122);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnInicio.Size = new System.Drawing.Size(230, 54);
            this.btnInicio.TabIndex = 1;
            this.btnInicio.Text = "Inicio";
            this.btnInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInicio.UseVisualStyleBackColor = false;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            //
            // panelMarca
            //
            this.panelMarca.Controls.Add(this.lblSubtituloAplicacion);
            this.panelMarca.Controls.Add(this.lblNombreAplicacion);
            this.panelMarca.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMarca.Location = new System.Drawing.Point(0, 0);
            this.panelMarca.Name = "panelMarca";
            this.panelMarca.Size = new System.Drawing.Size(230, 122);
            this.panelMarca.TabIndex = 0;
            //
            // lblSubtituloAplicacion
            //
            this.lblSubtituloAplicacion.AutoSize = true;
            this.lblSubtituloAplicacion.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtituloAplicacion.ForeColor = System.Drawing.Color.FromArgb(164, 177, 196);
            this.lblSubtituloAplicacion.Location = new System.Drawing.Point(22, 66);
            this.lblSubtituloAplicacion.Name = "lblSubtituloAplicacion";
            this.lblSubtituloAplicacion.Size = new System.Drawing.Size(143, 15);
            this.lblSubtituloAplicacion.TabIndex = 1;
            this.lblSubtituloAplicacion.Text = "PRESUPUESTOS DE EVENTOS";
            //
            // lblNombreAplicacion
            //
            this.lblNombreAplicacion.AutoSize = true;
            this.lblNombreAplicacion.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblNombreAplicacion.ForeColor = System.Drawing.Color.White;
            this.lblNombreAplicacion.Location = new System.Drawing.Point(18, 27);
            this.lblNombreAplicacion.Name = "lblNombreAplicacion";
            this.lblNombreAplicacion.Size = new System.Drawing.Size(119, 32);
            this.lblNombreAplicacion.TabIndex = 0;
            this.lblNombreAplicacion.Text = "Eventos";
            //
            // panelPrincipal
            //
            this.panelPrincipal.BackColor = System.Drawing.Color.FromArgb(246, 248, 251);
            this.panelPrincipal.Controls.Add(this.panelContenido);
            this.panelPrincipal.Controls.Add(this.panelEncabezado);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.TabIndex = 1;
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(246, 248, 251);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(22);
            this.panelContenido.TabIndex = 1;
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = System.Drawing.Color.White;
            this.panelEncabezado.Controls.Add(this.lblDescripcionSeccion);
            this.panelEncabezado.Controls.Add(this.lblTituloSeccion);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(28, 13, 0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1050, 88);
            this.panelEncabezado.TabIndex = 0;
            //
            // lblDescripcionSeccion
            //
            this.lblDescripcionSeccion.AutoSize = true;
            this.lblDescripcionSeccion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescripcionSeccion.ForeColor = System.Drawing.Color.FromArgb(104, 116, 132);
            this.lblDescripcionSeccion.Location = new System.Drawing.Point(31, 51);
            this.lblDescripcionSeccion.Name = "lblDescripcionSeccion";
            this.lblDescripcionSeccion.Size = new System.Drawing.Size(0, 15);
            this.lblDescripcionSeccion.TabIndex = 1;
            //
            // lblTituloSeccion
            //
            this.lblTituloSeccion.AutoSize = true;
            this.lblTituloSeccion.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.lblTituloSeccion.ForeColor = System.Drawing.Color.FromArgb(35, 45, 61);
            this.lblTituloSeccion.Location = new System.Drawing.Point(27, 14);
            this.lblTituloSeccion.Name = "lblTituloSeccion";
            this.lblTituloSeccion.Size = new System.Drawing.Size(0, 31);
            this.lblTituloSeccion.TabIndex = 0;
            //
            // SolapaIzquierdaForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(246, 248, 251);
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panelLateral);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1050, 650);
            this.Name = "SolapaIzquierdaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Presupuestos para Eventos";
            this.panelLateral.ResumeLayout(false);
            this.panelMarca.ResumeLayout(false);
            this.panelMarca.PerformLayout();
            this.panelPrincipal.ResumeLayout(false);
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
