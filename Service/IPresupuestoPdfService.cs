using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    // Contrato del servicio que genera el PDF de un presupuesto.
    public interface IPresupuestoPdfService
    {
        string Guardar(Presupuesto presupuesto, string ruta);
    }
}

// Genera el PDF del presupuesto y devuelve la ruta donde fue guardado