using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_integrador.models; // Llama y accede a la clase ServicioAdicional. Que se encuentra dentro de model


namespace proyecto_integrador.repository
{
    public class ServiciosAdicionalesRepository
    {
        private readonly List<ServiciosAdicionales> _servicios = new List<ServiciosAdicionales>();

        public List<ServiciosAdicionales> ObtenerTodos()
        {
            return _servicios.ToList();
        }

        public void Crear(ServiciosAdicionales servicio)
        {
            servicio.Id = _servicios.Any() ? _servicios.Max(s => s.Id) + 1 : 1;
            _servicios.Add(servicio);
        }

        public bool Eliminar(int id)
        {
            var servicio = _servicios.FirstOrDefault(s => s.Id == id);
            if (servicio != null)
            {
                _servicios.Remove(servicio);
                return true;
            }
            return false;
        }

        public bool Modificar(ServiciosAdicionales servicio)
        {
            var existente = _servicios.FirstOrDefault(s => s.Id == servicio.Id);
            if (existente != null)
            {
                existente.Descripcion = servicio.Descripcion;
                existente.CostoDelServicio = servicio.CostoDelServicio;
                return true;
            }
            return false;
        }
    }
}