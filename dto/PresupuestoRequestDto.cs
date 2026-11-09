using System;
using System.Collections.Generic;
using System.ComponentModel;
using proyecto_integrador.models;

namespace proyecto_integrador.dto
{
    // dto utilizado para transportar los datos de un servicio personalizado.
    public sealed class ServicioPersonalizadoDto
    {
        public string Descripcion { get; set; }
        public decimal MontoFijo { get; set; }
    }

    // dto que representa una fila de menu dentro de un presupuesto.
    // Implementa INotifyPropertyChanged para notificar cambios a la interfaz.
    public sealed class DetalleMenuDto : INotifyPropertyChanged
    {
        private int _menuId;
        private int _cantidadAdultos;
        private int _cantidadNinos;
        private TipoDeExclusionDeMenu _particularidad;

        // Evento utilizado para informar cambios en las propiedades.
        public event PropertyChangedEventHandler PropertyChanged;

        public int MenuId
        {
            get { return _menuId; }
            set
            {
                if (_menuId != value)
                {
                    _menuId = value;
                    NotificarCambio("MenuId");
                }
            }
        }

        public int CantidadAdultos
        {
            get { return _cantidadAdultos; }
            set
            {
                if (_cantidadAdultos != value)
                {
                    _cantidadAdultos = value;
                    NotificarCambio("CantidadAdultos");
                }
            }
        }

        public int CantidadNinos
        {
            get { return _cantidadNinos; }
            set
            {
                if (_cantidadNinos != value)
                {
                    _cantidadNinos = value;
                    NotificarCambio("CantidadNinos");
                }
            }
        }

        public TipoDeExclusionDeMenu Particularidad
        {
            get { return _particularidad; }
            set
            {
                if (_particularidad != value)
                {
                    _particularidad = value;
                    NotificarCambio("Particularidad");
                }
            }
        }

        // Notifica a la interfaz que una propiedad fue modificada
        private void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }
    }

    // DTO utilizado para transportar las cantidades de invitados asignados.
    public sealed class AsignacionMenuDto
    {
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }

        public AsignacionMenuDto(int cantidadAdultos, int cantidadNinos)
        {
            CantidadAdultos = cantidadAdultos;
            CantidadNinos = cantidadNinos;
        }
    }

    // dto de entrada que contiene toda la información necesaria
    // para crear o modificar un presupuesto.
    public sealed class PresupuestoRequestDto
    {
        public string NombreContacto { get; set; }
        public string EmailContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string Observaciones { get; set; }
        public string UbicacionEvento { get; set; }
        public DateTime FechaEvento { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public int SalonId { get; set; }
        public int DuracionHoras { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
        public decimal PorcentajeAdicionalMenuPrincipal { get; set; }
        public decimal PorcentajeAdicionalMenuPersonalizado { get; set; }
        public List<DetalleMenuDto> Menues { get; set; }
        public List<int> ServiciosAdicionalesIds { get; set; }
        public List<ServicioPersonalizadoDto> ServiciosPersonalizados { get; set; }

        // inicializa las colecciones para evitar referencias nulas
        public PresupuestoRequestDto()
        {
            Menues = new List<DetalleMenuDto>();
            ServiciosAdicionalesIds = new List<int>();
            ServiciosPersonalizados = new List<ServicioPersonalizadoDto>();
        }
    }
}