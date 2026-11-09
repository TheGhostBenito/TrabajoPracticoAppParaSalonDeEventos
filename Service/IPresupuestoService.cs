// Contrato que define las operaciones disponibles
// para la gestión de presupuestos.
using proyecto_integrador.dto;
using System.Collections.Generic;

public interface IPresupuestoService
{
    List<PresupuestoDto> ObtenerTodos();

    List<SalonDto> ObtenerSalonesDisponibles(int presupuestoId);

    List<MenuDto> ObtenerMenusDisponibles(int presupuestoId);

    List<ServicioAdicionalDto> ObtenerServiciosDisponibles(int presupuestoId);

    AsignacionMenuDto CalcularInvitadosRestantes(
        int cantidadAdultos,
        int cantidadNinos,
        List<DetalleMenuDto> menusPersonalizados);

    // Calcula una estimación del costo total del presupuesto
    decimal CalcularEstimacion(
        PresupuestoRequestDto solicitud,
        int presupuestoExistenteId);

    // Valida un servicio personalizado ingresado por el usuario
    void ValidarServicioPersonalizado(
        string descripcion,
        decimal monto);

    PresupuestoDto Crear(PresupuestoRequestDto solicitud);

    PresupuestoDto Modificar(
        int id,
        PresupuestoRequestDto solicitud);

    // Genera y exporta el presupuesto a PDF
    string ExportarPdf(
        int presupuestoId,
        string ruta);
}