// Controller encargado de comunicar la vista con el Service
// de servicios adicionales. No contiene lógica de negocio.
using proyecto_integrador.dto;
using proyecto_integrador.Service;
using System;
using System.Collections.Generic;

public sealed class ServiciosAdicionalesController
{
    // Service utilizado para gestionar los servicios adicionales
    // Referencia al Service Se asigna una sola vez en el constructor
    private readonly IServiciosAdicionalesService _service;

    // Recibe el Service mediante inyección de dependencias
    public ServiciosAdicionalesController(IServiciosAdicionalesService service)
    {
        // Verifica que el Service exista
        if (service == null)
        {
            throw new ArgumentNullException("service");
        }

        _service = service;
    }

    // Solicita al Service todos los servicios adicionales
    public List<ServicioAdicionalDto> ObtenerTodos()
    {
        return _service.ObtenerTodos();
    }

    // Solicita al Service crear un nuevo servicio
    public void Crear(ServicioAdicionalDto servicio)
    {
        _service.Crear(servicio);
    }

    // Solicita al Service modificar un servicio existente
    public bool Modificar(ServicioAdicionalDto servicio)
    {
        return _service.Modificar(servicio);
    }

    // Solicita al Service eliminar un servicio
    public bool Eliminar(int id)
    {
        return _service.Eliminar(id);
    }
}