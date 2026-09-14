using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
