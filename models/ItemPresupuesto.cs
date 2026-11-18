using System;

namespace proyecto_integrador.models
{
    // Base de los ítems que se pueden cobrar en un presupuesto.
    // El presupuesto y el service trabajan siempre con esta abstracción:
    // si aparece un concepto nuevo, alcanza con crear una subclase.
    public abstract class ItemPresupuesto
    {
        public int Id { get; protected set; }
        public string Descripcion { get; protected set; }
        public abstract string Tipo { get; }

        protected ItemPresupuesto(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion ?? string.Empty;
        }

        // Cada ítem sabe cuánto cuesta. Acá está el polimorfismo:
        // quien suma el total no necesita saber de qué tipo es cada ítem.
        public abstract decimal CalcularSubtotal();

        // Texto listo para mostrar en el PDF o en el detalle del presupuesto.
        public abstract string ObtenerDetalle();
    }
}
