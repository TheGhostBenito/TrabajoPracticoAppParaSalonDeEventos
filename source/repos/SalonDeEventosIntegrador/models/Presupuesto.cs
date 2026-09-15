using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_integrador.models
{
    public class Presupuesto
    {
        private int id ;
        private DateTime fechaCreacionDePresupuesto ;
        private Evento evento;
        private Salon salonDelEvento;
        private MenuDeComida menuDeComidaSegunAsistencia;
        private List<ServiciosAdicionales> serviciosAdicionales;
        private TipoDeExclusionDeMenu exclusionDeMenu;


        public Presupuesto(int _id, DateTime _fechaCreacion, Evento _evento, Salon _salon, MenuDeComida _menu, List<ServiciosAdicionales> _serviciosAdicionales, TipoDeExclusionDeMenu _exclusion)
        {
            id = _id;
            fechaCreacionDePresupuesto = _fechaCreacion;
            evento = _evento;
            salonDelEvento = _salon;
            menuDeComidaSegunAsistencia = _menu;
            serviciosAdicionales = _serviciosAdicionales;
            exclusionDeMenu = _exclusion;
        }

        public decimal CalcularCostoTotal()
        {
            decimal total = salonDelEvento.CostoBase; // Trae el costo base del salón

            total *= evento.Duracion; // Multiplicamos por la duración del evento

            // Usamos las cantidades del evento
            total += (evento.CantidadAdultos * menuDeComidaSegunAsistencia.CostoPorAdulto);
            total += (evento.CantidadNinos * menuDeComidaSegunAsistencia.CostoPorNiño);

            // Sumamos servicios adicionales
            foreach (var servicio in serviciosAdicionales)
            {
                total += servicio.CostoDelServicio;
            }

            return total;
        }
    }


}
