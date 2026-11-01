using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using proyecto_integrador.controller;
using proyecto_integrador.dto;
using proyecto_integrador.models;

namespace proyecto_integrador.pantallas
{
    public partial class PresupuestoForm : Form
    {
        private readonly PresupuestoController _controller;
        private readonly Presupuesto _presupuestoOriginal;
        private readonly BindingList<DetalleMenuDto> _menusPrincipales;
        private readonly BindingList<DetalleMenuDto> _menusPersonalizados;
        private List<MenuDeComida> _catalogoMenus;
        private bool _ajustandoFecha;

        public event Action<Presupuesto> Guardado;

        public PresupuestoForm()
        {
            _controller = null;
            _presupuestoOriginal = null;
            _menusPrincipales = new BindingList<DetalleMenuDto>();
            _menusPersonalizados = new BindingList<DetalleMenuDto>();
            _catalogoMenus = new List<MenuDeComida>();
            InitializeComponent();
        }

        public PresupuestoForm(PresupuestoController controller)
            : this(controller, null)
        {
        }

        public PresupuestoForm(PresupuestoController controller, Presupuesto presupuesto)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _controller = controller;
            _presupuestoOriginal = presupuesto;
            _menusPrincipales = new BindingList<DetalleMenuDto>();
            _menusPersonalizados = new BindingList<DetalleMenuDto>();
            _catalogoMenus = new List<MenuDeComida>();

            InitializeComponent();
            _grillaMenusPrincipales.DataSource = _menusPrincipales;
            _grillaMenusPersonalizados.DataSource = _menusPersonalizados;
            CargarCatalogos();
            ConfigurarEventos();

            if (_presupuestoOriginal != null)
            {
                CargarPresupuestoExistente(_presupuestoOriginal);
                Text = "Modificar presupuesto";
            }
            else
            {
                if (_catalogoMenus.Count > 0)
                {
                    AgregarDetalleMenu(_menusPrincipales, _catalogoMenus[0].Id, TipoDeExclusionDeMenu.Ninguna);
                }

                ActualizarUbicacion();
                ActualizarTotal();
            }

            ActualizarTotal();
        }

        private void CargarCatalogos()
        {
            List<Salon> salones = _controller.ObtenerSalones();
            _catalogoMenus = _controller.ObtenerMenus();
            List<ServiciosAdicionales> servicios = _controller.ObtenerServiciosAdicionales();

            if (_presupuestoOriginal != null)
            {
                if (!salones.Exists(salon => salon.Id == _presupuestoOriginal.SalonDelEvento.Id))
                {
                    salones.Add(_presupuestoOriginal.SalonDelEvento);
                }

                foreach (DetalleDeMenus detalle in _presupuestoOriginal.Menues)
                {
                    if (!_catalogoMenus.Exists(menu => menu.Id == detalle.MenuElegido.Id))
                    {
                        _catalogoMenus.Add(detalle.MenuElegido);
                    }
                }

                foreach (ServiciosAdicionales servicio in _presupuestoOriginal.ServiciosAdicionales)
                {
                    if (servicio.Id > 0 && !servicios.Exists(item => item.Id == servicio.Id))
                    {
                        servicios.Add(servicio);
                    }
                }
            }

            _comboSalon.DisplayMember = "Nombre";
            _comboSalon.DataSource = salones;
            DataGridViewComboBoxColumn columnaMenuPrincipal = _grillaMenusPrincipales.Columns["columnaMenuPrincipal"] as DataGridViewComboBoxColumn;
            DataGridViewComboBoxColumn columnaMenuPersonalizado = _grillaMenusPersonalizados.Columns["columnaMenuPersonalizado"] as DataGridViewComboBoxColumn;
            DataGridViewComboBoxColumn columnaParticularidad = _grillaMenusPersonalizados.Columns["columnaParticularidad"] as DataGridViewComboBoxColumn;
            List<MenuDeComida> menusDisponibles = new List<MenuDeComida>(_catalogoMenus);
            columnaMenuPrincipal.DataSource = new List<MenuDeComida>(menusDisponibles);
            columnaMenuPersonalizado.DataSource = new List<MenuDeComida>(menusDisponibles);
            columnaMenuPrincipal.DisplayMember = "Nombre";
            columnaMenuPersonalizado.DisplayMember = "Nombre";
            columnaMenuPrincipal.ValueMember = "Id";
            columnaMenuPersonalizado.ValueMember = "Id";
            columnaParticularidad.DataSource = new object[]
            {
                TipoDeExclusionDeMenu.Celiaco,
                TipoDeExclusionDeMenu.Vegetariano,
                TipoDeExclusionDeMenu.Diabetico,
                TipoDeExclusionDeMenu.Vegano
            };

            _listaServiciosDisponibles.Items.Clear();
            foreach (ServiciosAdicionales servicio in servicios)
            {
                _listaServiciosDisponibles.Items.Add(servicio);
            }
        }

