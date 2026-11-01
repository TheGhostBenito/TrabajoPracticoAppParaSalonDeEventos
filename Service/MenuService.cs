using System;
using System.Collections.Generic;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    public class MenuService
    {
        private readonly MenuRepository _repo;

        public MenuService(MenuRepository repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("repo");
            }

            _repo = repo;
        }

        public List<MenuDeComida> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        public void Crear(string nombre, decimal costoPorAdulto, decimal costoPorNino)
        {
            Validar(nombre, costoPorAdulto, costoPorNino);
            MenuDeComida nuevo = new MenuDeComida(0, nombre.Trim(), costoPorAdulto, costoPorNino);
            _repo.Crear(nuevo);
        }

        public bool Modificar(int id, string nombre, decimal costoPorAdulto, decimal costoPorNino)
        {
            Validar(nombre, costoPorAdulto, costoPorNino);
            MenuDeComida menu = new MenuDeComida(id, nombre.Trim(), costoPorAdulto, costoPorNino);
            return _repo.Modificar(menu);
        }

        public bool Eliminar(int id)
        {
            return _repo.Eliminar(id);
        }

        private static void Validar(string nombre, decimal costoPorAdulto, decimal costoPorNino)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("Ingresá el nombre del menú.");
            }

            if (costoPorAdulto < 0)
            {
                throw new ArgumentException("El costo por adulto tiene que ser mayor o igual a 0.");
            }

            if (costoPorNino < 0)
            {
                throw new ArgumentException("El costo por niño tiene que ser mayor o igual a 0.");
            }
        }
    }
}