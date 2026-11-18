using System;

namespace proyecto_integrador.models
{
    // Ítem correspondiente a un servicio adicional (de catálogo o escrito a mano).
    public sealed class ItemServicio : ItemPresupuesto
    {
        public ServiciosAdicionales Servicio { get; private set; }

        // Los servicios personalizados tienen Id = 0 y no vienen del catálogo.
        public bool EsPersonalizado { get; private set; }

        public override string Tipo { get { return "Servicio"; } }

        public ItemServicio(ServiciosAdicionales servicio, bool esPersonalizado)
            : base(ObtenerId(servicio), ObtenerDescripcion(servicio))
        {
            if (servicio == null)
            {
                throw new ArgumentNullException("servicio");
            }

            Servicio = servicio;
            EsPersonalizado = esPersonalizado;
        }

        public override decimal CalcularSubtotal()
        {
            return Servicio.CostoDelServicio;
        }

        public override string ObtenerDetalle()
        {
            return Descripcion;
        }

        private static int ObtenerId(ServiciosAdicionales servicio)
        {
            return servicio == null ? 0 : servicio.Id;
        }

        private static string ObtenerDescripcion(ServiciosAdicionales servicio)
        {
            return servicio == null ? string.Empty : servicio.Descripcion;
        }
    }
}
