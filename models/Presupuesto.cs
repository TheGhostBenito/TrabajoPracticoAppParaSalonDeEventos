using System;
using System.Collections.Generic;

namespace proyecto_integrador.models
{
    public class Presupuesto
    {
        public int Id { get; internal set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime FechaEvento { get; private set; }
        public TimeSpan HoraInicio { get; private set; }
        public string NombreContacto { get; private set; }
        public string EmailContacto { get; private set; }
        public string TelefonoContacto { get; private set; }
        public string UbicacionEvento { get; private set; }
        public string Observaciones { get; private set; }
        public Evento Evento { get; private set; }
        public Salon SalonDelEvento { get; private set; }
        public List<DetalleDeMenus> Menues { get; private set; }
        public List<ServiciosAdicionales> ServiciosAdicionales { get; private set; }
        public decimal PorcentajeAdicionalMenuPrincipal { get; private set; }
        public decimal PorcentajeAdicionalMenuPersonalizado { get; private set; }
        public string RutaPdf { get; internal set; }

        public string FechaHoraEvento
        {
            get { return FechaEvento.Date.Add(HoraInicio).ToString("g"); }
        }

        public string NombreSalon
        {
            get { return SalonDelEvento.Nombre; }
        }

        public int CantidadTotalInvitados
        {
            get { return Evento.CantidadAdultos + Evento.CantidadNinos; }
        }

        public decimal CostoSalon
        {
            get { return SalonDelEvento.CostoBase * Evento.Duracion; }
        }

        public decimal SubtotalMenusPrincipales
        {
            get
            {
                decimal subtotal = 0m;
                foreach (DetalleDeMenus detalle in Menues)
                {
                    if (detalle.ExclusionDelMenu == TipoDeExclusionDeMenu.Ninguna)
                    {
                        subtotal += detalle.CalcularSubtotal();
                    }
                }

                return subtotal;
            }
        }

        public decimal SubtotalMenusPersonalizados
        {
            get
            {
                decimal subtotal = 0m;
                foreach (DetalleDeMenus detalle in Menues)
                {
                    if (detalle.ExclusionDelMenu != TipoDeExclusionDeMenu.Ninguna)
                    {
                        subtotal += detalle.CalcularSubtotal();
                    }
                }

                return subtotal;
            }
        }

        public decimal RecargoMenusPrincipales
        {
            get { return decimal.Round(SubtotalMenusPrincipales * PorcentajeAdicionalMenuPrincipal / 100m, 2); }
        }

        public decimal RecargoMenusPersonalizados
        {
            get { return decimal.Round(SubtotalMenusPersonalizados * PorcentajeAdicionalMenuPersonalizado / 100m, 2); }
        }

        public decimal CostoMenus
        {
            get { return SubtotalMenusPrincipales + SubtotalMenusPersonalizados + RecargoMenusPrincipales + RecargoMenusPersonalizados; }
        }

        public decimal CostoServiciosAdicionales
        {
            get
            {
                decimal subtotal = 0m;
                foreach (ServiciosAdicionales servicio in ServiciosAdicionales)
                {
                    subtotal += servicio.CostoDelServicio;
                }

                return subtotal;
            }
        }

        public decimal TotalFinal
        {
            get { return decimal.Round(CostoSalon + CostoMenus + CostoServiciosAdicionales, 2); }
        }

        public Presupuesto(
            int id,
            DateTime fechaCreacion,
            DateTime fechaEvento,
            TimeSpan horaInicio,
            string nombreContacto,
            string emailContacto,
            string telefonoContacto,
            string ubicacionEvento,
            string observaciones,
            Evento evento,
            Salon salon,
            List<DetalleDeMenus> menues,
            List<ServiciosAdicionales> serviciosAdicionales,
            decimal porcentajeAdicionalMenuPrincipal,
            decimal porcentajeAdicionalMenuPersonalizado,
            string rutaPdf)
        {
            if (evento == null)
            {
                throw new ArgumentNullException("evento");
            }

            if (salon == null)
            {
                throw new ArgumentNullException("salon");
            }

            if (menues == null)
            {
                throw new ArgumentNullException("menues");
            }

            if (serviciosAdicionales == null)
            {
                throw new ArgumentNullException("serviciosAdicionales");
            }

            Id = id;
            FechaCreacion = fechaCreacion;
            FechaEvento = fechaEvento;
            HoraInicio = horaInicio;
            NombreContacto = nombreContacto ?? string.Empty;
            EmailContacto = emailContacto ?? string.Empty;
            TelefonoContacto = telefonoContacto ?? string.Empty;
            UbicacionEvento = ubicacionEvento ?? string.Empty;
            Observaciones = observaciones ?? string.Empty;
            Evento = evento;
            SalonDelEvento = salon;
            Menues = new List<DetalleDeMenus>(menues);
            ServiciosAdicionales = new List<ServiciosAdicionales>(serviciosAdicionales);
            PorcentajeAdicionalMenuPrincipal = porcentajeAdicionalMenuPrincipal;
            PorcentajeAdicionalMenuPersonalizado = porcentajeAdicionalMenuPersonalizado;
            RutaPdf = rutaPdf ?? string.Empty;
        }
    }
}
