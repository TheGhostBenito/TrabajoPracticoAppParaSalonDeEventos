using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_integrador.models; // Llama y accede a la clase MenuDeComida. Que se encuentra dentro de model

namespace proyecto_integrador.repository
{
    public sealed class MenuRepository
    {
        private static readonly MenuRepository instancia = new MenuRepository();
        private readonly List<MenuDeComida> _menus = new List<MenuDeComida>();

        private MenuRepository()
        {
            // Menús de ejemplo para tener algo en la grilla
            _menus.Add(new MenuDeComida(1, "Menu A", 50.00m, 30.00m));
            _menus.Add(new MenuDeComida(2, "Menu B", 75.00m, 45.00m));
            _menus.Add(new MenuDeComida(3, "Menu C", 100.00m, 60.00m));
        }

        public static MenuRepository Instance
        {
            get { return instancia; }
        }

        public List<MenuDeComida> ObtenerTodos()
        {
            return _menus.ToList();
        }

        public void Crear(MenuDeComida menu)
        {
            menu.Id = _menus.Any() ? _menus.Max(m => m.Id) + 1 : 1;
            _menus.Add(menu);
        }

        public bool Eliminar(int id)
        {
            MenuDeComida menu = _menus.FirstOrDefault(m => m.Id == id);
            if (menu != null)
            {
                _menus.Remove(menu);
                return true;
            }
            return false;
        }

        public bool Modificar(MenuDeComida menu)
        {
            MenuDeComida existente = _menus.FirstOrDefault(m => m.Id == menu.Id);
            if (existente != null)
            {
                existente.Nombre = menu.Nombre;
                existente.CostoPorAdulto = menu.CostoPorAdulto;
                existente.CostoPorNiño = menu.CostoPorNiño;
                return true;
            }
            return false;
        }
    }
}