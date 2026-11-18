using System;
using System.Configuration;

namespace proyecto_integrador.repository
{
    // Servicio singleton que guarda el modo de guardado elegido para los catálogos.
    // Los repositorios de catálogo le consultan en cada operación, así que el cambio
    // se aplica en caliente sin reconstruir controllers ni services.
    public sealed class ServicioModoDatos
    {
        private static readonly ServicioModoDatos _instancia = new ServicioModoDatos();

        public event EventHandler ModoCambiado;

        private ModoDatos _modoActual;

        private ServicioModoDatos()
        {
            _modoActual = LeerModoDeConfiguracion();
        }

        public static ServicioModoDatos Instance
        {
            get { return _instancia; }
        }

        public ModoDatos ModoActual
        {
            get { return _modoActual; }
        }

        // El archivo local siempre está disponible, así que el cambio no puede fallar.
        // Se devuelve bool para mantener la firma estable por si mañana hay más modos.
        public bool Cambiar(ModoDatos nuevoModo)
        {
            if (_modoActual == nuevoModo)
            {
                return true;
            }

            _modoActual = nuevoModo;

            EventHandler handler = ModoCambiado;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }

            return true;
        }

        private static ModoDatos LeerModoDeConfiguracion()
        {
            string configurado = ConfigurationManager.AppSettings["ModoCatalogos"];
            if (string.IsNullOrEmpty(configurado))
            {
                return ModoDatos.Memoria;
            }

            // Se aceptan valores viejos del app.config para no romper instalaciones
            // que todavía digan MySql: ahora "guardar" significa archivo local.
            if (configurado.Equals("Local", StringComparison.OrdinalIgnoreCase) ||
                configurado.Equals("ArchivoLocal", StringComparison.OrdinalIgnoreCase) ||
                configurado.Equals("MySql", StringComparison.OrdinalIgnoreCase) ||
                configurado.Equals("BaseDeDatos", StringComparison.OrdinalIgnoreCase))
            {
                return ModoDatos.ArchivoLocal;
            }

            return ModoDatos.Memoria;
        }
    }
}
