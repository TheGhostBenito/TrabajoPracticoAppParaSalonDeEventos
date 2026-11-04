namespace proyecto_integrador.dto
{
    // DTO: datos de un servicio adicional (descripción y monto fijo).
    public class ServicioAdicionalDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoDelServicio { get; set; }
    }
}
