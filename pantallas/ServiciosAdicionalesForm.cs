using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proyecto_integrador.repository; // Llama y accede a la clase ServiciosAdicionalesRepository. Que se encuentra dentro de repository
using proyecto_integrador.models; // Llama y accede a la clase ServiciosAdicionales. Que se encuentra dentro de model

namespace proyecto_integrador.pantallas
{
    public partial class ServiciosAdicionalesForm : Form
    {
        public ServiciosAdicionalesForm()
        {
            InitializeComponent();
            CargarServicios();
        }

        private void CargarServicios()
        {
            var servicios = new ServiciosAdicionalesRepository().ObtenerTodos();
            dgvServiciosCreados.DataSource = servicios;
        }

        private void btnCrearServicio_Click(object sender, EventArgs e)
        {
            string descripcion = txtBoxDescripcion.Text;
            decimal monto = decimal.Parse(txtBoxMonto.Text);

            var nuevoServicio = new models.ServiciosAdicionales(0, descripcion, monto);
            new ServiciosAdicionalesRepository().Crear(nuevoServicio);

            CargarServicios();
        }

        private void btnEliminarServicio_Click(object sender, EventArgs e)
        {
            if (dgvServiciosCreados.CurrentRow != null)
            {
                int id = (int)dgvServiciosCreados.SelectedRows[0].Cells["dgvID"].Value;
                new ServiciosAdicionalesRepository().Eliminar(id);

                CargarServicios();
            }
        }

        private void btnModificarServicio_Click(object sender, EventArgs e)
        {
            if (dgvServiciosCreados.CurrentRow != null)
            {
                int id = (int)dgvServiciosCreados.SelectedRows[0].Cells["dgvID"].Value;
                string descripcion = txtBoxDescripcion.Text;
                decimal monto = decimal.Parse(txtBoxMonto.Text);


                var servicioModificado = new models.ServiciosAdicionales(id, descripcion, monto);
                new ServiciosAdicionalesRepository().Modificar(servicioModificado);


                CargarServicios();
            }
        }
    }
}
