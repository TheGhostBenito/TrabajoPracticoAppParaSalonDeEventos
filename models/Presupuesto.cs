using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_integrador.repository;

namespace proyecto_integrador.models
{
    public class Presupuesto
    {
        private readonly int id;
        private readonly DateTime fechaCreacionDePresupuesto;
        private readonly Evento evento;
        private readonly Salon salonDelEvento;
        private readonly List<DetalleDeMenus> menues;
        private readonly List<ServiciosAdicionales> serviciosAdicionales;

        public Presupuesto(int _id, DateTime _fechaCreacion, Evento _evento, Salon _salon, List<DetalleDeMenus> _menues, List<ServiciosAdicionales> _serviciosAdicionales)
        {
            id = _id;
            fechaCreacionDePresupuesto = _fechaCreacion;
            evento = _evento;
            salonDelEvento = _salon;
            menues = _menues;
            serviciosAdicionales = _serviciosAdicionales;
        }

        public decimal CalcularCostoTotal()
        {
            if (salonDelEvento == null || evento == null || menues == null || serviciosAdicionales == null) // Verificar si alguno de los datos necesarios es nulo
            {
                throw new InvalidOperationException("No se puede calcular el costo total del presupuesto porque faltan datos.");
            }

            if (salonDelEvento != null && evento != null && menues != null && serviciosAdicionales != null) // Verificar si todos los datos necesarios son distintos que nulo
            {
                decimal total = salonDelEvento.CostoBase;
                total *= evento.Duracion;
                foreach (var detalle in menues)
                {
                    total += detalle.CantidadDeMenusUnitaria * detalle.MenuElegido.CostoPorAdulto;
                    total += detalle.CantidadDeMenusConExcepciones * detalle.MenuElegido.CostoPorAdulto;
                }
                foreach (var servicio in serviciosAdicionales)
                {
                    total += servicio.CostoDelServicio;
                }
                return total;
            }
            else // Si alguno de los datos necesarios es nulo, lanzar una excepción
            {
                throw new InvalidOperationException("No se puede calcular el costo total del presupuesto porque faltan datos o no existen los valores enviados");
            }
        }
    }
}