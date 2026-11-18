using System;
using System.Net.Mail;
using proyecto_integrador.dto;

namespace proyecto_integrador.Service
{
    // Reglas generales del presupuesto. Las reglas de cada ítem viven en su constructor.
    public sealed class PresupuestoRequestValidator
    {
        public void Validar(PresupuestoRequestDto solicitud)
        {
            if (solicitud == null)
            {
                throw new ArgumentNullException("solicitud");
            }

            if (solicitud.Menues == null || solicitud.ServiciosAdicionalesIds == null || solicitud.ServiciosPersonalizados == null)
            {
                throw new ArgumentException("Las listas de menús o servicios del presupuesto no son válidas.");
            }

            if (string.IsNullOrWhiteSpace(solicitud.NombreContacto))
            {
                throw new ArgumentException("Ingresá el nombre de contacto.");
            }

            ValidarEmail(solicitud.EmailContacto);

            if (string.IsNullOrWhiteSpace(solicitud.TelefonoContacto))
            {
                throw new ArgumentException("Ingresá un teléfono de contacto.");
            }

            if (string.IsNullOrWhiteSpace(solicitud.UbicacionEvento)
                || string.Equals(solicitud.UbicacionEvento.Trim(), "Sin definir", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Ingresá la ubicación concreta donde se realizará el evento.");
            }

            int cantidadDigitosTelefono = 0;
            foreach (char caracter in solicitud.TelefonoContacto)
            {
                if (char.IsDigit(caracter))
                {
                    cantidadDigitosTelefono++;
                }
            }

            if (cantidadDigitosTelefono < 7 || cantidadDigitosTelefono > 15)
            {
                throw new ArgumentException("Ingresá un teléfono válido, con entre 7 y 15 dígitos.");
            }

            if (solicitud.DuracionHoras <= 0)
            {
                throw new ArgumentException("La duración tiene que ser de al menos una hora.");
            }

            if (solicitud.FechaEvento.Date < DateTime.Today)
            {
                throw new ArgumentException("Estás seleccionando una fecha que no tiene validez en torno al contexto actual. Solo se pueden elegir fechas actuales o a futuro.");
            }

            if (solicitud.HoraInicio < TimeSpan.Zero || solicitud.HoraInicio >= TimeSpan.FromDays(1))
            {
                throw new ArgumentException("Elegí una hora de inicio válida para el evento.");
            }

            if (solicitud.CantidadAdultos < 0 || solicitud.CantidadNinos < 0 || solicitud.CantidadAdultos + solicitud.CantidadNinos <= 0)
            {
                throw new ArgumentException("Ingresá al menos un invitado y revisá las cantidades.");
            }

            if (solicitud.PorcentajeAdicionalMenuPrincipal < 0 || solicitud.PorcentajeAdicionalMenuPrincipal > 500
                || solicitud.PorcentajeAdicionalMenuPersonalizado < 0 || solicitud.PorcentajeAdicionalMenuPersonalizado > 500)
            {
                throw new ArgumentException("Los porcentajes adicionales deben estar entre 0 y 500.");
            }
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Ingresá el correo electrónico de contacto.");
            }

            try
            {
                MailAddress correo = new MailAddress(email.Trim());
                if (!string.Equals(correo.Address, email.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("Ingresá un correo electrónico válido.");
                }
            }
            catch (FormatException)
            {
                throw new ArgumentException("Ingresá un correo electrónico válido.");
            }
        }
    }
}
