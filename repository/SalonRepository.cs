using System;
using System.Collections.Generic;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Repositorio de salones
    // Implementa Singleton para mantener una única lista en memoria.
    public sealed class SalonRepository : IRepository<Salon>
    {
        // Única instancia del repositorio (Singleton)
        private static readonly SalonRepository instancia = new SalonRepository();

        // Lista donde se almacenan los salones
        private readonly List<Salon> _salones;

        // Contador utilizado para generar los ids automático
        private int _proximoId;

        // constructor privado para evitar crear nuevas instancias
        private SalonRepository()
        {
            _salones = new List<Salon>();

            // Datos iniciales del sistema
            _salones.Add(new Salon(1, "Salon A", "Sin definir", 100.00m));
            _salones.Add(new Salon(2, "Salon B", "Sin definir", 150.00m));
            _salones.Add(new Salon(3, "Salon C", "Sin definir", 200.00m));

            _proximoId = 4;
        }

        // Permite acceder a la única instancia del repositorio
        public static SalonRepository Instance
        {
            get { return instancia; }
        }

        // Devuelve una copia de la lista de salones
        public List<Salon> ObtenerTodos()
        {
            return new List<Salon>(_salones);
        }

        // Busca un salón por su id
        public Salon ObtenerPorId(int id)
        {
            foreach (Salon salon in _salones)
            {
                if (salon.Id == id)
                {
                    return salon;
                }
            }

            // No se encontró el salón
            return null;
        }

        // se agrega un nuevo salón a la colección
        public void Crear(Salon salon)
        {
            // Valida que el objeto recibido exista
            if (salon == null)
            {
                throw new ArgumentNullException("salon");
            }

            // Asigna un id único al nuevo salón
            salon.Id = _proximoId;

            _proximoId++;

            // Guarda el salón en la lista
            _salones.Add(salon);
        }

        // modifica los datos de un salón existente
        public bool Modificar(Salon salon)
        {
            // Busca el salón que se quiere modificar
            Salon existente = ObtenerPorId(salon.Id);

            if (existente == null)
            {
                return false;
            }

            // Actualiza los datos
            existente.Nombre = salon.Nombre;
            existente.Ubicacion = salon.Ubicacion;
            existente.CostoBase = salon.CostoBase;

            return true;
        }

        // Elimina un salón segun la id
        public bool Eliminar(int id)
        {
            // B+usca el salón en la lista
            Salon existente = ObtenerPorId(id);

            if (existente == null)
            {
                return false;
            }

            // Lo elimina de la colección
            _salones.Remove(existente);

            return true;
        }
    }
}