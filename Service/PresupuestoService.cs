using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    // Orquestador: delega la preparación/validación de cada ítem, las consultas de
    // catálogos y el mapeo a DTO. Toda la lógica por concepto vive en su propia clase.
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IRepository<Presupuesto> _presupuestoRepository;
        private readonly IRepository<Salon> _salonRepository;
        private readonly IRepository<MenuDeComida> _menuRepository;
        private readonly IRepository<ServiciosAdicionales> _servicioRepository;
        private readonly IPresupuestoPdfService _pdfService;
        private readonly PresupuestoCatalogosService _catalogos;
        private readonly PresupuestoRequestValidator _validator;
        private readonly List<IConstructorItemPresupuesto> _constructores;
        private readonly PresupuestoDtoMapper _dtoMapper;

        public PresupuestoService(
            IRepository<Presupuesto> presupuestoRepository,
            IRepository<Salon> salonRepository,
            IRepository<MenuDeComida> menuRepository,
            IRepository<ServiciosAdicionales> servicioRepository,
            IPresupuestoPdfService pdfService,
            PresupuestoCatalogosService catalogos,
            PresupuestoRequestValidator validator,
            List<IConstructorItemPresupuesto> constructores,
            PresupuestoDtoMapper dtoMapper)
        {
            if (presupuestoRepository == null) throw new ArgumentNullException("presupuestoRepository");
            if (salonRepository == null) throw new ArgumentNullException("salonRepository");
            if (menuRepository == null) throw new ArgumentNullException("menuRepository");
            if (servicioRepository == null) throw new ArgumentNullException("servicioRepository");
            if (pdfService == null) throw new ArgumentNullException("pdfService");
            if (catalogos == null) throw new ArgumentNullException("catalogos");
            if (validator == null) throw new ArgumentNullException("validator");
            if (constructores == null) throw new ArgumentNullException("constructores");
            if (dtoMapper == null) throw new ArgumentNullException("dtoMapper");

            _presupuestoRepository = presupuestoRepository;
            _salonRepository = salonRepository;
            _menuRepository = menuRepository;
            _servicioRepository = servicioRepository;
            _pdfService = pdfService;
            _catalogos = catalogos;
            _validator = validator;
            _constructores = constructores;
            _dtoMapper = dtoMapper;
        }

        // ---------- Consultas ----------

        public List<PresupuestoDto> ObtenerTodos()
        {
            List<PresupuestoDto> resultado = new List<PresupuestoDto>();
            foreach (Presupuesto presupuesto in _presupuestoRepository.ObtenerTodos())
            {
                resultado.Add(_dtoMapper.Mapear(presupuesto));
            }

            return resultado;
        }

        public List<SalonDto> ObtenerSalonesDisponibles(int presupuestoId)
        {
            return _catalogos.ObtenerSalonesDisponibles(presupuestoId);
        }

        public List<MenuDto> ObtenerMenusDisponibles(int presupuestoId)
        {
            return _catalogos.ObtenerMenusDisponibles(presupuestoId);
        }

        public List<ServicioAdicionalDto> ObtenerServiciosDisponibles(int presupuestoId)
        {
            return _catalogos.ObtenerServiciosDisponibles(presupuestoId);
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

            return new AsignacionMenuDto(
                Math.Max(0, cantidadAdultos - adultosPersonalizados),
                Math.Max(0, cantidadNinos - ninosPersonalizados));
        }

        // ---------- Cálculo, alta, modificación y PDF ----------

        public decimal CalcularEstimacion(PresupuestoRequestDto solicitud, int presupuestoExistenteId)
        {
            if (solicitud == null)
            {
                return 0m;
            }

            // El mismo modelo que se guarda calcula la estimación: ya no hay dos
            // fórmulas distintas que puedan quedar desincronizadas.
            Presupuesto borrador = Construir(
                solicitud,
                0,
                DateTime.Today,
                ObtenerExistente(presupuestoExistenteId),
                false);

            return borrador.TotalFinal;
        }

        public PresupuestoDto Crear(PresupuestoRequestDto solicitud)
        {
            Presupuesto presupuesto = Construir(solicitud, 0, DateTime.Now, null, true);
            _presupuestoRepository.Crear(presupuesto);
            return _dtoMapper.Mapear(presupuesto);
        }

        public PresupuestoDto Modificar(int id, PresupuestoRequestDto solicitud)
        {
            Presupuesto existente = ObtenerExistente(id);
            if (existente == null)
            {
                throw new ArgumentException("No se encontró el presupuesto seleccionado.");
            }

            Presupuesto modificado = Construir(solicitud, existente.Id, existente.FechaCreacion, existente, true);
            if (!_presupuestoRepository.Modificar(modificado))
            {
                throw new InvalidOperationException("No se pudo actualizar el presupuesto.");
            }

            return _dtoMapper.Mapear(modificado);
        }

        public bool Eliminar(int id)
        {
            return _presupuestoRepository.Eliminar(id);
        }

        public string ExportarPdf(int presupuestoId, string ruta)
        {
            Presupuesto presupuesto = ObtenerExistente(presupuestoId);
            if (presupuesto == null)
            {
                throw new ArgumentException("No se encontró el presupuesto seleccionado.");
            }

            presupuesto.RutaPdf = _pdfService.Guardar(presupuesto, ruta);
            return presupuesto.RutaPdf;
        }

        public void ValidarServicioPersonalizado(string descripcion, decimal monto)
        {
            ConstructorItemServicio.ValidarServicioPersonalizado(descripcion, monto);
        }

        // ---------- Construcción del modelo ----------

        private Presupuesto Construir(
            PresupuestoRequestDto solicitud,
            int id,
            DateTime fechaCreacion,
            Presupuesto existente,
            bool validar)
        {
            if (solicitud == null)
            {
                throw new ArgumentNullException("solicitud");
            }

            ContextoPresupuesto contexto = new ContextoPresupuesto(
                _salonRepository,
                _menuRepository,
                _servicioRepository,
                existente);

            foreach (IConstructorItemPresupuesto constructor in _constructores)
            {
                constructor.Preparar(solicitud);
            }

            if (validar)
            {
                _validator.Validar(solicitud);
                foreach (IConstructorItemPresupuesto constructor in _constructores)
                {
                    constructor.Validar(solicitud, contexto);
                }
            }

            List<ItemPresupuesto> items = new List<ItemPresupuesto>();
            foreach (IConstructorItemPresupuesto constructor in _constructores)
            {
                items.AddRange(constructor.Construir(solicitud, contexto));
            }

            // En la estimación el formulario todavía puede estar incompleto:
            // se evitan valores inválidos del Evento sin alterar los importes.
            int duracion = solicitud.DuracionHoras > 0 ? solicitud.DuracionHoras : 1;
            Evento evento = new Evento(
                0,
                duracion,
                Math.Max(0, solicitud.CantidadAdultos),
                Math.Max(0, solicitud.CantidadNinos));

            return new Presupuesto(
                id,
                fechaCreacion,
                solicitud.FechaEvento,
                solicitud.HoraInicio,
                Recortar(solicitud.NombreContacto),
                Recortar(solicitud.EmailContacto),
                Recortar(solicitud.TelefonoContacto),
                Recortar(solicitud.UbicacionEvento),
                Recortar(solicitud.Observaciones),
                evento,
                items,
                solicitud.PorcentajeAdicionalMenuPrincipal,
                solicitud.PorcentajeAdicionalMenuPersonalizado,
                string.Empty);
        }

        private Presupuesto ObtenerExistente(int presupuestoId)
        {
            return presupuestoId > 0 ? _presupuestoRepository.ObtenerPorId(presupuestoId) : null;
        }

        private static string Recortar(string texto)
        {
            return texto == null ? string.Empty : texto.Trim();
        }
    }
}
