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
        private int id;
        private DateTime fechaCreacionDePresupuesto;
        private Evento evento;
        private Salon salonDelEvento;
        private List<DetalleDeMenus> menues;
        private ServiciosAdicionalesRepository serviciosAdicionales;

        public Presupuesto(int _id, DateTime _fechaCreacion, Evento _evento, Salon _salon, List<DetalleDeMenus> _menues, ServiciosAdicionalesRepository _serviciosAdicionales)
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
    }
}