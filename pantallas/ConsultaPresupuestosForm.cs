using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using proyecto_integrador.controller;
using proyecto_integrador.dto;

namespace proyecto_integrador.pantallas
{
    public partial class ConsultaPresupuestosForm : Form
    {
        // La vista solo muestra lo que le devuelve el Controller (PresupuestoDto con los importes ya calculados).
        private readonly PresupuestoController _controller;

        public event Action<PresupuestoDto> SolicitarEdicion;
        public event Action SolicitarInicio;

        // Constructor sin parámetros: lo necesita el diseñador de Visual Studio.
        public ConsultaPresupuestosForm()
        {
            _controller = null;
            InitializeComponent();
        }

        public ConsultaPresupuestosForm(PresupuestoController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _controller = controller;
            InitializeComponent();
            _grilla.SelectionChanged += grilla_SelectionChanged;
            _grilla.CellDoubleClick += grilla_CellDoubleClick;
            _botonModificar.Click += botonModificar_Click;
            _botonImprimirPresupuesto.Click += botonImprimirPresupuesto_Click;
            _botonVolverInicio.Click += botonVolverInicio_Click;
            ActualizarLista();
        }

        public void ActualizarLista()
        {
            ActualizarLista(0);
        }

        public void ActualizarLista(int presupuestoSeleccionadoId)
        {
            _grilla.DataSource = null;
            _grilla.DataSource = _controller.ObtenerPresupuestos();
            _grilla.ClearSelection();
            _grilla.CurrentCell = null;

            if (_grilla.Rows.Count == 0)
            {
                _detalle.Text = "Todavía no hay presupuestos creados en esta sesión.";
            }
            else
            {
                _detalle.Text = "Seleccioná un presupuesto para consultar su detalle.";
            }

            if (presupuestoSeleccionadoId > 0)
            {
                foreach (DataGridViewRow fila in _grilla.Rows)
                {
                    PresupuestoDto presupuesto = fila.DataBoundItem as PresupuestoDto;
                    if (presupuesto != null && presupuesto.Id == presupuestoSeleccionadoId)
                    {
                        fila.Selected = true;
                        _grilla.CurrentCell = fila.Cells[0];
                        _detalle.Text = CrearDetalle(presupuesto);
                        break;
                    }
                }
            }

            ActualizarEstadoBotones();
        }

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            PresupuestoDto presupuesto = ObtenerSeleccionado();
            if (presupuesto == null)
            {
                ActualizarEstadoBotones();
                return;
            }

            _detalle.Text = CrearDetalle(presupuesto);
            ActualizarEstadoBotones();
        }

