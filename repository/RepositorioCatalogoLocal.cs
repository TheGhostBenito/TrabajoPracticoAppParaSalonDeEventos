using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace proyecto_integrador.repository
{
    // Guarda un catálogo en un archivo XML ubicado en la carpeta de datos local
    // del usuario. No necesita servidor ni paquetes externos: usa System.Xml.Linq.
    // Se inicializa recién cuando se usa por primera vez, por eso el archivo no
    // se crea si la aplicación queda siempre en modo memoria.
    public class RepositorioCatalogoLocal<T> : IRepository<T> where T : class
    {
        private readonly object _cerrojo = new object();
        private readonly string _rutaArchivo;
        private readonly string _nombreElemento;
        private readonly Func<XElement, T> _leer;
        private readonly Func<T, XElement> _escribir;
        private readonly Func<T, int> _obtenerId;
        private readonly Action<T, int> _asignarId;
        private readonly List<T> _semilla;

        private List<T> _items;
        private bool _cargado;

        public RepositorioCatalogoLocal(
            string rutaArchivo,
            string nombreElemento,
            IEnumerable<T> semilla,
            Func<XElement, T> leer,
            Func<T, XElement> escribir,
            Func<T, int> obtenerId,
            Action<T, int> asignarId)
        {
            if (string.IsNullOrEmpty(rutaArchivo)) throw new ArgumentNullException("rutaArchivo");
            if (string.IsNullOrEmpty(nombreElemento)) throw new ArgumentNullException("nombreElemento");
            if (semilla == null) throw new ArgumentNullException("semilla");
            if (leer == null) throw new ArgumentNullException("leer");
            if (escribir == null) throw new ArgumentNullException("escribir");
            if (obtenerId == null) throw new ArgumentNullException("obtenerId");
            if (asignarId == null) throw new ArgumentNullException("asignarId");

            _rutaArchivo = rutaArchivo;
            _nombreElemento = nombreElemento;
            _semilla = new List<T>(semilla);
            _leer = leer;
            _escribir = escribir;
            _obtenerId = obtenerId;
            _asignarId = asignarId;
        }

        public string RutaArchivo
        {
            get { return _rutaArchivo; }
        }

        public List<T> ObtenerTodos()
        {
            AsegurarCargado();

            lock (_cerrojo)
            {
                return new List<T>(_items);
            }
        }

        public T ObtenerPorId(int id)
        {
            AsegurarCargado();

            lock (_cerrojo)
            {
                return _items.FirstOrDefault(item => _obtenerId(item) == id);
            }
        }

        public void Crear(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            AsegurarCargado();

            lock (_cerrojo)
            {
                int proximoId = _items.Count == 0 ? 1 : _items.Max(_obtenerId) + 1;
                _asignarId(item, proximoId);
                _items.Add(item);
                Guardar();
            }
        }

        public bool Modificar(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            AsegurarCargado();

            lock (_cerrojo)
            {
                int id = _obtenerId(item);
                for (int indice = 0; indice < _items.Count; indice++)
                {
                    if (_obtenerId(_items[indice]) == id)
                    {
                        _items[indice] = item;
                        Guardar();
                        return true;
                    }
                }

                return false;
            }
        }

        public bool Eliminar(int id)
        {
            AsegurarCargado();

            lock (_cerrojo)
            {
                T item = _items.FirstOrDefault(actual => _obtenerId(actual) == id);
                if (item == null)
                {
                    return false;
                }

                _items.Remove(item);
                Guardar();
                return true;
            }
        }

        private void AsegurarCargado()
        {
            if (_cargado)
            {
                return;
            }

            lock (_cerrojo)
            {
                if (_cargado)
                {
                    return;
                }

                _items = LeerArchivo();
                if (_items == null)
                {
                    // Primera vez (o archivo dañado): se arranca con los datos de ejemplo.
                    _items = new List<T>(_semilla);
                    Guardar();
                }

                _cargado = true;
            }
        }

        private List<T> LeerArchivo()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return null;
            }

            try
            {
                XDocument documento = XDocument.Load(_rutaArchivo);
                if (documento.Root == null)
                {
                    return null;
                }

                List<T> resultado = new List<T>();
                foreach (XElement elemento in documento.Root.Elements(_nombreElemento))
                {
                    resultado.Add(_leer(elemento));
                }

                return resultado;
            }
            catch (Exception)
            {
                // Si el archivo se corrompió, se guarda una copia con fecha y se
                // continúa con los datos iniciales en vez de trabar la aplicación.
                try
                {
                    string copia = _rutaArchivo + ".error-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    File.Copy(_rutaArchivo, copia, true);
                }
                catch (Exception)
                {
                    // No se pudo hacer la copia; igual se sigue con datos iniciales.
                }

                return null;
            }
        }

        private void Guardar()
        {
            string directorio = Path.GetDirectoryName(_rutaArchivo);
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            XElement raiz = new XElement("catalogo");
            foreach (T item in _items)
            {
                raiz.Add(_escribir(item));
            }

            // Escritura atómica: se escribe un temporal y recién después se
            // reemplaza el archivo, para no dejar un XML a medias si se corta la app.
            string temporal = _rutaArchivo + ".tmp";
            raiz.Save(temporal);

            if (File.Exists(_rutaArchivo))
            {
                File.Delete(_rutaArchivo);
            }

            File.Move(temporal, _rutaArchivo);
        }
    }
}
