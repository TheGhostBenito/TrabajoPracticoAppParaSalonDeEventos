using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_integrador.models; // Llama y accede a la clase ServicioAdicional. Que se encuentra dentro de model


namespace proyecto_integrador.repository
{
    public static class ServiciosAdicionalesRepository
    {
        public static List<ServiciosAdicionales> ObtenerServiciosAdicionales() // Método que devuelve una lista de servicios adicionales con .ObtenerServiciosAdicionales()
        {
            List<ServiciosAdicionales> serviciosAdicionales = new List<ServiciosAdicionales>
            {
                new ServiciosAdicionales(1, "Servicio A", 20.00m),
                new ServiciosAdicionales(2, "Servicio B", 30.00m),
                new ServiciosAdicionales(3, "Servicio C", 40.00m)
            };
            return serviciosAdicionales;
        }
    }
}
