using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.Service;

namespace proyecto_integrador.controller
{
    // El Controller solo recibe lo que manda la vista y se lo pasa al Service
    public sealed class MenuController
    {
        // Service utilizado para gestionar los menús
        private readonly IService<MenuDto, MenuDto> _service;

        // Recibe el Service mediante inyeccion de dependencias
        public MenuController(IService<MenuDto, MenuDto> service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            _service = service;
        }

        public List<MenuDto> ObtenerTodos()
        {
            return _service.ObtenerTodos();
        }

        public void Crear(MenuDto menu)
        {
            _service.Crear(menu);
        }

        // Solicita al Service modificar un menú existente
        public void Modificar(MenuDto menu)
        {
            _service.Modificar(menu.Id, menu);
        }

        public bool Eliminar(int id)
        {
            return _service.Eliminar(id);
        }
    }
}
