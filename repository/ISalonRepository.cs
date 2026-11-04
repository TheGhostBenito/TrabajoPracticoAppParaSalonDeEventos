using System.Collections.Generic;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Contrato del repositorio de salones: el Service depende de esta interfaz, no de la clase concreta.
    public interface ISalonRepository
    {
        List<Salon> ObtenerTodos();
        Salon ObtenerPorId(int id);
        void Crear(Salon salon);
        bool Modificar(Salon salon);
        bool Eliminar(int id);
    }
}
