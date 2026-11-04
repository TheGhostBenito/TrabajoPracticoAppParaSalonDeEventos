using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.Service;

namespace proyecto_integrador.controller
{
    public sealed class ServiciosAdicionalesController
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

        public bool Modificar(ServicioAdicionalDto servicio)
        {
            return _service.Modificar(servicio);
        }

        public bool Eliminar(int id)
        {
            return _service.Eliminar(id);
        }
    }
}
