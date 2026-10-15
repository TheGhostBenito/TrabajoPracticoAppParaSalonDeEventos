using System;
using System.Globalization;
using System.Windows.Forms;
using proyecto_integrador.models;
using proyecto_integrador.service;

namespace proyecto_integrador.pantallas
{
    public partial class SalonForm : Form
    {
        public SalonForm()
        {
            InitializeComponent();
            dgvSalonesCreados.AutoGenerateColumns = false; // Usamos las columnas que ya están definidas en el diseñador
            CargarGrilla();

            // Al abrir la pantalla, que no quede ninguna fila seleccionada
            this.Shown += delegate { dgvSalonesCreados.ClearSelection(); };
        }

        private void CargarGrilla()
        {
            dgvSalonesCreados.DataSource = null;
            dgvSalonesCreados.DataSource = SalonService.ObtenerSalones();
            dgvSalonesCreados.ClearSelection();
        }

        // Al hacer clic en una fila, se cargan sus datos en los textbox
        private void dgvSalonesCreados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Salon seleccionado = dgvSalonesCreados.Rows[e.RowIndex].DataBoundItem as Salon;
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
                SalonService.CrearSalon(txtBoxNombre.Text, txtBoxUbicacion.Text, monto);
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
            Salon seleccionado = ObtenerSalonSeleccionado();
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
                SalonService.ModificarSalon(seleccionado, txtBoxNombre.Text, txtBoxUbicacion.Text, monto);
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
            Salon seleccionado = ObtenerSalonSeleccionado();
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
                SalonService.EliminarSalon(seleccionado);
                CargarGrilla();
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private Salon ObtenerSalonSeleccionado()
        {
            if (dgvSalonesCreados.SelectedRows.Count == 0)
            {
                return null;
            }
            return dgvSalonesCreados.SelectedRows[0].DataBoundItem as Salon;
        }

        // Convierte el texto del monto a número. Eso es de la pantalla; las reglas (>= 0, etc.) las valida el service
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