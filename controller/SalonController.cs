using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.Service;

namespace proyecto_integrador.controller
{
    public sealed class SalonController
    {
        private readonly ISalonService _service;

        public SalonController(ISalonService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            _service = service;
        }

        public List<SalonDto> ObtenerTodos()
        {
            return _service.ObtenerTodos();
        }

        public void Crear(SalonDto salon)
        {
            _service.Crear(salon);
        }

        public bool Modificar(SalonDto salon)
        {
            return _service.Modificar(salon);
        }

        public bool Eliminar(int id)
        {
            return _service.Eliminar(id);
        }
    }
}
