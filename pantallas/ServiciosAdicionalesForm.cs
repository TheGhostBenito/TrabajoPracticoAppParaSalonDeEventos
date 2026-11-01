using System;
using System.Globalization;
using System.Windows.Forms;
using proyecto_integrador.Service;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.pantallas
{
    public partial class ServiciosAdicionalesForm : Form
    {
        private readonly ServiciosAdicionalesService servicio;

        public ServiciosAdicionalesForm()
        {
            InitializeComponent();
            servicio = new ServiciosAdicionalesService(ServiciosAdicionalesRepository.Instance);
            dgvServiciosCreados.AutoGenerateColumns = false;
            CargarServicios();
        }

        private void CargarServicios()
        {
            dgvServiciosCreados.DataSource = null;
            dgvServiciosCreados.DataSource = servicio.ObtenerTodos();
            dgvServiciosCreados.ClearSelection();
        }

        private void dgvServiciosCreados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            ServiciosAdicionales seleccionado = dgvServiciosCreados.Rows[e.RowIndex].DataBoundItem as ServiciosAdicionales;
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
                servicio.Crear(txtBoxDescripcion.Text, monto);
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
            ServiciosAdicionales seleccionado = ObtenerServicioSeleccionado();
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
                servicio.Eliminar(seleccionado.Id);
                CargarServicios();
                LimpiarCampos();
            }
        }

        private void btnModificarServicio_Click(object sender, EventArgs e)
        {
            ServiciosAdicionales seleccionado = ObtenerServicioSeleccionado();
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
                bool modificado = servicio.Modificar(seleccionado.Id, txtBoxDescripcion.Text, monto);
                if (!modificado)
                {
                    MessageBox.Show("No se encontró el servicio seleccionado.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CargarServicios();
                LimpiarCampos();
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
        }

        private ServiciosAdicionales ObtenerServicioSeleccionado()
        {
            if (dgvServiciosCreados.CurrentRow == null)
            {
                return null;
            }

            return dgvServiciosCreados.CurrentRow.DataBoundItem as ServiciosAdicionales;
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
