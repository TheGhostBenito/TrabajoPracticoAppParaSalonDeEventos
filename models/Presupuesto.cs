using System;
using System.Collections.Generic;

namespace proyecto_integrador.models
{
    public class Presupuesto
    {
        // Un sola colección polimórfica: salón, menús, servicios y cualquier
        // concepto nuevo conviven acá sin que el modelo tenga que cambiar.
        private readonly List<ItemPresupuesto> _items;

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
        public decimal PorcentajeAdicionalMenuPrincipal { get; private set; }
        public decimal PorcentajeAdicionalMenuPersonalizado { get; private set; }
        public string RutaPdf { get; internal set; }

        public string FechaHoraEvento
        {
            get { return FechaEvento.Date.Add(HoraInicio).ToString("g"); }
        }

        public List<ItemPresupuesto> Items
        {
            get { return new List<ItemPresupuesto>(_items); }
        }

        // Estas tres propiedades se mantienen para la vista y el PDF actuales,
        // pero ya no son el almacenamiento del presupuesto: se derivan de Items.
        public Salon SalonDelEvento
        {
            get
            {
                foreach (ItemPresupuesto item in _items)
                {
                    ItemSalon itemSalon = item as ItemSalon;
                    if (itemSalon != null)
                    {
                        return itemSalon.Salon;
                    }
                }

                return null;
            }
        }

        public List<DetalleDeMenus> Menues
        {
            get
            {
                List<DetalleDeMenus> resultado = new List<DetalleDeMenus>();
                foreach (ItemPresupuesto item in _items)
                {
                    ItemMenu itemMenu = item as ItemMenu;
                    if (itemMenu != null)
                    {
                        resultado.Add(itemMenu.Detalle);
                    }
                }

                return resultado;
            }
        }

        public List<ServiciosAdicionales> ServiciosAdicionales
        {
            get
            {
                List<ServiciosAdicionales> resultado = new List<ServiciosAdicionales>();
                foreach (ItemPresupuesto item in _items)
                {
                    ItemServicio itemServicio = item as ItemServicio;
                    if (itemServicio != null)
                    {
                        resultado.Add(itemServicio.Servicio);
                    }
                }

                return resultado;
            }
        }

        public string NombreSalon
        {
            get { return SalonDelEvento == null ? string.Empty : SalonDelEvento.Nombre; }
        }

        public int CantidadTotalInvitados
        {
            get { return Evento.CantidadAdultos + Evento.CantidadNinos; }
        }

        public decimal CostoSalon
        {
            get { return SumarItems<ItemSalon>(); }
        }

        public decimal SubtotalMenusPrincipales
        {
            get
            {
                decimal subtotal = 0m;
                foreach (ItemPresupuesto item in _items)
                {
                    ItemMenu menu = item as ItemMenu;
                    if (menu != null && menu.EsMenuPrincipal)
                    {
                        subtotal += menu.CalcularSubtotal();
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
                foreach (ItemPresupuesto item in _items)
                {
                    ItemMenu menu = item as ItemMenu;
                    if (menu != null && !menu.EsMenuPrincipal)
                    {
                        subtotal += menu.CalcularSubtotal();
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
            get { return SumarItems<ItemServicio>(); }
        }

        // Cualquier ítem que no sea uno de los tres conocidos entra acá.
        public decimal CostoOtrosItems
        {
            get
            {
                decimal subtotal = 0m;
                foreach (ItemPresupuesto item in _items)
                {
                    if (!(item is ItemSalon) && !(item is ItemMenu) && !(item is ItemServicio))
                    {
                        subtotal += item.CalcularSubtotal();
                    }
                }

                return subtotal;
            }
        }

        public decimal TotalFinal
        {
            get
            {
                // Polimorfismo puro: se suman todos los ítems sin importar su tipo.
                decimal subtotal = 0m;
                foreach (ItemPresupuesto item in _items)
                {
                    subtotal += item.CalcularSubtotal();
                }

                return decimal.Round(subtotal + RecargoMenusPrincipales + RecargoMenusPersonalizados, 2);
            }
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
            IEnumerable<ItemPresupuesto> items,
            decimal porcentajeAdicionalMenuPrincipal,
            decimal porcentajeAdicionalMenuPersonalizado,
            string rutaPdf)
        {
            if (evento == null)
            {
                throw new ArgumentNullException("evento");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
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
            _items = new List<ItemPresupuesto>();
            foreach (ItemPresupuesto item in items)
            {
                AgregarItem(item);
            }

            PorcentajeAdicionalMenuPrincipal = porcentajeAdicionalMenuPrincipal;
            PorcentajeAdicionalMenuPersonalizado = porcentajeAdicionalMenuPersonalizado;
            RutaPdf = rutaPdf ?? string.Empty;
        }

        public void AgregarItem(ItemPresupuesto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _items.Add(item);
        }

        private decimal SumarItems<T>() where T : ItemPresupuesto
        {
            decimal subtotal = 0m;
            foreach (ItemPresupuesto item in _items)
            {
                T itemTipado = item as T;
                if (itemTipado != null)
                {
                    subtotal += itemTipado.CalcularSubtotal();
                }
            }

            return subtotal;
        }
    }
}
