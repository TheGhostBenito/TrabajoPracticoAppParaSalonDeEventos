using System;
using System.Globalization;
using System.Windows.Forms;
using proyecto_integrador.controller;
using proyecto_integrador.dto;

namespace proyecto_integrador.pantallas
{
    public partial class MenuDeComidaForm : Form
    {
        // La vista solo conoce al Controller y a los DTO.
        private readonly MenuController _controller;

        // Constructor sin parámetros: lo necesita el diseñador de Visual Studio.
        public MenuDeComidaForm()
        {
            InitializeComponent();
        }

        public MenuDeComidaForm(MenuController controller)
            : this()
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _controller = controller;
            dgvMenusCreados.AutoGenerateColumns = false; // Usamos las columnas definidas en el diseñador
            CargarGrilla();

            // Al abrir la pantalla, que no quede ninguna fila seleccionada
            this.Shown += MenuDeComidaForm_Shown;
        }

        private void MenuDeComidaForm_Shown(object sender, EventArgs e)
        {
            dgvMenusCreados.ClearSelection();
        }

        // ---------- READ ----------
        private void CargarGrilla()
        {
            dgvMenusCreados.DataSource = null;
            dgvMenusCreados.DataSource = _controller.ObtenerTodos();
            dgvMenusCreados.ClearSelection();
        }

        // Al hacer clic en una fila, se cargan sus datos en los textbox
        private void dgvMenusCreados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            MenuDto seleccionado = dgvMenusCreados.Rows[e.RowIndex].DataBoundItem as MenuDto;
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
                _controller.Crear(CrearDto(0, costoAdulto, costoNino));
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
            MenuDto seleccionado = ObtenerMenuSeleccionado();
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
                bool modificado = _controller.Modificar(CrearDto(seleccionado.Id, costoAdulto, costoNino));
                if (!modificado)
                {
                    MessageBox.Show("No se encontró el menú seleccionado.", "Atención",
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

        // ---------- DELETE ----------
        private void btnEliminarMenu_Click(object sender, EventArgs e)
        {
            MenuDto seleccionado = ObtenerMenuSeleccionado();
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
                _controller.Eliminar(seleccionado.Id);
                CargarGrilla();
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // ---------- Auxiliares ----------
        private MenuDto ObtenerMenuSeleccionado()
        {
            if (dgvMenusCreados.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgvMenusCreados.SelectedRows[0].DataBoundItem as MenuDto;
        }

        // Arma el DTO con lo escrito en pantalla. Las reglas las valida el Service.
        private MenuDto CrearDto(int id, decimal costoAdulto, decimal costoNino)
        {
            MenuDto dto = new MenuDto();
            dto.Id = id;
            dto.Nombre = txtBoxNombre.Text;
            dto.CostoPorAdulto = costoAdulto;
            dto.CostoPorNiño = costoNino;
            return dto;
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
