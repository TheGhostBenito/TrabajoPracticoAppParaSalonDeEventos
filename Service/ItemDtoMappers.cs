using proyecto_integrador.dto;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    public sealed class MapeadorItemSalon : IItemDtoMapper
    {
        public bool PuedeMapear(ItemPresupuesto item)
        {
            return item is ItemSalon;
        }

        public ItemPresupuestoDto Mapear(ItemPresupuesto item)
        {
            ItemSalon salon = (ItemSalon)item;
            ItemSalonDto dto = new ItemSalonDto();
            dto.Tipo = salon.Tipo;
            dto.Descripcion = salon.Descripcion;
            dto.Detalle = salon.ObtenerDetalle();
            dto.Subtotal = salon.CalcularSubtotal();
            dto.SalonId = salon.Id;
            dto.DuracionHoras = salon.DuracionHoras;
            return dto;
        }
    }

    public sealed class MapeadorItemMenu : IItemDtoMapper
    {
        public bool PuedeMapear(ItemPresupuesto item)
        {
            return item is ItemMenu;
        }

        public ItemPresupuestoDto Mapear(ItemPresupuesto item)
        {
            ItemMenu menu = (ItemMenu)item;
            ItemMenuDto dto = new ItemMenuDto();
            dto.Tipo = menu.Tipo;
            dto.Descripcion = menu.Descripcion;
            dto.Detalle = menu.ObtenerDetalle();
            dto.Subtotal = menu.CalcularSubtotal();
            dto.MenuId = menu.Id;
            dto.Particularidad = menu.Particularidad;
            dto.CantidadAdultos = menu.Detalle.CantidadAdultos;
            dto.CantidadNinos = menu.Detalle.CantidadNinos;
            return dto;
        }
    }

    public sealed class MapeadorItemServicio : IItemDtoMapper
    {
        public bool PuedeMapear(ItemPresupuesto item)
        {
            return item is ItemServicio;
        }

        public ItemPresupuestoDto Mapear(ItemPresupuesto item)
        {
            ItemServicio servicio = (ItemServicio)item;
            ItemServicioDto dto = new ItemServicioDto();
            dto.Tipo = servicio.Tipo;
            dto.Descripcion = servicio.Descripcion;
            dto.Detalle = servicio.ObtenerDetalle();
            dto.Subtotal = servicio.CalcularSubtotal();
            dto.ServicioId = servicio.Id;
            dto.EsPersonalizado = servicio.EsPersonalizado;
            return dto;
        }
    }
}
