// Repositorio de presupuestos: mantiene el Singleton en memoria (acierto original)
// y además persiste el historial en presupuestos.xml dentro de la carpeta local.
// La API pública no cambia: sigue siendo IRepository<Presupuesto>.
using proyecto_integrador.models;
using proyecto_integrador.repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public sealed class PresupuestoRepository : IRepository<Presupuesto>
{
    // Única instancia del repositorio.
    private static readonly PresupuestoRepository instancia = new PresupuestoRepository();

    private readonly List<Presupuesto> _presupuestos;
    private readonly string _rutaArchivo;

    // Próximo id disponible.
    private int _proximoId;

    private PresupuestoRepository()
        : this(CatalogosLocales.ObtenerRutaArchivo("presupuestos.xml"))
    {
    }

    // Constructor usado por el Singleton y, por reflexión, por las pruebas con
    // una ruta temporal (así no se tocan los datos reales del usuario).
    private PresupuestoRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
        _presupuestos = new List<Presupuesto>();
        _proximoId = 1;

        CargarDesdeArchivo();
    }

    // Punto de acceso a la instancia única.
    public static PresupuestoRepository Instance
    {
        get { return instancia; }
    }

    // Devuelve todos los presupuestos, mostrando primero los más recientes.
    public List<Presupuesto> ObtenerTodos()
    {
        List<Presupuesto> resultado = new List<Presupuesto>();

        for (int indice = _presupuestos.Count - 1; indice >= 0; indice--)
        {
            resultado.Add(_presupuestos[indice]);
        }

        return resultado;
    }

    // Busca un presupuesto según su id.
    public Presupuesto ObtenerPorId(int id)
    {
        foreach (Presupuesto presupuesto in _presupuestos)
        {
            if (presupuesto.Id == id)
            {
                return presupuesto;
            }
        }

        return null;
    }

    // Guarda un nuevo presupuesto y actualiza el archivo.
    public void Crear(Presupuesto presupuesto)
    {
        if (presupuesto == null)
        {
            throw new ArgumentNullException("presupuesto");
        }

        presupuesto.Id = _proximoId;
        _proximoId++;

        _presupuestos.Add(presupuesto);
        GuardarEnArchivo();
    }

    // Actualiza un presupuesto existente y actualiza el archivo.
    public bool Modificar(Presupuesto presupuesto)
    {
        if (presupuesto == null)
        {
            throw new ArgumentNullException("presupuesto");
        }

        for (int indice = 0; indice < _presupuestos.Count; indice++)
        {
            if (_presupuestos[indice].Id == presupuesto.Id)
            {
                _presupuestos[indice] = presupuesto;
                GuardarEnArchivo();
                return true;
            }
        }

        return false;
    }

    // Elimina un presupuesto y actualiza el archivo.
    public bool Eliminar(int id)
    {
        Presupuesto existente = ObtenerPorId(id);

        if (existente == null)
        {
            return false;
        }

        _presupuestos.Remove(existente);
        GuardarEnArchivo();
        return true;
    }

    // ---------- Persistencia local ----------

    private void CargarDesdeArchivo()
    {
        if (!File.Exists(_rutaArchivo))
        {
            return;
        }

        try
        {
            XDocument documento = XDocument.Load(_rutaArchivo);
            if (documento.Root == null)
            {
                return;
            }

            foreach (XElement nodo in documento.Root.Elements("presupuesto"))
            {
                _presupuestos.Add(MapeadorPresupuestoXml.DesdeXml(nodo));
            }

            _proximoId = _presupuestos.Count == 0 ? 1 : _presupuestos.Max(presupuesto => presupuesto.Id) + 1;
        }
        catch (Exception)
        {
            RespaldarArchivoDanado();
            _presupuestos.Clear();
            _proximoId = 1;
        }
    }

    private void GuardarEnArchivo()
    {
        string directorio = Path.GetDirectoryName(_rutaArchivo);
        if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
        {
            Directory.CreateDirectory(directorio);
        }

        XElement raiz = new XElement("presupuestos", new XAttribute("version", "1"));
        foreach (Presupuesto presupuesto in _presupuestos)
        {
            raiz.Add(MapeadorPresupuestoXml.AXml(presupuesto));
        }

        // Escritura atómica: se completa el temporal y recién después se reemplaza.
        string temporal = _rutaArchivo + ".tmp";
        raiz.Save(temporal);

        if (File.Exists(_rutaArchivo))
        {
            File.Delete(_rutaArchivo);
        }

        File.Move(temporal, _rutaArchivo);
    }

    private void RespaldarArchivoDanado()
    {
        try
        {
            string copia = _rutaArchivo + ".error-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            File.Copy(_rutaArchivo, copia, true);
        }
        catch (Exception)
        {
            // Si no se puede hacer la copia, se continúa con el historial vacío.
        }
    }
}
