using System;
using System.Windows.Forms;
using proyecto_integrador.controller;
using proyecto_integrador.dto;
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

            // Repositorios (cada uno es un Singleton). Todos se usan por la misma interfaz genérica IRepository<T>
            IRepository<Salon> salonRepository = SalonRepository.Instance;
            IRepository<MenuDeComida> menuRepository = MenuRepository.Instance;
            IRepository<ServiciosAdicionales> serviciosRepository = ServiciosAdicionalesRepository.Instance;
            IRepository<Presupuesto> presupuestoRepository = PresupuestoRepository.Instance;

            // Services: reciben la interfaz genérica del repositorio
            IService<SalonDto, SalonDto> salonService = new SalonService(salonRepository);
            IService<MenuDto, MenuDto> menuService = new MenuService(menuRepository);
            IService<ServicioAdicionalDto, ServicioAdicionalDto> serviciosService = new ServiciosAdicionalesService(serviciosRepository);
            PresupuestoPdfService pdfService = new PresupuestoPdfService();
            PresupuestoService presupuestoService = new PresupuestoService(
                presupuestoRepository,
                salonRepository,
                menuRepository,
                serviciosRepository,
                pdfService);

            // Controllers: reciben el service
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
