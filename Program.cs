using System;
using System.Windows.Forms;
using proyecto_integrador.controller;
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

            // Repositorios (cada uno es un Singleton)
            ISalonRepository salonRepository = SalonRepository.Instance;
            IMenuRepository menuRepository = MenuRepository.Instance;
            IServiciosAdicionalesRepository serviciosRepository = ServiciosAdicionalesRepository.Instance;
            IPresupuestoRepository presupuestoRepository = PresupuestoRepository.Instance;

            // Services: reciben interfaces de repositorio
            ISalonService salonService = new SalonService(salonRepository);
            IMenuService menuService = new MenuService(menuRepository);
            IServiciosAdicionalesService serviciosService = new ServiciosAdicionalesService(serviciosRepository);
            IPresupuestoPdfService pdfService = new PresupuestoPdfService();
            IPresupuestoService presupuestoService = new PresupuestoService(
                presupuestoRepository,
                salonRepository,
                menuRepository,
                serviciosRepository,
                pdfService);

            // Controllers: reciben interfaces de service
            SalonController salonController = new SalonController(salonService);
            MenuController menuController = new MenuController(menuService);
            ServiciosAdicionalesController serviciosController = new ServiciosAdicionalesController(serviciosService);
            PresupuestoController presupuestoController = new PresupuestoController(presupuestoService);

            // Vista principal: recibe los controllers
            Application.Run(new SolapaIzquierdaForm(
                salonController,
                menuController,
                serviciosController,
                presupuestoController));
        }
    }
}
