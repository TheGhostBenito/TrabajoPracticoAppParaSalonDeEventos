using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    // Transforma el modelo en el DTO que consumen las pantallas.
    // Los importes salen del modelo (la vista sigue sin calcular nada).
    public sealed class PresupuestoDtoMapper
    {
        private readonly List<IItemDtoMapper> _mapeadores;

        public PresupuestoDtoMapper(List<IItemDtoMapper> mapeadores)
        {
            if (mapeadores == null)
            {
                throw new ArgumentNullException("mapeadores");
            }

            _mapeadores = mapeadores;
        }

        public PresupuestoDto Mapear(Presupuesto presupuesto)
        {
            PresupuestoDto dto = new PresupuestoDto();
            dto.Id = presupuesto.Id;
            dto.FechaCreacion = presupuesto.FechaCreacion;
            dto.FechaEvento = presupuesto.FechaEvento;
            dto.HoraInicio = presupuesto.HoraInicio;
            dto.FechaHoraEvento = presupuesto.FechaHoraEvento;
            dto.NombreContacto = presupuesto.NombreContacto;
            dto.EmailContacto = presupuesto.EmailContacto;
            dto.TelefonoContacto = presupuesto.TelefonoContacto;
            dto.UbicacionEvento = presupuesto.UbicacionEvento;
            dto.Observaciones = presupuesto.Observaciones;
            dto.SalonId = presupuesto.SalonDelEvento == null ? 0 : presupuesto.SalonDelEvento.Id;
            dto.NombreSalon = presupuesto.NombreSalon;
            dto.DuracionHoras = presupuesto.Evento.Duracion;
            dto.CantidadAdultos = presupuesto.Evento.CantidadAdultos;
            dto.CantidadNinos = presupuesto.Evento.CantidadNinos;
            dto.CantidadTotalInvitados = presupuesto.CantidadTotalInvitados;
            dto.PorcentajeAdicionalMenuPrincipal = presupuesto.PorcentajeAdicionalMenuPrincipal;
            dto.PorcentajeAdicionalMenuPersonalizado = presupuesto.PorcentajeAdicionalMenuPersonalizado;
            dto.CostoSalon = presupuesto.CostoSalon;
            dto.SubtotalMenusPrincipales = presupuesto.SubtotalMenusPrincipales;
            dto.RecargoMenusPrincipales = presupuesto.RecargoMenusPrincipales;
            dto.SubtotalMenusPersonalizados = presupuesto.SubtotalMenusPersonalizados;
            dto.RecargoMenusPersonalizados = presupuesto.RecargoMenusPersonalizados;
            dto.CostoMenus = presupuesto.CostoMenus;
            dto.CostoServiciosAdicionales = presupuesto.CostoServiciosAdicionales;
            dto.CostoOtrosItems = presupuesto.CostoOtrosItems;
            dto.TotalFinal = presupuesto.TotalFinal;
            dto.RutaPdf = presupuesto.RutaPdf;

            // Recorrido polimórfico: cualquier ítem nuevo se mapeado si tiene un mapper registrado.
            foreach (ItemPresupuesto item in presupuesto.Items)
            {
                foreach (IItemDtoMapper mapeador in _mapeadores)
                {
                    if (mapeador.PuedeMapear(item))
                    {
                        dto.Items.Add(mapeador.Mapear(item));
                        break;
                    }
                }

                // Campos históricos que sigue usando la vista actual.
                ItemMenu itemMenu = item as ItemMenu;
                if (itemMenu != null)
                {
                    dto.Menues.Add(CatalogoDtoConverter.DetalleMenuADto(itemMenu));
                }

                ItemServicio itemServicio = item as ItemServicio;
                if (itemServicio != null)
                {
                    dto.ServiciosAdicionales.Add(CatalogoDtoConverter.ServicioADto(itemServicio.Servicio));
                }
            }

            return dto;
        }
    }
}
