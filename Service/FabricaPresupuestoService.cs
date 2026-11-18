using System.Collections.Generic;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    // Punto donde se registran las piezas del subsystem de presupuestos.
    // Si se agrega un tipo de ítem nuevo: nueva clase Item + Constructor + Mapper,
    // y se registran acá. Ni Presupuesto ni PresupuestoService se modifican.
    public static class FabricaPresupuestoService
    {
        public static PresupuestoService Crear(
            IRepository<Presupuesto> presupuestoRepository,
            IRepository<Salon> salonRepository,
            IRepository<MenuDeComida> menuRepository,
            IRepository<ServiciosAdicionales> servicioRepository,
            IPresupuestoPdfService pdfService)
        {
            PresupuestoCatalogosService catalogos = new PresupuestoCatalogosService(
                presupuestoRepository,
                salonRepository,
                menuRepository,
                servicioRepository);

            List<IConstructorItemPresupuesto> constructores = new List<IConstructorItemPresupuesto>();
            constructores.Add(new ConstructorItemSalon());
            constructores.Add(new ConstructorItemMenu());
            constructores.Add(new ConstructorItemServicio());

            List<IItemDtoMapper> mapeadores = new List<IItemDtoMapper>();
            mapeadores.Add(new MapeadorItemSalon());
            mapeadores.Add(new MapeadorItemMenu());
            mapeadores.Add(new MapeadorItemServicio());

            return new PresupuestoService(
                presupuestoRepository,
                salonRepository,
                menuRepository,
                servicioRepository,
                pdfService,
                catalogos,
                new PresupuestoRequestValidator(),
                constructores,
                new PresupuestoDtoMapper(mapeadores));
        }
    }
}
