using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using proyecto_integrador.models;

namespace proyecto_integrador.pantallas
{
    partial class PresupuestoForm
    {
        private IContainer components = null;
        private Panel _panelDatos;
        private Panel _panelPie;
        private Panel _panelAccion;
        private GroupBox _grupoContactoEvento;
        private GroupBox _grupoMenus;
        private GroupBox _grupoServicios;
        private GroupBox _grupoObservaciones;
        private ComboBox _comboSalon;
        private TextBox _textoUbicacion;
        private DateTimePicker _selectorFecha;
        private DateTimePicker _selectorHoraInicio;
        private TextBox _textoNombreContacto;
        private TextBox _textoEmail;
        private TextBox _textoTelefono;
        private TextBox _textoObservaciones;
        private NumericUpDown _numeroDuracion;
        private NumericUpDown _numeroAdultos;
        private NumericUpDown _numeroNinos;
        private NumericUpDown _porcentajeMenuPrincipal;
        private NumericUpDown _porcentajeMenuPersonalizado;
        private DataGridView _grillaMenusPrincipales;
        private DataGridView _grillaMenusPersonalizados;
        private DataGridViewComboBoxColumn _columnaMenuPrincipal;
        private DataGridViewComboBoxColumn _columnaParticularidad;
        private DataGridViewComboBoxColumn _columnaMenuPersonalizado;
        private DataGridViewTextBoxColumn _columnaAdultosMenuPrincipal;
        private DataGridViewTextBoxColumn _columnaNinosMenuPrincipal;
        private DataGridViewTextBoxColumn _columnaAdultosMenuPersonalizado;
        private DataGridViewTextBoxColumn _columnaNinosMenuPersonalizado;
        private CheckedListBox _listaServiciosDisponibles;
        private ListBox _listaServiciosPersonalizados;
        private TextBox _textoServicioPersonalizado;
        private TextBox _textoCostoPersonalizado;
        private Label _etiquetaTotal;
        private Label _etiquetaAyudaMenus;
        private Button _botonAgregarMenuPrincipal;
        private Button _botonQuitarMenuPrincipal;
        private Button _botonAgregarMenuPersonalizado;
        private Button _botonQuitarMenuPersonalizado;
        private Button _botonAgregarServicio;
        private Button _botonQuitarServicio;
        private Button _botonGuardar;

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
            this._panelDatos = new Panel();
            this._panelPie = new Panel();
            this._panelAccion = new Panel();
            this._grupoContactoEvento = new GroupBox();
            this._grupoMenus = new GroupBox();
            this._grupoServicios = new GroupBox();
            this._grupoObservaciones = new GroupBox();
            this._comboSalon = new ComboBox();
            this._textoUbicacion = new TextBox();
            this._selectorFecha = new DateTimePicker();
            this._selectorHoraInicio = new DateTimePicker();
            this._textoNombreContacto = new TextBox();
            this._textoEmail = new TextBox();
            this._textoTelefono = new TextBox();
            this._textoObservaciones = new TextBox();
            this._numeroDuracion = new NumericUpDown();
            this._numeroAdultos = new NumericUpDown();
            this._numeroNinos = new NumericUpDown();
            this._porcentajeMenuPrincipal = new NumericUpDown();
            this._porcentajeMenuPersonalizado = new NumericUpDown();
            this._grillaMenusPrincipales = new DataGridView();
            this._grillaMenusPersonalizados = new DataGridView();
            this._columnaMenuPrincipal = new DataGridViewComboBoxColumn();
            this._columnaParticularidad = new DataGridViewComboBoxColumn();
            this._columnaMenuPersonalizado = new DataGridViewComboBoxColumn();
            this._columnaAdultosMenuPrincipal = new DataGridViewTextBoxColumn();
            this._columnaNinosMenuPrincipal = new DataGridViewTextBoxColumn();
            this._columnaAdultosMenuPersonalizado = new DataGridViewTextBoxColumn();
            this._columnaNinosMenuPersonalizado = new DataGridViewTextBoxColumn();
            this._listaServiciosDisponibles = new CheckedListBox();
            this._listaServiciosPersonalizados = new ListBox();
            this._textoServicioPersonalizado = new TextBox();
            this._textoCostoPersonalizado = new TextBox();
            this._etiquetaTotal = new Label();
            this._etiquetaAyudaMenus = new Label();
            this._botonAgregarMenuPrincipal = new Button();
            this._botonQuitarMenuPrincipal = new Button();
            this._botonAgregarMenuPersonalizado = new Button();
            this._botonQuitarMenuPersonalizado = new Button();
            this._botonAgregarServicio = new Button();
            this._botonQuitarServicio = new Button();
            this._botonGuardar = new Button();
            this._lblNombre = new Label();
            this._lblCorreo = new Label();
            this._lblTelefono = new Label();
            this._lblFecha = new Label();
            this._lblHoraInicio = new Label();
            this._lblDuracion = new Label();
            this._lblSalon = new Label();
            this._lblUbicacion = new Label();
            this._lblAyudaUbicacion = new Label();
            this._lblAdultos = new Label();
            this._lblNinos = new Label();
            this._lblPorcentajePrincipal = new Label();
            this._lblPorcentajePersonalizado = new Label();
            this._lblTituloMenusPrincipales = new Label();
            this._lblTituloMenusPersonalizados = new Label();
            this._lblServiciosCatalogo = new Label();
            this._lblServicioPersonalizado = new Label();
            this._lblCostoFijo = new Label();
            ((ISupportInitialize)(this._numeroDuracion)).BeginInit();
            ((ISupportInitialize)(this._numeroAdultos)).BeginInit();
            ((ISupportInitialize)(this._numeroNinos)).BeginInit();
            ((ISupportInitialize)(this._porcentajeMenuPrincipal)).BeginInit();
            ((ISupportInitialize)(this._porcentajeMenuPersonalizado)).BeginInit();
            ((ISupportInitialize)(this._grillaMenusPrincipales)).BeginInit();
            ((ISupportInitialize)(this._grillaMenusPersonalizados)).BeginInit();
            this._panelDatos.SuspendLayout();
            this._panelPie.SuspendLayout();
            this._panelAccion.SuspendLayout();
            this._grupoContactoEvento.SuspendLayout();
            this._grupoMenus.SuspendLayout();
            this._grupoServicios.SuspendLayout();
            this._grupoObservaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelDatos
            // 
            this._panelDatos.AutoScroll = true;
            this._panelDatos.AutoScrollMinSize = new Size(0, 970);
            this._panelDatos.BackColor = Color.FromArgb(246, 248, 251);
            this._panelDatos.Controls.Add(this._grupoObservaciones);
            this._panelDatos.Controls.Add(this._grupoServicios);
            this._panelDatos.Controls.Add(this._grupoMenus);
            this._panelDatos.Controls.Add(this._grupoContactoEvento);
            this._panelDatos.Dock = DockStyle.Fill;
            this._panelDatos.Name = "_panelDatos";
            // 
            // _grupoContactoEvento
            // 
            this._grupoContactoEvento.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._grupoContactoEvento.BackColor = Color.White;
            this._grupoContactoEvento.Location = new Point(18, 18);
            this._grupoContactoEvento.Name = "_grupoContactoEvento";
            this._grupoContactoEvento.Size = new Size(960, 220);
            this._grupoContactoEvento.TabStop = false;
            this._grupoContactoEvento.Text = "Contacto y evento";
            this._grupoContactoEvento.Controls.Add(this._textoNombreContacto);
            this._grupoContactoEvento.Controls.Add(this._textoEmail);
            this._grupoContactoEvento.Controls.Add(this._textoTelefono);
            this._grupoContactoEvento.Controls.Add(this._selectorFecha);
            this._grupoContactoEvento.Controls.Add(this._selectorHoraInicio);
            this._grupoContactoEvento.Controls.Add(this._numeroDuracion);
            this._grupoContactoEvento.Controls.Add(this._comboSalon);
            this._grupoContactoEvento.Controls.Add(this._textoUbicacion);
            this._grupoContactoEvento.Controls.Add(this._lblNombre);
            this._grupoContactoEvento.Controls.Add(this._lblCorreo);
            this._grupoContactoEvento.Controls.Add(this._lblTelefono);
            this._grupoContactoEvento.Controls.Add(this._lblFecha);
            this._grupoContactoEvento.Controls.Add(this._lblHoraInicio);
            this._grupoContactoEvento.Controls.Add(this._lblDuracion);
            this._grupoContactoEvento.Controls.Add(this._lblSalon);
            this._grupoContactoEvento.Controls.Add(this._lblUbicacion);
            this._grupoContactoEvento.Controls.Add(this._lblAyudaUbicacion);
            // 
            // Contacto y evento controls
            // 
            this._lblNombre.Location = new Point(18, 30);
            this._lblNombre.Size = new Size(110, 24);
            this._lblNombre.Text = "Nombre";
            this._lblNombre.TextAlign = ContentAlignment.MiddleLeft;
            this._lblCorreo.Location = new Point(420, 30);
            this._lblCorreo.Size = new Size(80, 24);
            this._lblCorreo.Text = "Correo";
            this._lblCorreo.TextAlign = ContentAlignment.MiddleLeft;
            this._lblTelefono.Location = new Point(18, 72);
            this._lblTelefono.Size = new Size(110, 24);
            this._lblTelefono.Text = "Teléfono";
            this._lblTelefono.TextAlign = ContentAlignment.MiddleLeft;
            this._lblFecha.Location = new Point(335, 72);
            this._lblFecha.Size = new Size(110, 24);
            this._lblFecha.Text = "Fecha del evento";
            this._lblFecha.TextAlign = ContentAlignment.MiddleLeft;
            this._lblHoraInicio.Location = new Point(625, 72);
            this._lblHoraInicio.Size = new Size(100, 24);
            this._lblHoraInicio.Text = "Hora de inicio";
            this._lblHoraInicio.TextAlign = ContentAlignment.MiddleLeft;
            this._lblDuracion.Location = new Point(18, 114);
            this._lblDuracion.Size = new Size(110, 24);
            this._lblDuracion.Text = "Duración (horas)";
            this._lblDuracion.TextAlign = ContentAlignment.MiddleLeft;
            this._lblSalon.Location = new Point(255, 114);
            this._lblSalon.Size = new Size(55, 24);
            this._lblSalon.Text = "Salón";
            this._lblSalon.TextAlign = ContentAlignment.MiddleLeft;
            this._lblUbicacion.Location = new Point(625, 114);
            this._lblUbicacion.Size = new Size(100, 24);
            this._lblUbicacion.Text = "Ubicación";
            this._lblUbicacion.TextAlign = ContentAlignment.MiddleLeft;
            this._lblAyudaUbicacion.AutoSize = true;
            this._lblAyudaUbicacion.ForeColor = Color.FromArgb(104, 116, 132);
            this._lblAyudaUbicacion.Location = new Point(135, 156);
            this._lblAyudaUbicacion.Text = "Completá la dirección del evento; el salón seleccionado carga una ubicación inicial.";
            this._textoNombreContacto.Location = new Point(135, 27);
            this._textoNombreContacto.Size = new Size(265, 25);
            this._textoEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._textoEmail.Location = new Point(505, 27);
            this._textoEmail.Size = new Size(415, 25);
            this._textoTelefono.Location = new Point(135, 69);
            this._textoTelefono.Size = new Size(180, 25);
            this._selectorFecha.Format = DateTimePickerFormat.Short;
            this._selectorFecha.Location = new Point(450, 69);
            this._selectorFecha.Size = new Size(145, 25);
            this._selectorHoraInicio.Format = DateTimePickerFormat.Time;
            this._selectorHoraInicio.ShowUpDown = true;
            this._selectorHoraInicio.Location = new Point(730, 69);
            this._selectorHoraInicio.Size = new Size(150, 25);
            this._numeroDuracion.DecimalPlaces = 0;
            this._numeroDuracion.Minimum = 1m;
            this._numeroDuracion.Maximum = 48m;
            this._numeroDuracion.Value = 1m;
            this._numeroDuracion.Location = new Point(135, 111);
            this._numeroDuracion.Size = new Size(75, 25);
            this._comboSalon.DropDownStyle = ComboBoxStyle.DropDownList;
            this._comboSalon.Location = new Point(315, 111);
            this._comboSalon.Size = new Size(280, 25);
            this._textoUbicacion.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._textoUbicacion.Location = new Point(730, 111);
            this._textoUbicacion.Size = new Size(190, 25);
            // 
            // _grupoMenus
            // 
            this._grupoMenus.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._grupoMenus.BackColor = Color.White;
            this._grupoMenus.Location = new Point(18, 250);
            this._grupoMenus.Name = "_grupoMenus";
            this._grupoMenus.Size = new Size(960, 365);
            this._grupoMenus.TabStop = false;
            this._grupoMenus.Text = "Invitados y menús";
            this._grupoMenus.Controls.Add(this._numeroAdultos);
            this._grupoMenus.Controls.Add(this._numeroNinos);
            this._grupoMenus.Controls.Add(this._porcentajeMenuPrincipal);
            this._grupoMenus.Controls.Add(this._porcentajeMenuPersonalizado);
            this._grupoMenus.Controls.Add(this._grillaMenusPrincipales);
            this._grupoMenus.Controls.Add(this._grillaMenusPersonalizados);
            this._grupoMenus.Controls.Add(this._botonAgregarMenuPrincipal);
            this._grupoMenus.Controls.Add(this._botonQuitarMenuPrincipal);
            this._grupoMenus.Controls.Add(this._botonAgregarMenuPersonalizado);
            this._grupoMenus.Controls.Add(this._botonQuitarMenuPersonalizado);
            this._grupoMenus.Controls.Add(this._etiquetaAyudaMenus);
            this._grupoMenus.Controls.Add(this._lblAdultos);
            this._grupoMenus.Controls.Add(this._lblNinos);
            this._grupoMenus.Controls.Add(this._lblPorcentajePrincipal);
            this._grupoMenus.Controls.Add(this._lblPorcentajePersonalizado);
            this._grupoMenus.Controls.Add(this._lblTituloMenusPrincipales);
            this._grupoMenus.Controls.Add(this._lblTituloMenusPersonalizados);
            // 
            // Menús controls
            // 
            this._lblAdultos.Location = new Point(18, 30);
            this._lblAdultos.Size = new Size(60, 24);
            this._lblAdultos.Text = "Adultos";
            this._lblAdultos.TextAlign = ContentAlignment.MiddleLeft;
            this._lblNinos.Location = new Point(180, 30);
            this._lblNinos.Size = new Size(50, 24);
            this._lblNinos.Text = "Niños";
            this._lblNinos.TextAlign = ContentAlignment.MiddleLeft;
            this._lblPorcentajePrincipal.Location = new Point(340, 30);
            this._lblPorcentajePrincipal.Size = new Size(145, 24);
            this._lblPorcentajePrincipal.Text = "% adicional principal";
            this._lblPorcentajePrincipal.TextAlign = ContentAlignment.MiddleLeft;
            this._lblPorcentajePersonalizado.Location = new Point(590, 30);
            this._lblPorcentajePersonalizado.Size = new Size(180, 24);
            this._lblPorcentajePersonalizado.Text = "% adicional personalizado";
            this._lblPorcentajePersonalizado.TextAlign = ContentAlignment.MiddleLeft;
            this._lblTituloMenusPrincipales.AutoSize = true;
            this._lblTituloMenusPrincipales.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._lblTituloMenusPrincipales.Location = new Point(18, 70);
            this._lblTituloMenusPrincipales.Text = "Menús principales (se pueden agregar varios)";
            this._lblTituloMenusPersonalizados.AutoSize = true;
            this._lblTituloMenusPersonalizados.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._lblTituloMenusPersonalizados.Location = new Point(18, 185);
            this._lblTituloMenusPersonalizados.Text = "Menús con particularidades (una fila por categoría y menú)";
            this._numeroAdultos.DecimalPlaces = 0;
            this._numeroAdultos.Minimum = 0m;
            this._numeroAdultos.Maximum = 10000m;
            this._numeroAdultos.Location = new Point(82, 27);
            this._numeroAdultos.Size = new Size(75, 25);
            this._numeroNinos.DecimalPlaces = 0;
            this._numeroNinos.Minimum = 0m;
            this._numeroNinos.Maximum = 10000m;
            this._numeroNinos.Location = new Point(235, 27);
            this._numeroNinos.Size = new Size(75, 25);
            this._porcentajeMenuPrincipal.DecimalPlaces = 2;
            this._porcentajeMenuPrincipal.Increment = 1m;
            this._porcentajeMenuPrincipal.Minimum = 0m;
            this._porcentajeMenuPrincipal.Maximum = 500m;
            this._porcentajeMenuPrincipal.Location = new Point(490, 27);
            this._porcentajeMenuPrincipal.Size = new Size(70, 25);
            this._porcentajeMenuPersonalizado.DecimalPlaces = 2;
            this._porcentajeMenuPersonalizado.Increment = 1m;
            this._porcentajeMenuPersonalizado.Minimum = 0m;
            this._porcentajeMenuPersonalizado.Maximum = 500m;
            this._porcentajeMenuPersonalizado.Location = new Point(775, 27);
            this._porcentajeMenuPersonalizado.Size = new Size(70, 25);
            this._botonAgregarMenuPrincipal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._botonAgregarMenuPrincipal.BackColor = Color.FromArgb(229, 237, 247);
            this._botonAgregarMenuPrincipal.FlatAppearance.BorderSize = 0;
            this._botonAgregarMenuPrincipal.FlatStyle = FlatStyle.Flat;
            this._botonAgregarMenuPrincipal.ForeColor = Color.FromArgb(48, 99, 157);
            this._botonAgregarMenuPrincipal.Location = new Point(690, 62);
            this._botonAgregarMenuPrincipal.Size = new Size(115, 30);
            this._botonAgregarMenuPrincipal.Text = "Agregar menú";
            this._botonAgregarMenuPrincipal.UseVisualStyleBackColor = false;
            this._botonQuitarMenuPrincipal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._botonQuitarMenuPrincipal.BackColor = Color.FromArgb(230, 234, 239);
            this._botonQuitarMenuPrincipal.FlatAppearance.BorderSize = 0;
            this._botonQuitarMenuPrincipal.FlatStyle = FlatStyle.Flat;
            this._botonQuitarMenuPrincipal.ForeColor = Color.FromArgb(45, 55, 70);
            this._botonQuitarMenuPrincipal.Location = new Point(815, 62);
            this._botonQuitarMenuPrincipal.Size = new Size(105, 30);
            this._botonQuitarMenuPrincipal.Text = "Quitar fila";
            this._botonQuitarMenuPrincipal.UseVisualStyleBackColor = false;
            this._grillaMenusPrincipales.AllowUserToAddRows = false;
            this._grillaMenusPrincipales.AllowUserToDeleteRows = false;
            this._grillaMenusPrincipales.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._grillaMenusPrincipales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._grillaMenusPrincipales.BackgroundColor = Color.White;
            this._grillaMenusPrincipales.Columns.AddRange(new DataGridViewColumn[] {
                this._columnaMenuPrincipal,
                this._columnaAdultosMenuPrincipal,
                this._columnaNinosMenuPrincipal});
            this._grillaMenusPrincipales.Location = new Point(18, 98);
            this._grillaMenusPrincipales.MultiSelect = false;
            this._grillaMenusPrincipales.Name = "_grillaMenusPrincipales";
            this._grillaMenusPrincipales.ReadOnly = false;
            this._grillaMenusPrincipales.RowHeadersVisible = false;
            this._grillaMenusPrincipales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this._grillaMenusPrincipales.Size = new Size(920, 72);
            this._columnaMenuPrincipal.DataPropertyName = "MenuId";
            this._columnaMenuPrincipal.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            this._columnaMenuPrincipal.FillWeight = 145F;
            this._columnaMenuPrincipal.HeaderText = "Menú principal";
            this._columnaMenuPrincipal.Name = "columnaMenuPrincipal";
            this._columnaMenuPrincipal.ValueType = typeof(int);
            this._columnaAdultosMenuPrincipal.DataPropertyName = "CantidadAdultos";
            this._columnaAdultosMenuPrincipal.FillWeight = 95F;
            this._columnaAdultosMenuPrincipal.HeaderText = "Adultos";
            this._columnaAdultosMenuPrincipal.Name = "columnaCantidadAdultosPrincipal";
            this._columnaAdultosMenuPrincipal.ValueType = typeof(int);
            this._columnaNinosMenuPrincipal.DataPropertyName = "CantidadNinos";
            this._columnaNinosMenuPrincipal.FillWeight = 95F;
            this._columnaNinosMenuPrincipal.HeaderText = "Niños";
            this._columnaNinosMenuPrincipal.Name = "columnaCantidadNinosPrincipal";
            this._columnaNinosMenuPrincipal.ValueType = typeof(int);
            this._botonAgregarMenuPersonalizado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._botonAgregarMenuPersonalizado.BackColor = Color.FromArgb(229, 237, 247);
            this._botonAgregarMenuPersonalizado.FlatAppearance.BorderSize = 0;
            this._botonAgregarMenuPersonalizado.FlatStyle = FlatStyle.Flat;
            this._botonAgregarMenuPersonalizado.ForeColor = Color.FromArgb(48, 99, 157);
            this._botonAgregarMenuPersonalizado.Location = new Point(680, 177);
            this._botonAgregarMenuPersonalizado.Size = new Size(145, 30);
            this._botonAgregarMenuPersonalizado.Text = "Agregar particularidad";
            this._botonAgregarMenuPersonalizado.UseVisualStyleBackColor = false;
            this._botonQuitarMenuPersonalizado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._botonQuitarMenuPersonalizado.BackColor = Color.FromArgb(230, 234, 239);
            this._botonQuitarMenuPersonalizado.FlatAppearance.BorderSize = 0;
            this._botonQuitarMenuPersonalizado.FlatStyle = FlatStyle.Flat;
            this._botonQuitarMenuPersonalizado.ForeColor = Color.FromArgb(45, 55, 70);
            this._botonQuitarMenuPersonalizado.Location = new Point(835, 177);
            this._botonQuitarMenuPersonalizado.Size = new Size(105, 30);
            this._botonQuitarMenuPersonalizado.Text = "Quitar fila";
            this._botonQuitarMenuPersonalizado.UseVisualStyleBackColor = false;
            this._grillaMenusPersonalizados.AllowUserToAddRows = false;
            this._grillaMenusPersonalizados.AllowUserToDeleteRows = false;
            this._grillaMenusPersonalizados.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._grillaMenusPersonalizados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._grillaMenusPersonalizados.BackgroundColor = Color.White;
            this._grillaMenusPersonalizados.Columns.AddRange(new DataGridViewColumn[] {
                this._columnaParticularidad,
                this._columnaMenuPersonalizado,
                this._columnaAdultosMenuPersonalizado,
                this._columnaNinosMenuPersonalizado});
            this._grillaMenusPersonalizados.Location = new Point(18, 213);
            this._grillaMenusPersonalizados.MultiSelect = false;
            this._grillaMenusPersonalizados.Name = "_grillaMenusPersonalizados";
            this._grillaMenusPersonalizados.ReadOnly = false;
            this._grillaMenusPersonalizados.RowHeadersVisible = false;
            this._grillaMenusPersonalizados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this._grillaMenusPersonalizados.Size = new Size(920, 105);
            this._columnaParticularidad.DataPropertyName = "Particularidad";
            this._columnaParticularidad.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            this._columnaParticularidad.FillWeight = 130F;
            this._columnaParticularidad.HeaderText = "Particularidad";
            this._columnaParticularidad.Name = "columnaParticularidad";
            this._columnaParticularidad.ValueType = typeof(TipoDeExclusionDeMenu);
            this._columnaMenuPersonalizado.DataPropertyName = "MenuId";
            this._columnaMenuPersonalizado.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            this._columnaMenuPersonalizado.FillWeight = 160F;
            this._columnaMenuPersonalizado.HeaderText = "Menú personalizado";
            this._columnaMenuPersonalizado.Name = "columnaMenuPersonalizado";
            this._columnaMenuPersonalizado.ValueType = typeof(int);
            this._columnaAdultosMenuPersonalizado.DataPropertyName = "CantidadAdultos";
            this._columnaAdultosMenuPersonalizado.FillWeight = 95F;
            this._columnaAdultosMenuPersonalizado.HeaderText = "Adultos";
            this._columnaAdultosMenuPersonalizado.Name = "columnaCantidadAdultosPersonalizado";
            this._columnaAdultosMenuPersonalizado.ValueType = typeof(int);
            this._columnaNinosMenuPersonalizado.DataPropertyName = "CantidadNinos";
            this._columnaNinosMenuPersonalizado.FillWeight = 95F;
            this._columnaNinosMenuPersonalizado.HeaderText = "Niños";
            this._columnaNinosMenuPersonalizado.Name = "columnaCantidadNinosPersonalizado";
            this._columnaNinosMenuPersonalizado.ValueType = typeof(int);
            this._etiquetaAyudaMenus.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._etiquetaAyudaMenus.ForeColor = Color.FromArgb(104, 116, 132);
            this._etiquetaAyudaMenus.Location = new Point(18, 323);
            this._etiquetaAyudaMenus.Size = new Size(920, 25);
            this._etiquetaAyudaMenus.Text = "Con un solo menú principal, recibe automáticamente los invitados restantes. Si agregás varios, distribuí las cantidades. La suma debe coincidir con los invitados.";
            // 
            // _grupoServicios
            // 
            this._grupoServicios.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._grupoServicios.BackColor = Color.White;
            this._grupoServicios.Location = new Point(18, 635);
            this._grupoServicios.Name = "_grupoServicios";
            this._grupoServicios.Size = new Size(960, 190);
            this._grupoServicios.TabStop = false;
            this._grupoServicios.Text = "Servicios adicionales";
            this._grupoServicios.Controls.Add(this._listaServiciosDisponibles);
            this._grupoServicios.Controls.Add(this._textoServicioPersonalizado);
            this._grupoServicios.Controls.Add(this._textoCostoPersonalizado);
            this._grupoServicios.Controls.Add(this._listaServiciosPersonalizados);
            this._grupoServicios.Controls.Add(this._botonAgregarServicio);
            this._grupoServicios.Controls.Add(this._botonQuitarServicio);
            this._grupoServicios.Controls.Add(this._lblServiciosCatalogo);
            this._grupoServicios.Controls.Add(this._lblServicioPersonalizado);
            this._grupoServicios.Controls.Add(this._lblCostoFijo);
            this._listaServiciosDisponibles.CheckOnClick = true;
            this._listaServiciosDisponibles.DisplayMember = "Descripcion";
            this._listaServiciosDisponibles.Location = new Point(18, 47);
            this._listaServiciosDisponibles.Size = new Size(405, 124);
            this._listaServiciosPersonalizados.DisplayMember = "Descripcion";
            this._listaServiciosPersonalizados.Location = new Point(595, 101);
            this._listaServiciosPersonalizados.Size = new Size(343, 70);
            this._lblServiciosCatalogo.AutoSize = true;
            this._lblServiciosCatalogo.Location = new Point(18, 25);
            this._lblServiciosCatalogo.Text = "Servicios del catálogo";
            this._lblServicioPersonalizado.Location = new Point(445, 29);
            this._lblServicioPersonalizado.Size = new Size(150, 24);
            this._lblServicioPersonalizado.Text = "Servicio personalizado";
            this._lblServicioPersonalizado.TextAlign = ContentAlignment.MiddleLeft;
            this._lblCostoFijo.Location = new Point(812, 29);
            this._lblCostoFijo.Size = new Size(70, 24);
            this._lblCostoFijo.Text = "Monto fijo";
            this._lblCostoFijo.TextAlign = ContentAlignment.MiddleLeft;
            this._textoServicioPersonalizado.Location = new Point(595, 25);
            this._textoServicioPersonalizado.Size = new Size(210, 25);
            this._textoCostoPersonalizado.Location = new Point(883, 25);
            this._textoCostoPersonalizado.Size = new Size(55, 25);
            this._botonAgregarServicio.Location = new Point(595, 60);
            this._botonAgregarServicio.BackColor = Color.FromArgb(229, 237, 247);
            this._botonAgregarServicio.FlatAppearance.BorderSize = 0;
            this._botonAgregarServicio.FlatStyle = FlatStyle.Flat;
            this._botonAgregarServicio.ForeColor = Color.FromArgb(48, 99, 157);
            this._botonAgregarServicio.Size = new Size(115, 32);
            this._botonAgregarServicio.Text = "Agregar";
            this._botonAgregarServicio.UseVisualStyleBackColor = false;
            this._botonQuitarServicio.Location = new Point(718, 60);
            this._botonQuitarServicio.BackColor = Color.FromArgb(230, 234, 239);
            this._botonQuitarServicio.FlatAppearance.BorderSize = 0;
            this._botonQuitarServicio.FlatStyle = FlatStyle.Flat;
            this._botonQuitarServicio.ForeColor = Color.FromArgb(45, 55, 70);
            this._botonQuitarServicio.Size = new Size(170, 32);
            this._botonQuitarServicio.Text = "Quitar seleccionado";
            this._botonQuitarServicio.UseVisualStyleBackColor = false;
            // 
            // _grupoObservaciones
            // 
            this._grupoObservaciones.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._grupoObservaciones.BackColor = Color.White;
            this._grupoObservaciones.Location = new Point(18, 840);
            this._grupoObservaciones.Name = "_grupoObservaciones";
            this._grupoObservaciones.Size = new Size(960, 100);
            this._grupoObservaciones.TabStop = false;
            this._grupoObservaciones.Text = "Observaciones";
            this._grupoObservaciones.Controls.Add(this._textoObservaciones);
            this._textoObservaciones.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this._textoObservaciones.Location = new Point(18, 27);
            this._textoObservaciones.Multiline = true;
            this._textoObservaciones.ScrollBars = ScrollBars.Vertical;
            this._textoObservaciones.Size = new Size(920, 55);
            // 
            // Footer
            // 
            this._panelPie.BackColor = Color.White;
            this._panelPie.Controls.Add(this._etiquetaTotal);
            this._panelPie.Controls.Add(this._panelAccion);
            this._panelPie.Dock = DockStyle.Bottom;
            this._panelPie.Height = 72;
            this._etiquetaTotal.AutoSize = true;
            this._etiquetaTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this._etiquetaTotal.ForeColor = Color.FromArgb(35, 45, 61);
            this._etiquetaTotal.Location = new Point(24, 24);
            this._panelAccion.Dock = DockStyle.Right;
            this._panelAccion.Padding = new Padding(10, 15, 10, 15);
            this._panelAccion.Width = 180;
            this._botonGuardar.Dock = DockStyle.Fill;
            this._botonGuardar.BackColor = Color.FromArgb(48, 99, 157);
            this._botonGuardar.FlatAppearance.BorderSize = 0;
            this._botonGuardar.FlatStyle = FlatStyle.Flat;
            this._botonGuardar.ForeColor = Color.White;
            this._botonGuardar.Text = "Terminar presupuesto";
            this._botonGuardar.UseVisualStyleBackColor = false;
            // 
            // PresupuestoForm
            // 
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(246, 248, 251);
            this.ClientSize = new Size(1010, 680);
            this.Controls.Add(this._panelDatos);
            this.Controls.Add(this._panelPie);
            this.Font = new Font("Segoe UI", 9F);
            this.MinimumSize = new Size(900, 620);
            this.Name = "PresupuestoForm";
            this.Text = "Crear presupuesto";
            this._panelAccion.Controls.Add(this._botonGuardar);
            ((ISupportInitialize)(this._numeroDuracion)).EndInit();
            ((ISupportInitialize)(this._numeroAdultos)).EndInit();
            ((ISupportInitialize)(this._numeroNinos)).EndInit();
            ((ISupportInitialize)(this._porcentajeMenuPrincipal)).EndInit();
            ((ISupportInitialize)(this._porcentajeMenuPersonalizado)).EndInit();
            ((ISupportInitialize)(this._grillaMenusPrincipales)).EndInit();
            ((ISupportInitialize)(this._grillaMenusPersonalizados)).EndInit();
            this._panelDatos.ResumeLayout(false);
            this._panelPie.ResumeLayout(false);
            this._panelPie.PerformLayout();
            this._panelAccion.ResumeLayout(false);
            this._grupoContactoEvento.ResumeLayout(false);
            this._grupoContactoEvento.PerformLayout();
            this._grupoMenus.ResumeLayout(false);
            this._grupoMenus.PerformLayout();
            this._grupoServicios.ResumeLayout(false);
            this._grupoServicios.PerformLayout();
            this._grupoObservaciones.ResumeLayout(false);
            this._grupoObservaciones.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Label _lblNombre;
        private Label _lblCorreo;
        private Label _lblTelefono;
        private Label _lblFecha;
        private Label _lblHoraInicio;
        private Label _lblDuracion;
        private Label _lblSalon;
        private Label _lblUbicacion;
        private Label _lblAyudaUbicacion;
        private Label _lblAdultos;
        private Label _lblNinos;
        private Label _lblPorcentajePrincipal;
        private Label _lblPorcentajePersonalizado;
        private Label _lblTituloMenusPrincipales;
        private Label _lblTituloMenusPersonalizados;
        private Label _lblServiciosCatalogo;
        private Label _lblServicioPersonalizado;
        private Label _lblCostoFijo;
    }
}
