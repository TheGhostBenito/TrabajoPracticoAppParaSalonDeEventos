using System.Collections.Generic;

namespace proyecto_integrador.repository
{
    // la interfaz genérica del repositorio.
   
    public interface IRepository<T>
    {
        // devuelve todos los elementos
        List<T> ObtenerTodos();

        // busca un elemento por su id (devuelve null si no existe)
        T ObtenerPorId(int id);

        // guarda un elemento nuevo
        void Crear(T item);

        // modifica un elemento existente (devuelve false si no lo encuentra)
        bool Modificar(T item);

        // elimina un elemento por su id (devuelve false si no lo encuentra)
        bool Eliminar(int id);
    }
}
