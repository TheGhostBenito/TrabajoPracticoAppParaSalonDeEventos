using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    public sealed class ConstructorItemSalon : IConstructorItemPresupuesto
    {
        public void Preparar(PresupuestoRequestDto solicitud)
        {
        }

        public void Validar(PresupuestoRequestDto solicitud, ContextoPresupuesto contexto)
        {
            if (contexto.ObtenerSalon(solicitud.SalonId) == null)
            {
                throw new ArgumentException("Seleccioná un salón disponible.");
            }
        }

        public IEnumerable<ItemPresupuesto> Construir(PresupuestoRequestDto solicitud, ContextoPresupuesto contexto)
        {
            if (solicitud.DuracionHoras <= 0)
            {
                yield break;
            }

            Salon salon = contexto.ObtenerSalon(solicitud.SalonId);
            if (salon == null)
            {
                yield break;
            }

            yield return new ItemSalon(salon, solicitud.DuracionHoras);
        }
    }
}
