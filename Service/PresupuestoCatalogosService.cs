using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    // Arma los listados que se muestran en el formulario. Antes estas tres
    // consultas estaban dentro del PresupuestoService.
    public sealed class PresupuestoCatalogosService
    {
        private readonly IRepository<Presupuesto> _presupuestoRepository;
        private readonly IRepository<Salon> _salonRepository;
        private readonly IRepository<MenuDeComida> _menuRepository;
        private readonly IRepository<ServiciosAdicionales> _servicioRepository;

        public PresupuestoCatalogosService(
            IRepository<Presupuesto> presupuestoRepository,
            IRepository<Salon> salonRepository,
            IRepository<MenuDeComida> menuRepository,
            IRepository<ServiciosAdicionales> servicioRepository)
        {
            if (presupuestoRepository == null) throw new ArgumentNullException("presupuestoRepository");
            if (salonRepository == null) throw new ArgumentNullException("salonRepository");
            if (menuRepository == null) throw new ArgumentNullException("menuRepository");
            if (servicioRepository == null) throw new ArgumentNullException("servicioRepository");

            _presupuestoRepository = presupuestoRepository;
            _salonRepository = salonRepository;
            _menuRepository = menuRepository;
            _servicioRepository = servicioRepository;
        }

        public List<SalonDto> ObtenerSalonesDisponibles(int presupuestoId)
        {
            ContextoPresupuesto contexto = CrearContexto(presupuestoId);
            List<SalonDto> resultado = new List<SalonDto>();
            foreach (Salon salon in contexto.ObtenerSalones())
            {
                resultado.Add(CatalogoDtoConverter.SalonADto(salon));
            }

            return resultado;
        }

        public List<MenuDto> ObtenerMenusDisponibles(int presupuestoId)
        {
            ContextoPresupuesto contexto = CrearContexto(presupuestoId);
            List<MenuDto> resultado = new List<MenuDto>();
            foreach (MenuDeComida menu in contexto.ObtenerMenus())
            {
                resultado.Add(CatalogoDtoConverter.MenuADto(menu));
            }

            return resultado;
        }

        public List<ServicioAdicionalDto> ObtenerServiciosDisponibles(int presupuestoId)
        {
            ContextoPresupuesto contexto = CrearContexto(presupuestoId);
            List<ServicioAdicionalDto> resultado = new List<ServicioAdicionalDto>();
            foreach (ServiciosAdicionales servicio in contexto.ObtenerServicios())
            {
                resultado.Add(CatalogoDtoConverter.ServicioADto(servicio));
            }

            return resultado;
        }

        private ContextoPresupuesto CrearContexto(int presupuestoId)
        {
            Presupuesto existente = null;
            if (presupuestoId > 0)
            {
                existente = _presupuestoRepository.ObtenerPorId(presupuestoId);
            }

            return new ContextoPresupuesto(_salonRepository, _menuRepository, _servicioRepository, existente);
        }
    }
}
