using System;
using System.Collections.Generic;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Singleton: una única instancia que guarda la lista de servicios adicionales mientras la app está abierta.
    public sealed class ServiciosAdicionalesRepository : IServiciosAdicionalesRepository
    {
        private static readonly ServiciosAdicionalesRepository instancia = new ServiciosAdicionalesRepository();
        private readonly List<ServiciosAdicionales> _servicios;
        private int _proximoId;

        private ServiciosAdicionalesRepository()
        {
            _servicios = new List<ServiciosAdicionales>();
            _proximoId = 1;
        }

        public static ServiciosAdicionalesRepository Instance
        {
            get { return instancia; }
        }

        public List<ServiciosAdicionales> ObtenerTodos()
        {
            return new List<ServiciosAdicionales>(_servicios);
        }

        public ServiciosAdicionales ObtenerPorId(int id)
        {
            foreach (ServiciosAdicionales servicio in _servicios)
            {
                if (servicio.Id == id)
                {
                    return servicio;
                }
            }

            return null;
        }

        public void Crear(ServiciosAdicionales servicio)
        {
            if (servicio == null)
            {
                throw new ArgumentNullException("servicio");
            }

            servicio.Id = _proximoId;
            _proximoId++;
            _servicios.Add(servicio);
        }

        public bool Modificar(ServiciosAdicionales servicio)
        {
            ServiciosAdicionales existente = ObtenerPorId(servicio.Id);
            if (existente == null)
            {
                return false;
            }

            existente.Descripcion = servicio.Descripcion;
            existente.CostoDelServicio = servicio.CostoDelServicio;
            return true;
        }

        public bool Eliminar(int id)
        {
            ServiciosAdicionales existente = ObtenerPorId(id);
            if (existente == null)
            {
                return false;
            }

            _servicios.Remove(existente);
            return true;
        }
    }
}
