using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_integrador.repository;

namespace proyecto_integrador.models
{
    public class DetalleDeMenus
    {
        private int cantidadDeMenusUnitaria;
        private int cantidadDeMenusConExcepciones;

        public int CantidadDeMenusUnitaria
        {
            get => cantidadDeMenusUnitaria;
            set
            {
                if (value < 0) throw new ArgumentException("La cantidad no puede ser negativa.");
                cantidadDeMenusUnitaria = value;
            }
        }

        public int CantidadDeMenusConExcepciones
        {
            get => cantidadDeMenusConExcepciones;
            set
            {
                if (value < 0) throw new ArgumentException("La cantidad no puede ser negativa.");
                cantidadDeMenusConExcepciones = value;
            }
        }




        public TipoDeExclusionDeMenu ExclusionDelMenu;
        public MenuDeComida MenuElegido;

       

        public DetalleDeMenus(int cantidadUnitaria, int cantidadConExcepciones, TipoDeExclusionDeMenu exclusion, MenuDeComida menu) //constructor de la clase DetalleDeMenus refactorizado para usar propiedades en lugar de campos privados
        {
            CantidadDeMenusUnitaria = cantidadUnitaria;
            CantidadDeMenusConExcepciones = cantidadConExcepciones;
            ExclusionDelMenu = exclusion;
            MenuElegido = menu;
        }
    }
}