using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Repositorio de menús que guarda los datos en la base de datos MySQL (tabla menu_comida).
    // Hace lo mismo que MenuRepository (en memoria), por eso implementa la misma interfaz IRepository<MenuDeComida>.
    public sealed class MenuMySqlRepository : IRepository<MenuDeComida>
    {
        // Única instancia (Singleton)
        private static readonly MenuMySqlRepository instancia = new MenuMySqlRepository();

        private MenuMySqlRepository()
        {
        }

        public static MenuMySqlRepository Instance
        {
            get { return instancia; }
        }

        // Trae todos los menús de la tabla
        public List<MenuDeComida> ObtenerTodos()
        {
            List<MenuDeComida> menus = new List<MenuDeComida>();

            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "SELECT id, nombre, costo_por_adulto, costo_por_nino FROM menu_comida ORDER BY id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        menus.Add(LeerMenu(lector));
                    }
                }
            }

            return menus;
        }

        // Busca un menú por su id (devuelve null si no existe)
        public MenuDeComida ObtenerPorId(int id)
        {
            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "SELECT id, nombre, costo_por_adulto, costo_por_nino FROM menu_comida WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return LeerMenu(lector);
                        }
                    }
                }
            }

            return null;
        }

        // Guarda un menú nuevo. La base le asigna el id y se lo pasamos al objeto
        public void Crear(MenuDeComida menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException("menu");
            }

            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "INSERT INTO menu_comida (nombre, costo_por_adulto, costo_por_nino) VALUES (@nombre, @adulto, @nino)";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", menu.Nombre);
                    comando.Parameters.AddWithValue("@adulto", menu.CostoPorAdulto);
                    comando.Parameters.AddWithValue("@nino", menu.CostoPorNiño);

                    comando.ExecuteNonQuery();

                    // id que generó la base para la fila nueva
                    menu.Id = (int)comando.LastInsertedId;
                }
            }
        }

        // Modifica un menú existente. Devuelve false si no encuentra ese id
        public bool Modificar(MenuDeComida menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException("menu");
            }

            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "UPDATE menu_comida SET nombre = @nombre, costo_por_adulto = @adulto, costo_por_nino = @nino WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", menu.Nombre);
                    comando.Parameters.AddWithValue("@adulto", menu.CostoPorAdulto);
                    comando.Parameters.AddWithValue("@nino", menu.CostoPorNiño);
                    comando.Parameters.AddWithValue("@id", menu.Id);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Elimina un menú. Devuelve false si no encuentra ese id
        public bool Eliminar(int id)
        {
            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "DELETE FROM menu_comida WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Convierte la fila que está leyendo el lector en un objeto MenuDeComida
        private MenuDeComida LeerMenu(MySqlDataReader lector)
        {
            return new MenuDeComida(
                Convert.ToInt32(lector["id"]),
                lector["nombre"].ToString(),
                Convert.ToDecimal(lector["costo_por_adulto"]),
                Convert.ToDecimal(lector["costo_por_nino"]));
        }
    }
}
