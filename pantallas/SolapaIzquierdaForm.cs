using System;
using System.Drawing;
using System.Windows.Forms;
using proyecto_integrador.controller;
using proyecto_integrador.models;
using proyecto_integrador.pantallas;

namespace proyecto_integrador.pantallas
{
    public partial class SolapaIzquierdaForm : Form
    {
        private Panel pantallaInicio;
        private SalonForm formularioSalon;
        private MenuDeComidaForm formularioMenu;
        private ServiciosAdicionalesForm formularioServiciosAdicionales;
        private PresupuestoForm formularioPresupuesto;
        private PresupuestosForm formularioPresupuestos;
        private readonly PresupuestoController _presupuestoController;
        private Button botonCancelarPresupuesto;
        private Button botonSeccionActiva;

        public SolapaIzquierdaForm()
        {
            _presupuestoController = new PresupuestoController();
            InitializeComponent();
            CrearAccionCancelarPresupuesto();
            CrearPantallaInicio();
            MostrarInicio();
        }

        private void CrearAccionCancelarPresupuesto()
        {
            botonCancelarPresupuesto = new Button();
            botonCancelarPresupuesto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            botonCancelarPresupuesto.BackColor = Color.FromArgb(230, 234, 239);
            botonCancelarPresupuesto.FlatAppearance.BorderSize = 0;
            botonCancelarPresupuesto.FlatStyle = FlatStyle.Flat;
            botonCancelarPresupuesto.Font = new Font("Segoe UI", 9F);
            botonCancelarPresupuesto.ForeColor = Color.FromArgb(45, 55, 70);
            botonCancelarPresupuesto.Location = new Point(panelEncabezado.ClientSize.Width - 210, 20);
            botonCancelarPresupuesto.Size = new Size(185, 40);
            botonCancelarPresupuesto.Text = "Cancelar presupuesto";
            botonCancelarPresupuesto.UseVisualStyleBackColor = false;
            botonCancelarPresupuesto.Visible = false;
            botonCancelarPresupuesto.Click += botonCancelarPresupuesto_Click;
            panelEncabezado.Controls.Add(botonCancelarPresupuesto);
        }

