using System;
using System.Collections.Generic;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    public class ServiciosAdicionalesService
    {
        private readonly ServiciosAdicionalesRepository _repo;

        public ServiciosAdicionalesService(ServiciosAdicionalesRepository repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("repo");
            }

            _repo = repo;
        }

        public List<ServiciosAdicionales> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        public void Crear(string descripcion, decimal monto)
        {
            Validar(descripcion, monto);
            ServiciosAdicionales nuevo = new ServiciosAdicionales(0, descripcion.Trim(), monto);
            _repo.Crear(nuevo);
        }

        public bool Eliminar(int id)
        {
            return _repo.Eliminar(id);
        }

        public bool Modificar(int id, string descripcion, decimal monto)
        {
            Validar(descripcion, monto);
            ServiciosAdicionales servicio = new ServiciosAdicionales(id, descripcion.Trim(), monto);
            return _repo.Modificar(servicio);
        }

        private static void Validar(string descripcion, decimal monto)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("Ingresá la descripción del servicio.");
            }

            if (monto < 0)
            {
                throw new ArgumentException("El monto tiene que ser mayor o igual a 0.");
            }
        }
    }
}
