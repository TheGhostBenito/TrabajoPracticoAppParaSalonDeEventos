using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    public sealed class ConstructorItemMenu : IConstructorItemPresupuesto
    {
        // Regla de negocio: si hay un único menú principal y quedó sin invitados
        // (porque todos comen menús con particularidad), esa fila se descarta.
        public void Preparar(PresupuestoRequestDto solicitud)
        {
            if (solicitud == null || solicitud.Menues == null)
            {
                return;
            }

            int cantidadPrincipales = 0;
            foreach (DetalleMenuDto detalle in solicitud.Menues)
            {
                if (detalle != null && detalle.Particularidad == TipoDeExclusionDeMenu.Ninguna)
                {
                    cantidadPrincipales++;
                }
            }

            List<DetalleMenuDto> depurados = new List<DetalleMenuDto>();
            foreach (DetalleMenuDto detalle in solicitud.Menues)
            {
                bool esPrincipalUnicoVacio = detalle != null
                    && detalle.Particularidad == TipoDeExclusionDeMenu.Ninguna
                    && cantidadPrincipales == 1
                    && detalle.CantidadAdultos + detalle.CantidadNinos <= 0;

                if (!esPrincipalUnicoVacio)
                {
                    depurados.Add(detalle);
                }
            }

            solicitud.Menues = depurados;
        }

        public void Validar(PresupuestoRequestDto solicitud, ContextoPresupuesto contexto)
        {
            int adultosAsignados = 0;
            int ninosAsignados = 0;

            foreach (DetalleMenuDto detalle in solicitud.Menues)
            {
                if (detalle == null || detalle.CantidadAdultos < 0 || detalle.CantidadNinos < 0
                    || detalle.CantidadAdultos + detalle.CantidadNinos <= 0)
                {
                    throw new ArgumentException("Cada fila de menú debe tener cantidades válidas de adultos o niños.");
                }

                if (!Enum.IsDefined(typeof(TipoDeExclusionDeMenu), detalle.Particularidad))
                {
                    throw new ArgumentException("Elegí una particularidad de menú válida.");
                }

                if (contexto.ObtenerMenu(detalle.MenuId) == null)
                {
                    if (detalle.Particularidad == TipoDeExclusionDeMenu.Ninguna)
                    {
                        throw new ArgumentException("Seleccioná un menú principal para cada fila.");
                    }

                    throw new ArgumentException("Seleccioná un menú para cada particularidad.");
                }

                adultosAsignados += detalle.CantidadAdultos;
                ninosAsignados += detalle.CantidadNinos;
            }

            if (adultosAsignados != solicitud.CantidadAdultos || ninosAsignados != solicitud.CantidadNinos)
            {
                throw new ArgumentException("La suma de adultos y niños asignados a los menús debe coincidir exactamente con la cantidad total de invitados.");
            }
        }

        public IEnumerable<ItemPresupuesto> Construir(PresupuestoRequestDto solicitud, ContextoPresupuesto contexto)
        {
            if (solicitud.Menues == null)
            {
                yield break;
            }

            foreach (DetalleMenuDto detalle in solicitud.Menues)
            {
                if (detalle == null || detalle.CantidadAdultos < 0 || detalle.CantidadNinos < 0)
                {
                    continue;
                }

                MenuDeComida menu = contexto.ObtenerMenu(detalle.MenuId);
                if (menu == null)
                {
                    continue;
                }

                DetalleDeMenus detalleMenu = new DetalleDeMenus(
                    detalle.CantidadAdultos,
                    detalle.CantidadNinos,
                    detalle.Particularidad,
                    menu);

                yield return new ItemMenu(detalleMenu);
            }
        }
    }
}
