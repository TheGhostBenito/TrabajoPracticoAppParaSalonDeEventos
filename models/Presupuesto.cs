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
        private int duracionDelEventoEnHoras ;
        private int cantidadDeAdultosQueAsisten;
        private int cantidadDeNiñosQueAsisten;
        private Salon salonDelEvento;
        private MenuDeComida menuDeComidaSegunAsistencia;
        private List<ServiciosAdicionales> serviciosAdicionales;
        private TipoDeExclusionDeMenu exclusionDeMenu;


        public Presupuesto(Salon _salon, MenuDeComida _menu, List<ServiciosAdicionales> _servicios, TipoDeExclusionDeMenu _exclusion)
        {
            salonDelEvento = _salon;
            menuDeComidaSegunAsistencia = _menu;
            serviciosAdicionales = _servicios;
            exclusionDeMenu = _exclusion;
            fechaCreacionDePresupuesto = DateTime.Now;
        }


        public decimal CalcularCostoTotal()
        {
            decimal total = salonDelEvento.CostoBase;
            total *= duracionDelEventoEnHoras;
            total += (cantidadDeAdultosQueAsisten * menuDeComidaSegunAsistencia.CostoPorAdulto);
            total += (cantidadDeNiñosQueAsisten * menuDeComidaSegunAsistencia.CostoPorNiño);
            foreach (var servicio in serviciosAdicionales)
            {
                total += servicio.CostoDelServicio;
            }
            return total;
        }
    }


}
