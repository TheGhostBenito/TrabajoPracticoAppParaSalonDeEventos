using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_integrador.models
{
    public class Salon
    {
        private int id;
        private string nombreDelSalon;
        private decimal costoBase;



        public Salon(int _id, string _nombreDelSalon, decimal _costoBase)
        {
            id = _id;
            nombreDelSalon = _nombreDelSalon;
            costoBase = _costoBase;
        }

    }

}