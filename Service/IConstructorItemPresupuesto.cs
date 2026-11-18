using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    // Cada tipo de ítem encapsula su propia preparación, validación y construcción.
    // Para agregar un concepto nuevo se escribe una nueva implementación;
    // PresupuestoService no se toca.
    public interface IConstructorItemPresupuesto
    {
        void Preparar(PresupuestoRequestDto solicitud);

        void Validar(PresupuestoRequestDto solicitud, ContextoPresupuesto contexto);

        IEnumerable<ItemPresupuesto> Construir(PresupuestoRequestDto solicitud, ContextoPresupuesto contexto);
    }
}
