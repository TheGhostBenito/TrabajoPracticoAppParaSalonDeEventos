using System.Collections.Generic;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Contrato del repositorio de presupuestos.
    public interface IPresupuestoRepository
    {
        List<Presupuesto> ObtenerTodos();
        Presupuesto ObtenerPorId(int id);
        Presupuesto Agregar(Presupuesto presupuesto);
        bool Actualizar(Presupuesto presupuesto);
    }
}
