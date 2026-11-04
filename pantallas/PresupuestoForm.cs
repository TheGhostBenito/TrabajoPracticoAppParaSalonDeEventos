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
        // La vista solo captura datos, arma DTOs, se los pasa al Controller y muestra lo que vuelve.
        private readonly PresupuestoController _controller;
        private readonly PresupuestoDto _presupuestoOriginal;
        private readonly BindingList<DetalleMenuDto> _menusPrincipales;
        private readonly BindingList<DetalleMenuDto> _menusPersonalizados;
        private List<MenuDto> _catalogoMenus;

        public event Action<PresupuestoDto> Guardado;

        // Constructor sin parámetros: lo necesita el diseñador de Visual Studio.
        public PresupuestoForm()
        {
            _controller = null;
            _presupuestoOriginal = null;
            _menusPrincipales = new BindingList<DetalleMenuDto>();
            _menusPersonalizados = new BindingList<DetalleMenuDto>();
            _catalogoMenus = new List<MenuDto>();
            InitializeComponent();
        }

        public PresupuestoForm(PresupuestoController controller)
            : this(controller, null)
        {
        }

        public PresupuestoForm(PresupuestoController controller, PresupuestoDto presupuesto)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _controller = controller;
            _presupuestoOriginal = presupuesto;
            _menusPrincipales = new BindingList<DetalleMenuDto>();
            _menusPersonalizados = new BindingList<DetalleMenuDto>();
            _catalogoMenus = new List<MenuDto>();

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
            }

            ActualizarTotal();
        }

        private int ObtenerIdPresupuestoOriginal()
        {
            if (_presupuestoOriginal == null)
            {
                return 0;
            }

            return _presupuestoOriginal.Id;
        }

        // El Controller devuelve los catálogos ya completos ,incluso con los datos del presupuesto que se modifica
        private void CargarCatalogos()
        {
            int presupuestoId = ObtenerIdPresupuestoOriginal();
            List<SalonDto> salones = _controller.ObtenerSalones(presupuestoId);
            _catalogoMenus = _controller.ObtenerMenus(presupuestoId);
            List<ServicioAdicionalDto> servicios = _controller.ObtenerServiciosAdicionales(presupuestoId);

            _comboSalon.DisplayMember = "Nombre";
            _comboSalon.DataSource = salones;
            DataGridViewComboBoxColumn columnaMenuPrincipal = _grillaMenusPrincipales.Columns["columnaMenuPrincipal"] as DataGridViewComboBoxColumn;
            DataGridViewComboBoxColumn columnaMenuPersonalizado = _grillaMenusPersonalizados.Columns["columnaMenuPersonalizado"] as DataGridViewComboBoxColumn;
            DataGridViewComboBoxColumn columnaParticularidad = _grillaMenusPersonalizados.Columns["columnaParticularidad"] as DataGridViewComboBoxColumn;
            columnaMenuPrincipal.DataSource = new List<MenuDto>(_catalogoMenus);
            columnaMenuPersonalizado.DataSource = new List<MenuDto>(_catalogoMenus);
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
            foreach (ServicioAdicionalDto servicio in servicios)
            {
                _listaServiciosDisponibles.Items.Add(servicio);
            }
        }

        private void ConfigurarEventos()
        {
            _comboSalon.SelectedIndexChanged += comboSalon_SelectedIndexChanged;
            _selectorFecha.ValueChanged += ActualizarTotalEvento;
            _selectorHoraInicio.ValueChanged += ActualizarTotalEvento;
            _numeroDuracion.ValueChanged += ActualizarTotalEvento;
            _numeroAdultos.ValueChanged += ActualizarTotalEvento;
            _numeroNinos.ValueChanged += ActualizarTotalEvento;
            _porcentajeMenuPrincipal.ValueChanged += ActualizarTotalEvento;
            _porcentajeMenuPersonalizado.ValueChanged += ActualizarTotalEvento;
            _grillaMenusPrincipales.DataError += grillaMenus_DataError;
            _grillaMenusPersonalizados.DataError += grillaMenus_DataError;
            _grillaMenusPrincipales.CurrentCellDirtyStateChanged += grillaMenus_CurrentCellDirtyStateChanged;
            _grillaMenusPersonalizados.CurrentCellDirtyStateChanged += grillaMenus_CurrentCellDirtyStateChanged;
            _grillaMenusPrincipales.CellValueChanged += ActualizarTotalEvento;
            _grillaMenusPersonalizados.CellValueChanged += ActualizarTotalEvento;
            _botonAgregarMenuPrincipal.Click += botonAgregarMenuPrincipal_Click;
            _botonQuitarMenuPrincipal.Click += botonQuitarMenuPrincipal_Click;
            _botonAgregarMenuPersonalizado.Click += botonAgregarMenuPersonalizado_Click;
            _botonQuitarMenuPersonalizado.Click += botonQuitarMenuPersonalizado_Click;
            _botonAgregarServicio.Click += botonAgregarServicio_Click;
            _botonQuitarServicio.Click += botonQuitarServicio_Click;
            _botonGuardar.Click += botonGuardar_Click;
            _menusPrincipales.ListChanged += ActualizarTotalEvento;
            _menusPersonalizados.ListChanged += ActualizarTotalEvento;
            _listaServiciosDisponibles.ItemCheck += listaServiciosDisponibles_ItemCheck;
            _listaServiciosPersonalizados.SelectedIndexChanged += ActualizarTotalEvento;
        }

        // Un único manejador para todos los controles que, al cambiar, solo tienen que refrescar el total.
        private void ActualizarTotalEvento(object sender, EventArgs e)
        {
            ActualizarTotal();
        }

        private void comboSalon_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarUbicacion();
            ActualizarTotal();
        }

        // ItemCheck se dispara artes de que cambie el tilde. Por eso se desconecta el evento un instante,
        // se aplica el tilde a mano, se vuelve a conectar y recién ahí se actualiza el total
        private void listaServiciosDisponibles_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            _listaServiciosDisponibles.ItemCheck -= listaServiciosDisponibles_ItemCheck;
            _listaServiciosDisponibles.SetItemCheckState(e.Index, e.NewValue);
            _listaServiciosDisponibles.ItemCheck += listaServiciosDisponibles_ItemCheck;
            ActualizarTotal();
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

        private void CargarPresupuestoExistente(PresupuestoDto presupuesto)
        {
            _textoNombreContacto.Text = presupuesto.NombreContacto;
            _textoEmail.Text = presupuesto.EmailContacto;
            _textoTelefono.Text = presupuesto.TelefonoContacto;
            _selectorFecha.Value = presupuesto.FechaEvento;
            _selectorHoraInicio.Value = DateTime.Today.Add(presupuesto.HoraInicio);
            _numeroDuracion.Value = presupuesto.DuracionHoras;
            _numeroAdultos.Value = presupuesto.CantidadAdultos;
            _numeroNinos.Value = presupuesto.CantidadNinos;
            _porcentajeMenuPrincipal.Value = presupuesto.PorcentajeAdicionalMenuPrincipal;
            _porcentajeMenuPersonalizado.Value = presupuesto.PorcentajeAdicionalMenuPersonalizado;
            SeleccionarSalon(presupuesto.SalonId);
            _textoUbicacion.Text = presupuesto.UbicacionEvento;

            _menusPrincipales.Clear();
            _menusPersonalizados.Clear();
            foreach (DetalleMenuPresupuestoDto detalle in presupuesto.Menues)
            {
                DetalleMenuDto detalleDto = new DetalleMenuDto();
                detalleDto.MenuId = detalle.MenuId;
                detalleDto.CantidadAdultos = detalle.CantidadAdultos;
                detalleDto.CantidadNinos = detalle.CantidadNinos;
                detalleDto.Particularidad = detalle.Particularidad;

                if (detalle.Particularidad == TipoDeExclusionDeMenu.Ninguna)
                {
                    _menusPrincipales.Add(detalleDto);
                }
                else
                {
                    _menusPersonalizados.Add(detalleDto);
                }
            }

            foreach (ServicioAdicionalDto servicio in presupuesto.ServiciosAdicionales)
            {
                int indice = BuscarServicioEnCatalogo(servicio.Id);
                if (servicio.Id > 0 && indice >= 0)
                {
                    _listaServiciosDisponibles.SetItemChecked(indice, true);
                }
                else
                {
                    ServicioAdicionalDto personalizado = new ServicioAdicionalDto();
                    personalizado.Id = 0;
                    personalizado.Descripcion = servicio.Descripcion;
                    personalizado.CostoDelServicio = servicio.CostoDelServicio;
                    _listaServiciosPersonalizados.Items.Add(personalizado);
                }
            }

            _textoObservaciones.Text = presupuesto.Observaciones;
        }

        private void botonAgregarMenuPrincipal_Click(object sender, EventArgs e)
        {
            AgregarDetalleMenu(_menusPrincipales, ObtenerIdPrimerMenu(), TipoDeExclusionDeMenu.Ninguna);
        }

        private void botonQuitarMenuPrincipal_Click(object sender, EventArgs e)
        {
            QuitarFila(_grillaMenusPrincipales, _menusPrincipales);
        }

        private void botonAgregarMenuPersonalizado_Click(object sender, EventArgs e)
        {
            AgregarDetalleMenu(_menusPersonalizados, ObtenerIdPrimerMenu(), TipoDeExclusionDeMenu.Celiaco);
        }

        private void botonQuitarMenuPersonalizado_Click(object sender, EventArgs e)
        {
            QuitarFila(_grillaMenusPersonalizados, _menusPersonalizados);
        }

        private int ObtenerIdPrimerMenu()
        {
            if (_catalogoMenus.Count == 0)
            {
                return 0;
            }

            return _catalogoMenus[0].Id;
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
                SalonDto salon = item as SalonDto;
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
                ServicioAdicionalDto servicio = _listaServiciosDisponibles.Items[indice] as ServicioAdicionalDto;
                if (servicio != null && servicio.Id == id)
                {
                    return indice;
                }
            }

            return -1;
        }

        private void ActualizarUbicacion()
        {
            SalonDto salon = _comboSalon.SelectedItem as SalonDto;
            if (salon == null)
            {
                _textoUbicacion.Text = string.Empty;
            }
            else
            {
                _textoUbicacion.Text = salon.Ubicacion;
            }
        }

        // El cálculo lo hace el Service; la vista solo muestra el resultado.
        private void ActualizarTotal()
        {
            ActualizarAsignacionMenuPrincipalUnico();
            decimal total = _controller.CalcularEstimacion(CrearSolicitud(), ObtenerIdPresupuestoOriginal());
            _etiquetaTotal.Text = "Total estimado: " + total.ToString("C2", CultureInfo.CurrentCulture);
        }

        // Con un único menú principal, las cantidades las reparte el Service (invitados que no comen menús especiales).
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
            decimal monto;
            if (!decimal.TryParse(_textoCostoPersonalizado.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out monto))
            {
                MostrarError("Ingresá un monto fijo válido.");
                _textoCostoPersonalizado.Focus();
                return;
            }

            string descripcion = _textoServicioPersonalizado.Text.Trim();

            // Las reglas (descripción obligatoria, monto >= 0) las valida el Service.
            try
            {
                _controller.ValidarServicioPersonalizado(descripcion, monto);
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
                _textoServicioPersonalizado.Focus();
                return;
            }

            ServicioAdicionalDto servicio = new ServicioAdicionalDto();
            servicio.Id = 0;
            servicio.Descripcion = descripcion;
            servicio.CostoDelServicio = monto;
            _listaServiciosPersonalizados.Items.Add(servicio);
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

                PresupuestoDto presupuestoGuardado;
                if (_presupuestoOriginal == null)
                {
                    presupuestoGuardado = _controller.Crear(solicitud);
                }
                else
                {
                    presupuestoGuardado = _controller.Modificar(_presupuestoOriginal.Id, solicitud);
                }

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

        // Arma el DTO con lo que hay en pantalla. No decide nada: el Service depura y valida.
        private PresupuestoRequestDto CrearSolicitud()
        {
            SalonDto salon = _comboSalon.SelectedItem as SalonDto;
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
            solicitud.SalonId = 0;
            if (salon != null)
            {
                solicitud.SalonId = salon.Id;
            }

            foreach (DetalleMenuDto detalle in _menusPrincipales)
            {
                solicitud.Menues.Add(detalle);
            }

            foreach (DetalleMenuDto detalle in _menusPersonalizados)
            {
                solicitud.Menues.Add(detalle);
            }

            foreach (object item in _listaServiciosDisponibles.CheckedItems)
            {
                ServicioAdicionalDto servicio = item as ServicioAdicionalDto;
                if (servicio != null)
                {
                    solicitud.ServiciosAdicionalesIds.Add(servicio.Id);
                }
            }

            foreach (object item in _listaServiciosPersonalizados.Items)
            {
                ServicioAdicionalDto servicio = item as ServicioAdicionalDto;
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

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
