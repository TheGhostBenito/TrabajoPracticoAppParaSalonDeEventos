using System;
using System.Collections.Generic;
using System.Net.Mail;
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    public class PresupuestoService : IPresupuestoService
    {
        // Depende de INTERFACES, no de las clases concretas: así se puede cambiar el repositorio sin tocar este service.
        private readonly IPresupuestoRepository _presupuestoRepository;
        private readonly ISalonRepository _salonRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IServiciosAdicionalesRepository _serviciosRepository;
        private readonly IPresupuestoPdfService _pdfService;

        public PresupuestoService(
            IPresupuestoRepository presupuestoRepository,
            ISalonRepository salonRepository,
            IMenuRepository menuRepository,
            IServiciosAdicionalesRepository serviciosRepository,
            IPresupuestoPdfService pdfService)
        {
            if (presupuestoRepository == null)
            {
                throw new ArgumentNullException("presupuestoRepository");
            }

            if (salonRepository == null)
            {
                throw new ArgumentNullException("salonRepository");
            }

            if (menuRepository == null)
            {
                throw new ArgumentNullException("menuRepository");
            }

            if (serviciosRepository == null)
            {
                throw new ArgumentNullException("serviciosRepository");
            }

            if (pdfService == null)
            {
                throw new ArgumentNullException("pdfService");
            }

            _presupuestoRepository = presupuestoRepository;
            _salonRepository = salonRepository;
            _menuRepository = menuRepository;
            _serviciosRepository = serviciosRepository;
            _pdfService = pdfService;
        }

        // ---------- Consultas ----------

        public List<PresupuestoDto> ObtenerTodos()
        {
            List<PresupuestoDto> resultado = new List<PresupuestoDto>();
            foreach (Presupuesto presupuesto in _presupuestoRepository.ObtenerTodos())
            {
                resultado.Add(ConvertirADto(presupuesto));
            }

            return resultado;
        }

        // Salones para elegir. Si se está modificando un presupuesto, se incluye el salón que ya tenía aunque se haya borrado.
        public List<SalonDto> ObtenerSalonesDisponibles(int presupuestoId)
        {
            List<SalonDto> resultado = new List<SalonDto>();
            foreach (Salon salon in _salonRepository.ObtenerTodos())
            {
                resultado.Add(ConvertirSalonADto(salon));
            }

            Presupuesto existente = _presupuestoRepository.ObtenerPorId(presupuestoId);
            if (existente != null)
            {
                bool yaEstaEnLaLista = false;
                foreach (SalonDto salonDto in resultado)
                {
                    if (salonDto.Id == existente.SalonDelEvento.Id)
                    {
                        yaEstaEnLaLista = true;
                    }
                }

                if (!yaEstaEnLaLista)
                {
                    resultado.Add(ConvertirSalonADto(existente.SalonDelEvento));
                }
            }

            return resultado;
        }

        public List<MenuDto> ObtenerMenusDisponibles(int presupuestoId)
        {
            List<MenuDto> resultado = new List<MenuDto>();
            foreach (MenuDeComida menu in _menuRepository.ObtenerTodos())
            {
                resultado.Add(ConvertirMenuADto(menu));
            }

            Presupuesto existente = _presupuestoRepository.ObtenerPorId(presupuestoId);
            if (existente != null)
            {
                foreach (DetalleDeMenus detalle in existente.Menues)
                {
                    bool yaEstaEnLaLista = false;
                    foreach (MenuDto menuDto in resultado)
                    {
                        if (menuDto.Id == detalle.MenuElegido.Id)
                        {
                            yaEstaEnLaLista = true;
                        }
                    }

                    if (!yaEstaEnLaLista)
                    {
                        resultado.Add(ConvertirMenuADto(detalle.MenuElegido));
                    }
                }
            }

            return resultado;
        }

        public List<ServicioAdicionalDto> ObtenerServiciosDisponibles(int presupuestoId)
        {
            List<ServicioAdicionalDto> resultado = new List<ServicioAdicionalDto>();
            foreach (ServiciosAdicionales servicio in _serviciosRepository.ObtenerTodos())
            {
                resultado.Add(ConvertirServicioADto(servicio));
            }

            Presupuesto existente = _presupuestoRepository.ObtenerPorId(presupuestoId);
            if (existente != null)
            {
                foreach (ServiciosAdicionales servicio in existente.ServiciosAdicionales)
                {
                    bool yaEstaEnLaLista = false;
                    foreach (ServicioAdicionalDto servicioDto in resultado)
                    {
                        if (servicioDto.Id == servicio.Id)
                        {
                            yaEstaEnLaLista = true;
                        }
                    }

                    if (servicio.Id > 0 && !yaEstaEnLaLista)
                    {
                        resultado.Add(ConvertirServicioADto(servicio));
                    }
                }
            }

            return resultado;
        }

        // ---------- Cálculos ----------

        public AsignacionMenuDto CalcularInvitadosRestantes(
            int cantidadAdultos,
            int cantidadNinos,
            List<DetalleMenuDto> menusPersonalizados)
        {
            if (menusPersonalizados == null)
            {
                throw new ArgumentNullException("menusPersonalizados");
            }

            int adultosPersonalizados = 0;
            int ninosPersonalizados = 0;
            foreach (DetalleMenuDto detalle in menusPersonalizados)
            {
                if (detalle != null)
                {
                    adultosPersonalizados += detalle.CantidadAdultos;
                    ninosPersonalizados += detalle.CantidadNinos;
                }
            }

            int adultosRestantes = Math.Max(0, cantidadAdultos - adultosPersonalizados);
            int ninosRestantes = Math.Max(0, cantidadNinos - ninosPersonalizados);
            return new AsignacionMenuDto(adultosRestantes, ninosRestantes);
        }

        public decimal CalcularEstimacion(PresupuestoRequestDto solicitud, int presupuestoExistenteId)
        {
            if (solicitud == null)
            {
                return 0m;
            }

            DepurarMenus(solicitud);

            Presupuesto existente = null;
            if (presupuestoExistenteId > 0)
            {
                existente = _presupuestoRepository.ObtenerPorId(presupuestoExistenteId);
            }

            decimal costoSalon = 0m;
            Salon salon = BuscarSalon(solicitud.SalonId, existente);
            if (salon != null && solicitud.DuracionHoras > 0)
            {
                costoSalon = salon.CostoBase * solicitud.DuracionHoras;
            }

            decimal subtotalMenusPrincipales = 0m;
            decimal subtotalMenusPersonalizados = 0m;
            if (solicitud.Menues != null)
            {
                foreach (DetalleMenuDto detalle in solicitud.Menues)
                {
                    if (detalle == null || detalle.CantidadAdultos < 0 || detalle.CantidadNinos < 0)
                    {
                        continue;
                    }

                    MenuDeComida menu = BuscarMenu(detalle.MenuId, existente);
                    if (menu == null)
                    {
                        continue;
                    }

                    decimal subtotal = detalle.CantidadAdultos * menu.CostoPorAdulto
                        + detalle.CantidadNinos * menu.CostoPorNiño;
                    if (detalle.Particularidad == TipoDeExclusionDeMenu.Ninguna)
                    {
                        subtotalMenusPrincipales += subtotal;
                    }
                    else
                    {
                        subtotalMenusPersonalizados += subtotal;
                    }
                }
            }

            decimal recargoPrincipal = decimal.Round(
                subtotalMenusPrincipales * solicitud.PorcentajeAdicionalMenuPrincipal / 100m, 2);
            decimal recargoPersonalizado = decimal.Round(
                subtotalMenusPersonalizados * solicitud.PorcentajeAdicionalMenuPersonalizado / 100m, 2);
            decimal costoServicios = CalcularCostoServiciosEstimados(solicitud, existente);
            return decimal.Round(
                costoSalon + subtotalMenusPrincipales + subtotalMenusPersonalizados
                + recargoPrincipal + recargoPersonalizado + costoServicios,
                2);
        }

        private decimal CalcularCostoServiciosEstimados(PresupuestoRequestDto solicitud, Presupuesto existente)
        {
            decimal subtotal = 0m;

            if (solicitud.ServiciosAdicionalesIds != null)
            {
                foreach (int servicioId in QuitarRepetidos(solicitud.ServiciosAdicionalesIds))
                {
                    ServiciosAdicionales servicio = BuscarServicio(servicioId, existente);
                    if (servicio != null)
                    {
                        subtotal += servicio.CostoDelServicio;
                    }
                }
            }

            if (solicitud.ServiciosPersonalizados != null)
            {
                foreach (ServicioPersonalizadoDto servicio in solicitud.ServiciosPersonalizados)
                {
                    if (servicio != null && servicio.MontoFijo >= 0)
                    {
                        subtotal += servicio.MontoFijo;
                    }
                }
            }

            return subtotal;
        }

        // ---------- Alta, modificación y PDF ----------

        public PresupuestoDto Crear(PresupuestoRequestDto solicitud)
        {
            DepurarMenus(solicitud);
            Validar(solicitud, null);
            Presupuesto presupuesto = ConstruirPresupuesto(solicitud, 0, DateTime.Now, null);
            _presupuestoRepository.Agregar(presupuesto);
            return ConvertirADto(presupuesto);
        }

        public PresupuestoDto Modificar(int id, PresupuestoRequestDto solicitud)
        {
            Presupuesto existente = _presupuestoRepository.ObtenerPorId(id);
            if (existente == null)
            {
                throw new ArgumentException("No se encontró el presupuesto seleccionado.");
            }

            DepurarMenus(solicitud);
            Validar(solicitud, existente);
            Presupuesto modificado = ConstruirPresupuesto(solicitud, existente.Id, existente.FechaCreacion, existente);

            if (!_presupuestoRepository.Actualizar(modificado))
            {
                throw new InvalidOperationException("No se pudo actualizar el presupuesto.");
            }

            return ConvertirADto(modificado);
        }

        public string ExportarPdf(int presupuestoId, string ruta)
        {
            Presupuesto presupuesto = _presupuestoRepository.ObtenerPorId(presupuestoId);
            if (presupuesto == null)
            {
                throw new ArgumentException("No se encontró el presupuesto seleccionado.");
            }

            presupuesto.RutaPdf = _pdfService.Guardar(presupuesto, ruta);
            return presupuesto.RutaPdf;
        }

        public void ValidarServicioPersonalizado(string descripcion, decimal monto)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("Cada servicio personalizado debe tener una descripción.");
            }

            if (monto < 0)
            {
                throw new ArgumentException("El costo de un servicio personalizado no puede ser negativo.");
            }
        }

        // ---------- Construcción del modelo ----------

        private Presupuesto ConstruirPresupuesto(
            PresupuestoRequestDto solicitud,
            int id,
            DateTime fechaCreacion,
            Presupuesto existente)
        {
            Salon salonSeleccionado = BuscarSalon(solicitud.SalonId, existente);
            Evento evento = new Evento(
                0,
                solicitud.DuracionHoras,
                solicitud.CantidadAdultos,
                solicitud.CantidadNinos);

            List<DetalleDeMenus> detallesDeMenus = new List<DetalleDeMenus>();
            foreach (DetalleMenuDto detalleDto in solicitud.Menues)
            {
                MenuDeComida menu = BuscarMenu(detalleDto.MenuId, existente);
                detallesDeMenus.Add(new DetalleDeMenus(
                    detalleDto.CantidadAdultos,
                    detalleDto.CantidadNinos,
                    detalleDto.Particularidad,
                    CopiarMenu(menu)));
            }

            List<ServiciosAdicionales> serviciosSeleccionados = ConstruirServicios(solicitud, existente);

            return new Presupuesto(
                id,
                fechaCreacion,
                solicitud.FechaEvento,
                solicitud.HoraInicio,
                solicitud.NombreContacto.Trim(),
                solicitud.EmailContacto.Trim(),
                solicitud.TelefonoContacto.Trim(),
                solicitud.UbicacionEvento.Trim(),
                solicitud.Observaciones.Trim(),
                evento,
                CopiarSalon(salonSeleccionado),
                detallesDeMenus,
                serviciosSeleccionados,
                solicitud.PorcentajeAdicionalMenuPrincipal,
                solicitud.PorcentajeAdicionalMenuPersonalizado,
                string.Empty);
        }

        private List<ServiciosAdicionales> ConstruirServicios(PresupuestoRequestDto solicitud, Presupuesto existente)
        {
            List<ServiciosAdicionales> servicios = new List<ServiciosAdicionales>();

            foreach (int servicioId in QuitarRepetidos(solicitud.ServiciosAdicionalesIds))
            {
                ServiciosAdicionales servicio = BuscarServicio(servicioId, existente);
                if (servicio != null)
                {
                    servicios.Add(new ServiciosAdicionales(servicio.Id, servicio.Descripcion, servicio.CostoDelServicio));
                }
            }

            foreach (ServicioPersonalizadoDto servicio in solicitud.ServiciosPersonalizados)
            {
                servicios.Add(new ServiciosAdicionales(0, servicio.Descripcion.Trim(), servicio.MontoFijo));
            }

            return servicios;
        }

        // ---------- Validaciones (reglas de negocio) ----------

        private void Validar(PresupuestoRequestDto solicitud, Presupuesto existente)
        {
            if (solicitud == null)
            {
                throw new ArgumentNullException("solicitud");
            }

            if (solicitud.Menues == null || solicitud.ServiciosAdicionalesIds == null || solicitud.ServiciosPersonalizados == null)
            {
                throw new ArgumentException("Las listas de menús o servicios del presupuesto no son válidas.");
            }

            if (string.IsNullOrWhiteSpace(solicitud.EmailContacto))
            {
                throw new ArgumentException("Ingresá el correo electrónico de contacto.");
            }

            if (string.IsNullOrWhiteSpace(solicitud.NombreContacto))
            {
                throw new ArgumentException("Ingresá el nombre de contacto.");
            }

            try
            {
                MailAddress correo = new MailAddress(solicitud.EmailContacto.Trim());
                if (!string.Equals(correo.Address, solicitud.EmailContacto.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("Ingresá un correo electrónico válido.");
                }
            }
            catch (FormatException)
            {
                throw new ArgumentException("Ingresá un correo electrónico válido.");
            }

            if (string.IsNullOrWhiteSpace(solicitud.TelefonoContacto))
            {
                throw new ArgumentException("Ingresá un teléfono de contacto.");
            }

            if (string.IsNullOrWhiteSpace(solicitud.UbicacionEvento)
                || string.Equals(solicitud.UbicacionEvento.Trim(), "Sin definir", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Ingresá la ubicación concreta donde se realizará el evento.");
            }

            int cantidadDigitosTelefono = 0;
            foreach (char caracter in solicitud.TelefonoContacto)
            {
                if (char.IsDigit(caracter))
                {
                    cantidadDigitosTelefono++;
                }
            }

            if (cantidadDigitosTelefono < 7 || cantidadDigitosTelefono > 15)
            {
                throw new ArgumentException("Ingresá un teléfono válido, con entre 7 y 15 dígitos.");
            }

            if (solicitud.DuracionHoras <= 0)
            {
                throw new ArgumentException("La duración tiene que ser de al menos una hora.");
            }

            if (solicitud.FechaEvento.Date < DateTime.Today)
            {
                throw new ArgumentException("Estás seleccionando una fecha que no tiene validez en torno al contexto actual. Solo se pueden elegir fechas actuales o a futuro.");
            }

            if (solicitud.HoraInicio < TimeSpan.Zero || solicitud.HoraInicio >= TimeSpan.FromDays(1))
            {
                throw new ArgumentException("Elegí una hora de inicio válida para el evento.");
            }

            if (solicitud.CantidadAdultos < 0 || solicitud.CantidadNinos < 0 || solicitud.CantidadAdultos + solicitud.CantidadNinos <= 0)
            {
                throw new ArgumentException("Ingresá al menos un invitado y revisá las cantidades.");
            }

            if (solicitud.PorcentajeAdicionalMenuPrincipal < 0 || solicitud.PorcentajeAdicionalMenuPrincipal > 500
                || solicitud.PorcentajeAdicionalMenuPersonalizado < 0 || solicitud.PorcentajeAdicionalMenuPersonalizado > 500)
            {
                throw new ArgumentException("Los porcentajes adicionales deben estar entre 0 y 500.");
            }

            if (BuscarSalon(solicitud.SalonId, existente) == null)
            {
                throw new ArgumentException("Seleccioná un salón disponible.");
            }

            int adultosAsignados = 0;
            int ninosAsignados = 0;
            foreach (DetalleMenuDto detalle in solicitud.Menues)
            {
                if (detalle == null || detalle.CantidadAdultos < 0 || detalle.CantidadNinos < 0
                    || detalle.CantidadAdultos + detalle.CantidadNinos <= 0)
                {
                    throw new ArgumentException("Cada fila de menú debe tener cantidades válidas de adultos o niños.");
                }

                if (!Enum.IsDefined(typeof(TipoDeExclusionDeMenu), detalle.Particularidad))
                {
                    throw new ArgumentException("Elegí una particularidad de menú válida.");
                }

                if (BuscarMenu(detalle.MenuId, existente) == null)
                {
                    if (detalle.Particularidad == TipoDeExclusionDeMenu.Ninguna)
                    {
                        throw new ArgumentException("Seleccioná un menú principal para cada fila.");
                    }

                    throw new ArgumentException("Seleccioná un menú para cada particularidad.");
                }

                adultosAsignados += detalle.CantidadAdultos;
                ninosAsignados += detalle.CantidadNinos;
            }

            if (adultosAsignados != solicitud.CantidadAdultos || ninosAsignados != solicitud.CantidadNinos)
            {
                throw new ArgumentException("La suma de adultos y niños asignados a los menús debe coincidir exactamente con la cantidad total de invitados.");
            }

            foreach (int servicioId in QuitarRepetidos(solicitud.ServiciosAdicionalesIds))
            {
                if (BuscarServicio(servicioId, existente) == null)
                {
                    throw new ArgumentException("Uno de los servicios seleccionados ya no está disponible.");
                }
            }

            foreach (ServicioPersonalizadoDto servicio in solicitud.ServiciosPersonalizados)
            {
                if (servicio == null)
                {
                    throw new ArgumentException("Cada servicio personalizado debe tener una descripción.");
                }

                ValidarServicioPersonalizado(servicio.Descripcion, servicio.MontoFijo);
            }
        }

        // Regla de negocio: si hay un único menú principal y quedó sin invitados (porque todos
        // comen menús con particularidad), esa fila vacía se descarta antes de validar y calcular.
        private void DepurarMenus(PresupuestoRequestDto solicitud)
        {
            if (solicitud == null || solicitud.Menues == null)
            {
                return;
            }

            int cantidadPrincipales = 0;
            foreach (DetalleMenuDto detalle in solicitud.Menues)
            {
                if (detalle != null && detalle.Particularidad == TipoDeExclusionDeMenu.Ninguna)
                {
                    cantidadPrincipales++;
                }
            }

            List<DetalleMenuDto> depurados = new List<DetalleMenuDto>();
            foreach (DetalleMenuDto detalle in solicitud.Menues)
            {
                bool esPrincipalUnicoVacio = detalle != null
                    && detalle.Particularidad == TipoDeExclusionDeMenu.Ninguna
                    && cantidadPrincipales == 1
                    && detalle.CantidadAdultos + detalle.CantidadNinos <= 0;

                if (!esPrincipalUnicoVacio)
                {
                    depurados.Add(detalle);
                }
            }

            solicitud.Menues = depurados;
        }

        // ---------- Búsquedas auxiliares ----------

        private Salon BuscarSalon(int salonId, Presupuesto existente)
        {
            Salon salon = _salonRepository.ObtenerPorId(salonId);
            if (salon == null && existente != null && existente.SalonDelEvento.Id == salonId)
            {
                salon = existente.SalonDelEvento;
            }

            return salon;
        }

        private MenuDeComida BuscarMenu(int menuId, Presupuesto existente)
        {
            MenuDeComida menu = _menuRepository.ObtenerPorId(menuId);
            if (menu == null && existente != null)
            {
                foreach (DetalleDeMenus detalle in existente.Menues)
                {
                    if (detalle.MenuElegido.Id == menuId)
                    {
                        menu = detalle.MenuElegido;
                        break;
                    }
                }
            }

            return menu;
        }

        private ServiciosAdicionales BuscarServicio(int servicioId, Presupuesto existente)
        {
            ServiciosAdicionales servicio = _serviciosRepository.ObtenerPorId(servicioId);
            if (servicio == null && existente != null)
            {
                foreach (ServiciosAdicionales item in existente.ServiciosAdicionales)
                {
                    if (item.Id == servicioId)
                    {
                        servicio = item;
                        break;
                    }
                }
            }

            return servicio;
        }

        private List<int> QuitarRepetidos(List<int> ids)
        {
            List<int> unicos = new List<int>();
            foreach (int id in ids)
            {
                if (!unicos.Contains(id))
                {
                    unicos.Add(id);
                }
            }

            return unicos;
        }

        // ---------- Copias y conversiones a DTO ----------

        private Salon CopiarSalon(Salon salon)
        {
            return new Salon(salon.Id, salon.Nombre, salon.Ubicacion, salon.CostoBase);
        }

        private MenuDeComida CopiarMenu(MenuDeComida menu)
        {
            return new MenuDeComida(menu.Id, menu.Nombre, menu.CostoPorAdulto, menu.CostoPorNiño);
        }

        private SalonDto ConvertirSalonADto(Salon salon)
        {
            SalonDto dto = new SalonDto();
            dto.Id = salon.Id;
            dto.Nombre = salon.Nombre;
            dto.Ubicacion = salon.Ubicacion;
            dto.CostoBase = salon.CostoBase;
            return dto;
        }

        private MenuDto ConvertirMenuADto(MenuDeComida menu)
        {
            MenuDto dto = new MenuDto();
            dto.Id = menu.Id;
            dto.Nombre = menu.Nombre;
            dto.CostoPorAdulto = menu.CostoPorAdulto;
            dto.CostoPorNiño = menu.CostoPorNiño;
            return dto;
        }

        private ServicioAdicionalDto ConvertirServicioADto(ServiciosAdicionales servicio)
        {
            ServicioAdicionalDto dto = new ServicioAdicionalDto();
            dto.Id = servicio.Id;
            dto.Descripcion = servicio.Descripcion;
            dto.CostoDelServicio = servicio.CostoDelServicio;
            return dto;
        }

        private PresupuestoDto ConvertirADto(Presupuesto presupuesto)
        {
            PresupuestoDto dto = new PresupuestoDto();
            dto.Id = presupuesto.Id;
            dto.FechaCreacion = presupuesto.FechaCreacion;
            dto.FechaEvento = presupuesto.FechaEvento;
            dto.HoraInicio = presupuesto.HoraInicio;
            dto.FechaHoraEvento = presupuesto.FechaHoraEvento;
            dto.NombreContacto = presupuesto.NombreContacto;
            dto.EmailContacto = presupuesto.EmailContacto;
            dto.TelefonoContacto = presupuesto.TelefonoContacto;
            dto.UbicacionEvento = presupuesto.UbicacionEvento;
            dto.Observaciones = presupuesto.Observaciones;
            dto.SalonId = presupuesto.SalonDelEvento.Id;
            dto.NombreSalon = presupuesto.NombreSalon;
            dto.DuracionHoras = presupuesto.Evento.Duracion;
            dto.CantidadAdultos = presupuesto.Evento.CantidadAdultos;
            dto.CantidadNinos = presupuesto.Evento.CantidadNinos;
            dto.CantidadTotalInvitados = presupuesto.CantidadTotalInvitados;
            dto.PorcentajeAdicionalMenuPrincipal = presupuesto.PorcentajeAdicionalMenuPrincipal;
            dto.PorcentajeAdicionalMenuPersonalizado = presupuesto.PorcentajeAdicionalMenuPersonalizado;
            dto.CostoSalon = presupuesto.CostoSalon;
            dto.SubtotalMenusPrincipales = presupuesto.SubtotalMenusPrincipales;
            dto.RecargoMenusPrincipales = presupuesto.RecargoMenusPrincipales;
            dto.SubtotalMenusPersonalizados = presupuesto.SubtotalMenusPersonalizados;
            dto.RecargoMenusPersonalizados = presupuesto.RecargoMenusPersonalizados;
            dto.CostoMenus = presupuesto.CostoMenus;
            dto.CostoServiciosAdicionales = presupuesto.CostoServiciosAdicionales;
            dto.TotalFinal = presupuesto.TotalFinal;
            dto.RutaPdf = presupuesto.RutaPdf;

            foreach (DetalleDeMenus detalle in presupuesto.Menues)
            {
                DetalleMenuPresupuestoDto detalleDto = new DetalleMenuPresupuestoDto();
                detalleDto.MenuId = detalle.MenuElegido.Id;
                detalleDto.NombreMenu = detalle.MenuElegido.Nombre;
                detalleDto.Particularidad = detalle.ExclusionDelMenu;
                detalleDto.CantidadAdultos = detalle.CantidadAdultos;
                detalleDto.CantidadNinos = detalle.CantidadNinos;
                detalleDto.Subtotal = detalle.CalcularSubtotal();
                dto.Menues.Add(detalleDto);
            }

            foreach (ServiciosAdicionales servicio in presupuesto.ServiciosAdicionales)
            {
                dto.ServiciosAdicionales.Add(ConvertirServicioADto(servicio));
            }

            return dto;
        }
    }
}
