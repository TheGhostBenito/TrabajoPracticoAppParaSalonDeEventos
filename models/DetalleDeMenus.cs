using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_integrador.models
{
    public class DetalleDeMenus
    {
        private int cantidadDeMenusUnitaria;
        private int cantidadDeMenusConExcepciones;
        private TipoDeExclusionDeMenu exclusionDelMenu;
        private Evento evento;
        private MenuDeComida menuElegido;

        public DetalleDeMenus(int _cantidadUnitaria, int _cantidadConExcepciones, TipoDeExclusionDeMenu _exclusion, Evento _evento, MenuDeComida _menu)
        {
            cantidadDeMenusUnitaria = _cantidadUnitaria;
            cantidadDeMenusConExcepciones = _cantidadConExcepciones;
            exclusionDelMenu = _exclusion;
            evento = _evento;
            menuElegido = _menu;
        }

        public int CantidadDeMenusUnitaria
        {
            get { return cantidadDeMenusUnitaria; }
        }

        public int CantidadDeMenusConExcepciones
        {
            get { return cantidadDeMenusConExcepciones; }
        }

        public TipoDeExclusionDeMenu ExclusionDelMenu
        {
            get { return exclusionDelMenu; }
        }

        public Evento Evento
        {
            get { return evento; }
        }

        public MenuDeComida MenuElegido
        {
            get { return menuElegido; }
        }
    }
}