using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Repositorio de salones que guarda los datos en la base de datos MySQL (tabla salon).
    // Hace lo mismo que SalonRepository (en memoria), por eso implementa la misma interfaz IRepository<Salon>.
    public sealed class SalonMySqlRepository : IRepository<Salon>
    {
        // Única instancia (Singleton)
        private static readonly SalonMySqlRepository instancia = new SalonMySqlRepository();

        private SalonMySqlRepository()
        {
        }

        public static SalonMySqlRepository Instance
        {
            get { return instancia; }
        }

        // Trae todos los salones de la tabla
        public List<Salon> ObtenerTodos()
        {
            List<Salon> salones = new List<Salon>();

            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "SELECT id, nombre, ubicacion, costo_base FROM salon ORDER BY id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        salones.Add(LeerSalon(lector));
                    }
                }
            }

            return salones;
        }

        // Busca un salón por su id (devuelve null si no existe)
        public Salon ObtenerPorId(int id)
        {
            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "SELECT id, nombre, ubicacion, costo_base FROM salon WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return LeerSalon(lector);
                        }
                    }
                }
            }

            return null;
        }

        // Guarda un salón nuevo. La base le asigna el id y se lo pasamos al objeto
        public void Crear(Salon salon)
        {
            if (salon == null)
            {
                throw new ArgumentNullException("salon");
            }

            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "INSERT INTO salon (nombre, ubicacion, costo_base) VALUES (@nombre, @ubicacion, @costo)";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", salon.Nombre);
                    comando.Parameters.AddWithValue("@ubicacion", salon.Ubicacion);
                    comando.Parameters.AddWithValue("@costo", salon.CostoBase);

                    comando.ExecuteNonQuery();

                    // id que generó la base para la fila nueva
                    salon.Id = (int)comando.LastInsertedId;
                }
            }
        }

        // Modifica un salón existente. Devuelve false si no encuentra ese id
        public bool Modificar(Salon salon)
        {
            if (salon == null)
            {
                throw new ArgumentNullException("salon");
            }

            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "UPDATE salon SET nombre = @nombre, ubicacion = @ubicacion, costo_base = @costo WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", salon.Nombre);
                    comando.Parameters.AddWithValue("@ubicacion", salon.Ubicacion);
                    comando.Parameters.AddWithValue("@costo", salon.CostoBase);
                    comando.Parameters.AddWithValue("@id", salon.Id);

                    // ExecuteNonQuery devuelve cuántas filas se modificaron
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Elimina un salón. Devuelve false si no encuentra ese id
        public bool Eliminar(int id)
        {
            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "DELETE FROM salon WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Convierte la fila que está leyendo el lector en un objeto Salon
        private Salon LeerSalon(MySqlDataReader lector)
        {
            return new Salon(
                Convert.ToInt32(lector["id"]),
                lector["nombre"].ToString(),
                lector["ubicacion"].ToString(),
                Convert.ToDecimal(lector["costo_base"]));
        }
    }
}
