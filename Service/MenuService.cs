// Service encargado de aplicar las reglas de negocio
// relacionadas con los menús.
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;
using proyecto_integrador.Service;
using System;
using System.Collections.Generic;

public class MenuService : IService<MenuDto, MenuDto>
{
    // Repositorio utilizado para acceder a los datos de los menús
    private readonly IRepository<MenuDeComida> _repo;

    // Recibe el repositorio mediante inyección de dependencias
    public MenuService(IRepository<MenuDeComida> repo)
    {
        // Verifica que el repositorio exista
        if (repo == null)
        {
            throw new ArgumentNullException("repo");
        }

        _repo = repo;
    }

    // Obtiene todos los menús y los convierte a DTO
    public List<MenuDto> ObtenerTodos()
    {
        List<MenuDto> resultado = new List<MenuDto>();

        foreach (MenuDeComida menu in _repo.ObtenerTodos())
        {
            resultado.Add(ConvertirADto(menu));
        }

        return resultado;
    }

    // Valida los datos y crea un nuevo menú
    public MenuDto Crear(MenuDto menu)
    {
        Validar(menu);

        // Convierte el Ddto en una entidad del modelo
        MenuDeComida nuevo = new MenuDeComida(
            0,
            menu.Nombre.Trim(),
            menu.CostoPorAdulto,
            menu.CostoPorNiño);

        _repo.Crear(nuevo);

        // Devuelve el elemento creado, ya convertido a DTO
        return ConvertirADto(nuevo);
    }

    // Valida los datos y modifica un menú existente
    public MenuDto Modificar(int id, MenuDto menu)
    {
        Validar(menu);

        // Convierte el DTO en una entidad actualizada
        MenuDeComida modificado = new MenuDeComida(
            id,
            menu.Nombre.Trim(),
            menu.CostoPorAdulto,
            menu.CostoPorNiño);

        // Si no se encuentra, se avisa con un error (la pantalla muestra el mensaje)
        if (!_repo.Modificar(modificado))
        {
            throw new ArgumentException("No se encontró el menú seleccionado.");
        }

        // Devuelve el elemento modificado, ya convertido a DTO
        return ConvertirADto(modificado);
    }

    // Solicita al repositorio eliminar un menú
    public bool Eliminar(int id)
    {
        return _repo.Eliminar(id);
    }

    // Reglas de negocio para los menús
    private void Validar(MenuDto menu)
    {
        // Verifica que el DTO exista
        if (menu == null)
        {
            throw new ArgumentNullException("menu");
        }

        // El nombre es obligatorio
        if (string.IsNullOrWhiteSpace(menu.Nombre))
        {
            throw new ArgumentException("Ingresá el nombre del menú.");
        }

        // El costo por adulto no puede ser negativo
        if (menu.CostoPorAdulto < 0)
        {
            throw new ArgumentException("El costo por adulto tiene que ser mayor o igual a 0.");
        }

        // El costo por niño no puede ser negativo
        if (menu.CostoPorNiño < 0)
        {
            throw new ArgumentException("El costo por niño tiene que ser mayor o igual a 0.");
        }
    }

    // Convierte una entidad MenuDeComida en un DTO
    private MenuDto ConvertirADto(MenuDeComida menu)
    {
        MenuDto dto = new MenuDto();

        dto.Id = menu.Id;
        dto.Nombre = menu.Nombre;
        dto.CostoPorAdulto = menu.CostoPorAdulto;
        dto.CostoPorNiño = menu.CostoPorNiño;

        return dto;
    }
}