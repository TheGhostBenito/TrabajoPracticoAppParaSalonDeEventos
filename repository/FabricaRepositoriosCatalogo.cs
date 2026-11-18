using System;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Compone los repositorios de catálogo. Siempre arma las dos implementaciones
    // (memoria y archivo local) y las envuelve en un proxy que cambia de destino
    // en caliente cuando el usuario mueve el interruptor de la solapa izquierda.
    public static class FabricaRepositoriosCatalogo
    {
        public static RepositoriosCatalogo Crear(ServicioModoDatos servicioModo)
        {
            if (servicioModo == null)
            {
                throw new ArgumentNullException("servicioModo");
            }

            IRepository<Salon> salones = new RepositorioCatalogoConmutable<Salon>(
                SalonRepository.Instance,
                CatalogosLocales.CrearRepositorioSalones(),
                servicioModo);

            IRepository<MenuDeComida> menues = new RepositorioCatalogoConmutable<MenuDeComida>(
                MenuRepository.Instance,
                CatalogosLocales.CrearRepositorioMenues(),
                servicioModo);

            IRepository<ServiciosAdicionales> servicios = new RepositorioCatalogoConmutable<ServiciosAdicionales>(
                ServiciosAdicionalesRepository.Instance,
                CatalogosLocales.CrearRepositorioServicios(),
                servicioModo);

            return new RepositoriosCatalogo(salones, menues, servicios);
        }
    }

    public class RepositoriosCatalogo
    {
        public IRepository<Salon> Salones { get; private set; }
        public IRepository<MenuDeComida> Menues { get; private set; }
        public IRepository<ServiciosAdicionales> Servicios { get; private set; }

        public RepositoriosCatalogo(
            IRepository<Salon> salones,
            IRepository<MenuDeComida> menues,
            IRepository<ServiciosAdicionales> servicios)
        {
            Salones = salones;
            Menues = menues;
            Servicios = servicios;
        }
    }
}
