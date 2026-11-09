// controller encargado de comunicar la vista con el Service de menús.
// o contiene reglas de negocio ni cálculos.
using proyecto_integrador.dto;
using proyecto_integrador.Service;
using System;
using System.Collections.Generic;

public sealed class MenuController
{
    // Service utilizado para gestionar los menús
    private readonly IService<MenuDto, MenuDto> _service;

    // Recibe el Service mediante inyeccion de dependencias
    public MenuController(IService<MenuDto, MenuDto> service)
    {
        // Verifica que el Service exista
        if (service == null)
        {
            throw new ArgumentNullException("service");
        }

        _service = service;
    }

    // Solicita al Service la lista de menús}
    public List<MenuDto> ObtenerTodos()
    {
        return _service.ObtenerTodos();
    }

    // Solicita al Service crear un nuevo menú
    public void Crear(MenuDto menu)
    {
        _service.Crear(menu);
    }

    // Solicita al Service modificar un menú existente
    public void Modificar(MenuDto menu)
    {
        _service.Modificar(menu.Id, menu);
    }

    // Solicita al Service eliminar un menú
    public bool Eliminar(int id)
    {
        return _service.Eliminar(id);
    }
}