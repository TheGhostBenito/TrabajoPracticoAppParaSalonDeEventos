using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.Service;

namespace proyecto_integrador.controller
{
    public sealed class SalonController
    {
        private readonly IService<SalonDto, SalonDto> _service;

        public SalonController(IService<SalonDto, SalonDto> service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            _service = service;
        }

        public List<SalonDto> ObtenerTodos()
        {                                       //pide la lista de salones para mostrar en la grilla
            return _service.ObtenerTodos();
        }

        public void Crear(SalonDto salon)
        {                                   //da del alta un salon
            _service.Crear(salon);
        }

        public void Modificar(SalonDto salon)
        {
            _service.Modificar(salon.Id, salon);   // modifica el salon; si no lo encuentra, el service lanza un error
        }

        public bool Eliminar(int id)
        {
            return _service.Eliminar(id);  //borra el salon con su id
        }
    }
}