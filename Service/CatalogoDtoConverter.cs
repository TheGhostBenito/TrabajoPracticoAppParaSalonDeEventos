using proyecto_integrador.dto;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    // Convierte entidades del catálogo a DTO. Lo usa tanto el service de
    // presupuestos como el service que arma los listados disponibles.
    public static class CatalogoDtoConverter
    {
        public static SalonDto SalonADto(Salon salon)
        {
            SalonDto dto = new SalonDto();
            dto.Id = salon.Id;
            dto.Nombre = salon.Nombre;
            dto.Ubicacion = salon.Ubicacion;
            dto.CostoBase = salon.CostoBase;
            return dto;
        }

        public static MenuDto MenuADto(MenuDeComida menu)
        {
            MenuDto dto = new MenuDto();
            dto.Id = menu.Id;
            dto.Nombre = menu.Nombre;
            dto.CostoPorAdulto = menu.CostoPorAdulto;
            dto.CostoPorNiño = menu.CostoPorNiño;
            return dto;
        }

        public static ServicioAdicionalDto ServicioADto(ServiciosAdicionales servicio)
        {
            ServicioAdicionalDto dto = new ServicioAdicionalDto();
            dto.Id = servicio.Id;
            dto.Descripcion = servicio.Descripcion;
            dto.CostoDelServicio = servicio.CostoDelServicio;
            return dto;
        }

        public static DetalleMenuPresupuestoDto DetalleMenuADto(ItemMenu item)
        {
            DetalleMenuPresupuestoDto dto = new DetalleMenuPresupuestoDto();
            dto.MenuId = item.Id;
            dto.NombreMenu = item.Descripcion;
            dto.Particularidad = item.Particularidad;
            dto.CantidadAdultos = item.Detalle.CantidadAdultos;
            dto.CantidadNinos = item.Detalle.CantidadNinos;
            dto.Subtotal = item.CalcularSubtotal();
            return dto;
        }
    }
}
