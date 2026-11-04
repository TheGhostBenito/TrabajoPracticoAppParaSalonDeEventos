using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repo;

        public MenuService(IMenuRepository repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("repo");
            }

            _repo = repo;
        }

        public List<MenuDto> ObtenerTodos()
        {
            List<MenuDto> resultado = new List<MenuDto>();
            foreach (MenuDeComida menu in _repo.ObtenerTodos())
            {
                resultado.Add(ConvertirADto(menu));
            }

            return resultado;
        }

        public void Crear(MenuDto menu)
        {
            Validar(menu);
            MenuDeComida nuevo = new MenuDeComida(0, menu.Nombre.Trim(), menu.CostoPorAdulto, menu.CostoPorNiño);
            _repo.Crear(nuevo);
        }

        public bool Modificar(MenuDto menu)
        {
            Validar(menu);
            MenuDeComida modificado = new MenuDeComida(menu.Id, menu.Nombre.Trim(), menu.CostoPorAdulto, menu.CostoPorNiño);
            return _repo.Modificar(modificado);
        }

        public bool Eliminar(int id)
        {
            return _repo.Eliminar(id);
        }

        private void Validar(MenuDto menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException("menu");
            }

            if (string.IsNullOrWhiteSpace(menu.Nombre))
            {
                throw new ArgumentException("Ingresá el nombre del menú.");
            }

            if (menu.CostoPorAdulto < 0)
            {
                throw new ArgumentException("El costo por adulto tiene que ser mayor o igual a 0.");
            }

            if (menu.CostoPorNiño < 0)
            {
                throw new ArgumentException("El costo por niño tiene que ser mayor o igual a 0.");
            }
        }

        private MenuDto ConvertirADto(MenuDeComida menu)
        {
            MenuDto dto = new MenuDto();
            dto.Id = menu.Id;
            dto.Nombre = menu.Nombre;
            dto.CostoPorAdulto = menu.CostoPorAdulto;
            dto.CostoPorNiño = menu.CostoPorNiño;
            return dto;
        }
    }
}