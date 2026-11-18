using System.Collections.Generic;
using proyecto_integrador.dto;

namespace proyecto_integrador.Service
{
    // Contrato que define las operaciones disponibles para la gestión de presupuestos.
    public interface IPresupuestoService : IService<PresupuestoDto, PresupuestoRequestDto>
    {
        List<SalonDto> ObtenerSalonesDisponibles(int presupuestoId);

        List<MenuDto> ObtenerMenusDisponibles(int presupuestoId);

        List<ServicioAdicionalDto> ObtenerServiciosDisponibles(int presupuestoId);

        AsignacionMenuDto CalcularInvitadosRestantes(
            int cantidadAdultos,
            int cantidadNinos,
            List<DetalleMenuDto> menusPersonalizados);

        // Calcula una estimación del costo total del presupuesto.
        decimal CalcularEstimacion(PresupuestoRequestDto solicitud, int presupuestoExistenteId);

        // Valida un servicio personalizado ingresado por el usuario.
        void ValidarServicioPersonalizado(string descripcion, decimal monto);

        // Genera y exporta el presupuesto a PDF.
        string ExportarPdf(int presupuestoId, string ruta);
    }
}
