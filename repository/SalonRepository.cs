using System;
using System.Collections.Generic;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Singleton: una única instancia que guarda la lista de salones mientras la app está abierta.
    public sealed class SalonRepository : ISalonRepository
    {
        private static readonly SalonRepository instancia = new SalonRepository();
        private readonly List<Salon> _salones;
        private int _proximoId;

        private SalonRepository()
        {
            _salones = new List<Salon>();
            _salones.Add(new Salon(1, "Salon A", "Sin definir", 100.00m));
            _salones.Add(new Salon(2, "Salon B", "Sin definir", 150.00m));
            _salones.Add(new Salon(3, "Salon C", "Sin definir", 200.00m));
            _proximoId = 4;
        }

        public static SalonRepository Instance
        {
            get { return instancia; }
        }

        public List<Salon> ObtenerTodos()
        {
            return new List<Salon>(_salones);
        }

        public Salon ObtenerPorId(int id)
        {
            foreach (Salon salon in _salones)
            {
                if (salon.Id == id)
                {
                    return salon;
                }
            }

            return null;
        }

        public void Crear(Salon salon)
        {
            if (salon == null)
            {
                throw new ArgumentNullException("salon");
            }

            salon.Id = _proximoId;
            _proximoId++;
            _salones.Add(salon);
        }

        public bool Modificar(Salon salon)
        {
            Salon existente = ObtenerPorId(salon.Id);
            if (existente == null)
            {
                return false;
            }

            existente.Nombre = salon.Nombre;
            existente.Ubicacion = salon.Ubicacion;
            existente.CostoBase = salon.CostoBase;
            return true;
        }

        public bool Eliminar(int id)
        {
            Salon existente = ObtenerPorId(id);
            if (existente == null)
            {
                return false;
            }

            _salones.Remove(existente);
            return true;
        }
    }
}