        private void ConfigurarEventos()
        {
            _comboSalon.SelectedIndexChanged += delegate { ActualizarUbicacion(); ActualizarTotal(); };
            _selectorFecha.ValueChanged += selectorFecha_ValueChanged;
            _selectorHoraInicio.ValueChanged += delegate { ActualizarTotal(); };
            _numeroDuracion.ValueChanged += delegate { ActualizarTotal(); };
            _numeroAdultos.ValueChanged += delegate { ActualizarTotal(); };
            _numeroNinos.ValueChanged += delegate { ActualizarTotal(); };
            _porcentajeMenuPrincipal.ValueChanged += delegate { ActualizarTotal(); };
            _porcentajeMenuPersonalizado.ValueChanged += delegate { ActualizarTotal(); };
            _grillaMenusPrincipales.DataError += grillaMenus_DataError;
            _grillaMenusPersonalizados.DataError += grillaMenus_DataError;
            _grillaMenusPrincipales.CurrentCellDirtyStateChanged += grillaMenus_CurrentCellDirtyStateChanged;
            _grillaMenusPersonalizados.CurrentCellDirtyStateChanged += grillaMenus_CurrentCellDirtyStateChanged;
            _grillaMenusPrincipales.CellValueChanged += delegate { ActualizarTotal(); };
            _grillaMenusPersonalizados.CellValueChanged += delegate { ActualizarTotal(); };
            _botonAgregarMenuPrincipal.Click += delegate { AgregarMenuPrincipal(); };
            _botonQuitarMenuPrincipal.Click += delegate { QuitarFila(_grillaMenusPrincipales, _menusPrincipales); };
            _botonAgregarMenuPersonalizado.Click += delegate { AgregarMenuPersonalizado(); };
            _botonQuitarMenuPersonalizado.Click += delegate { QuitarFila(_grillaMenusPersonalizados, _menusPersonalizados); };
            _botonAgregarServicio.Click += botonAgregarServicio_Click;
            _botonQuitarServicio.Click += botonQuitarServicio_Click;
            _botonGuardar.Click += botonGuardar_Click;
            _menusPrincipales.ListChanged += delegate { ActualizarTotal(); };
            _menusPersonalizados.ListChanged += delegate { ActualizarTotal(); };
            _listaServiciosDisponibles.ItemCheck += delegate
            {
                BeginInvoke((MethodInvoker)delegate { ActualizarTotal(); });
            };
            _listaServiciosPersonalizados.SelectedIndexChanged += delegate { ActualizarTotal(); };
        }