        private void CrearPantallaInicio()
        {
            pantallaInicio = new Panel();
            pantallaInicio.BackColor = Color.FromArgb(246, 248, 251);
            pantallaInicio.Dock = DockStyle.Fill;

            Panel tarjetaInicio = new Panel();
            tarjetaInicio.BackColor = Color.White;
            tarjetaInicio.Location = new Point(28, 28);
            tarjetaInicio.Size = new Size(440, 245);
            tarjetaInicio.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            Label tituloInicio = new Label();
            tituloInicio.AutoSize = true;
            tituloInicio.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            tituloInicio.ForeColor = Color.FromArgb(35, 45, 61);
            tituloInicio.Location = new Point(28, 27);
            tituloInicio.Text = "Crear presupuesto";

            Label descripcionInicio = new Label();
            descripcionInicio.AutoSize = false;
            descripcionInicio.Font = new Font("Segoe UI", 10F);
            descripcionInicio.ForeColor = Color.FromArgb(104, 116, 132);
            descripcionInicio.Location = new Point(32, 78);
            descripcionInicio.Size = new Size(380, 58);
            descripcionInicio.Text = "Cargá los datos del cliente, el evento, sus menús y los servicios que quiera agregar.";

            Button botonCrearPresupuesto = new Button();
            botonCrearPresupuesto.BackColor = Color.FromArgb(48, 99, 157);
            botonCrearPresupuesto.FlatAppearance.BorderSize = 0;
            botonCrearPresupuesto.FlatStyle = FlatStyle.Flat;
            botonCrearPresupuesto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            botonCrearPresupuesto.ForeColor = Color.White;
            botonCrearPresupuesto.Location = new Point(32, 165);
            botonCrearPresupuesto.Size = new Size(190, 42);
            botonCrearPresupuesto.Text = "Nuevo presupuesto";
            botonCrearPresupuesto.UseVisualStyleBackColor = false;
            botonCrearPresupuesto.Click += botonCrearPresupuesto_Click;

            tarjetaInicio.Controls.Add(tituloInicio);
            tarjetaInicio.Controls.Add(descripcionInicio);
            tarjetaInicio.Controls.Add(botonCrearPresupuesto);
            pantallaInicio.Controls.Add(tarjetaInicio);

            Panel tarjetaConsulta = new Panel();
            tarjetaConsulta.BackColor = Color.White;
            tarjetaConsulta.Location = new Point(490, 28);
            tarjetaConsulta.Size = new Size(440, 245);
            tarjetaConsulta.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            Label tituloConsulta = new Label();
            tituloConsulta.AutoSize = true;
            tituloConsulta.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            tituloConsulta.ForeColor = Color.FromArgb(35, 45, 61);
            tituloConsulta.Location = new Point(28, 27);
            tituloConsulta.Text = "Historial de presupuestos";

            Label descripcionConsulta = new Label();
            descripcionConsulta.AutoSize = false;
            descripcionConsulta.Font = new Font("Segoe UI", 10F);
            descripcionConsulta.ForeColor = Color.FromArgb(104, 116, 132);
            descripcionConsulta.Location = new Point(32, 78);
            descripcionConsulta.Size = new Size(380, 58);
            descripcionConsulta.Text = "Consultá el detalle de cada presupuesto, abrí su PDF y modificá los datos cuando sea necesario.";

            Button botonVerPresupuestos = new Button();
            botonVerPresupuestos.BackColor = Color.FromArgb(229, 237, 247);
            botonVerPresupuestos.FlatAppearance.BorderSize = 0;
            botonVerPresupuestos.FlatStyle = FlatStyle.Flat;
            botonVerPresupuestos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            botonVerPresupuestos.ForeColor = Color.FromArgb(48, 99, 157);
            botonVerPresupuestos.Location = new Point(32, 165);
            botonVerPresupuestos.Size = new Size(190, 42);
            botonVerPresupuestos.Text = "Consultar historial";
            botonVerPresupuestos.UseVisualStyleBackColor = false;
            botonVerPresupuestos.Click += botonVerPresupuestos_Click;

            tarjetaConsulta.Controls.Add(tituloConsulta);
            tarjetaConsulta.Controls.Add(descripcionConsulta);
            tarjetaConsulta.Controls.Add(botonVerPresupuestos);
            pantallaInicio.Controls.Add(tarjetaConsulta);

            panelContenido.Controls.Add(pantallaInicio);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            MostrarInicio();
        }

        private void btnSalon_Click(object sender, EventArgs e)
        {
            if (formularioSalon == null || formularioSalon.IsDisposed)
            {
                formularioSalon = PrepararFormularioHijo(new SalonForm());
            }

            MostrarPantalla(formularioSalon, btnSalon, "Salón", "Administrá los salones disponibles para los eventos.");
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (formularioMenu == null || formularioMenu.IsDisposed)
            {
                formularioMenu = PrepararFormularioHijo(new MenuDeComidaForm());
            }

            MostrarPantalla(formularioMenu, btnMenu, "Menú", "Opciones de menú y valores por invitado.");
        }

        private void btnServiciosAdicionales_Click(object sender, EventArgs e)
        {
            if (formularioServiciosAdicionales == null || formularioServiciosAdicionales.IsDisposed)
            {
                formularioServiciosAdicionales = PrepararFormularioHijo(new ServiciosAdicionalesForm());
            }

            MostrarPantalla(formularioServiciosAdicionales, btnServiciosAdicionales, "Servicios adicionales", "Administrá los servicios de monto fijo que se pueden sumar a un evento.");
        }

        private void MostrarInicio()
        {
            MostrarPantalla(pantallaInicio, btnInicio, "Inicio", "Accedé a las herramientas de presupuestos para eventos.");
        }

