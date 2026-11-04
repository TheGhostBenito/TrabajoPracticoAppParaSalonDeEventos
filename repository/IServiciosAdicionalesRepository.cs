using System.Collections.Generic;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Define las operaciones que debe implementar cualquier repositorio de servicios adicionales
    public interface IServiciosAdicionalesRepository
    {
        List<ServiciosAdicionales> ObtenerTodos();

        ServiciosAdicionales ObtenerPorId(int id);

        void Crear(ServiciosAdicionales servicio);

        bool Modificar(ServiciosAdicionales servicio);

        bool Eliminar(int id);
    }
}