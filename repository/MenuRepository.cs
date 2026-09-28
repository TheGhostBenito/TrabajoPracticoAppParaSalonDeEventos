using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_integrador.models; // Llama y accede a la clase Salon. Que se encuentra dentro de model


namespace proyecto_integrador.repository
{
   public static class MenuRepository
    {
        public static List<MenuDeComida> ObtenerMenus() // Método que devuelve una lista de menús con .ObtenerMenus()
        {
            List<MenuDeComida> menus = new List<MenuDeComida>
            {
                new MenuDeComida(1, "Menu A", 50.00m, 30.00m),
                new MenuDeComida(2, "Menu B", 75.00m, 45.00m),
                new MenuDeComida(3, "Menu C", 100.00m, 60.00m)
            };
            return menus;
        }
    }
}
