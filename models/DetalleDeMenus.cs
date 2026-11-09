using System;

namespace proyecto_integrador.models
{
    public class DetalleDeMenus
    {
        public int CantidadAdultos { get; private set; }
        public int CantidadNinos { get; private set; }
        public TipoDeExclusionDeMenu ExclusionDelMenu { get; private set; }
        public MenuDeComida MenuElegido { get; private set; }

        public DetalleDeMenus(int cantidadAdultos, int cantidadNinos, TipoDeExclusionDeMenu exclusion, MenuDeComida menu)
        {
            if (cantidadAdultos < 0 || cantidadNinos < 0)
            {
                throw new ArgumentException("Las cantidades de menús no pueden ser negativas.");
            }

            if (menu == null)
            {
                throw new ArgumentNullException("menu");
            }

            CantidadAdultos = cantidadAdultos;
            CantidadNinos = cantidadNinos;
            ExclusionDelMenu = exclusion;
            MenuElegido = menu;
        }

        public decimal CalcularSubtotal()
        {
            decimal costoAdultos = CantidadAdultos * MenuElegido.CostoPorAdulto;
            decimal costoNinos = CantidadNinos * MenuElegido.CostoPorNiño;
            return costoAdultos + costoNinos;
        }
    }
}