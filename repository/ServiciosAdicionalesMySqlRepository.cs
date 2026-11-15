using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Repositorio de servicios adicionales que guarda los datos en MySQL (tabla servicio_adicional).
    // Hace lo mismo que ServiciosAdicionalesRepository (en memoria), por eso implementa la misma interfaz.
    public sealed class ServiciosAdicionalesMySqlRepository : IRepository<ServiciosAdicionales>
    {
        // Única instancia (Singleton)
        private static readonly ServiciosAdicionalesMySqlRepository instancia = new ServiciosAdicionalesMySqlRepository();

        private ServiciosAdicionalesMySqlRepository()
        {
        }

        public static ServiciosAdicionalesMySqlRepository Instance
        {
            get { return instancia; }
        }

        // Trae todos los servicios de la tabla
        public List<ServiciosAdicionales> ObtenerTodos()
        {
            List<ServiciosAdicionales> servicios = new List<ServiciosAdicionales>();

            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "SELECT id, descripcion, monto_fijo FROM servicio_adicional ORDER BY id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        servicios.Add(LeerServicio(lector));
                    }
                }
            }

            return servicios;
        }

        // Busca un servicio por su id (devuelve null si no existe)
        public ServiciosAdicionales ObtenerPorId(int id)
        {
            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "SELECT id, descripcion, monto_fijo FROM servicio_adicional WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return LeerServicio(lector);
                        }
                    }
                }
            }

            return null;
        }

        // Guarda un servicio nuevo. La base le asigna el id y se lo pasamos al objeto
        public void Crear(ServiciosAdicionales servicio)
        {
            if (servicio == null)
            {
                throw new ArgumentNullException("servicio");
            }

            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "INSERT INTO servicio_adicional (descripcion, monto_fijo) VALUES (@descripcion, @monto)";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@descripcion", servicio.Descripcion);
                    comando.Parameters.AddWithValue("@monto", servicio.CostoDelServicio);

                    comando.ExecuteNonQuery();

                    // id que generó la base para la fila nueva
                    servicio.Id = (int)comando.LastInsertedId;
                }
            }
        }

        // Modifica un servicio existente. Devuelve false si no encuentra ese id
        public bool Modificar(ServiciosAdicionales servicio)
        {
            if (servicio == null)
            {
                throw new ArgumentNullException("servicio");
            }

            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "UPDATE servicio_adicional SET descripcion = @descripcion, monto_fijo = @monto WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@descripcion", servicio.Descripcion);
                    comando.Parameters.AddWithValue("@monto", servicio.CostoDelServicio);
                    comando.Parameters.AddWithValue("@id", servicio.Id);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Elimina un servicio. Devuelve false si no encuentra ese id
        public bool Eliminar(int id)
        {
            using (MySqlConnection conexion = ConexionBD.Instance.CrearConexion())
            {
                conexion.Open();

                string sql = "DELETE FROM servicio_adicional WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Convierte la fila que está leyendo el lector en un objeto ServiciosAdicionales
        private ServiciosAdicionales LeerServicio(MySqlDataReader lector)
        {
            return new ServiciosAdicionales(
                Convert.ToInt32(lector["id"]),
                lector["descripcion"].ToString(),
                Convert.ToDecimal(lector["monto_fijo"]));
        }
    }
}
