using proyecto_integrador.dto;
using proyecto_integrador.Service;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace proyecto_integrador.controller
{
    // El Controller solo recibe lo que manda la vista y se lo pasa al Service. No calcula ni valida nada.
    public sealed class PresupuestoController
    {
        private readonly PresupuestoService _service;

        public PresupuestoController(PresupuestoService service)
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

        public List<MenuDto> ObtenerMenus(int presupuestoId)                    //t<raen las opciones para los combos y
                                                                                //las listas del formulario.
        {
            return _service.ObtenerMenusDisponibles(presupuestoId);
        }

        public List<ServicioAdicionalDto> ObtenerServiciosAdicionales(int presupuestoId)
        {
            return _service.ObtenerServiciosDisponibles(presupuestoId);
        }

        public List<PresupuestoDto> ObtenerPresupuestos()  //trae el historial
        {
            return _service.ObtenerTodos();
        }

        public AsignacionMenuDto CalcularInvitadosRestantes(
            int cantidadAdultos,
            int cantidadNinos,
            List<DetalleMenuDto> menusPersonalizados)
        {
            return _service.CalcularInvitadosRestantes(cantidadAdultos, cantidadNinos, menusPersonalizados);  //alcula cuántos adultos y niños quedan para el menú principal.
        }

        public decimal CalcularEstimacion(PresupuestoRequestDto solicitud, int presupuestoExistenteId)
        {
            return _service.CalcularEstimacion(solicitud, presupuestoExistenteId);      //calcula el total mientras se completa el formulario.
        }

        public void ValidarServicioPersonalizado(string descripcion, decimal monto)
        {
            _service.ValidarServicioPersonalizado(descripcion, monto);//chequea que un servicio personalizado
                                                                      //tenga descripción y monto válidos.
        }

        public PresupuestoDto Crear(PresupuestoRequestDto solicitud)
        {
            return _service.Crear(solicitud);
        }

        //guardan un presupuesto nuevo o editado.
        public PresupuestoDto Modificar(int id, PresupuestoRequestDto solicitud)
        {
            return _service.Modificar(id, solicitud);
        }

        public string ExportarPdf(int presupuestoId, string ruta)
        {
            return _service.ExportarPdf(presupuestoId, ruta);  //genera el PDF.
        }
    }
}