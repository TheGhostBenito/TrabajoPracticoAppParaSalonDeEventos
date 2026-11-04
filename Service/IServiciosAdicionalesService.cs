using System.Collections.Generic;
using proyecto_integrador.dto;

namespace proyecto_integrador.Service
{
    // Contrato del service de servicios adicionales.
    public interface IServiciosAdicionalesService
    {
        List<ServicioAdicionalDto> ObtenerTodos();
        void Crear(ServicioAdicionalDto servicio);
        bool Modificar(ServicioAdicionalDto servicio);
        bool Eliminar(int id);
    }
}
