using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Arma los repositorios locales de catálogos. Los archivos van a
    // %LOCALAPPDATA%\EventosPresupuestos, así que no hace falta permisos
    // especiales ni un servidor: cada usuario guarda sus propios catálogos.
    public static class CatalogosLocales
    {
        private static string Directorio
        {
            get
            {
                string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (string.IsNullOrEmpty(local))
                {
                    local = AppDomain.CurrentDomain.BaseDirectory;
                }

                return Path.Combine(local, "EventosPresupuestos");
            }
        }

        // El parámetro opcional existe para poder probar los repositorios con una
        // carpeta temporal sin tocar los datos reales del usuario.
        private static string ObtenerDirectorio(string directorio)
        {
            return string.IsNullOrEmpty(directorio) ? Directorio : directorio;
        }

        public static IRepository<Salon> CrearRepositorioSalones(string directorio = null)
        {
            List<Salon> semilla = new List<Salon>();
            foreach (Salon salon in SalonRepository.Instance.ObtenerTodos())
            {
                semilla.Add(new Salon(salon.Id, salon.Nombre, salon.Ubicacion, salon.CostoBase));
            }

            return new RepositorioCatalogoLocal<Salon>(
                Path.Combine(ObtenerDirectorio(directorio),"salones.xml"),
                "salon",
                semilla,
                elemento => new Salon(
                    LeyendoEntero(elemento, "id"),
                    (string)elemento.Attribute("nombre"),
                    (string)elemento.Attribute("ubicacion"),
                    LeyendoDecimal(elemento, "costoBase")),
                salon => new XElement(
                    "salon",
                    new XAttribute("id", salon.Id),
                    new XAttribute("nombre", salon.Nombre),
                    new XAttribute("ubicacion", salon.Ubicacion),
                    new XAttribute("costoBase", Numero(salon.CostoBase))),
                salon => salon.Id,
                (salon, id) => salon.Id = id);
        }

        public static IRepository<MenuDeComida> CrearRepositorioMenues(string directorio = null)
        {
            List<MenuDeComida> semilla = new List<MenuDeComida>();
            foreach (MenuDeComida menu in MenuRepository.Instance.ObtenerTodos())
            {
                semilla.Add(new MenuDeComida(menu.Id, menu.Nombre, menu.CostoPorAdulto, menu.CostoPorNiño));
            }

            return new RepositorioCatalogoLocal<MenuDeComida>(
                Path.Combine(ObtenerDirectorio(directorio),"menues.xml"),
                "menu",
                semilla,
                elemento => new MenuDeComida(
                    LeyendoEntero(elemento, "id"),
                    (string)elemento.Attribute("nombre"),
                    LeyendoDecimal(elemento, "costoAdulto"),
                    LeyendoDecimal(elemento, "costoNino")),
                menu => new XElement(
                    "menu",
                    new XAttribute("id", menu.Id),
                    new XAttribute("nombre", menu.Nombre),
                    new XAttribute("costoAdulto", Numero(menu.CostoPorAdulto)),
                    new XAttribute("costoNino", Numero(menu.CostoPorNiño))),
                menu => menu.Id,
                (menu, id) => menu.Id = id);
        }

        public static IRepository<ServiciosAdicionales> CrearRepositorioServicios(string directorio = null)
        {
            List<ServiciosAdicionales> semilla = new List<ServiciosAdicionales>();
            foreach (ServiciosAdicionales servicio in ServiciosAdicionalesRepository.Instance.ObtenerTodos())
            {
                semilla.Add(new ServiciosAdicionales(servicio.Id, servicio.Descripcion, servicio.CostoDelServicio));
            }

            return new RepositorioCatalogoLocal<ServiciosAdicionales>(
                Path.Combine(ObtenerDirectorio(directorio),"servicios.xml"),
                "servicio",
                semilla,
                elemento => new ServiciosAdicionales(
                    LeyendoEntero(elemento, "id"),
                    (string)elemento.Attribute("descripcion"),
                    LeyendoDecimal(elemento, "montoFijo")),
                servicio => new XElement(
                    "servicio",
                    new XAttribute("id", servicio.Id),
                    new XAttribute("descripcion", servicio.Descripcion),
                    new XAttribute("montoFijo", Numero(servicio.CostoDelServicio))),
                servicio => servicio.Id,
                (servicio, id) => servicio.Id = id);
        }

        private static int LeyendoEntero(XElement elemento, string nombre)
        {
            string valor = (string)elemento.Attribute(nombre);
            int resultado;
            if (int.TryParse(valor, NumberStyles.Integer, CultureInfo.InvariantCulture, out resultado))
            {
                return resultado;
            }

            return 0;
        }

        private static decimal LeyendoDecimal(XElement elemento, string nombre)
        {
            string valor = (string)elemento.Attribute(nombre);
            decimal resultado;
            if (decimal.TryParse(valor, NumberStyles.Number, CultureInfo.InvariantCulture, out resultado))
            {
                return resultado;
            }

            return 0m;
        }

        private static string Numero(decimal valor)
        {
            return valor.ToString(CultureInfo.InvariantCulture);
        }
    }
}
