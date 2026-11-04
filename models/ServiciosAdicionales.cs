using System;
using System.Collections.Generic;

namespace proyecto_integrador.models
{
    public class ServiciosAdicionales
    {
        private int id;
        private string descripcion;
        private decimal montoFijo;

        public ServiciosAdicionales(int _id, string _descripcion, decimal _montoFijo)
        {
            id = _id;
            descripcion = _descripcion;
            montoFijo = _montoFijo;
        }

        public decimal CostoDelServicio
        {
            set { 
                if (value >= 0)
                {
                    montoFijo = value;
                }
            }
            get { return montoFijo; }
        }
        public string Descripcion
        {
            set { descripcion = value; }
            get { return descripcion; }
        }
        
        public int Id
        {
            set { id = value; }
            get { return id; }
        }
    }
}
