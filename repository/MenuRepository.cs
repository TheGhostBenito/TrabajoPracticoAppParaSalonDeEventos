using System;
using System.Collections.Generic;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Singleton: una única instancia que guarda la lista de menús mientras la app está abierta.
    public sealed class MenuRepository : IMenuRepository
    {
        private static readonly MenuRepository instancia = new MenuRepository();
        private readonly List<MenuDeComida> _menus;
        private int _proximoId;

        private MenuRepository()
        {
            // Menús de ejemplo para tener algo en la grilla
            _menus = new List<MenuDeComida>();
            _menus.Add(new MenuDeComida(1, "Menu A", 50.00m, 30.00m));
            _menus.Add(new MenuDeComida(2, "Menu B", 75.00m, 45.00m));
            _menus.Add(new MenuDeComida(3, "Menu C", 100.00m, 60.00m));
            _proximoId = 4;
        }

        public static MenuRepository Instance
        {
            get { return instancia; }
        }

        public List<MenuDeComida> ObtenerTodos()
        {
            return new List<MenuDeComida>(_menus);
        }

        public MenuDeComida ObtenerPorId(int id)
        {
            foreach (MenuDeComida menu in _menus)
            {
                if (menu.Id == id)
                {
                    return menu;
                }
            }

            return null;
        }

        public void Crear(MenuDeComida menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException("menu");
            }

            menu.Id = _proximoId;
            _proximoId++;
            _menus.Add(menu);
        }

        public bool Modificar(MenuDeComida menu)
        {
            MenuDeComida existente = ObtenerPorId(menu.Id);
            if (existente == null)
            {
                return false;
            }

            existente.Nombre = menu.Nombre;
            existente.CostoPorAdulto = menu.CostoPorAdulto;
            existente.CostoPorNiño = menu.CostoPorNiño;
            return true;
        }

        public bool Eliminar(int id)
        {
            MenuDeComida existente = ObtenerPorId(id);
            if (existente == null)
            {
                return false;
            }

            _menus.Remove(existente);
            return true;
        }
    }
}
