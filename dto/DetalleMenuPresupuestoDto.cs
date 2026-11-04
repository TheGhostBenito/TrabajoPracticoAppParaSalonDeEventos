using proyecto_integrador.models;

namespace proyecto_integrador.dto
{
    // DTO de salida: una fila de menú de un presupuesto ya calculada por el Service.
    public class DetalleMenuPresupuestoDto
    {
        public int MenuId { get; set; }
        public string NombreMenu { get; set; }
        public TipoDeExclusionDeMenu Particularidad { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
        public decimal Subtotal { get; set; }
    }
}
