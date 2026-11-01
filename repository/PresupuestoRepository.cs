using System;
using System.Collections.Generic;
using System.Linq;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    public sealed class PresupuestoRepository
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

        public List<Presupuesto> ObtenerTodos()
        {
            return _presupuestos.OrderByDescending(presupuesto => presupuesto.FechaCreacion).ToList();
        }

        public Presupuesto ObtenerPorId(int id)
        {
            return _presupuestos.FirstOrDefault(presupuesto => presupuesto.Id == id);
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

            int indice = _presupuestos.FindIndex(actual => actual.Id == presupuesto.Id);
            if (indice < 0)
            {
                return false;
            }

            _presupuestos[indice] = presupuesto;
            return true;
        }
    }
}
