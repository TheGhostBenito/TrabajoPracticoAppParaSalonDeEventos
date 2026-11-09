using System.Collections.Generic;

namespace proyecto_integrador.Service
{
    public interface IService<TDto, TRequest>
    {
        // Devuelve todos los elementos como DTO
        List<TDto> ObtenerTodos();

        // Valida y crea un elemento nuevo
        TDto Crear(TRequest solicitud);

        // Valida y modifica el elemento con ese id
        TDto Modificar(int id, TRequest solicitud);

        // Elimina un elemento por su id
        bool Eliminar(int id);
    }
}

// Interfaz genérica del services, tienen dos tipos;
//   TDto     = el DTO que el service le devuelve a la pantalla
//   TRequest = el DTO que la pantalla le manda para crear o modificar
// En salones menús y servicios los dos son el mismo DTO, En presupuestos son distintos