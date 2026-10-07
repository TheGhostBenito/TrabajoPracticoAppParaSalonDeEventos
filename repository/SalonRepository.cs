using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_integrador.models; // Llama y accede a la clase salon que se encuentra dentro de model

namespace proyecto_integrador.repository
{
    public static class SalonRepository
    {
        // Lista que vive mientras la app está abierta, salones y los que se agreguen
        private static int proximoId = 4;

        private static readonly List<Salon> salones = new List<Salon>
        {
            new Salon(1, "Salon A", "Sin definir", 100.00m),
            new Salon(2, "Salon B", "Sin definir", 150.00m),
            new Salon(3, "Salon C", "Sin definir", 200.00m)
        };

        public static List<Salon> ObtenerSalones() // el metod que devuelve una lista de salones con .obtenerSalones
        {
            return new List<Salon>(salones);
        }

        public static Salon AgregarSalon(string nombre, string ubicacion, decimal costoBase)
        {
            Salon nuevo = new Salon(proximoId, nombre, ubicacion, costoBase);
            proximoId++;
            salones.Add(nuevo);
            return nuevo;
        }

        public static void ModificarSalon(Salon salon, string nombre, string ubicacion, decimal costoBase)
        {
            salon.Nombre = nombre;
            salon.Ubicacion = ubicacion;
            salon.CostoBase = costoBase;
        }

        public static void EliminarSalon(Salon salon)
        {
            salones.Remove(salon);
        }
    }
}