using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.Service;

namespace proyecto_integrador.controller
{
    public sealed class MenuController
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
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

        public bool Modificar(MenuDto menu)
        {
            return _service.Modificar(menu);
        }

        public bool Eliminar(int id)
        {
            return _service.Eliminar(id);
        }
    }
}
