// Service encargado de aplicar las reglas de negocio
// relacionadas con los servicios adicionales.
using proyecto_integrador.dto;
using proyecto_integrador.models;
using proyecto_integrador.repository;
using proyecto_integrador.Service;
using System;
using System.Collections.Generic;

public class ServiciosAdicionalesService : IService<ServicioAdicionalDto, ServicioAdicionalDto>
{
    // Repositorio utilizado para acceder a los datos
    private readonly IRepository<ServiciosAdicionales> _repo;

    //La inyección de dependencias permite que el Service reciba el Repository desde afuera en lugar de crearlo él mismo
    //Así el Service depende de una interfaz y no de una clase concreta
    public ServiciosAdicionalesService(IRepository<ServiciosAdicionales> repo)
    {
        // Verifica que el repositorio exista
        if (repo == null)
        {
            throw new ArgumentNullException("repo");
        }

        _repo = repo;
    }

    // Obtiene todos los servicios y los convierte a DTO
    public List<ServicioAdicionalDto> ObtenerTodos()
    {
        List<ServicioAdicionalDto> resultado = new List<ServicioAdicionalDto>();

        foreach (ServiciosAdicionales servicio in _repo.ObtenerTodos())
        {
            resultado.Add(ConvertirADto(servicio));
        }

        return resultado;
    }

    // Valida los datos y crea un nuevo servicio adicional
    public ServicioAdicionalDto Crear(ServicioAdicionalDto servicio)
    {
        Validar(servicio);

        // Convierte el DTO en una entidad del modelo
        ServiciosAdicionales nuevo =
            new ServiciosAdicionales(
                0,
                servicio.Descripcion.Trim(),
                servicio.CostoDelServicio);

        _repo.Crear(nuevo);

        // Devuelve el elemento creado, ya convertido a DTO
        return ConvertirADto(nuevo);
    }

    // Valida los datos y modifica un servicio existente
    public ServicioAdicionalDto Modificar(int id, ServicioAdicionalDto servicio)
    {
        Validar(servicio);

        ServiciosAdicionales modificado =
            new ServiciosAdicionales(
                id,
                servicio.Descripcion.Trim(),
                servicio.CostoDelServicio);

        // Si no se encuentra, se avisa con un error (la pantalla muestra el mensaje)
        if (!_repo.Modificar(modificado))
        {
            throw new ArgumentException("No se encontró el servicio seleccionado.");
        }

        // Devuelve el elemento modificado, ya convertido a DTO
        return ConvertirADto(modificado);
    }

    // Solicita al repositorio eliminar un servicio
    public bool Eliminar(int id)
    {
        return _repo.Eliminar(id);
    }

    // Reglas de negocio para los servicios adicionales
    private void Validar(ServicioAdicionalDto servicio)
    {
        // Verifica que el DTO exista
        if (servicio == null)
        {
            throw new ArgumentNullException("servicio");
        }

        // La descripción es obligatoria
        if (string.IsNullOrWhiteSpace(servicio.Descripcion))
        {
            throw new ArgumentException("Ingresá la descripción del servicio.");
        }

        // El costo no puede ser negativo
        if (servicio.CostoDelServicio < 0)
        {
            throw new ArgumentException("El monto tiene que ser mayor o igual a 0.");
        }
    }

    // onvierte una entidad en un DTO para enviarlo a otras capas
    private ServicioAdicionalDto ConvertirADto(ServiciosAdicionales servicio)
    {
        ServicioAdicionalDto dto = new ServicioAdicionalDto();

        dto.Id = servicio.Id;
        dto.Descripcion = servicio.Descripcion;
        dto.CostoDelServicio = servicio.CostoDelServicio;

        return dto;
    }
}