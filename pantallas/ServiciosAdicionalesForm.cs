using System;
using System.Globalization;
using System.Windows.Forms;
using proyecto_integrador.controller;
using proyecto_integrador.dto;

namespace proyecto_integrador.pantallas
{
    public partial class ServiciosAdicionalesForm : Form
    {
        // La vista solo conoce al Controller y a los DTO.
        private readonly ServiciosAdicionalesController _controller;

        // Constructor sin parámetros: lo necesita el diseñador de Visual Studio.
        public ServiciosAdicionalesForm()
        {
            InitializeComponent();
        }

        public ServiciosAdicionalesForm(ServiciosAdicionalesController controller)
            : this()
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _controller = controller;
            dgvServiciosCreados.AutoGenerateColumns = false;
            CargarServicios();
        }

        private void CargarServicios()
        {
            dgvServiciosCreados.DataSource = null;
            dgvServiciosCreados.DataSource = _controller.ObtenerTodos();
            dgvServiciosCreados.ClearSelection();
        }

        private void dgvServiciosCreados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            ServicioAdicionalDto seleccionado = dgvServiciosCreados.Rows[e.RowIndex].DataBoundItem as ServicioAdicionalDto;
            if (seleccionado == null)
            {
                return;
            }

            txtBoxDescripcion.Text = seleccionado.Descripcion;
            txtBoxMonto.Text = seleccionado.CostoDelServicio.ToString("0.##", CultureInfo.CurrentCulture);
        }

        private void btnCrearServicio_Click(object sender, EventArgs e)
        {
            decimal monto;
            if (!TryObtenerMonto(out monto))
            {
                return;
            }

            try
            {
                _controller.Crear(CrearDto(0, monto));
                CargarServicios();
                LimpiarCampos();
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnEliminarServicio_Click(object sender, EventArgs e)
        {
            ServicioAdicionalDto seleccionado = ObtenerServicioSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccioná un servicio de la lista para eliminarlo.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que querés eliminar el servicio \"" + seleccionado.Descripcion + "\"?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                _controller.Eliminar(seleccionado.Id);
                CargarServicios();
                LimpiarCampos();
            }
        }

        private void btnModificarServicio_Click(object sender, EventArgs e)
        {
            ServicioAdicionalDto seleccionado = ObtenerServicioSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccioná un servicio de la lista para modificarlo.", "Atención",
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
                _controller.Modificar(CrearDto(seleccionado.Id, monto));
                CargarServicios();
                LimpiarCampos();
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
        }

        private ServicioAdicionalDto ObtenerServicioSeleccionado()
        {
            if (dgvServiciosCreados.CurrentRow == null)
            {
                return null;
            }

            return dgvServiciosCreados.CurrentRow.DataBoundItem as ServicioAdicionalDto;
        }

        // Arma el DTO con lo escrito en pantalla. Las reglas las valida el Service.
        private ServicioAdicionalDto CrearDto(int id, decimal monto)
        {
            ServicioAdicionalDto dto = new ServicioAdicionalDto();
            dto.Id = id;
            dto.Descripcion = txtBoxDescripcion.Text;
            dto.CostoDelServicio = monto;
            return dto;
        }

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
            txtBoxDescripcion.Clear();
            txtBoxMonto.Clear();
            dgvServiciosCreados.ClearSelection();
            txtBoxDescripcion.Focus();
        }
    }
}
