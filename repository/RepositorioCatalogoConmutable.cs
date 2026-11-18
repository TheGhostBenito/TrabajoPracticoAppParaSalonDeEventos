using System;
using System.Collections.Generic;

namespace proyecto_integrador.repository
{
    // Repositorio proxy: no guarda datos propios, delega cada operación al
    // repositorio correspondiente según el modo elegido en la solapa izquierda.
    public class RepositorioCatalogoConmutable<T> : IRepository<T> where T : class
    {
        private readonly IRepository<T> _repositorioMemoria;
        private readonly IRepository<T> _repositorioLocal;
        private readonly ServicioModoDatos _servicioModo;

        public RepositorioCatalogoConmutable(
            IRepository<T> repositorioMemoria,
            IRepository<T> repositorioLocal,
            ServicioModoDatos servicioModo)
        {
            if (repositorioMemoria == null) throw new ArgumentNullException("repositorioMemoria");
            if (repositorioLocal == null) throw new ArgumentNullException("repositorioLocal");
            if (servicioModo == null) throw new ArgumentNullException("servicioModo");

            _repositorioMemoria = repositorioMemoria;
            _repositorioLocal = repositorioLocal;
            _servicioModo = servicioModo;
        }

        private IRepository<T> RepositorioActual
        {
            get
            {
                if (_servicioModo.ModoActual == ModoDatos.ArchivoLocal)
                {
                    return _repositorioLocal;
                }

                return _repositorioMemoria;
            }
        }

        public List<T> ObtenerTodos()
        {
            // Se copia la lista para que el llamador no quede atado al repositorio
            // que estaba activo al momento de la consulta.
            return new List<T>(RepositorioActual.ObtenerTodos());
        }

        public T ObtenerPorId(int id)
        {
            return RepositorioActual.ObtenerPorId(id);
        }

        public void Crear(T item)
        {
            RepositorioActual.Crear(item);
        }

        public bool Modificar(T item)
        {
            return RepositorioActual.Modificar(item);
        }

        public bool Eliminar(int id)
        {
            return RepositorioActual.Eliminar(id);
        }
    }
}
