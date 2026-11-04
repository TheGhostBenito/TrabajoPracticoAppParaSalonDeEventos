using System;
using System.Collections.Generic;

namespace proyecto_integrador.dto
{
    // DTO de salida: todo lo que las pantallas necesitan mostrar de un presupuesto.
    // Los importes ya vienen calculados desde el Service; la View solo los muestra.
    public class PresupuestoDto
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEvento { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public string FechaHoraEvento { get; set; }
        public string NombreContacto { get; set; }
        public string EmailContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string UbicacionEvento { get; set; }
        public string Observaciones { get; set; }
        public int SalonId { get; set; }
        public string NombreSalon { get; set; }
        public int DuracionHoras { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
        public int CantidadTotalInvitados { get; set; }
        public decimal PorcentajeAdicionalMenuPrincipal { get; set; }
        public decimal PorcentajeAdicionalMenuPersonalizado { get; set; }
        public List<DetalleMenuPresupuestoDto> Menues { get; set; }
        public List<ServicioAdicionalDto> ServiciosAdicionales { get; set; }
        public decimal CostoSalon { get; set; }
        public decimal SubtotalMenusPrincipales { get; set; }
        public decimal RecargoMenusPrincipales { get; set; }
        public decimal SubtotalMenusPersonalizados { get; set; }
        public decimal RecargoMenusPersonalizados { get; set; }
        public decimal CostoMenus { get; set; }
        public decimal CostoServiciosAdicionales { get; set; }
        public decimal TotalFinal { get; set; }
        public string RutaPdf { get; set; }

        public PresupuestoDto()
        {
            Menues = new List<DetalleMenuPresupuestoDto>();
            ServiciosAdicionales = new List<ServicioAdicionalDto>();
        }
    }
}
