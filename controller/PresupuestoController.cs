using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;
using proyecto_integrador.Service;
using proyecto_integrador.service;

namespace proyecto_integrador.controller
{
    public sealed class PresupuestoController
    {
        private readonly PresupuestoService _service;

        public PresupuestoController()
        {
            _service = new PresupuestoService(
                PresupuestoRepository.Instance,
                MenuRepository.Instance,
                ServiciosAdicionalesRepository.Instance);
        }

        public List<Salon> ObtenerSalones()
        {
            return SalonService.ObtenerSalones();
        }

        public List<MenuDeComida> ObtenerMenus()
        {
            return MenuRepository.Instance.ObtenerTodos();
        }

        public List<ServiciosAdicionales> ObtenerServiciosAdicionales()
        {
            return ServiciosAdicionalesRepository.Instance.ObtenerTodos();
        }

        public List<Presupuesto> ObtenerPresupuestos()
        {
            return _service.ObtenerTodos();
        }

        public AsignacionMenuDto CalcularInvitadosRestantes(
            int cantidadAdultos,
            int cantidadNinos,
            List<DetalleMenuDto> menusPersonalizados)
        {
            return _service.CalcularInvitadosRestantes(cantidadAdultos, cantidadNinos, menusPersonalizados);
        }

        public decimal CalcularEstimacion(PresupuestoRequestDto solicitud, int presupuestoExistenteId)
        {
            return _service.CalcularEstimacion(solicitud, presupuestoExistenteId);
        }

        public Presupuesto Crear(PresupuestoRequestDto solicitud)
        {
            return _service.Crear(solicitud);
        }

        public Presupuesto Modificar(int id, PresupuestoRequestDto solicitud)
        {
            return _service.Modificar(id, solicitud);
        }

        public string ExportarPdf(int presupuestoId, string ruta)
        {
            return _service.ExportarPdf(presupuestoId, ruta);
        }
    }
}
