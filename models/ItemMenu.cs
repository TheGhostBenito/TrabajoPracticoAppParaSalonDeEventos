using System;
using System.Globalization;

namespace proyecto_integrador.models
{
    // Ítem correspondiente a una fila de menú (principal o con particularidad).
    public sealed class ItemMenu : ItemPresupuesto
    {
        public DetalleDeMenus Detalle { get; private set; }

        public TipoDeExclusionDeMenu Particularidad
        {
            get { return Detalle.ExclusionDelMenu; }
        }

        public bool EsMenuPrincipal
        {
            get { return Particularidad == TipoDeExclusionDeMenu.Ninguna; }
        }

        public override string Tipo { get { return "Menu"; } }

        public ItemMenu(DetalleDeMenus detalle)
            : base(ObtenerId(detalle), ObtenerNombre(detalle))
        {
            if (detalle == null)
            {
                throw new ArgumentNullException("detalle");
            }

            Detalle = detalle;
        }

        public override decimal CalcularSubtotal()
        {
            return Detalle.CalcularSubtotal();
        }

        public override string ObtenerDetalle()
        {
            string tipo = EsMenuPrincipal ? "menú principal" : "particularidad: " + Particularidad;
            return Descripcion + " (" + tipo + "): "
                + Detalle.CantidadAdultos.ToString(CultureInfo.InvariantCulture) + " adultos, "
                + Detalle.CantidadNinos.ToString(CultureInfo.InvariantCulture) + " niños";
        }

        private static int ObtenerId(DetalleDeMenus detalle)
        {
            return detalle == null || detalle.MenuElegido == null ? 0 : detalle.MenuElegido.Id;
        }

        private static string ObtenerNombre(DetalleDeMenus detalle)
        {
            return detalle == null || detalle.MenuElegido == null ? string.Empty : detalle.MenuElegido.Nombre;
        }
    }
}
