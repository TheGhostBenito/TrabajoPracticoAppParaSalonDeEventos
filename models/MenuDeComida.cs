using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_integrador.models
{
    public class MenuDeComida
    {
        private int id;
        private string nombre;
        private decimal costoPorAdulto;
        private decimal costoPorNiño;



        public MenuDeComida(int _id, string _nombre, decimal _costoPorAdulto, decimal _costoPorNiño)
        {
            id = _id;
            nombre = _nombre;
            costoPorAdulto = _costoPorAdulto;
            costoPorNiño = _costoPorNiño;
        }
    }
}
