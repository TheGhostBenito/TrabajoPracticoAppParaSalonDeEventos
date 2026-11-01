using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proyecto_integrador.models;
using proyecto_integrador.repository;
using proyecto_integrador.Service;

namespace proyecto_integrador.pantallas
{
    public partial class MenuDeComidaForm : Form
    {
        private readonly MenuService _service;

        public MenuDeComidaForm()
        {
            InitializeComponent();
            _service = new MenuService(MenuRepository.Instance);

            dgvMenusCreados.AutoGenerateColumns = false; // Usamos las columnas definidas en el diseñador
            CargarGrilla();

            // Al abrir la pantalla, que no quede ninguna fila seleccionada
            this.Shown += delegate { dgvMenusCreados.ClearSelection(); };
        }

        // ---------- READ ----------
        private void CargarGrilla()
        {
            dgvMenusCreados.DataSource = null;
            dgvMenusCreados.DataSource = _service.ObtenerTodos();
            dgvMenusCreados.ClearSelection();
        }

        // Al hacer clic en una fila, se cargan sus datos en los textbox
        private void dgvMenusCreados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            MenuDeComida seleccionado = dgvMenusCreados.Rows[e.RowIndex].DataBoundItem as MenuDeComida;
            if (seleccionado == null)
            {
                return;
            }

            txtBoxNombre.Text = seleccionado.Nombre;
            txtBoxCostoAdulto.Text = seleccionado.CostoPorAdulto.ToString("0.##", CultureInfo.CurrentCulture);
            txtBoxCostoNino.Text = seleccionado.CostoPorNiño.ToString("0.##", CultureInfo.CurrentCulture);
        }

        // ---------- CREATE ----------
        private void btnCrearMenu_Click(object sender, EventArgs e)
        {
            decimal costoAdulto, costoNino;
            if (!TryObtenerCostos(out costoAdulto, out costoNino))
            {
                return;
            }

            try
            {
                _service.Crear(txtBoxNombre.Text, costoAdulto, costoNino);
                CargarGrilla();
                LimpiarCampos();
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
        }

        // ---------- UPDATE ----------
        private void btnModificarMenu_Click(object sender, EventArgs e)
        {
            MenuDeComida seleccionado = ObtenerMenuSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccioná un menú de la lista para modificarlo.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal costoAdulto, costoNino;
            if (!TryObtenerCostos(out costoAdulto, out costoNino))
            {
                return;
            }

            try
            {
                _service.Modificar(seleccionado.Id, txtBoxNombre.Text, costoAdulto, costoNino);
                CargarGrilla();
                LimpiarCampos();
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
        }

        // ---------- DELETE ----------
        private void btnEliminarMenu_Click(object sender, EventArgs e)
        {
            MenuDeComida seleccionado = ObtenerMenuSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccioná un menú de la lista para eliminarlo.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que querés eliminar el menú \"" + seleccionado.Nombre + "\"?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                _service.Eliminar(seleccionado.Id);
                CargarGrilla();
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // ---------- Auxiliares ----------
        private MenuDeComida ObtenerMenuSeleccionado()
        {
            if (dgvMenusCreados.SelectedRows.Count == 0)
            {
                return null;
            }
            return dgvMenusCreados.SelectedRows[0].DataBoundItem as MenuDeComida;
        }

        // Convierte el texto a números. Eso es de la pantalla; las reglas (>= 0, nombre vacío) las valida el service
        private bool TryObtenerCostos(out decimal costoAdulto, out decimal costoNino)
        {
            costoNino = 0;

            if (!decimal.TryParse(txtBoxCostoAdulto.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out costoAdulto))
            {
                MostrarError("El costo por adulto tiene que ser un número.");
                txtBoxCostoAdulto.Focus();
                return false;
            }

            if (!decimal.TryParse(txtBoxCostoNino.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out costoNino))
            {
                MostrarError("El costo por niño tiene que ser un número.");
                txtBoxCostoNino.Focus();
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
            txtBoxCostoAdulto.Clear();
            txtBoxCostoNino.Clear();
            dgvMenusCreados.ClearSelection();
            txtBoxNombre.Focus();
        }
    }
}