        private void grilla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarSeleccionado();
        }

        private void botonModificar_Click(object sender, EventArgs e)
        {
            EditarSeleccionado();
        }

        private void botonVolverInicio_Click(object sender, EventArgs e)
        {
            if (SolicitarInicio != null)
            {
                SolicitarInicio();
            }
        }

        private void EditarSeleccionado()
        {
            PresupuestoDto presupuesto = ObtenerSeleccionado();
            if (presupuesto == null)
            {
                MessageBox.Show("Seleccioná un presupuesto para modificarlo.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (SolicitarEdicion != null)
            {
                SolicitarEdicion(presupuesto);
            }
        }

        private void botonImprimirPresupuesto_Click(object sender, EventArgs e)
        {
            PresupuestoDto presupuesto = ObtenerSeleccionado();
            if (presupuesto == null)
            {
                MessageBox.Show("Seleccioná un presupuesto para exportarlo a PDF.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                string documentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (string.IsNullOrWhiteSpace(documentos) || !Directory.Exists(documentos))
                {
                    documentos = AppDomain.CurrentDomain.BaseDirectory;
                }

                dialogo.AddExtension = true;
                dialogo.DefaultExt = "pdf";
                dialogo.FileName = "Presupuesto_" + presupuesto.Id.ToString(CultureInfo.InvariantCulture) + ".pdf";
                dialogo.Filter = "Documento PDF (*.pdf)|*.pdf";
                dialogo.InitialDirectory = documentos;
                dialogo.OverwritePrompt = true;
                dialogo.Title = "Exportar presupuesto como PDF";

                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    string rutaPdf = _controller.ExportarPdf(presupuesto.Id, dialogo.FileName);
                    ActualizarEstadoBotones();
                    MessageBox.Show("Se exportó el presupuesto a PDF:\n" + rutaPdf, "PDF generado",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        ProcessStartInfo informacion = new ProcessStartInfo(rutaPdf);
                        informacion.UseShellExecute = true;
                        Process.Start(informacion);
                    }
                    catch (InvalidOperationException ex)
                    {
                        MostrarErrorAlAbrirPdf(ex.Message);
                    }
                    catch (Win32Exception ex)
                    {
                        MostrarErrorAlAbrirPdf(ex.Message);
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "No se pudo exportar el PDF",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("No se pudo guardar el archivo PDF: " + ex.Message, "Error de archivo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("No hay permiso para guardar en esa ubicación: " + ex.Message, "Acceso denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void MostrarErrorAlAbrirPdf(string mensaje)
        {
            MessageBox.Show("El PDF se generó, pero no se pudo abrir automáticamente: " + mensaje,
                "PDF generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private PresupuestoDto ObtenerSeleccionado()
        {
            if (_grilla.CurrentRow == null)
            {
                return null;
            }

            return _grilla.CurrentRow.DataBoundItem as PresupuestoDto;
        }

        private void ActualizarEstadoBotones()
        {
            PresupuestoDto seleccionado = ObtenerSeleccionado();
            _botonModificar.Enabled = seleccionado != null;
            _botonImprimirPresupuesto.Enabled = seleccionado != null;
        }

        // Solo da formato de texto a datos que ya vienen calculados en el DTO. No calcula nada.
        private string CrearDetalle(PresupuestoDto presupuesto)
        {
            StringBuilder detalle = new StringBuilder();
            detalle.AppendLine("PRESUPUESTO #" + presupuesto.Id);
            detalle.AppendLine("Fecha y hora del evento: " + presupuesto.FechaEvento.ToString("dd/MM/yyyy")
                + " " + presupuesto.HoraInicio.ToString(@"hh\:mm"));
            detalle.AppendLine("Cliente: " + presupuesto.NombreContacto);
            detalle.AppendLine("Contacto: " + presupuesto.EmailContacto + "  |  Teléfono: " + presupuesto.TelefonoContacto);
            detalle.AppendLine("Salón: " + presupuesto.NombreSalon + " — " + presupuesto.UbicacionEvento);
            detalle.AppendLine("Duración: " + presupuesto.DuracionHoras + " horas");
            detalle.AppendLine("Invitados: " + presupuesto.CantidadAdultos + " adultos y " + presupuesto.CantidadNinos + " niños");
            detalle.AppendLine();
            detalle.AppendLine("Menús:");

            foreach (DetalleMenuPresupuestoDto menu in presupuesto.Menues)
            {
                detalle.AppendLine("• " + menu.NombreMenu + " — " + menu.Particularidad
                    + ": " + menu.CantidadAdultos + " adultos, " + menu.CantidadNinos + " niños; "
                    + menu.Subtotal.ToString("C2", CultureInfo.CurrentCulture));
            }

            detalle.AppendLine("Servicios adicionales:");
            if (presupuesto.ServiciosAdicionales.Count == 0)
            {
                detalle.AppendLine("• Ninguno");
            }
            else
            {
                foreach (ServicioAdicionalDto servicio in presupuesto.ServiciosAdicionales)
                {
                    detalle.AppendLine("• " + servicio.Descripcion + " — "
                        + servicio.CostoDelServicio.ToString("C2", CultureInfo.CurrentCulture));
                }
            }

            detalle.AppendLine();
            detalle.AppendLine("Salón: " + presupuesto.CostoSalon.ToString("C2", CultureInfo.CurrentCulture));
            detalle.AppendLine("Subtotal menús principales: " + presupuesto.SubtotalMenusPrincipales.ToString("C2", CultureInfo.CurrentCulture));
            detalle.AppendLine("Adicional menú principal (" + presupuesto.PorcentajeAdicionalMenuPrincipal.ToString("0.##", CultureInfo.CurrentCulture)
                + "%): " + presupuesto.RecargoMenusPrincipales.ToString("C2", CultureInfo.CurrentCulture));
            detalle.AppendLine("Subtotal menús personalizados: " + presupuesto.SubtotalMenusPersonalizados.ToString("C2", CultureInfo.CurrentCulture));
            detalle.AppendLine("Adicional menú personalizado (" + presupuesto.PorcentajeAdicionalMenuPersonalizado.ToString("0.##", CultureInfo.CurrentCulture)
                + "%): " + presupuesto.RecargoMenusPersonalizados.ToString("C2", CultureInfo.CurrentCulture));
            detalle.AppendLine("Total menús: " + presupuesto.CostoMenus.ToString("C2", CultureInfo.CurrentCulture));
            detalle.AppendLine("Servicios: " + presupuesto.CostoServiciosAdicionales.ToString("C2", CultureInfo.CurrentCulture));
            detalle.AppendLine("TOTAL: " + presupuesto.TotalFinal.ToString("C2", CultureInfo.CurrentCulture));

            if (!string.IsNullOrWhiteSpace(presupuesto.Observaciones))
            {
                detalle.AppendLine();
                detalle.AppendLine("Observaciones: " + presupuesto.Observaciones);
            }

            return detalle.ToString();
        }
    }
}
