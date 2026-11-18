using proyecto_integrador.models;

namespace proyecto_integrador.dto
{
    // DTO base: la vista y el PDF pueden recorrer cualquier ítem sin conocer su tipo.
    public class ItemPresupuestoDto
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public decimal Subtotal { get; set; }
    }

    public sealed class ItemSalonDto : ItemPresupuestoDto
    {
        public int SalonId { get; set; }
        public int DuracionHoras { get; set; }
    }

    public sealed class ItemMenuDto : ItemPresupuestoDto
    {
        public int MenuId { get; set; }
        public TipoDeExclusionDeMenu Particularidad { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
    }

    public sealed class ItemServicioDto : ItemPresupuestoDto
    {
        public int ServicioId { get; set; }
        public bool EsPersonalizado { get; set; }
    }
}
