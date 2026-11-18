using System;
using System.Collections.Generic;
using proyecto_integrador.models;
using proyecto_integrador.repository;

namespace proyecto_integrador.Service
{
    // Acceso único a los catálogos dentro de una operación de presupuesto.
    // También recupera los datos que ya tenía el presupuesto que se está modificando,
    // para que un menú o servicio dado de baja siga siendo válido en ese presupuesto.
    public sealed class ContextoPresupuesto
    {
        private readonly IRepository<Salon> _salonRepository;
        private readonly IRepository<MenuDeComida> _menuRepository;
        private readonly IRepository<ServiciosAdicionales> _servicioRepository;

        public Presupuesto Existente { get; private set; }

        public ContextoPresupuesto(
            IRepository<Salon> salonRepository,
            IRepository<MenuDeComida> menuRepository,
            IRepository<ServiciosAdicionales> servicioRepository,
            Presupuesto existente)
        {
            if (salonRepository == null) throw new ArgumentNullException("salonRepository");
            if (menuRepository == null) throw new ArgumentNullException("menuRepository");
            if (servicioRepository == null) throw new ArgumentNullException("servicioRepository");

            _salonRepository = salonRepository;
            _menuRepository = menuRepository;
            _servicioRepository = servicioRepository;
            Existente = existente;
        }

        public List<Salon> ObtenerSalones()
        {
            List<Salon> resultado = new List<Salon>();
            foreach (Salon salon in _salonRepository.ObtenerTodos())
            {
                resultado.Add(Copiar(salon));
            }

            if (Existente != null && Existente.SalonDelEvento != null
                && !ContieneSalon(resultado, Existente.SalonDelEvento.Id))
            {
                resultado.Add(Copiar(Existente.SalonDelEvento));
            }

            return resultado;
        }

        public List<MenuDeComida> ObtenerMenus()
        {
            List<MenuDeComida> resultado = new List<MenuDeComida>();
            foreach (MenuDeComida menu in _menuRepository.ObtenerTodos())
            {
                resultado.Add(Copiar(menu));
            }

            if (Existente != null)
            {
                foreach (ItemPresupuesto item in Existente.Items)
                {
                    ItemMenu itemMenu = item as ItemMenu;
                    if (itemMenu != null && !ContieneMenu(resultado, itemMenu.Id))
                    {
                        resultado.Add(Copiar(itemMenu.Detalle.MenuElegido));
                    }
                }
            }

            return resultado;
        }

        public List<ServiciosAdicionales> ObtenerServicios()
        {
            List<ServiciosAdicionales> resultado = new List<ServiciosAdicionales>();
            foreach (ServiciosAdicionales servicio in _servicioRepository.ObtenerTodos())
            {
                resultado.Add(Copiar(servicio));
            }

            if (Existente != null)
            {
                foreach (ItemPresupuesto item in Existente.Items)
                {
                    ItemServicio itemServicio = item as ItemServicio;
                    if (itemServicio != null && !itemServicio.EsPersonalizado
                        && itemServicio.Id > 0 && !ContieneServicio(resultado, itemServicio.Id))
                    {
                        resultado.Add(Copiar(itemServicio.Servicio));
                    }
                }
            }

            return resultado;
        }

        public Salon ObtenerSalon(int id)
        {
            Salon salon = _salonRepository.ObtenerPorId(id);
            if (salon == null && Existente != null && Existente.SalonDelEvento != null
                && Existente.SalonDelEvento.Id == id)
            {
                salon = Existente.SalonDelEvento;
            }

            return Copiar(salon);
        }

        public MenuDeComida ObtenerMenu(int id)
        {
            MenuDeComida menu = _menuRepository.ObtenerPorId(id);
            if (menu == null && Existente != null)
            {
                foreach (ItemPresupuesto item in Existente.Items)
                {
                    ItemMenu itemMenu = item as ItemMenu;
                    if (itemMenu != null && itemMenu.Id == id)
                    {
                        menu = itemMenu.Detalle.MenuElegido;
                        break;
                    }
                }
            }

            return Copiar(menu);
        }

        public ServiciosAdicionales ObtenerServicio(int id)
        {
            ServiciosAdicionales servicio = _servicioRepository.ObtenerPorId(id);
            if (servicio == null && Existente != null)
            {
                foreach (ItemPresupuesto item in Existente.Items)
                {
                    ItemServicio itemServicio = item as ItemServicio;
                    if (itemServicio != null && !itemServicio.EsPersonalizado && itemServicio.Id == id)
                    {
                        servicio = itemServicio.Servicio;
                        break;
                    }
                }
            }

            return Copiar(servicio);
        }

        private static bool ContieneSalon(List<Salon> items, int id)
        {
            foreach (Salon item in items)
            {
                if (item.Id == id) return true;
            }

            return false;
        }

        private static bool ContieneMenu(List<MenuDeComida> items, int id)
        {
            foreach (MenuDeComida item in items)
            {
                if (item.Id == id) return true;
            }

            return false;
        }

        private static bool ContieneServicio(List<ServiciosAdicionales> items, int id)
        {
            foreach (ServiciosAdicionales item in items)
            {
                if (item.Id == id) return true;
            }

            return false;
        }

        private static Salon Copiar(Salon salon)
        {
            return salon == null ? null : new Salon(salon.Id, salon.Nombre, salon.Ubicacion, salon.CostoBase);
        }

        private static MenuDeComida Copiar(MenuDeComida menu)
        {
            return menu == null ? null : new MenuDeComida(menu.Id, menu.Nombre, menu.CostoPorAdulto, menu.CostoPorNiño);
        }

        private static ServiciosAdicionales Copiar(ServiciosAdicionales servicio)
        {
            return servicio == null ? null : new ServiciosAdicionales(servicio.Id, servicio.Descripcion, servicio.CostoDelServicio);
        }
    }
}
