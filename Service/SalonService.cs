using System;

using System.Collections.Generic;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.service
{
    public static class SalonService
    {
        public static List<Salon> ObtenerSalones()
        {
            return SalonRepository.ObtenerSalones();
        }

        public static Salon CrearSalon(string nombre, string ubicacion, decimal costoBase)
        {
            Validar(nombre, ubicacion, costoBase);
            return SalonRepository.AgregarSalon(nombre.Trim(), ubicacion.Trim(), costoBase);
        }

        public static void ModificarSalon(Salon salon, string nombre, string ubicacion, decimal costoBase)
        {
            if (salon == null)
            {
                throw new ArgumentNullException("salon", "No hay un salón seleccionado.");
            }

            Validar(nombre, ubicacion, costoBase);
            SalonRepository.ModificarSalon(salon, nombre.Trim(), ubicacion.Trim(), costoBase);
        }

        public static void EliminarSalon(Salon salon)
        {
            if (salon == null)
            {
                throw new ArgumentNullException("salon", "No hay un salón seleccionado.");
            }

            SalonRepository.EliminarSalon(salon);
        }

        // Reglas de negocio: si algo no se cumple, se lanza una excepción con el mensaje para el usuario
        private static void Validar(string nombre, string ubicacion, decimal costoBase)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("Ingresá el nombre del salón.");
            }

            if (string.IsNullOrWhiteSpace(ubicacion))
            {
                throw new ArgumentException("Ingresá la ubicación del salón.");
            }

            if (costoBase < 0)
            {
                throw new ArgumentException("El monto tiene que ser mayor o igual a 0.");
            }
        }
    }
}