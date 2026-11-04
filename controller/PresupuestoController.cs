using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.Service;

namespace proyecto_integrador.controller
{
    // El Controller solo recibe lo que manda la vista y se lo pasa al Service. No calcula ni valida nada.
    public sealed class PresupuestoController
    {
        private readonly IPresupuestoService _service;

        public PresupuestoController(IPresupuestoService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            _service = service;
        }

        public List<SalonDto> ObtenerSalones(int presupuestoId)
        {
            return _service.ObtenerSalonesDisponibles(presupuestoId);
        }

        public List<MenuDto> ObtenerMenus(int presupuestoId)
        {
            return _service.ObtenerMenusDisponibles(presupuestoId);
        }

        public List<ServicioAdicionalDto> ObtenerServiciosAdicionales(int presupuestoId)
        {
            return _service.ObtenerServiciosDisponibles(presupuestoId);
        }

        public List<PresupuestoDto> ObtenerPresupuestos()
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

        public void ValidarServicioPersonalizado(string descripcion, decimal monto)
        {
            _service.ValidarServicioPersonalizado(descripcion, monto);
        }

        public PresupuestoDto Crear(PresupuestoRequestDto solicitud)
        {
            return _service.Crear(solicitud);
        }

        public PresupuestoDto Modificar(int id, PresupuestoRequestDto solicitud)
        {
            return _service.Modificar(id, solicitud);
        }

        public string ExportarPdf(int presupuestoId, string ruta)
        {
            return _service.ExportarPdf(presupuestoId, ruta);
        }
    }
}
