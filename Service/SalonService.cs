// Service encargado de aplicar las reglas de negocio relacionadas con los salones.
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;
using proyecto_integrador.Service;
using System;
using System.Collections.Generic;

public class SalonService : IService<SalonDto, SalonDto>
{
    // repositorio utilizado para acceder a los datos de los salones
    private readonly IRepository<Salon> _repo;

    // recibe el repositorio mediante inyección de dependencias
    public SalonService(IRepository<Salon> repo)
    {
        //     Verifica que el repositorio exista
        if (repo == null)
        {
            throw new ArgumentNullException("repo");
        }

        _repo = repo;
    }

    // Obtiene todos los salones y los convierte a dto
    public List<SalonDto> ObtenerTodos()
    {
        List<SalonDto> resultado = new List<SalonDto>();

        foreach (Salon salon in _repo.ObtenerTodos())
        {
            resultado.Add(ConvertirADto(salon));
        }

        return resultado;
    }

    // Valida los datos y crea un nuevo salón
    public SalonDto Crear(SalonDto salon)
    {
        Validar(salon);

        // Convierte el dto en una entidad del modelo
        Salon nuevo = new Salon(
            0,
            salon.Nombre.Trim(),
            salon.Ubicacion.Trim(),
            salon.CostoBase);

        _repo.Crear(nuevo);

        // Devuelve el elemento creado, ya convertido a DTO
        return ConvertirADto(nuevo);
    }

    // valida los datos y modifica un salón existente
    public SalonDto Modificar(int id, SalonDto salon)
    {
        Validar(salon);

        Salon modificado = new Salon(
            id,
            salon.Nombre.Trim(),
            salon.Ubicacion.Trim(),
            salon.CostoBase);

        // Si no se encuentra, se avisa con un error (la pantalla muestra el mensaje)
        if (!_repo.Modificar(modificado))
        {
            throw new ArgumentException("No se encontró el salón seleccionado.");
        }

        // Devuelve el elemento modificado, ya convertido a DTO
        return ConvertirADto(modificado);
    }

    // solicita al repositorio eliminar un salón
    public bool Eliminar(int id)
    {
        return _repo.Eliminar(id);
    }

    // Reglas de negocio para los salones
    private void Validar(SalonDto salon)
    {
        // Verifica que el dto exista
        if (salon == null)
        {
            throw new ArgumentNullException("salon");
        }

        // El nombre es obligatorio
        if (string.IsNullOrWhiteSpace(salon.Nombre))
        {
            throw new ArgumentException("Ingresá el nombre del salón.");
        }

        // La ubicación es obligatoria
        if (string.IsNullOrWhiteSpace(salon.Ubicacion))
        {
            throw new ArgumentException("Ingresá la ubicación del salón.");
        }

        // El costo no puede ser negativo
        if (salon.CostoBase < 0)
        {
            throw new ArgumentException("El monto tiene que ser mayor o igual a 0.");
        }
    }

    // C¿onvierte una entidad Salon en un SalonDto
    private SalonDto ConvertirADto(Salon salon)
    {
        SalonDto dto = new SalonDto();

        dto.Id = salon.Id;
        dto.Nombre = salon.Nombre;
        dto.Ubicacion = salon.Ubicacion;
        dto.CostoBase = salon.CostoBase;

        return dto;
    }
}