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
        private static List<ServiciosAdicionales> _servicios = new List<ServiciosAdicionales>
        {
            new ServiciosAdicionales(1, "Servicio A", 20.00m),
            new ServiciosAdicionales(2, "Servicio B", 30.00m),
            new ServiciosAdicionales(3, "Servicio C", 40.00m)
        };

        public static List<ServiciosAdicionales> ObtenerServiciosAdicionales()
        {
            return _servicios.ToList();
        }

        public static void CrearServicio(ServiciosAdicionales servicio)
        {
            servicio.Id = _servicios.Any() ? _servicios.Max(s => s.Id) + 1 : 1;
            _servicios.Add(servicio);
        }

        public static bool EliminarServicio(int id)
        {
            var servicio = _servicios.FirstOrDefault(s => s.Id == id);
            if (servicio != null)
            {
                _servicios.Remove(servicio);
                return true;
            }
            return false;
        }

        public static bool ModificarServicio(ServiciosAdicionales servicio)
        {
            var existente = _servicios.FirstOrDefault(s => s.Id == servicio.Id);
            if (existente != null)
            {
                existente.Descripcion = servicio.Descripcion;
                existente.Monto = servicio.Monto;
                return true;
            }
            return false;
        }
    }
}