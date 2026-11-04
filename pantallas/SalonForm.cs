using System;
using System.Globalization;
using System.Windows.Forms;
using proyecto_integrador.controller;
using proyecto_integrador.dto;

namespace proyecto_integrador.pantallas
{
    public partial class SalonForm : Form
    {
        // La vista solo conoce al Controller y a los DTO. No conoce Services, Repositories ni entidades.
        private readonly SalonController _controller;

        // Constructor sin parámetros: lo necesita el diseñador de Visual Studio.
        public SalonForm()
        {
            InitializeComponent();
        }

        public SalonForm(SalonController controller)
            : this()
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _controller = controller;
            dgvSalonesCreados.AutoGenerateColumns = false; // Usamos las columnas que ya están definidas en el diseñador
            CargarGrilla();

            // Al abrir la pantalla, que no quede ninguna fila seleccionada
            this.Shown += SalonForm_Shown;
        }

        private void SalonForm_Shown(object sender, EventArgs e)
        {
            dgvSalonesCreados.ClearSelection();
        }

        private void CargarGrilla()
        {
            dgvSalonesCreados.DataSource = null;
            dgvSalonesCreados.DataSource = _controller.ObtenerTodos();
            dgvSalonesCreados.ClearSelection();
        }

        // Al hacer clic en una fila, se cargan sus datos en los textbox
        private void dgvSalonesCreados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            SalonDto seleccionado = dgvSalonesCreados.Rows[e.RowIndex].DataBoundItem as SalonDto;
            if (seleccionado == null)
            {
                return;
            }

            txtBoxNombre.Text = seleccionado.Nombre;
            txtBoxUbicacion.Text = seleccionado.Ubicacion;
            txtBoxMonto.Text = seleccionado.CostoBase.ToString("0.##", CultureInfo.CurrentCulture);
        }

        private void btnCrearSalon_Click(object sender, EventArgs e)
        {
            decimal monto;
            if (!TryObtenerMonto(out monto))
            {
                return;
            }

            try
            {
                _controller.Crear(CrearDto(0, monto));
                CargarGrilla();
                LimpiarCampos();
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnModificarSalon_Click(object sender, EventArgs e)
        {
            SalonDto seleccionado = ObtenerSalonSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccioná un salón de la lista para modificarlo.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal monto;
            if (!TryObtenerMonto(out monto))
            {
                return;
            }

            try
            {
                bool modificado = _controller.Modificar(CrearDto(seleccionado.Id, monto));
                if (!modificado)
                {
                    MessageBox.Show("No se encontró el salón seleccionado.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CargarGrilla();
                LimpiarCampos();
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnEliminarSalon_Click(object sender, EventArgs e)
        {
            SalonDto seleccionado = ObtenerSalonSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccioná un salón de la lista para eliminarlo.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que querés eliminar el salón \"" + seleccionado.Nombre + "\"?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                _controller.Eliminar(seleccionado.Id);
                CargarGrilla();
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private SalonDto ObtenerSalonSeleccionado()
        {
            if (dgvSalonesCreados.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgvSalonesCreados.SelectedRows[0].DataBoundItem as SalonDto;
        }

        // Arma el DTO con lo que se escribió en pantalla. Las reglas (>= 0, nombre vacío, etc.) las valida el Service.
        private SalonDto CrearDto(int id, decimal monto)
        {
            SalonDto dto = new SalonDto();
            dto.Id = id;
            dto.Nombre = txtBoxNombre.Text;
            dto.Ubicacion = txtBoxUbicacion.Text;
            dto.CostoBase = monto;
            return dto;
        }

        // Convierte el texto del monto a número. Eso es de la pantalla; las reglas las valida el service
        private bool TryObtenerMonto(out decimal monto)
        {
            if (!decimal.TryParse(txtBoxMonto.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out monto))
            {
                MostrarError("El monto tiene que ser un número.");
                txtBoxMonto.Focus();
                return false;
            }

            return true;
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LimpiarCampos()
        {
            txtBoxNombre.Clear();
            txtBoxUbicacion.Clear();
            txtBoxMonto.Clear();
            dgvSalonesCreados.ClearSelection();
            txtBoxNombre.Focus();
        }
    }
}
