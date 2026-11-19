using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using proyecto_integrador.models;

namespace proyecto_integrador.repository
{
    // Convierte el grafo completo de un presupuesto a XML y viceversa.
    // Se hace a mano (no con XmlSerializer) porque Presupuesto tiene propiedades
    // de solo lectura, un constructor con todos los datos y una lista polimórfica
    // de ítems. Además, guardar un snapshot de salón/menú/servicio evita que un
    // presupuesto viejo cambie si después se modifica el catálogo.
    public static class MapeadorPresupuestoXml
    {
        public static XElement AXml(Presupuesto presupuesto)
        {
            if (presupuesto == null)
            {
                throw new ArgumentNullException("presupuesto");
            }

            XElement items = new XElement("items");
            foreach (ItemPresupuesto item in presupuesto.Items)
            {
                items.Add(ItemAXml(item));
            }

            XElement evento = new XElement("evento",
                new XAttribute("id", presupuesto.Evento.Id),
                new XAttribute("duracion", presupuesto.Evento.Duracion),
                new XAttribute("adultos", presupuesto.Evento.CantidadAdultos),
                new XAttribute("ninos", presupuesto.Evento.CantidadNinos));

            return new XElement("presupuesto",
                new XAttribute("id", presupuesto.Id),
                new XAttribute("fechaCreacion", presupuesto.FechaCreacion.ToString("O", CultureInfo.InvariantCulture)),
                new XAttribute("fechaEvento", presupuesto.FechaEvento.ToString("O", CultureInfo.InvariantCulture)),
                new XAttribute("horaInicio", presupuesto.HoraInicio.ToString("c", CultureInfo.InvariantCulture)),
                new XAttribute("nombreContacto", presupuesto.NombreContacto),
                new XAttribute("emailContacto", presupuesto.EmailContacto),
                new XAttribute("telefonoContacto", presupuesto.TelefonoContacto),
                new XAttribute("ubicacionEvento", presupuesto.UbicacionEvento),
                new XAttribute("observaciones", presupuesto.Observaciones),
                new XAttribute("porcentajeMenuPrincipal", Numero(presupuesto.PorcentajeAdicionalMenuPrincipal)),
                new XAttribute("porcentajeMenuPersonalizado", Numero(presupuesto.PorcentajeAdicionalMenuPersonalizado)),
                new XAttribute("rutaPdf", presupuesto.RutaPdf ?? string.Empty),
                evento,
                items);
        }

        public static Presupuesto DesdeXml(XElement elemento)
        {
            if (elemento == null)
            {
                throw new ArgumentNullException("elemento");
            }

            XElement nodoEvento = elemento.Element("evento");
            Evento evento = nodoEvento == null ? new Evento(0, 1, 0, 0) : LeerEvento(nodoEvento);

            List<ItemPresupuesto> items = new List<ItemPresupuesto>();
            XElement nodoItems = elemento.Element("items");
            if (nodoItems != null)
            {
                foreach (XElement nodoItem in nodoItems.Elements("item"))
                {
                    items.Add(LeerItem(nodoItem));
                }
            }

            return new Presupuesto(
                LeerEntero(elemento, "id", 0),
                LeerFecha(elemento, "fechaCreacion"),
                LeerFecha(elemento, "fechaEvento"),
                LeerHora(elemento, "horaInicio"),
                LeerCadena(elemento, "nombreContacto"),
                LeerCadena(elemento, "emailContacto"),
                LeerCadena(elemento, "telefonoContacto"),
                LeerCadena(elemento, "ubicacionEvento"),
                LeerCadena(elemento, "observaciones"),
                evento,
                items,
                LeerDecimal(elemento, "porcentajeMenuPrincipal"),
                LeerDecimal(elemento, "porcentajeMenuPersonalizado"),
                LeerCadena(elemento, "rutaPdf"));
        }

        // ---------- Item a XML ----------

