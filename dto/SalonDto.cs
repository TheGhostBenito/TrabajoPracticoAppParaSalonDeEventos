namespace proyecto_integrador.dto
{
    // DTO: objeto simple, sin lógica, para llevar los datos de un salón entre capas.
    public class SalonDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public decimal CostoBase { get; set; }
    }
}
