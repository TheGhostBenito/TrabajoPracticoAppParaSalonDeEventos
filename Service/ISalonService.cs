using proyecto_integrador.dto;

namespace proyecto_integrador.Service
{
    // Contrato del service de salones: el Controller depende de esta interfaz.
    // Hereda el contrato genérico para no duplicar operaciones.
    public interface ISalonService : IService<SalonDto, SalonDto>
    {
    }
}
