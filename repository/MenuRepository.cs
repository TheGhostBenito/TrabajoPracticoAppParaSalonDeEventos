// repositorio encargado de almacenar los menús en memoria.
// rmplementa Singleton para mantener una única lista durante la ejecución.
using proyecto_integrador.models;
using proyecto_integrador.repository;
using System;
using System.Collections.Generic;

public sealed class MenuRepository : IMenuRepository
{
    // unica instancia del repositorio
    private static readonly MenuRepository instancia = new MenuRepository();

    // lista donde se guardan los menús
    private readonly List<MenuDeComida> _menus;

    private int _proximoId;

    // constructor privado para evitar crear nuevas instancias
    private MenuRepository()
    {
        // los menus de ejemplo cargados al iniciar la aplicación
        _menus = new List<MenuDeComida>();
        _menus.Add(new MenuDeComida(1, "Menu A", 50.00m, 30.00m));
        _menus.Add(new MenuDeComida(2, "Menu B", 75.00m, 45.00m));
        _menus.Add(new MenuDeComida(3, "Menu C", 100.00m, 60.00m));

        // Próximo ID disponible
        _proximoId = 4;
    }

    // permite acceder a la única instancia del repositorio
    public static MenuRepository Instance
    {
        get { return instancia; }
    }

    // devuelve una copia de la lista de menus
    public List<MenuDeComida> ObtenerTodos()
    {
        return new List<MenuDeComida>(_menus);
    }

    // busca un menú según su id
    public MenuDeComida ObtenerPorId(int id)
    {
        foreach (MenuDeComida menu in _menus)
        {
            if (menu.Id == id)
            {
                return menu;
            }
        }

        return null;
    }

    // agrega  nuevo menú a la colección
    public void Crear(MenuDeComida menu)
    {
        if (menu == null)
        {
            throw new ArgumentNullException("menu");
        }

        // asigna un id nico al menu
        menu.Id = _proximoId;

        _proximoId++;

        // guarda el menú en la lista
        _menus.Add(menu);
    }

    // Modifica los datos de un menú existente
    public bool Modificar(MenuDeComida menu)
    {
        // Busca el menú por su ID
        MenuDeComida existente = ObtenerPorId(menu.Id);

        if (existente == null)
        {
            return false;
        }