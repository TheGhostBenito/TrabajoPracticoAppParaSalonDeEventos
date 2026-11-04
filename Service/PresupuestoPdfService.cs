using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    public class PresupuestoPdfService : IPresupuestoPdfService
    {
        private const int LineasPorPagina = 48;

        public string Guardar(Presupuesto presupuesto, string ruta)
        {
            if (presupuesto == null)
            {
                throw new ArgumentNullException("presupuesto");
            }

            if (string.IsNullOrWhiteSpace(ruta))
            {
                throw new ArgumentException("Elegí una ubicación para guardar el PDF.", "ruta");
            }

            string rutaCompleta = Path.GetFullPath(ruta);
            string carpeta = Path.GetDirectoryName(rutaCompleta);
            Directory.CreateDirectory(carpeta);
            File.WriteAllBytes(rutaCompleta, CrearPdf(presupuesto));
            return rutaCompleta;
        }

        private byte[] CrearPdf(Presupuesto presupuesto)
        {
            List<string> lineas = CrearLineas(presupuesto);
            List<byte[]> contenidoPaginas = new List<byte[]>();
            Encoding encodingPdf = Encoding.GetEncoding(1252, EncoderFallback.ReplacementFallback, DecoderFallback.ReplacementFallback);

            for (int inicio = 0; inicio < lineas.Count; inicio += LineasPorPagina)
            {
                int cantidad = Math.Min(LineasPorPagina, lineas.Count - inicio);
                StringBuilder contenido = new StringBuilder();
                contenido.Append("BT\n/F1 11 Tf\n50 790 Td\n14 TL\n");

                for (int indice = 0; indice < cantidad; indice++)
                {
                    if (indice > 0)
                    {
                        contenido.Append("T*\n");
                    }

                    contenido.Append("(");
                    contenido.Append(EscaparTextoPdf(lineas[inicio + indice]));
                    contenido.Append(") Tj\n");
                }

                contenido.Append("ET\n");
                contenidoPaginas.Add(encodingPdf.GetBytes(contenido.ToString()));
            }

            int cantidadPaginas = contenidoPaginas.Count;
            int objetoFuente = 3 + cantidadPaginas * 2;
            byte[][] objetos = new byte[objetoFuente][];
            objetos[0] = Encoding.ASCII.GetBytes("<< /Type /Catalog /Pages 2 0 R >>");

            StringBuilder referenciasPaginas = new StringBuilder();
            for (int indice = 0; indice < cantidadPaginas; indice++)
            {
                int numeroObjetoPagina = 3 + indice * 2;
                referenciasPaginas.Append(numeroObjetoPagina.ToString(CultureInfo.InvariantCulture));
                referenciasPaginas.Append(" 0 R ");
            }

            objetos[1] = Encoding.ASCII.GetBytes(
                "<< /Type /Pages /Kids [" + referenciasPaginas + "] /Count "
                + cantidadPaginas.ToString(CultureInfo.InvariantCulture) + " >>");

            for (int indice = 0; indice < cantidadPaginas; indice++)
            {
                int numeroObjetoPagina = 3 + indice * 2;
                int numeroObjetoContenido = numeroObjetoPagina + 1;
                objetos[numeroObjetoPagina - 1] = Encoding.ASCII.GetBytes(
                    "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] "
                    + "/Resources << /Font << /F1 " + objetoFuente.ToString(CultureInfo.InvariantCulture) + " 0 R >> >> "
                    + "/Contents " + numeroObjetoContenido.ToString(CultureInfo.InvariantCulture) + " 0 R >>");

                byte[] prefijo = Encoding.ASCII.GetBytes(
                    "<< /Length " + contenidoPaginas[indice].Length.ToString(CultureInfo.InvariantCulture) + " >>\nstream\n");
                byte[] sufijo = Encoding.ASCII.GetBytes("\nendstream");
                objetos[numeroObjetoContenido - 1] = Concatenar(prefijo, contenidoPaginas[indice], sufijo);
            }

            objetos[objetoFuente - 1] = Encoding.ASCII.GetBytes(
                "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>");

            using (MemoryStream documento = new MemoryStream())
            {
                EscribirAscii(documento, "%PDF-1.4\n");
                List<long> desplazamientos = new List<long>();
                desplazamientos.Add(0L);

                for (int indice = 0; indice < objetos.Length; indice++)
                {
                    desplazamientos.Add(documento.Position);
                    EscribirAscii(documento, (indice + 1).ToString(CultureInfo.InvariantCulture) + " 0 obj\n");
                    documento.Write(objetos[indice], 0, objetos[indice].Length);
                    EscribirAscii(documento, "\nendobj\n");
                }

                long inicioTabla = documento.Position;
                EscribirAscii(documento, "xref\n0 " + desplazamientos.Count.ToString(CultureInfo.InvariantCulture) + "\n");
                EscribirAscii(documento, "0000000000 65535 f \n");

                for (int indice = 1; indice < desplazamientos.Count; indice++)
                {
                    EscribirAscii(documento, desplazamientos[indice].ToString("D10", CultureInfo.InvariantCulture) + " 00000 n \n");
                }

                EscribirAscii(documento,
                    "trailer\n<< /Size " + desplazamientos.Count.ToString(CultureInfo.InvariantCulture) + " /Root 1 0 R >>\n"
                    + "startxref\n" + inicioTabla.ToString(CultureInfo.InvariantCulture) + "\n%%EOF");
                return documento.ToArray();
            }
        }

        private List<string> CrearLineas(Presupuesto presupuesto)
        {
            List<string> lineas = new List<string>();
            AgregarLineaEnRenglones(lineas, "PRESUPUESTO DE EVENTO #" + presupuesto.Id.ToString(CultureInfo.InvariantCulture));
            lineas.Add("Fecha de emisión: " + presupuesto.FechaCreacion.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture));
            lineas.Add("Fecha y hora del evento: " + presupuesto.FechaEvento.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)
                + " " + presupuesto.HoraInicio.ToString(@"hh\:mm", CultureInfo.CurrentCulture));
            lineas.Add("Cliente: " + presupuesto.NombreContacto);
            lineas.Add("Contacto: " + presupuesto.EmailContacto);
            lineas.Add("Teléfono: " + presupuesto.TelefonoContacto);
            lineas.Add("Salón: " + presupuesto.NombreSalon);
            AgregarLineaEnRenglones(lineas, "Ubicación: " + presupuesto.UbicacionEvento);
            lineas.Add("Duración: " + presupuesto.Evento.Duracion.ToString(CultureInfo.InvariantCulture) + " horas");
            lineas.Add("Invitados: " + presupuesto.Evento.CantidadAdultos.ToString(CultureInfo.InvariantCulture)
                + " adultos y " + presupuesto.Evento.CantidadNinos.ToString(CultureInfo.InvariantCulture) + " niños");
            lineas.Add(string.Empty);
            lineas.Add("MENÚS");

            foreach (DetalleDeMenus detalle in presupuesto.Menues)
            {
                string exclusiones = "menú principal";
                if (detalle.ExclusionDelMenu != TipoDeExclusionDeMenu.Ninguna)
                {
                    exclusiones = "particularidad: " + detalle.ExclusionDelMenu.ToString();
                }

                string descripcion = detalle.MenuElegido.Nombre + " (" + exclusiones + "): "
                    + detalle.CantidadAdultos.ToString(CultureInfo.InvariantCulture) + " adultos, "
                    + detalle.CantidadNinos.ToString(CultureInfo.InvariantCulture) + " niños; subtotal "
                    + detalle.CalcularSubtotal().ToString("C2", CultureInfo.CurrentCulture);
                AgregarLineaEnRenglones(lineas, descripcion);
            }

            lineas.Add("Subtotal menús principales: " + presupuesto.SubtotalMenusPrincipales.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("Adicional menú principal (" + presupuesto.PorcentajeAdicionalMenuPrincipal.ToString("0.##", CultureInfo.CurrentCulture)
                + "%): " + presupuesto.RecargoMenusPrincipales.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("Subtotal menús personalizados: " + presupuesto.SubtotalMenusPersonalizados.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("Adicional menú personalizado (" + presupuesto.PorcentajeAdicionalMenuPersonalizado.ToString("0.##", CultureInfo.CurrentCulture)
                + "%): " + presupuesto.RecargoMenusPersonalizados.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("Total menús: " + presupuesto.CostoMenus.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add(string.Empty);
            lineas.Add("SERVICIOS ADICIONALES");

            if (presupuesto.ServiciosAdicionales.Count == 0)
            {
                lineas.Add("Sin servicios adicionales.");
            }
            else
            {
                foreach (ServiciosAdicionales servicio in presupuesto.ServiciosAdicionales)
                {
                    AgregarLineaEnRenglones(lineas,
                        servicio.Descripcion + ": " + servicio.CostoDelServicio.ToString("C2", CultureInfo.CurrentCulture));
                }
            }

            lineas.Add("Subtotal de servicios: " + presupuesto.CostoServiciosAdicionales.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add(string.Empty);
            lineas.Add("Salón: " + presupuesto.CostoSalon.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("TOTAL FINAL: " + presupuesto.TotalFinal.ToString("C2", CultureInfo.CurrentCulture));

            if (!string.IsNullOrWhiteSpace(presupuesto.Observaciones))
            {
                lineas.Add(string.Empty);
                lineas.Add("OBSERVACIONES");
                AgregarLineaEnRenglones(lineas, presupuesto.Observaciones);
            }

            return lineas;
        }

        private void AgregarLineaEnRenglones(List<string> destino, string texto)
        {
            if (texto == null)
            {
                texto = string.Empty;
            }

            string textoEnUnaLinea = texto.Replace('\r', ' ').Replace('\n', ' ');
            const int maximoCaracteres = 88;

            while (textoEnUnaLinea.Length > maximoCaracteres)
            {
                int corte = textoEnUnaLinea.LastIndexOf(' ', maximoCaracteres);
                if (corte <= 0)
                {
                    corte = maximoCaracteres;
                }

                destino.Add(textoEnUnaLinea.Substring(0, corte));
                textoEnUnaLinea = textoEnUnaLinea.Substring(corte).TrimStart();
            }

            destino.Add(textoEnUnaLinea);
        }

        private string EscaparTextoPdf(string texto)
        {
            if (texto == null)
            {
                texto = string.Empty;
            }

            string resultado = texto.Replace("\\", "\\\\");
            resultado = resultado.Replace("(", "\\(");
            resultado = resultado.Replace(")", "\\)");
            return resultado;
        }

        private byte[] Concatenar(byte[] primero, byte[] segundo, byte[] tercero)
        {
            byte[] resultado = new byte[primero.Length + segundo.Length + tercero.Length];
            Buffer.BlockCopy(primero, 0, resultado, 0, primero.Length);
            Buffer.BlockCopy(segundo, 0, resultado, primero.Length, segundo.Length);
            Buffer.BlockCopy(tercero, 0, resultado, primero.Length + segundo.Length, tercero.Length);
            return resultado;
        }

        private void EscribirAscii(Stream destino, string texto)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(texto);
            destino.Write(bytes, 0, bytes.Length);
        }
    }
}
