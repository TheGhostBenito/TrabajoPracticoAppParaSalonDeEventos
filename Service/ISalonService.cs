using System.Collections.Generic;
using proyecto_integrador.dto;

namespace proyecto_integrador.Service
{
    // Contrato del service de salones: el Controller depende de esta interfaz.
    public interface ISalonService
    {
        List<SalonDto> ObtenerTodos();
        void Crear(SalonDto salon);
        bool Modificar(SalonDto salon);
        bool Eliminar(int id);
    }
}