        private void MostrarPantalla(Control pantalla, Button botonActivo, string titulo, string descripcion)
        {
            foreach (Control control in panelContenido.Controls)
            {
                control.Visible = false;
            }

            if (pantalla.Parent != panelContenido)
            {
                panelContenido.Controls.Add(pantalla);
            }

            pantalla.Dock = DockStyle.Fill;
            Form formulario = pantalla as Form;
            if (formulario != null)
            {
                formulario.Show();
            }
            else
            {
                pantalla.Visible = true;
            }
            pantalla.BringToFront();
            lblTituloSeccion.Text = titulo;
            lblDescripcionSeccion.Text = descripcion;
            botonCancelarPresupuesto.Visible = formularioPresupuesto != null && pantalla == formularioPresupuesto;
            ActualizarBotonActivo(botonActivo);
        }

        private TFormulario PrepararFormularioHijo<TFormulario>(TFormulario formulario) where TFormulario : Form
        {
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.Visible = false;
            return formulario;
        }

        private void ActualizarBotonActivo(Button botonActivo)
        {
            if (botonSeccionActiva != null)
            {
                botonSeccionActiva.BackColor = Color.FromArgb(25, 35, 52);
                botonSeccionActiva.ForeColor = Color.FromArgb(225, 231, 239);
            }

            botonSeccionActiva = botonActivo;
            botonSeccionActiva.BackColor = Color.FromArgb(48, 99, 157);
            botonSeccionActiva.ForeColor = Color.White;
        }

        private void botonCrearPresupuesto_Click(object sender, EventArgs e)
        {
            AbrirFormularioPresupuesto(null);
        }

        private void botonVerPresupuestos_Click(object sender, EventArgs e)
        {
            if (formularioPresupuestos == null || formularioPresupuestos.IsDisposed)
            {
                formularioPresupuestos = PrepararFormularioHijo(new PresupuestosForm(_presupuestoController));
                formularioPresupuestos.SolicitarEdicion += AbrirFormularioPresupuesto;
                formularioPresupuestos.SolicitarInicio += MostrarInicio;
            }

            formularioPresupuestos.ActualizarLista();
            MostrarPantalla(formularioPresupuestos, btnInicio, "Historial de presupuestos", "Consultá el detalle, el PDF y las modificaciones de los presupuestos guardados.");
        }

        private void AbrirFormularioPresupuesto(Presupuesto presupuesto)
        {
            if (formularioPresupuesto != null && !formularioPresupuesto.IsDisposed)
            {
                formularioPresupuesto.Dispose();
            }

            formularioPresupuesto = PrepararFormularioHijo(
                presupuesto == null
                    ? new PresupuestoForm(_presupuestoController)
                    : new PresupuestoForm(_presupuestoController, presupuesto));
            formularioPresupuesto.Guardado += PresupuestoGuardado;

            string titulo = presupuesto == null ? "Crear presupuesto" : "Modificar presupuesto #" + presupuesto.Id;
            string descripcion = presupuesto == null
                ? "Completá los datos del contacto, el evento, los menús y los servicios."
                : "Actualizá los datos del presupuesto seleccionado.";
            MostrarPantalla(formularioPresupuesto, btnInicio, titulo, descripcion);
        }

        private void PresupuestoGuardado(Presupuesto presupuesto)
        {
            if (formularioPresupuestos == null || formularioPresupuestos.IsDisposed)
            {
                formularioPresupuestos = PrepararFormularioHijo(new PresupuestosForm(_presupuestoController));
                formularioPresupuestos.SolicitarEdicion += AbrirFormularioPresupuesto;
                formularioPresupuestos.SolicitarInicio += MostrarInicio;
            }

            formularioPresupuestos.ActualizarLista(presupuesto.Id);
            MostrarPantalla(formularioPresupuestos, btnInicio, "Historial de presupuestos", "El presupuesto quedó guardado y aparece seleccionado en su historial.");
        }

        private void FormularioPresupuestoCancelado()
        {
            if (formularioPresupuesto != null)
            {
                formularioPresupuesto.Dispose();
                formularioPresupuesto = null;
            }

            MostrarInicio();
        }

        private void botonCancelarPresupuesto_Click(object sender, EventArgs e)
        {
            FormularioPresupuestoCancelado();
        }
    }
}
