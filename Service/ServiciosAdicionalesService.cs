using proyecto_integrador.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    public class ServiciosAdicionalesService
    {
        private readonly ServiciosAdicionalesRepository _repo;

        public ServiciosAdicionalesService(ServiciosAdicionalesRepository repo)
        {
            _repo = repo;
        }

        public List<ServiciosAdicionales> ObtenerTodos() => _repo.ObtenerTodos();

        public void Crear(string descripcion, decimal monto)
        {
            var nuevo = new ServiciosAdicionales(0, descripcion, monto);
            _repo.Crear(nuevo);
        }

        public bool Eliminar(int id) => _repo.Eliminar(id);

        public bool Modificar(int id, string descripcion, decimal monto)
        {
            var servicio = new ServiciosAdicionales(id, descripcion, monto);
            return _repo.Modificar(servicio);
        }
    }
}