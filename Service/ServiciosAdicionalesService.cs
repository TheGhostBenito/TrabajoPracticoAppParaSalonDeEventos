using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    public class ServiciosAdicionalesService : IServiciosAdicionalesService
    {
        private readonly IServiciosAdicionalesRepository _repo;

        public ServiciosAdicionalesService(IServiciosAdicionalesRepository repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("repo");
            }

            _repo = repo;
        }

        public List<ServicioAdicionalDto> ObtenerTodos()
        {
            List<ServicioAdicionalDto> resultado = new List<ServicioAdicionalDto>();
            foreach (ServiciosAdicionales servicio in _repo.ObtenerTodos())
            {
                resultado.Add(ConvertirADto(servicio));
            }

            return resultado;
        }

        public void Crear(ServicioAdicionalDto servicio)
        {
            Validar(servicio);
            ServiciosAdicionales nuevo = new ServiciosAdicionales(0, servicio.Descripcion.Trim(), servicio.CostoDelServicio);
            _repo.Crear(nuevo);
        }

        public bool Modificar(ServicioAdicionalDto servicio)
        {
            Validar(servicio);
            ServiciosAdicionales modificado = new ServiciosAdicionales(servicio.Id, servicio.Descripcion.Trim(), servicio.CostoDelServicio);
            return _repo.Modificar(modificado);
        }

        public bool Eliminar(int id)
        {
            return _repo.Eliminar(id);
        }

        private void Validar(ServicioAdicionalDto servicio)
        {
            if (servicio == null)
            {
                throw new ArgumentNullException("servicio");
            }

            if (string.IsNullOrWhiteSpace(servicio.Descripcion))
            {
                throw new ArgumentException("Ingresá la descripción del servicio.");
            }

            if (servicio.CostoDelServicio < 0)
            {
                throw new ArgumentException("El monto tiene que ser mayor o igual a 0.");
            }
        }

        private ServicioAdicionalDto ConvertirADto(ServiciosAdicionales servicio)
        {
            ServicioAdicionalDto dto = new ServicioAdicionalDto();
            dto.Id = servicio.Id;
            dto.Descripcion = servicio.Descripcion;
            dto.CostoDelServicio = servicio.CostoDelServicio;
            return dto;
        }
    }
}
