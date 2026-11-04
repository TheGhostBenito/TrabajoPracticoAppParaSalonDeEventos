using System;
using System.Collections.Generic;
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _repo;

        public SalonService(ISalonRepository repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("repo");
            }

            _repo = repo;
        }

        public List<SalonDto> ObtenerTodos()
        {
            List<SalonDto> resultado = new List<SalonDto>();
            foreach (Salon salon in _repo.ObtenerTodos())
            {
                resultado.Add(ConvertirADto(salon));
            }

            return resultado;
        }

        public void Crear(SalonDto salon)
        {
            Validar(salon);
            Salon nuevo = new Salon(0, salon.Nombre.Trim(), salon.Ubicacion.Trim(), salon.CostoBase);
            _repo.Crear(nuevo);
        }

        public bool Modificar(SalonDto salon)
        {
            Validar(salon);
            Salon modificado = new Salon(salon.Id, salon.Nombre.Trim(), salon.Ubicacion.Trim(), salon.CostoBase);
            return _repo.Modificar(modificado);
        }

        public bool Eliminar(int id)
        {
            return _repo.Eliminar(id);
        }

        // Reglas de negocio: si algo no se cumple, se lanza una excepción con el mensaje para el usuario
        private void Validar(SalonDto salon)
        {
            if (salon == null)
            {
                throw new ArgumentNullException("salon");
            }

            if (string.IsNullOrWhiteSpace(salon.Nombre))
            {
                throw new ArgumentException("Ingresá el nombre del salón.");
            }

            if (string.IsNullOrWhiteSpace(salon.Ubicacion))
            {
                throw new ArgumentException("Ingresá la ubicación del salón.");
            }

            if (salon.CostoBase < 0)
            {
                throw new ArgumentException("El monto tiene que ser mayor o igual a 0.");
            }
        }

        private SalonDto ConvertirADto(Salon salon)
        {
            SalonDto dto = new SalonDto();
            dto.Id = salon.Id;
            dto.Nombre = salon.Nombre;
            dto.Ubicacion = salon.Ubicacion;
            dto.CostoBase = salon.CostoBase;
            return dto;
        }
    }
}
