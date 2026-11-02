using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace proyecto_integrador.pantallas
{
    partial class ConsultaPresupuestosForm
    {
        private IContainer components = null;
        private Panel _panelAcciones;
        private DataGridView _grilla;
        private TextBox _detalle;
        private Button _botonModificar;
        private Button _botonImprimirPresupuesto;
        private Button _botonVolverInicio;
        private DataGridViewTextBoxColumn _columnaId;
        private DataGridViewTextBoxColumn _columnaFechaHora;
        private DataGridViewTextBoxColumn _columnaCliente;
        private DataGridViewTextBoxColumn _columnaContacto;
        private DataGridViewTextBoxColumn _columnaSalon;
        private DataGridViewTextBoxColumn _columnaInvitados;
        private DataGridViewTextBoxColumn _columnaTotal;

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
            this.components = new Container();
            this._panelAcciones = new Panel();
            this._grilla = new DataGridView();
            this._detalle = new TextBox();
            this._botonModificar = new Button();
            this._botonImprimirPresupuesto = new Button();
            this._botonVolverInicio = new Button();
            this._columnaId = new DataGridViewTextBoxColumn();
            this._columnaFechaHora = new DataGridViewTextBoxColumn();
            this._columnaCliente = new DataGridViewTextBoxColumn();
            this._columnaContacto = new DataGridViewTextBoxColumn();
            this._columnaSalon = new DataGridViewTextBoxColumn();
            this._columnaInvitados = new DataGridViewTextBoxColumn();
            this._columnaTotal = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)(this._grilla)).BeginInit();
            this._panelAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelAcciones
            // 
            this._panelAcciones.BackColor = Color.FromArgb(246, 248, 251);
            this._panelAcciones.Controls.Add(this._botonImprimirPresupuesto);
            this._panelAcciones.Controls.Add(this._botonModificar);
            this._panelAcciones.Controls.Add(this._botonVolverInicio);
            this._panelAcciones.Dock = DockStyle.Top;
            this._panelAcciones.Height = 54;
            this._panelAcciones.Name = "_panelAcciones";
            this._panelAcciones.Size = new Size(1010, 54);
            // 
            // _botonImprimirPresupuesto
            // 
            this._botonImprimirPresupuesto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._botonImprimirPresupuesto.BackColor = Color.FromArgb(229, 237, 247);
            this._botonImprimirPresupuesto.FlatAppearance.BorderSize = 0;
            this._botonImprimirPresupuesto.FlatStyle = FlatStyle.Flat;
            this._botonImprimirPresupuesto.ForeColor = Color.FromArgb(48, 99, 157);
            this._botonImprimirPresupuesto.Location = new Point(650, 7);
            this._botonImprimirPresupuesto.Size = new Size(205, 38);
            this._botonImprimirPresupuesto.Text = "Imprimir presupuesto";
            this._botonImprimirPresupuesto.UseVisualStyleBackColor = false;
            // 
            // _botonModificar
            // 
            this._botonModificar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._botonModificar.BackColor = Color.FromArgb(48, 99, 157);
            this._botonModificar.FlatAppearance.BorderSize = 0;
            this._botonModificar.FlatStyle = FlatStyle.Flat;
            this._botonModificar.ForeColor = Color.White;
            this._botonModificar.Location = new Point(870, 7);
            this._botonModificar.Size = new Size(125, 38);
            this._botonModificar.Text = "Modificar";
            this._botonModificar.UseVisualStyleBackColor = false;
            // 
            // _botonVolverInicio
            // 
            this._botonVolverInicio.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._botonVolverInicio.BackColor = Color.FromArgb(230, 234, 239);
            this._botonVolverInicio.FlatAppearance.BorderSize = 0;
            this._botonVolverInicio.FlatStyle = FlatStyle.Flat;
            this._botonVolverInicio.ForeColor = Color.FromArgb(45, 55, 70);
            this._botonVolverInicio.Location = new Point(10, 7);
            this._botonVolverInicio.Size = new Size(155, 38);
            this._botonVolverInicio.Text = "Volver a Inicio";
            this._botonVolverInicio.UseVisualStyleBackColor = false;
            // 
            // _grilla
            // 
            this._grilla.AllowUserToAddRows = false;
            this._grilla.AllowUserToDeleteRows = false;
            this._grilla.AutoGenerateColumns = false;
            this._grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._grilla.BackgroundColor = Color.White;
            this._grilla.Columns.AddRange(new DataGridViewColumn[] {
                this._columnaId,
                this._columnaFechaHora,
                this._columnaCliente,
                this._columnaContacto,
                this._columnaSalon,
                this._columnaInvitados,
                this._columnaTotal});
            this._grilla.Dock = DockStyle.Top;
            this._grilla.Height = 290;
            this._grilla.MultiSelect = false;
            this._grilla.Name = "_grilla";
            this._grilla.ReadOnly = true;
            this._grilla.RowHeadersVisible = false;
            this._grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // 
            // Grid columns
            // 
            this._columnaId.DataPropertyName = "Id";
            this._columnaId.FillWeight = 55F;
            this._columnaId.HeaderText = "N°";
            this._columnaId.Name = "columnaId";
            this._columnaId.ReadOnly = true;
            this._columnaFechaHora.DataPropertyName = "FechaHoraEvento";
            this._columnaFechaHora.FillWeight = 130F;
            this._columnaFechaHora.HeaderText = "Fecha y hora";
            this._columnaFechaHora.Name = "columnaFechaHoraEvento";
            this._columnaFechaHora.ReadOnly = true;
            this._columnaCliente.DataPropertyName = "NombreContacto";
            this._columnaCliente.FillWeight = 120F;
            this._columnaCliente.HeaderText = "Cliente";
            this._columnaCliente.Name = "columnaNombreContacto";
            this._columnaCliente.ReadOnly = true;
            this._columnaContacto.DataPropertyName = "EmailContacto";
            this._columnaContacto.FillWeight = 190F;
            this._columnaContacto.HeaderText = "Contacto";
            this._columnaContacto.Name = "columnaEmailContacto";
            this._columnaContacto.ReadOnly = true;
            this._columnaSalon.DataPropertyName = "NombreSalon";
            this._columnaSalon.FillWeight = 120F;
            this._columnaSalon.HeaderText = "Salón";
            this._columnaSalon.Name = "columnaNombreSalon";
            this._columnaSalon.ReadOnly = true;
            this._columnaInvitados.DataPropertyName = "CantidadTotalInvitados";
            this._columnaInvitados.FillWeight = 85F;
            this._columnaInvitados.HeaderText = "Invitados";
            this._columnaInvitados.Name = "columnaCantidadTotalInvitados";
            this._columnaInvitados.ReadOnly = true;
            this._columnaTotal.DataPropertyName = "TotalFinal";
            this._columnaTotal.DefaultCellStyle.Format = "C2";
            this._columnaTotal.FillWeight = 115F;
            this._columnaTotal.HeaderText = "Total";
            this._columnaTotal.Name = "columnaTotalFinal";
            this._columnaTotal.ReadOnly = true;
            // 
            // _detalle
            // 
            this._detalle.BackColor = Color.White;
            this._detalle.Dock = DockStyle.Fill;
            this._detalle.Font = new Font("Segoe UI", 9F);
            this._detalle.Multiline = true;
            this._detalle.Name = "_detalle";
            this._detalle.ReadOnly = true;
            this._detalle.ScrollBars = ScrollBars.Vertical;
            // 
            // ConsultaPresupuestosForm
            // 
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(246, 248, 251);
            this.ClientSize = new Size(1010, 680);
            this.Controls.Add(this._detalle);
            this.Controls.Add(this._grilla);
            this.Controls.Add(this._panelAcciones);
            this.Font = new Font("Segoe UI", 9F);
            this.Name = "ConsultaPresupuestosForm";
            this.Text = "Historial de presupuestos";
            ((ISupportInitialize)(this._grilla)).EndInit();
            this._panelAcciones.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
