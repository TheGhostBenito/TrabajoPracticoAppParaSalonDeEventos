using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_integrador.models; // Llama y accede a la clase Salon. Que se encuentra dentro de model

namespace proyecto_integrador.repository
{
    public static class SalonRepository
    {
        public static List<Salon> ObtenerSalones() // Método que devuelve una lista de salones con .ObtenerSalones()
        {
            List<Salon> salones = new List<Salon>
            {
                new Salon(1, "Salon A", 100.00m),
                new Salon(2, "Salon B", 150.00m),
                new Salon(3, "Salon C", 200.00m)
            };
            return salones;
        }
    }
}
