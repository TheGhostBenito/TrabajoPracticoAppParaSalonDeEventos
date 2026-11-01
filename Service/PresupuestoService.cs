using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;
using proyecto_integrador.service;

namespace proyecto_integrador.Service
{
    public class PresupuestoService
    {
        private readonly PresupuestoRepository _presupuestoRepository;
        private readonly MenuRepository _menuRepository;
        private readonly ServiciosAdicionalesRepository _serviciosRepository;

        public PresupuestoService(
            PresupuestoRepository presupuestoRepository,
            MenuRepository menuRepository,
            ServiciosAdicionalesRepository serviciosRepository)
        {
            if (presupuestoRepository == null)
            {
                throw new ArgumentNullException("presupuestoRepository");
            }

            if (menuRepository == null)
            {
                throw new ArgumentNullException("menuRepository");
            }

            if (serviciosRepository == null)
            {
                throw new ArgumentNullException("serviciosRepository");
            }

            _presupuestoRepository = presupuestoRepository;
            _menuRepository = menuRepository;
            _serviciosRepository = serviciosRepository;
        }

        public List<Presupuesto> ObtenerTodos()
        {
            return _presupuestoRepository.ObtenerTodos();
        }

        public Presupuesto ObtenerPorId(int id)
        {
            return _presupuestoRepository.ObtenerPorId(id);
        }

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

            Presupuesto existente = presupuestoExistenteId > 0
                ? _presupuestoRepository.ObtenerPorId(presupuestoExistenteId)
                : null;
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
            List<ServiciosAdicionales> catalogo = _serviciosRepository.ObtenerTodos();

            if (solicitud.ServiciosAdicionalesIds != null)
            {
                foreach (int servicioId in solicitud.ServiciosAdicionalesIds.Distinct())
                {
                    ServiciosAdicionales servicio = catalogo.FirstOrDefault(item => item.Id == servicioId);
                    if (servicio == null && existente != null)
                    {
                        servicio = existente.ServiciosAdicionales.FirstOrDefault(item => item.Id == servicioId);
                    }

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

        public Presupuesto Crear(PresupuestoRequestDto solicitud)
        {
            Validar(solicitud, null);
            Presupuesto presupuesto = ConstruirPresupuesto(solicitud, 0, DateTime.Now, null, string.Empty);
            _presupuestoRepository.Agregar(presupuesto);
            return presupuesto;
        }

        public Presupuesto Modificar(int id, PresupuestoRequestDto solicitud)
        {
            Presupuesto existente = _presupuestoRepository.ObtenerPorId(id);
            if (existente == null)
            {
                throw new ArgumentException("No se encontró el presupuesto seleccionado.");
            }

            Validar(solicitud, existente);
            Presupuesto modificado = ConstruirPresupuesto(
                solicitud,
                existente.Id,
                existente.FechaCreacion,
                existente,
                string.Empty);

            if (!_presupuestoRepository.Actualizar(modificado))
            {
                throw new InvalidOperationException("No se pudo actualizar el presupuesto.");
            }

            return modificado;
        }

        public string ExportarPdf(int presupuestoId, string ruta)
        {
            Presupuesto presupuesto = _presupuestoRepository.ObtenerPorId(presupuestoId);
            if (presupuesto == null)
            {
                throw new ArgumentException("No se encontró el presupuesto seleccionado.");
            }

            presupuesto.RutaPdf = PresupuestoPdfService.Guardar(presupuesto, ruta);
            return presupuesto.RutaPdf;
        }

        private Presupuesto ConstruirPresupuesto(
            PresupuestoRequestDto solicitud,
            int id,
            DateTime fechaCreacion,
            Presupuesto existente,
            string rutaPdf)
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
                rutaPdf);
        }

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

            int cantidadDigitosTelefono = solicitud.TelefonoContacto.Count(caracter => char.IsDigit(caracter));
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

