using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_integrador.models
{
    public class Evento
    {
        private int id;
        private int DuracionDelEventoEnHoras;
        private int CantidadDeAdultosQueAsisten;
        private int CantidadDeNiñosQueAsisten;

        public Evento(int _id, int _duracion, int _cantidadAdultos, int _cantidadNinos)
        {
            id = _id;
            DuracionDelEventoEnHoras = _duracion;
            CantidadDeAdultosQueAsisten = _cantidadAdultos;
            CantidadDeNiñosQueAsisten = _cantidadNinos;
        }

        public int Duracion
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La duración del evento no puede ser negativa.");
                }
                DuracionDelEventoEnHoras = value;
            }

            get { return DuracionDelEventoEnHoras; }
        }

        public int CantidadAdultos
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La cantidad de adultos no puede ser negativa.");
                }
                CantidadDeAdultosQueAsisten = value;
            }

            get { return CantidadDeAdultosQueAsisten; }
        }

        public int CantidadNinos
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La cantidad de niños no puede ser negativa.");
                }
                CantidadDeNiñosQueAsisten = value;
            }
            get { return CantidadDeNiñosQueAsisten; }

        }
    }
}
