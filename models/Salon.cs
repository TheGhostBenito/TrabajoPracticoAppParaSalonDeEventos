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


        public decimal CostoBase  //getter y setter del costo base del salon
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El costo base no puede ser negativo.");
                }
                costoBase = value;
            }


            get { return costoBase; }
        }

    }
}