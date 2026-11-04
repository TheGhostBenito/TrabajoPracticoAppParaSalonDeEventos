using System.Collections.Generic;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Contrato del repositorio de menús de comida.
    public interface IMenuRepository
    {
        List<MenuDeComida> ObtenerTodos();
        MenuDeComida ObtenerPorId(int id);
        void Crear(MenuDeComida menu);
        bool Modificar(MenuDeComida menu);
        bool Eliminar(int id);
    }
}