        private void grillaMenus_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void grillaMenus_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView grilla = sender as DataGridView;
            if (grilla != null
                && grilla.IsCurrentCellDirty
                && grilla.CurrentCell is DataGridViewComboBoxCell)
            {
                grilla.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void CargarPresupuestoExistente(Presupuesto presupuesto)
        {
            _textoNombreContacto.Text = presupuesto.NombreContacto;
            _textoEmail.Text = presupuesto.EmailContacto;
            _textoTelefono.Text = presupuesto.TelefonoContacto;
            _selectorFecha.Value = presupuesto.FechaEvento;
            _selectorHoraInicio.Value = DateTime.Today.Add(presupuesto.HoraInicio);
            _numeroDuracion.Value = presupuesto.Evento.Duracion;
            _numeroAdultos.Value = presupuesto.Evento.CantidadAdultos;
            _numeroNinos.Value = presupuesto.Evento.CantidadNinos;
            _porcentajeMenuPrincipal.Value = presupuesto.PorcentajeAdicionalMenuPrincipal;
            _porcentajeMenuPersonalizado.Value = presupuesto.PorcentajeAdicionalMenuPersonalizado;
            SeleccionarSalon(presupuesto.SalonDelEvento.Id);
            _textoUbicacion.Text = presupuesto.UbicacionEvento;

            _menusPrincipales.Clear();
            _menusPersonalizados.Clear();
            foreach (DetalleDeMenus detalle in presupuesto.Menues)
            {
                DetalleMenuDto detalleDto = new DetalleMenuDto();
                detalleDto.MenuId = detalle.MenuElegido.Id;
                detalleDto.CantidadAdultos = detalle.CantidadAdultos;
                detalleDto.CantidadNinos = detalle.CantidadNinos;
                detalleDto.Particularidad = detalle.ExclusionDelMenu;

                if (detalle.ExclusionDelMenu == TipoDeExclusionDeMenu.Ninguna)
                {
                    _menusPrincipales.Add(detalleDto);
                }
                else
                {
                    _menusPersonalizados.Add(detalleDto);
                }
            }

            foreach (ServiciosAdicionales servicio in presupuesto.ServiciosAdicionales)
            {
                int indice = BuscarServicioEnCatalogo(servicio.Id);
                if (servicio.Id > 0 && indice >= 0)
                {
                    _listaServiciosDisponibles.SetItemChecked(indice, true);
                }
                else
                {
                    _listaServiciosPersonalizados.Items.Add(
                        new ServiciosAdicionales(0, servicio.Descripcion, servicio.CostoDelServicio));
                }
            }

            _textoObservaciones.Text = presupuesto.Observaciones;
        }

        private void selectorFecha_ValueChanged(object sender, EventArgs e)
        {
            if (!_ajustandoFecha && _selectorFecha.Value.Date < DateTime.Today)
            {
                _ajustandoFecha = true;
                MessageBox.Show(
                    "Estás seleccionando una fecha que no tiene validez en torno al contexto actual. Solo se pueden elegir fechas actuales o a futuro.",
                    "Fecha no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                _selectorFecha.Value = DateTime.Today;
                _ajustandoFecha = false;
            }

            ActualizarTotal();
        }

        private void AgregarMenuPrincipal()
        {
            int menuId = _catalogoMenus.Count == 0 ? 0 : _catalogoMenus[0].Id;
            AgregarDetalleMenu(_menusPrincipales, menuId, TipoDeExclusionDeMenu.Ninguna);
        }

        private void AgregarMenuPersonalizado()
        {
            int menuId = _catalogoMenus.Count == 0 ? 0 : _catalogoMenus[0].Id;
            AgregarDetalleMenu(_menusPersonalizados, menuId, TipoDeExclusionDeMenu.Celiaco);
        }

        private void AgregarDetalleMenu(BindingList<DetalleMenuDto> lista, int menuId, TipoDeExclusionDeMenu particularidad)
        {
            DetalleMenuDto detalle = new DetalleMenuDto();
            detalle.MenuId = menuId;
            detalle.Particularidad = particularidad;
            detalle.CantidadAdultos = 0;
            detalle.CantidadNinos = 0;
            lista.Add(detalle);
            ActualizarTotal();
        }

        private void QuitarFila(DataGridView grilla, BindingList<DetalleMenuDto> lista)
        {
            if (grilla.CurrentRow == null)
            {
                return;
            }

            DetalleMenuDto detalle = grilla.CurrentRow.DataBoundItem as DetalleMenuDto;
            if (detalle != null)
            {
                lista.Remove(detalle);
                ActualizarTotal();
            }
        }

        private void SeleccionarSalon(int id)
        {
            foreach (object item in _comboSalon.Items)
            {
                Salon salon = item as Salon;
                if (salon != null && salon.Id == id)
                {
                    _comboSalon.SelectedItem = item;
                    return;
                }
            }
        }

        private int BuscarServicioEnCatalogo(int id)
        {
            for (int indice = 0; indice < _listaServiciosDisponibles.Items.Count; indice++)
            {
                ServiciosAdicionales servicio = _listaServiciosDisponibles.Items[indice] as ServiciosAdicionales;
                if (servicio != null && servicio.Id == id)
                {
                    return indice;
                }
            }

            return -1;
        }

        private void ActualizarUbicacion()
        {
            Salon salon = _comboSalon.SelectedItem as Salon;
            _textoUbicacion.Text = salon == null ? string.Empty : salon.Ubicacion;
        }

        private void ActualizarTotal()
        {
            ActualizarAsignacionMenuPrincipalUnico();
            int presupuestoExistenteId = _presupuestoOriginal == null ? 0 : _presupuestoOriginal.Id;
            decimal total = _controller.CalcularEstimacion(CrearSolicitud(), presupuestoExistenteId);
            _etiquetaTotal.Text = "Total estimado: " + total.ToString("C2", CultureInfo.CurrentCulture);
        }

        private void ActualizarAsignacionMenuPrincipalUnico()
        {
            bool asignacionAutomatica = _menusPrincipales.Count == 1;
            DataGridViewColumn columnaAdultos = _grillaMenusPrincipales.Columns["columnaCantidadAdultosPrincipal"];
            DataGridViewColumn columnaNinos = _grillaMenusPrincipales.Columns["columnaCantidadNinosPrincipal"];

            if (columnaAdultos != null)
            {
                columnaAdultos.ReadOnly = asignacionAutomatica;
            }

            if (columnaNinos != null)
            {
                columnaNinos.ReadOnly = asignacionAutomatica;
            }

            if (!asignacionAutomatica)
            {
                return;
            }

            DetalleMenuDto menuPrincipal = _menusPrincipales[0];
            AsignacionMenuDto asignacion = _controller.CalcularInvitadosRestantes(
                decimal.ToInt32(_numeroAdultos.Value),
                decimal.ToInt32(_numeroNinos.Value),
                new List<DetalleMenuDto>(_menusPersonalizados));
            menuPrincipal.CantidadAdultos = asignacion.CantidadAdultos;
            menuPrincipal.CantidadNinos = asignacion.CantidadNinos;
        }

        private void botonAgregarServicio_Click(object sender, EventArgs e)
        {
            string descripcion = _textoServicioPersonalizado.Text.Trim();
            decimal monto;
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MostrarError("Ingresá la descripción del servicio personalizado.");
                _textoServicioPersonalizado.Focus();
                return;
            }

            if (!decimal.TryParse(_textoCostoPersonalizado.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out monto) || monto < 0)
            {
                MostrarError("Ingresá un monto fijo válido y mayor o igual a cero.");
                _textoCostoPersonalizado.Focus();
                return;
            }

            _listaServiciosPersonalizados.Items.Add(new ServiciosAdicionales(0, descripcion, monto));
            _textoServicioPersonalizado.Clear();
            _textoCostoPersonalizado.Clear();
            ActualizarTotal();
        }

        private void botonQuitarServicio_Click(object sender, EventArgs e)
        {
            int indice = _listaServiciosPersonalizados.SelectedIndex;
            if (indice >= 0)
            {
                _listaServiciosPersonalizados.Items.RemoveAt(indice);
                ActualizarTotal();
            }
        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                _grillaMenusPrincipales.EndEdit();
                _grillaMenusPersonalizados.EndEdit();
                PresupuestoRequestDto solicitud = CrearSolicitud();
                Presupuesto presupuestoGuardado = _presupuestoOriginal == null
                    ? _controller.Crear(solicitud)
                    : _controller.Modificar(_presupuestoOriginal.Id, solicitud);

                string mensaje = "Se terminó el presupuesto #"
                    + presupuestoGuardado.Id.ToString(CultureInfo.InvariantCulture)
                    + ".\n\nPodés exportarlo como PDF desde el Historial de presupuestos.";

                MessageBox.Show(mensaje, "Presupuesto terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (Guardado != null)
                {
                    Guardado(presupuestoGuardado);
                }
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MostrarError(ex.Message);
            }
        }

        private PresupuestoRequestDto CrearSolicitud()
        {
            Salon salon = _comboSalon.SelectedItem as Salon;
            PresupuestoRequestDto solicitud = new PresupuestoRequestDto();
            solicitud.NombreContacto = _textoNombreContacto.Text;
            solicitud.EmailContacto = _textoEmail.Text;
            solicitud.TelefonoContacto = _textoTelefono.Text;
            solicitud.UbicacionEvento = _textoUbicacion.Text;
            solicitud.Observaciones = _textoObservaciones.Text;
            solicitud.FechaEvento = _selectorFecha.Value.Date;
            solicitud.HoraInicio = _selectorHoraInicio.Value.TimeOfDay;
            solicitud.DuracionHoras = decimal.ToInt32(_numeroDuracion.Value);
            solicitud.CantidadAdultos = decimal.ToInt32(_numeroAdultos.Value);
            solicitud.CantidadNinos = decimal.ToInt32(_numeroNinos.Value);
            solicitud.PorcentajeAdicionalMenuPrincipal = _porcentajeMenuPrincipal.Value;
            solicitud.PorcentajeAdicionalMenuPersonalizado = _porcentajeMenuPersonalizado.Value;
            solicitud.SalonId = salon == null ? 0 : salon.Id;

            foreach (DetalleMenuDto detalle in _menusPrincipales)
            {
                if (_menusPrincipales.Count != 1 || detalle.CantidadAdultos + detalle.CantidadNinos > 0)
                {
                    solicitud.Menues.Add(CopiarDetalleMenu(detalle, TipoDeExclusionDeMenu.Ninguna));
                }
            }

            foreach (DetalleMenuDto detalle in _menusPersonalizados)
            {
                solicitud.Menues.Add(CopiarDetalleMenu(detalle, detalle.Particularidad));
            }

            foreach (object item in _listaServiciosDisponibles.CheckedItems)
            {
                ServiciosAdicionales servicio = item as ServiciosAdicionales;
                if (servicio != null)
                {
                    solicitud.ServiciosAdicionalesIds.Add(servicio.Id);
                }
            }

            foreach (object item in _listaServiciosPersonalizados.Items)
            {
                ServiciosAdicionales servicio = item as ServiciosAdicionales;
                if (servicio != null)
                {
                    ServicioPersonalizadoDto servicioDto = new ServicioPersonalizadoDto();
                    servicioDto.Descripcion = servicio.Descripcion;
                    servicioDto.MontoFijo = servicio.CostoDelServicio;
                    solicitud.ServiciosPersonalizados.Add(servicioDto);
                }
            }

            return solicitud;
        }

        private static DetalleMenuDto CopiarDetalleMenu(DetalleMenuDto detalle, TipoDeExclusionDeMenu particularidad)
        {
            DetalleMenuDto copia = new DetalleMenuDto();
            copia.MenuId = detalle.MenuId;
            copia.CantidadAdultos = detalle.CantidadAdultos;
            copia.CantidadNinos = detalle.CantidadNinos;
            copia.Particularidad = particularidad;
            return copia;
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
