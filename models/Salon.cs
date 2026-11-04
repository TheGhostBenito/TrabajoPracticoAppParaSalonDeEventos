using System;
using System.Collections.Generic;

namespace proyecto_integrador.models
{
    public class Salon
    {
        private int id;
        private string nombreDelSalon;
        private string ubicacion;
        private decimal costoBase;

        public Salon(int _id, string _nombreDelSalon, string _ubicacion, decimal _costoBase)
        {
            id = _id;
            nombreDelSalon = _nombreDelSalon;
            ubicacion = _ubicacion;
            costoBase = _costoBase;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombreDelSalon; }
            set { nombreDelSalon = value; }
        }

        public string Ubicacion
        {
            get { return ubicacion; }
            set { ubicacion = value; }
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