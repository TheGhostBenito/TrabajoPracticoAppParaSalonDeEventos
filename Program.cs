using System;
using System.Windows.Forms;
using proyecto_integrador.controller;
using proyecto_integrador.models;
using proyecto_integrador.pantallas;
using proyecto_integrador.repository;
using proyecto_integrador.Service;

namespace proyecto_integrador
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// Acá se "arma" el sistema de abajo hacia arriba: Repository -> Service -> Controller -> View.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Modo inicial de los catálogos: se lee de App.config, pero se puede
            // cambiar en caliente desde la solapa izquierda. Guardar ya no requiere
            // servidor: se usa un archivo XML en la carpeta local del usuario.
            ServicioModoDatos servicioModo = ServicioModoDatos.Instance;
            RepositoriosCatalogo catalogos = FabricaRepositoriosCatalogo.Crear(servicioModo);

            // Repositorios: todos se usan por la interfaz genérica IRepository<T>.
            IRepository<Presupuesto> presupuestoRepository = PresupuestoRepository.Instance;

            // Services: reciben interfaces, nunca clases concretas.
            ISalonService salonService = new SalonService(catalogos.Salones);
            IMenuService menuService = new MenuService(catalogos.Menues);
            IServiciosAdicionalesService serviciosService = new ServiciosAdicionalesService(catalogos.Servicios);
            IPresupuestoPdfService pdfService = new PresupuestoPdfService();
            IPresupuestoService presupuestoService = FabricaPresupuestoService.Crear(
                presupuestoRepository,
                catalogos.Salones,
                catalogos.Menues,
                catalogos.Servicios,
                pdfService);

            // Controllers: reciben el service por su interfaz.
            SalonController salonController = new SalonController(salonService);
            MenuController menuController = new MenuController(menuService);
            ServiciosAdicionalesController serviciosController = new ServiciosAdicionalesController(serviciosService);
            PresupuestoController presupuestoController = new PresupuestoController(presupuestoService);

            // Vista principal: recibe los controllers.
            Application.Run(new SolapaIzquierdaForm(
                salonController,
                menuController,
                serviciosController,
                presupuestoController,
                servicioModo));
        }
    }
}
