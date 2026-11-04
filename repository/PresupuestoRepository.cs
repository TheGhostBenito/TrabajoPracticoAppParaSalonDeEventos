// repositorio encargado de almacenar los presupuestos en memori
// implementa Singleton para que exista una única lista durante la ejecución
using proyecto_integrador.models;
using proyecto_integrador.repository;
using System;
using System.Collections.Generic;

public sealed class PresupuestoRepository : IPresupuestoRepository
{
    // unica instancia del repositorio
    private static readonly PresupuestoRepository instancia = new PresupuestoRepository();

    // lista donde se guardan los presupuestos
    private readonly List<Presupuesto> _presupuestos;

    // generae ids automaticos
    private int _proximoId;

    private PresupuestoRepository()
    {
        _presupuestos = new List<Presupuesto>();  // constructor privado para evitar nuevas instancias

        // Primer ID disponible
        _proximoId = 1;
    }

    // punto de acceso a la instancia única
    public static PresupuestoRepository Instance
    {
        get { return instancia; }
    }

    // Devuelve todos los presupuestos, mostrando primero los mas recientes
    public List<Presupuesto> ObtenerTodos()
    {
        List<Presupuesto> resultado = new List<Presupuesto>();

        // recorre la lista desde el final hacia el inicio
        for (int indice = _presupuestos.Count - 1; indice >= 0; indice--)
        {
            resultado.Add(_presupuestos[indice]);
        }

        return resultado;
    }

    // Busca un presupuesto según su id
    public Presupuesto ObtenerPorId(int id)
    {
        foreach (Presupuesto presupuesto in _presupuestos)
        {
            if (presupuesto.Id == id)
            {
                return presupuesto;
            }
        }

        return null;  //no se encontro
    }

    // Guarda un nuevo presupuesto
    public Presupuesto Agregar(Presupuesto presupuesto)
    {
        if (presupuesto == null)
        {
            throw new ArgumentNullException("presupuesto");
        }

        // Asigna un id único
        presupuesto.Id = _proximoId;

        // incrementa el contador para el poóximo presupuesto
        _proximoId++;

        // Agrega el presupuesto a la colecció{
        _presupuestos.Add(presupuesto);

        return presupuesto;
    }

    // Actualiza un presupuesto existente
    public bool Actualizar(Presupuesto presupuesto)
    {
        // verifica que el objeto recibido no sea nulo
        if (presupuesto == null)
        {
            throw new ArgumentNullException("presupuesto");
        }

        // busca el presupuesto dentro de la lista
        for (int indice = 0; indice < _presupuestos.Count; indice++)
        {
            if (_presupuestos[indice].Id == presupuesto.Id)
            {
                // reemplaza el presupuesto existente por el actualizado
                _presupuestos[indice] = presupuesto;
                return true;
            }
        }

        return false;
    }
}