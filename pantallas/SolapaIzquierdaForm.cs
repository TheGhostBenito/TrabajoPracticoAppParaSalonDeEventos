using System;
using System.Drawing;
using System.Windows.Forms;
using proyecto_integrador.pantallas;

namespace proyecto_integrador.pantallas
{
    public partial class SolapaIzquierdaForm : Form
    {
        private Panel pantallaInicio;
        private Panel pantallaMenu;
        private SalonForm formularioSalon;
        private ServiciosAdicionalesForm formularioServiciosAdicionales;
        private Button botonSeccionActiva;

        public SolapaIzquierdaForm()
        {
            InitializeComponent();
            CrearPantallasPendientes();
            MostrarInicio();
        }

        private void CrearPantallasPendientes()
        {
            pantallaInicio = new Panel();
            pantallaInicio.BackColor = Color.FromArgb(246, 248, 251);
            pantallaInicio.Dock = DockStyle.Fill;

            Panel tarjetaInicio = new Panel();
            tarjetaInicio.BackColor = Color.White;
            tarjetaInicio.Location = new Point(28, 28);
            tarjetaInicio.Size = new Size(650, 235);
            tarjetaInicio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label tituloInicio = new Label();
            tituloInicio.AutoSize = true;
            tituloInicio.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            tituloInicio.ForeColor = Color.FromArgb(35, 45, 61);
            tituloInicio.Location = new Point(28, 27);
            tituloInicio.Text = "Presupuestá tu próximo evento";

            Label descripcionInicio = new Label();
            descripcionInicio.AutoSize = false;
            descripcionInicio.Font = new Font("Segoe UI", 10F);
            descripcionInicio.ForeColor = Color.FromArgb(104, 116, 132);
            descripcionInicio.Location = new Point(32, 78);
            descripcionInicio.Size = new Size(560, 48);
            descripcionInicio.Text = "Administrá salones y servicios desde el menú. La creación de presupuestos se incorporará en el siguiente paso.";

            Button botonCrearPresupuesto = new Button();
            botonCrearPresupuesto.BackColor = Color.FromArgb(48, 99, 157);
            botonCrearPresupuesto.FlatAppearance.BorderSize = 0;
            botonCrearPresupuesto.FlatStyle = FlatStyle.Flat;
            botonCrearPresupuesto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            botonCrearPresupuesto.ForeColor = Color.White;
            botonCrearPresupuesto.Location = new Point(32, 150);
            botonCrearPresupuesto.Size = new Size(190, 42);
            botonCrearPresupuesto.Text = "Crear presupuesto";
            botonCrearPresupuesto.UseVisualStyleBackColor = false;
            botonCrearPresupuesto.Click += botonCrearPresupuesto_Click;

            tarjetaInicio.Controls.Add(tituloInicio);
            tarjetaInicio.Controls.Add(descripcionInicio);
            tarjetaInicio.Controls.Add(botonCrearPresupuesto);
            pantallaInicio.Controls.Add(tarjetaInicio);

            pantallaMenu = new Panel();
            pantallaMenu.BackColor = Color.FromArgb(246, 248, 251);
            pantallaMenu.Dock = DockStyle.Fill;

            Panel tarjetaMenu = new Panel();
            tarjetaMenu.BackColor = Color.White;
            tarjetaMenu.Location = new Point(28, 28);
            tarjetaMenu.Size = new Size(650, 190);
            tarjetaMenu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label tituloMenu = new Label();
            tituloMenu.AutoSize = true;
            tituloMenu.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            tituloMenu.ForeColor = Color.FromArgb(35, 45, 61);
            tituloMenu.Location = new Point(28, 25);
            tituloMenu.Text = "Menús";

            Label descripcionMenu = new Label();
            descripcionMenu.AutoSize = false;
            descripcionMenu.Font = new Font("Segoe UI", 10F);
            descripcionMenu.ForeColor = Color.FromArgb(104, 116, 132);
            descripcionMenu.Location = new Point(32, 72);
            descripcionMenu.Size = new Size(560, 70);
            descripcionMenu.Text = "Todavía no hay una pantalla de administración de menús. Por ahora, las opciones de ejemplo están definidas en el repositorio de menús.";

            tarjetaMenu.Controls.Add(tituloMenu);
            tarjetaMenu.Controls.Add(descripcionMenu);
            pantallaMenu.Controls.Add(tarjetaMenu);

            panelContenido.Controls.Add(pantallaInicio);
            panelContenido.Controls.Add(pantallaMenu);
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
            MostrarPantalla(pantallaMenu, btnMenu, "Menú", "Opciones de menú y valores por invitado.");
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
            MessageBox.Show(
                "La pantalla para crear y calcular presupuestos todavía no está implementada.",
                "Próxima etapa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
