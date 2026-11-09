using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.Service;

namespace proyecto_integrador.controller
{
    // service utilizado para gestionar los servicios adicionales
    // referencia al Service Se asigna una sola vez en el constructor
    private readonly IService<ServicioAdicionalDto, ServicioAdicionalDto> _service;

    // Recibe el Service mediante inyección de dependencias
    public ServiciosAdicionalesController(IService<ServicioAdicionalDto, ServicioAdicionalDto> service)
    {
        private readonly IServiciosAdicionalesService _service;

        public ServiciosAdicionalesController(IServiciosAdicionalesService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            _service = service;
        }

        public List<ServicioAdicionalDto> ObtenerTodos()
        {
            return _service.ObtenerTodos();
        }

        public void Crear(ServicioAdicionalDto servicio)
        {
            _service.Crear(servicio);
        }

    // Solicita al Service modificar un servicio existente
    public void Modificar(ServicioAdicionalDto servicio)
    {
        _service.Modificar(servicio.Id, servicio);
    }

        public bool Eliminar(int id)
        {
            return _service.Eliminar(id);
        }
    }
}