        private static XElement ItemAXml(ItemPresupuesto item)
        {
            ItemSalon itemSalon = item as ItemSalon;
            if (itemSalon != null)
            {
                return new XElement("item",
                    new XAttribute("tipo", "Salon"),
                    new XAttribute("duracionHoras", itemSalon.DuracionHoras),
                    SalonAXml(itemSalon.Salon));
            }

            ItemMenu itemMenu = item as ItemMenu;
            if (itemMenu != null)
            {
                DetalleDeMenus detalle = itemMenu.Detalle;
                return new XElement("item",
                    new XAttribute("tipo", "Menu"),
                    new XAttribute("cantidadAdultos", detalle.CantidadAdultos),
                    new XAttribute("cantidadNinos", detalle.CantidadNinos),
                    new XAttribute("exclusion", detalle.ExclusionDelMenu.ToString()),
                    MenuAXml(detalle.MenuElegido));
            }

            ItemServicio itemServicio = item as ItemServicio;
            if (itemServicio != null)
            {
                return new XElement("item",
                    new XAttribute("tipo", "Servicio"),
                    new XAttribute("personalizado", itemServicio.EsPersonalizado),
                    ServicioAXml(itemServicio.Servicio));
            }

            // Si mañana aparece un tipo nuevo y todavía no tiene mapeador, se
            // conserva igual el importe y el detalle para no perder el total.
            return new XElement("item",
                new XAttribute("tipo", item.Tipo),
                new XAttribute("descripcion", item.Descripcion),
                new XAttribute("subtotal", Numero(item.CalcularSubtotal())),
                new XAttribute("detalle", item.ObtenerDetalle()));
        }

        private static XElement SalonAXml(Salon salon)
        {
            return new XElement("salon",
                new XAttribute("id", salon.Id),
                new XAttribute("nombre", salon.Nombre),
                new XAttribute("ubicacion", salon.Ubicacion),
                new XAttribute("costoBase", Numero(salon.CostoBase)));
        }

        private static XElement MenuAXml(MenuDeComida menu)
        {
            return new XElement("menu",
                new XAttribute("id", menu.Id),
                new XAttribute("nombre", menu.Nombre),
                new XAttribute("costoAdulto", Numero(menu.CostoPorAdulto)),
                new XAttribute("costoNino", Numero(menu.CostoPorNiño)));
        }

        private static XElement ServicioAXml(ServiciosAdicionales servicio)
        {
            return new XElement("servicio",
                new XAttribute("id", servicio.Id),
                new XAttribute("descripcion", servicio.Descripcion),
                new XAttribute("montoFijo", Numero(servicio.CostoDelServicio)));
        }

        // ---------- XML a Item ----------

        private static ItemPresupuesto LeerItem(XElement elemento)
        {
            string tipo = LeerCadena(elemento, "tipo");

            if (tipo == "Salon")
            {
                XElement nodo = elemento.Element("salon");
                Salon salon = nodo == null ? new Salon(0, string.Empty, string.Empty, 0m) : LeerSalon(nodo);
                return new ItemSalon(salon, Math.Max(1, LeerEntero(elemento, "duracionHoras", 1)));
            }

            if (tipo == "Menu")
            {
                XElement nodo = elemento.Element("menu");
                MenuDeComida menu = nodo == null ? new MenuDeComida(0, string.Empty, 0m, 0m) : LeerMenu(nodo);
                DetalleDeMenus detalle = new DetalleDeMenus(
                    Math.Max(0, LeerEntero(elemento, "cantidadAdultos", 0)),
                    Math.Max(0, LeerEntero(elemento, "cantidadNinos", 0)),
                    LeerExclusion(elemento),
                    menu);
                return new ItemMenu(detalle);
            }

            if (tipo == "Servicio")
            {
                XElement nodo = elemento.Element("servicio");
                ServiciosAdicionales servicio = nodo == null
                    ? new ServiciosAdicionales(0, string.Empty, 0m)
                    : LeerServicio(nodo);
                return new ItemServicio(servicio, LeerBooleano(elemento, "personalizado"));
            }

            return new ItemGenerico(
                tipo,
                LeerCadena(elemento, "descripcion"),
                LeerDecimal(elemento, "subtotal"),
                LeerCadena(elemento, "detalle"));
        }

        private static Evento LeerEvento(XElement elemento)
        {
            return new Evento(
                LeerEntero(elemento, "id", 0),
                Math.Max(1, LeerEntero(elemento, "duracion", 1)),
                Math.Max(0, LeerEntero(elemento, "adultos", 0)),
                Math.Max(0, LeerEntero(elemento, "ninos", 0)));
        }

