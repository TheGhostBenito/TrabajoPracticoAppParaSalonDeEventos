namespace proyecto_integrador.dto
{
    // DTO: datos de un menú de comida. El nombre CostoPorNiño coincide con la columna de la grilla del diseñador.
    public class MenuDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal CostoPorAdulto { get; set; }
        public decimal CostoPorNiño { get; set; }
    }
}
