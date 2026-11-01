using System;

namespace proyecto_integrador.models
{
    public class Evento
    {
        private int _duracionDelEventoEnHoras;
        private int _cantidadDeAdultos;
        private int _cantidadDeNinos;

        public int Id { get; private set; }

        public Evento(int id, int duracion, int cantidadAdultos, int cantidadNinos)
        {
            Id = id;
            Duracion = duracion;
            CantidadAdultos = cantidadAdultos;
            CantidadNinos = cantidadNinos;
        }

        public int Duracion
        {
            get { return _duracionDelEventoEnHoras; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("La duración del evento tiene que ser mayor que cero.");
                }

                _duracionDelEventoEnHoras = value;
            }
        }

        public int CantidadAdultos
        {
            get { return _cantidadDeAdultos; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La cantidad de adultos no puede ser negativa.");
                }

                _cantidadDeAdultos = value;
            }
        }

        public int CantidadNinos
        {
            get { return _cantidadDeNinos; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La cantidad de niños no puede ser negativa.");
                }

                _cantidadDeNinos = value;
            }
        }
    }
}
