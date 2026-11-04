using System;
using System.Collections.Generic;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Singleton: mantiene la lista de presupuestos activa durante toda la ejecución del programa.
    public sealed class PresupuestoRepository : IPresupuestoRepository
    {
        private static readonly PresupuestoRepository instancia = new PresupuestoRepository();
        private readonly List<Presupuesto> _presupuestos;
        private int _proximoId;

        private PresupuestoRepository()
        {
            _presupuestos = new List<Presupuesto>();
            _proximoId = 1;
        }

        public static PresupuestoRepository Instance
        {
            get { return instancia; }
        }

        // Devuelve primero los más nuevos: se recorre la lista de atrás para adelante.
        public List<Presupuesto> ObtenerTodos()
        {
            List<Presupuesto> resultado = new List<Presupuesto>();
            for (int indice = _presupuestos.Count - 1; indice >= 0; indice--)
            {
                resultado.Add(_presupuestos[indice]);
            }

            return resultado;
        }

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

        public Presupuesto Agregar(Presupuesto presupuesto)
        {
            if (presupuesto == null)
            {
                throw new ArgumentNullException("presupuesto");
            }

            presupuesto.Id = _proximoId;
            _proximoId++;
            _presupuestos.Add(presupuesto);
            return presupuesto;
        }

        public bool Actualizar(Presupuesto presupuesto)
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
                    return true;
                }
            }

            return false;
        }
    }
}
