using System;
using System.Globalization;

namespace proyecto_integrador.models
{
    // Ítem correspondiente al alquiler del salón.
    public sealed class ItemSalon : ItemPresupuesto
    {
        public Salon Salon { get; private set; }
        public int DuracionHoras { get; private set; }

        public override string Tipo { get { return "Salon"; } }

        public ItemSalon(Salon salon, int duracionHoras)
            : base(ObtenerId(salon), ObtenerNombre(salon))
        {
            if (salon == null)
            {
                throw new ArgumentNullException("salon");
            }

            if (duracionHoras <= 0)
            {
                throw new ArgumentException("La duración del salón tiene que ser mayor que cero.");
            }

            Salon = salon;
            DuracionHoras = duracionHoras;
        }

        public override decimal CalcularSubtotal()
        {
            return Salon.CostoBase * DuracionHoras;
        }

        public override string ObtenerDetalle()
        {
            return Descripcion + " - " + DuracionHoras.ToString(CultureInfo.InvariantCulture) + " hs";
        }

        private static int ObtenerId(Salon salon)
        {
            return salon == null ? 0 : salon.Id;
        }

        private static string ObtenerNombre(Salon salon)
        {
            return salon == null ? string.Empty : salon.Nombre;
        }
    }
}