                if (detalle.Particularidad != TipoDeExclusionDeMenu.Ninguna
                    && BuscarMenu(detalle.MenuId, existente) == null)
                {
                    throw new ArgumentException("Seleccioná un menú para cada particularidad.");
                }

                if (detalle.Particularidad == TipoDeExclusionDeMenu.Ninguna
                    && BuscarMenu(detalle.MenuId, existente) == null)
                {
                    throw new ArgumentException("Seleccioná un menú principal para cada fila.");
                }

                adultosAsignados += detalle.CantidadAdultos;
                ninosAsignados += detalle.CantidadNinos;
            }

            if (adultosAsignados != solicitud.CantidadAdultos || ninosAsignados != solicitud.CantidadNinos)
            {
                throw new ArgumentException("La suma de adultos y niños asignados a los menús debe coincidir exactamente con la cantidad total de invitados.");
            }

            List<ServiciosAdicionales> catalogoServicios = _serviciosRepository.ObtenerTodos();
            foreach (int servicioId in solicitud.ServiciosAdicionalesIds.Distinct())
            {
                bool existeEnCatalogo = catalogoServicios.Any(servicio => servicio.Id == servicioId);
                bool existeEnPresupuesto = existente != null
                    && existente.ServiciosAdicionales.Any(servicio => servicio.Id == servicioId);

                if (!existeEnCatalogo && !existeEnPresupuesto)
                {
                    throw new ArgumentException("Uno de los servicios seleccionados ya no está disponible.");
                }
            }

            foreach (ServicioPersonalizadoDto servicio in solicitud.ServiciosPersonalizados)
            {
                if (servicio == null || string.IsNullOrWhiteSpace(servicio.Descripcion))
                {
                    throw new ArgumentException("Cada servicio personalizado debe tener una descripción.");
                }

                if (servicio.MontoFijo < 0)
                {
                    throw new ArgumentException("El costo de un servicio personalizado no puede ser negativo.");
                }
            }
        }

        private Salon BuscarSalon(int salonId, Presupuesto existente)
        {
            Salon salon = SalonService.ObtenerSalones().FirstOrDefault(item => item.Id == salonId);
            if (salon == null && existente != null && existente.SalonDelEvento.Id == salonId)
            {
                salon = existente.SalonDelEvento;
            }

            return salon;
        }

        private MenuDeComida BuscarMenu(int menuId, Presupuesto existente)
        {
            MenuDeComida menu = _menuRepository.ObtenerTodos().FirstOrDefault(item => item.Id == menuId);
            if (menu == null && existente != null)
            {
                DetalleDeMenus detalle = existente.Menues.FirstOrDefault(item => item.MenuElegido.Id == menuId);
                if (detalle != null)
                {
                    menu = detalle.MenuElegido;
                }
            }

            return menu;
        }

        private List<ServiciosAdicionales> ConstruirServicios(PresupuestoRequestDto solicitud, Presupuesto existente)
        {
            List<ServiciosAdicionales> servicios = new List<ServiciosAdicionales>();
            List<ServiciosAdicionales> catalogo = _serviciosRepository.ObtenerTodos();

            foreach (int servicioId in solicitud.ServiciosAdicionalesIds.Distinct())
            {
                ServiciosAdicionales servicio = catalogo.FirstOrDefault(item => item.Id == servicioId);
                if (servicio == null && existente != null)
                {
                    servicio = existente.ServiciosAdicionales.FirstOrDefault(item => item.Id == servicioId);
                }

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

        private static Salon CopiarSalon(Salon salon)
        {
            return new Salon(salon.Id, salon.Nombre, salon.Ubicacion, salon.CostoBase);
        }

        private static MenuDeComida CopiarMenu(MenuDeComida menu)
        {
            return new MenuDeComida(menu.Id, menu.Nombre, menu.CostoPorAdulto, menu.CostoPorNiño);
        }

    }
}
