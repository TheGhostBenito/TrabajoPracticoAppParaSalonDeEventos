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
            get { return DuracionDelEventoEnHoras; }
        }

        public int CantidadAdultos
        {
            get { return CantidadDeAdultosQueAsisten; }
        }

        public int CantidadNinos
        {
            get { return CantidadDeNiñosQueAsisten; }

        }
    }
}
