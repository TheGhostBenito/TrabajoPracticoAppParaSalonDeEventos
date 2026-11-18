using proyecto_integrador.dto;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    public interface IItemDtoMapper
    {
        bool PuedeMapear(ItemPresupuesto item);

        ItemPresupuestoDto Mapear(ItemPresupuesto item);
    }
}