        private static Salon LeerSalon(XElement elemento)
        {
            return new Salon(
                LeerEntero(elemento, "id", 0),
                LeerCadena(elemento, "nombre"),
                LeerCadena(elemento, "ubicacion"),
                Math.Max(0m, LeerDecimal(elemento, "costoBase")));
        }

        private static MenuDeComida LeerMenu(XElement elemento)
        {
            return new MenuDeComida(
                LeerEntero(elemento, "id", 0),
                LeerCadena(elemento, "nombre"),
                Math.Max(0m, LeerDecimal(elemento, "costoAdulto")),
                Math.Max(0m, LeerDecimal(elemento, "costoNino")));
        }

        private static ServiciosAdicionales LeerServicio(XElement elemento)
        {
            return new ServiciosAdicionales(
                LeerEntero(elemento, "id", 0),
                LeerCadena(elemento, "descripcion"),
                Math.Max(0m, LeerDecimal(elemento, "montoFijo")));
        }

        // ---------- Helpers ----------

        private static string LeerCadena(XElement elemento, string nombre)
        {
            XAttribute atributo = elemento.Attribute(nombre);
            return atributo == null ? string.Empty : atributo.Value;
        }

        private static int LeerEntero(XElement elemento, string nombre, int porDefecto)
        {
            XAttribute atributo = elemento.Attribute(nombre);
            int valor;
            if (atributo != null && int.TryParse(atributo.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out valor))
            {
                return valor;
            }

            return porDefecto;
        }

        private static decimal LeerDecimal(XElement elemento, string nombre)
        {
            XAttribute atributo = elemento.Attribute(nombre);
            decimal valor;
            if (atributo != null && decimal.TryParse(atributo.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out valor))
            {
                return valor;
            }

            return 0m;
        }

        private static bool LeerBooleano(XElement elemento, string nombre)
        {
            XAttribute atributo = elemento.Attribute(nombre);
            bool valor;
            if (atributo != null && bool.TryParse(atributo.Value, out valor))
            {
                return valor;
            }

            return false;
        }

        private static DateTime LeerFecha(XElement elemento, string nombre)
        {
            XAttribute atributo = elemento.Attribute(nombre);
            DateTime valor;
            if (atributo != null && DateTime.TryParse(atributo.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out valor))
            {
                return valor;
            }

            return DateTime.MinValue;
        }

        private static TimeSpan LeerHora(XElement elemento, string nombre)
        {
            XAttribute atributo = elemento.Attribute(nombre);
            TimeSpan valor;
            if (atributo != null && TimeSpan.TryParse(atributo.Value, CultureInfo.InvariantCulture, out valor))
            {
                return valor;
            }

            return TimeSpan.Zero;
        }

        private static TipoDeExclusionDeMenu LeerExclusion(XElement elemento)
        {
            TipoDeExclusionDeMenu valor;
            if (Enum.TryParse<TipoDeExclusionDeMenu>(LeerCadena(elemento, "exclusion"), out valor))
            {
                return valor;
            }

            return TipoDeExclusionDeMenu.Ninguna;
        }

        private static string Numero(decimal valor)
        {
            return valor.ToString(CultureInfo.InvariantCulture);
        }

        // Representación de contingencia para tipos de ítem que todavía no
        // tienen mapeador. Mantiene el importe y el texto del detalle.
        private sealed class ItemGenerico : ItemPresupuesto
        {
            private readonly string _tipo;
            private readonly decimal _subtotal;
            private readonly string _detalle;

            public ItemGenerico(string tipo, string descripcion, decimal subtotal, string detalle)
                : base(0, descripcion)
            {
                _tipo = string.IsNullOrEmpty(tipo) ? "Otro" : tipo;
                _subtotal = subtotal;
                _detalle = detalle;
            }

            public override string Tipo
            {
                get { return _tipo; }
            }

            public override decimal CalcularSubtotal()
            {
                return _subtotal;
            }

            public override string ObtenerDetalle()
            {
                return string.IsNullOrEmpty(_detalle) ? Descripcion : _detalle;
            }
        }
    }
}
