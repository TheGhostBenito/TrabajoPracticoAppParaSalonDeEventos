using System.Collections.Generic;
using proyecto_integrador.dto;

namespace proyecto_integrador.Service
{
    // Contrato del service de presupuestos: lo que el Controller puede pedirle.
    public interface IPresupuestoService
    {
        List<PresupuestoDto> ObtenerTodos();
        List<SalonDto> ObtenerSalonesDisponibles(int presupuestoId);
        List<MenuDto> ObtenerMenusDisponibles(int presupuestoId);
        List<ServicioAdicionalDto> ObtenerServiciosDisponibles(int presupuestoId);
        AsignacionMenuDto CalcularInvitadosRestantes(int cantidadAdultos, int cantidadNinos, List<DetalleMenuDto> menusPersonalizados);
        decimal CalcularEstimacion(PresupuestoRequestDto solicitud, int presupuestoExistenteId);
        void ValidarServicioPersonalizado(string descripcion, decimal monto);
        PresupuestoDto Crear(PresupuestoRequestDto solicitud);
        PresupuestoDto Modificar(int id, PresupuestoRequestDto solicitud);
        string ExportarPdf(int presupuestoId, string ruta);
    }
}
