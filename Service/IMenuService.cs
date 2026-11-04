using System.Collections.Generic;
using proyecto_integrador.dto;

namespace proyecto_integrador.Service
{
    // Contrato del service de menús de comida.
    public interface IMenuService
    {
        List<MenuDto> ObtenerTodos();
        void Crear(MenuDto menu);
        bool Modificar(MenuDto menu);
        bool Eliminar(int id);
    }
}
