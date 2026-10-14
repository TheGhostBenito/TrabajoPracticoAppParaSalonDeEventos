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

namespace proyecto_integrador.pantallas
{
    public partial class SalonForm : Form
    {
        public SalonForm()
        {
            InitializeComponent();
            dgvSalonesCreados.AutoGenerateColumns = false;
            CargarGrilla();

            // al abrir la pantalla, que no quede ninguna fila seleccionad
            this.Shown += delegate { dgvSalonesCreados.ClearSelection(); };
        }

        // ---------- READ ----------
        private void CargarGrilla()
        {
            dgvSalonesCreados.DataSource = null;
            dgvSalonesCreados.DataSource = SalonRepository.ObtenerSalones();
            dgvSalonesCreados.ClearSelection();
        }

        // Al hacer click se scargan los datos en los tex
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

        // create
        private void btnCrearSalon_Click(object sender, EventArgs e)
        {
            string nombre, ubicacion;
            decimal monto;

            if (!ValidarCampos(out nombre, out ubicacion, out monto))
            {
                return;
            }

            SalonRepository.AgregarSalon(nombre, ubicacion, monto);
            CargarGrilla();
            LimpiarCampos();
        }

        // update
        private void btnModificarSalon_Click(object sender, EventArgs e)
        {
            Salon seleccionado = ObtenerSalonSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccioná un salón de la lista para modificarlo.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string nombre, ubicacion;
            decimal monto;

            if (!ValidarCampos(out nombre, out ubicacion, out monto))
            {
                return;
            }

            SalonRepository.ModificarSalon(seleccionado, nombre, ubicacion, monto);
            CargarGrilla();
            LimpiarCampos();
        }

        // delete
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
                SalonRepository.EliminarSalon(seleccionado);
                CargarGrilla();
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        //auxiliates
        private Salon ObtenerSalonSeleccionado()
        {
            if (dgvSalonesCreados.SelectedRows.Count == 0)
            {
                return null;
            }
            return dgvSalonesCreados.SelectedRows[0].DataBoundItem as Salon;
        }

        private bool ValidarCampos(out string nombre, out string ubicacion, out decimal monto)
        {
            nombre = txtBoxNombre.Text.Trim();
            ubicacion = txtBoxUbicacion.Text.Trim();
            monto = 0;

            if (nombre.Length == 0)
            {
                MostrarError("Ingresá el nombre del salón.", txtBoxNombre);
                return false;
            }

            if (ubicacion.Length == 0)
            {
                MostrarError("Ingresá la ubicación del salón.", txtBoxUbicacion);
                return false;
            }

            if (!decimal.TryParse(txtBoxMonto.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out monto)
                || monto < 0)
            {
                MostrarError("El monto tiene que ser un número mayor o igual a 0.", txtBoxMonto);
                return false;
            }

            return true;
        }

        private void MostrarError(string mensaje, Control campo)
        {
            MessageBox.Show(mensaje, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            campo.Focus();
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