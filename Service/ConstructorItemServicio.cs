using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    public sealed class ConstructorItemServicio : IConstructorItemPresupuesto
    {
        public void Preparar(PresupuestoRequestDto solicitud)
        {
        }

        public void Validar(PresupuestoRequestDto solicitud, ContextoPresupuesto contexto)
        {
            foreach (int servicioId in QuitarRepetidos(solicitud.ServiciosAdicionalesIds))
            {
                if (contexto.ObtenerServicio(servicioId) == null)
                {
                    throw new ArgumentException("Uno de los servicios seleccionados ya no está disponible.");
                }
            }

            foreach (ServicioPersonalizadoDto servicio in solicitud.ServiciosPersonalizados)
            {
                if (servicio == null)
                {
                    throw new ArgumentException("Cada servicio personalizado debe tener una descripción.");
                }

                ValidarServicioPersonalizado(servicio.Descripcion, servicio.MontoFijo);
            }
        }

        public IEnumerable<ItemPresupuesto> Construir(PresupuestoRequestDto solicitud, ContextoPresupuesto contexto)
        {
            if (solicitud.ServiciosAdicionalesIds != null)
            {
                foreach (int servicioId in QuitarRepetidos(solicitud.ServiciosAdicionalesIds))
                {
                    ServiciosAdicionales servicio = contexto.ObtenerServicio(servicioId);
                    if (servicio != null)
                    {
                        yield return new ItemServicio(servicio, false);
                    }
                }
            }

            if (solicitud.ServiciosPersonalizados != null)
            {
                foreach (ServicioPersonalizadoDto servicio in solicitud.ServiciosPersonalizados)
                {
                    if (servicio == null || servicio.MontoFijo < 0 || string.IsNullOrWhiteSpace(servicio.Descripcion))
                    {
                        continue;
                    }

                    ServiciosAdicionales personalizado = new ServiciosAdicionales(
                        0,
                        servicio.Descripcion.Trim(),
                        servicio.MontoFijo);

                    yield return new ItemServicio(personalizado, true);
                }
            }
        }

        public static void ValidarServicioPersonalizado(string descripcion, decimal monto)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("Cada servicio personalizado debe tener una descripción.");
            }

            if (monto < 0)
            {
                throw new ArgumentException("El costo de un servicio personalizado no puede ser negativo.");
            }
        }

        private static List<int> QuitarRepetidos(List<int> ids)
        {
            List<int> unicos = new List<int>();
            if (ids == null)
            {
                return unicos;
            }

            foreach (int id in ids)
            {
                if (!unicos.Contains(id))
                {
                    unicos.Add(id);
                }
            }

            return unicos;
        }
    }
}
