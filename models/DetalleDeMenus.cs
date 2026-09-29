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
        private TipoDeExclusionDeMenu exclusionDelMenu;
        private Evento evento;
        private MenuRepository menuElegido;

        public DetalleDeMenus(int _cantidadUnitaria, int _cantidadConExcepciones, TipoDeExclusionDeMenu _exclusion, Evento _evento, MenuRepository _menu)
        {
            cantidadDeMenusUnitaria = _cantidadUnitaria;
            cantidadDeMenusConExcepciones = _cantidadConExcepciones;
            exclusionDelMenu = _exclusion;
            evento = _evento;
            menuElegido = _menu;
        }

        public int CantidadDeMenusUnitaria
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La cantidad de menús unitarios no puede ser negativa.");
                }
                cantidadDeMenusUnitaria = value;
            }
            get { return cantidadDeMenusUnitaria; }
        }

        public int CantidadDeMenusConExcepciones
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La cantidad de menús con excepciones no puede ser negativa.");
                }
                cantidadDeMenusConExcepciones = value;
            }
            get { return cantidadDeMenusConExcepciones; }
        }

        public TipoDeExclusionDeMenu ExclusionDelMenu
        { 
            get { return exclusionDelMenu; }
        }

        public Evento Event
        {
            get { return evento; }
        }

        public MenuRepository MenuElegido
        {
            get { return menuElegido; }
        }
    }
}