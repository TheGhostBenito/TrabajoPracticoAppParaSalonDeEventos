using System;
using MySql.Data.MySqlClient;

namespace proyecto_integrador.repository
{
    // Se encarga de crear las conexiones a la base de datos MySQL (XAMPP).
    // Es un Singleton: hay una sola instancia y todos los repositories usan la misma.
    public sealed class ConexionBD
    {
        // Única instancia (Singleton)
        private static readonly ConexionBD instancia = new ConexionBD();

        // Datos para conectarse. XAMPP trae el usuario root sin contraseña
        private readonly string _cadenaConexion =
            "Server=localhost;Port=3306;Database=salon_eventos;Uid=root;Pwd=;CharSet=utf8mb4;UseAffectedRows=false;";

        // Constructor privado para que nadie cree otra instancia
        private ConexionBD()
        {
        }

        // Permite acceder a la única instancia
        public static ConexionBD Instance
        {
            get { return instancia; }
        }

        // Devuelve una conexión nueva (todavía cerrada). Quien la pida la abre con Open()
        // y la cierra con un bloque using.
        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(_cadenaConexion);
        }
    }
}
