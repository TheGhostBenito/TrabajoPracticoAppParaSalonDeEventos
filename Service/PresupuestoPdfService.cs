using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using proyecto_integrador.models;

namespace proyecto_integrador.Service
{
    // Este service genera el archivo pdf de un presupuesto
    // no usa ninguna librería externa: arma el PDF , escrbiendo el texto
    // línea por línea con el formato que exige un archivo pdf
    // implementa IPresupuestoPdfService, así el PresupuestoService depende de la interfaz y no de esta clase
    public class PresupuestoPdfService : IPresupuestoPdfService
    {
        // Cantidad máxima de renglones de texto que entran en una hoja
        // ai se pasa  de 48, el texto sigue en una hoja nueva.
        private const int LineasPorPagina = 48;

        // metodo public: es el único que usa el resto del sistema.
        // Recibe el presupuesto y la ruta donde guardar, y devuelve la ruta final del archivo.

        public string Guardar(Presupuesto presupuesto, string ruta)
        {
            // Validaciones básicas: no se puede generar un pdf y sin presupuesto ni sin ruta
            if (presupuesto == null)
            {
                throw new ArgumentNullException("presupuesto");
            }

            if (string.IsNullOrWhiteSpace(ruta))
            {
                throw new ArgumentException("Elegí una ubicación para guardar el PDF.", "ruta");
            }

            // Convierte la ruta en una ruta completa
            string rutaCompleta = Path.GetFullPath(ruta);

            // Se asegura de que la carpeta exista; si no existe, la crea
            string carpeta = Path.GetDirectoryName(rutaCompleta);
            Directory.CreateDirectory(carpeta);

            // CrearPdf devuelve el contenido del archivo como bytes, y Filewr lo guarda en el disco
            File.WriteAllBytes(rutaCompleta, CrearPdf(presupuesto));
            return rutaCompleta;
        }

        // CrearPdf: arma el archivo pdf completo y lo devuelve como un arreglo de bytes.
        // Un pdf es un archivo de texto con una estructura fija: objetos numerados
        // (catálogo, páginas, fuente) y una tabla al final que dice en qué posición está cada uno.

        private byte[] CrearPdf(Presupuesto presupuesto)
        {
            // 1) Se arma la lista de renglones de texto que van a aparecer en el PDF
            List<string> lineas = CrearLineas(presupuesto);

            // Acá se guardan los contenidos de cada hoja, ya convertidos en bytes
            List<byte[]> contenidoPaginas = new List<byte[]>();

            // Codificación Windows-1252: permite escribir acentos, ñ, ¿ y ¡ en el pdf
            Encoding encodingPdf = Encoding.GetEncoding(1252, EncoderFallback.ReplacementFallback, DecoderFallback.ReplacementFallback);

            // aca se reparten los renglones en hojas, de a 48 por hoja
            for (int inicio = 0; inicio < lineas.Count; inicio += LineasPorPagina)
            {
                // en la última hoja pueden quedar menos de 48 renglones
                int cantidad = Math.Min(LineasPorPagina, lineas.Count - inicio);
                StringBuilder contenido = new StringBuilder();

                // Comandos propios del PDF:
                //   BT        = empieza un bloque de texto
                //   /F1 11 Tf = usa la fuente F1 con tamaño 11
                //   50 790 Td = se posiciona a 50 puntos del borde izquierdo y 790 desde abajo (arriba de la hoja)
                //   14 TL     = deja 14 puntos de separación entre renglones
                contenido.Append("BT\n/F1 11 Tf\n50 790 Td\n14 TL\n");

                for (int indice = 0; indice < cantidad; indice++)
                {
                    // T* = baja al renglón siguiente , el primero no lo necesita
                    if (indice > 0)
                    {
                        contenido.Append("T*\n");
                    }

                    //  Tj = escribe el texto en la hoja
                    contenido.Append("(");
                    contenido.Append(EscaparTextoPdf(lineas[inicio + indice]));
                    contenido.Append(") Tj\n");
                }

                // ET = termina el bloque de texto
                contenido.Append("ET\n");
                contenidoPaginas.Add(encodingPdf.GetBytes(contenido.ToString()));
            }

            //  Se calculan los números de objeto.
            // Numeración: 1 = catálogo, 2 = lista de páginas, y después, por cada hoja,
            // un objeto para la página y otro para su contenido. El último objeto es la fuente.
            int cantidadPaginas = contenidoPaginas.Count;
            int objetoFuente = 3 + cantidadPaginas * 2;
            byte[][] objetos = new byte[objetoFuente][];

            // Objeto 1: el catálogo, que dice cuál es el objeto que contiene las páginas (el 2)
            objetos[0] = Encoding.ASCII.GetBytes("<< /Type /Catalog /Pages 2 0 R >>");

            // Se arma el texto con las referencias a cada página (por ejemplo "3 0 R 5 0 R ")
            StringBuilder referenciasPaginas = new StringBuilder();
            for (int indice = 0; indice < cantidadPaginas; indice++)
            {
                int numeroObjetoPagina = 3 + indice * 2;
                referenciasPaginas.Append(numeroObjetoPagina.ToString(CultureInfo.InvariantCulture));
                referenciasPaginas.Append(" 0 R ");
            }

            // Objeto 2: la lista de páginas y cuántas hay
            objetos[1] = Encoding.ASCII.GetBytes(
                "<< /Type /Pages /Kids [" + referenciasPaginas + "] /Count "
                + cantidadPaginas.ToString(CultureInfo.InvariantCulture) + " >>");

            // 4) Se crean los objetos de cada hoja
            for (int indice = 0; indice < cantidadPaginas; indice++)
            {
                int numeroObjetoPagina = 3 + indice * 2;
                int numeroObjetoContenido = numeroObjetoPagina + 1;

                // Objeto de la página: tamaño A4 (595 x 842 puntos), qué fuente usa y cuál es su contenido
                objetos[numeroObjetoPagina - 1] = Encoding.ASCII.GetBytes(
                    "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] "
                    + "/Resources << /Font << /F1 " + objetoFuente.ToString(CultureInfo.InvariantCulture) + " 0 R >> >> "
                    + "/Contents " + numeroObjetoContenido.ToString(CultureInfo.InvariantCulture) + " 0 R >>");

                // Objeto del contenido: indica su largo en bytes y después el texto entre "stream" y "endstream"
                byte[] prefijo = Encoding.ASCII.GetBytes(
                    "<< /Length " + contenidoPaginas[indice].Length.ToString(CultureInfo.InvariantCulture) + " >>\nstream\n");
                byte[] sufijo = Encoding.ASCII.GetBytes("\nendstream");
                objetos[numeroObjetoContenido - 1] = Concatenar(prefijo, contenidoPaginas[indice], sufijo);
            }

            // Último objeto: la fuente. Helvetica es una de las que todo lector de PDF ya trae incorporadas.
            objetos[objetoFuente - 1] = Encoding.ASCII.GetBytes(
                "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>");

            // 5) Se escribe el archivo completo en memoria (MemoryStream)
            using (MemoryStream documento = new MemoryStream())
            {
                // Encabezado que identifica al archivo como PDF
                EscribirAscii(documento, "%PDF-1.4\n");

                // Se guarda la posición (en bytes) donde empieza cada objeto, porque el PDF lo necesita al final
                List<long> desplazamientos = new List<long>();
                desplazamientos.Add(0L);

                for (int indice = 0; indice < objetos.Length; indice++)
                {
                    desplazamientos.Add(documento.Position);
                    EscribirAscii(documento, (indice + 1).ToString(CultureInfo.InvariantCulture) + " 0 obj\n");
                    documento.Write(objetos[indice], 0, objetos[indice].Length);
                    EscribirAscii(documento, "\nendobj\n");
                }

                // Tabla "xref": un índice con la posición de cada objeto, para que el lector lo encuentre rápido
                long inicioTabla = documento.Position;
                EscribirAscii(documento, "xref\n0 " + desplazamientos.Count.ToString(CultureInfo.InvariantCulture) + "\n");
                EscribirAscii(documento, "0000000000 65535 f \n");

                for (int indice = 1; indice < desplazamientos.Count; indice++)
                {
                    // Cada posición se escribe con 10 dígitos (por eso "D10")
                    EscribirAscii(documento, desplazamientos[indice].ToString("D10", CultureInfo.InvariantCulture) + " 00000 n \n");
                }

                // Cierre del archivo: cantidad de objetos, cuál es el objeto raíz (el 1)
                // y en qué posición empieza la tabla xref
                EscribirAscii(documento,
                    "trailer\n<< /Size " + desplazamientos.Count.ToString(CultureInfo.InvariantCulture) + " /Root 1 0 R >>\n"
                    + "startxref\n" + inicioTabla.ToString(CultureInfo.InvariantCulture) + "\n%%EOF");

                // Devuelve todo el contenido del archivo como bytes
                return documento.ToArray();
            }
        }

        // CrearLineas: arma el texto del presupuesto, renglón por renglón.
        // Acá se decide qué se muestra en el PDF (datos del cliente, menús, servicios, total).
        // Los importes ya vienen calculados desde el modelo Presupuesto.

        private List<string> CrearLineas(Presupuesto presupuesto)
        {
            List<string> lineas = new List<string>();

            // Encabezado y datos del evento y del cliente
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
            lineas.Add(string.Empty); // renglón vacío para separar secciones
            lineas.Add("MENÚS");

            // Una línea por cada menú elegido
            foreach (DetalleDeMenus detalle in presupuesto.Menues)
            {
                // Si el menú tiene una particularidad (celíaco, vegetariano...) se indica; si no, es el principal
                string exclusiones = "menú principal";
                if (detalle.ExclusionDelMenu != TipoDeExclusionDeMenu.Ninguna)
                {
                    exclusiones = "particularidad: " + detalle.ExclusionDelMenu.ToString();
                }

                string descripcion = detalle.MenuElegido.Nombre + " (" + exclusiones + "): "
                    + detalle.CantidadAdultos.ToString(CultureInfo.InvariantCulture) + " adultos, "
                    + detalle.CantidadNinos.ToString(CultureInfo.InvariantCulture) + " niños; subtotal "
                    + detalle.CalcularSubtotal().ToString("C2", CultureInfo.CurrentCulture); // "C2" = formato moneda con 2 decimales
                AgregarLineaEnRenglones(lineas, descripcion);
            }

            // Subtotales y recargos de los menús, ya calculados por el modelo
            lineas.Add("Subtotal menús principales: " + presupuesto.SubtotalMenusPrincipales.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("Adicional menú principal (" + presupuesto.PorcentajeAdicionalMenuPrincipal.ToString("0.##", CultureInfo.CurrentCulture)
                + "%): " + presupuesto.RecargoMenusPrincipales.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("Subtotal menús personalizados: " + presupuesto.SubtotalMenusPersonalizados.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("Adicional menú personalizado (" + presupuesto.PorcentajeAdicionalMenuPersonalizado.ToString("0.##", CultureInfo.CurrentCulture)
                + "%): " + presupuesto.RecargoMenusPersonalizados.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("Total menús: " + presupuesto.CostoMenus.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add(string.Empty);
            lineas.Add("SERVICIOS ADICIONALES");

            // Servicios adicionales: si no hay ninguno, se aclara
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

            // Costo del salón y total final
            lineas.Add("Salón: " + presupuesto.CostoSalon.ToString("C2", CultureInfo.CurrentCulture));
            lineas.Add("TOTAL FINAL: " + presupuesto.TotalFinal.ToString("C2", CultureInfo.CurrentCulture));

            // Las observaciones solo aparecen si el usuario escribió algo
            if (!string.IsNullOrWhiteSpace(presupuesto.Observaciones))
            {
                lineas.Add(string.Empty);
                lineas.Add("OBSERVACIONES");
                AgregarLineaEnRenglones(lineas, presupuesto.Observaciones);
            }

            return lineas;
        }

        // AgregarLineaEnRenglones: si un texto es muy largo y no entra en el ancho de la hoja,
        // lo corta en varios renglones (de hasta 88 caracteres), cortando en un espacio
        // para no partir palabras por la mitad.
        private void AgregarLineaEnRenglones(List<string> destino, string texto)
        {
            if (texto == null)
            {
                texto = string.Empty;
            }

            // Se reemplazan los saltos de línea por espacios, para tratar todo como un solo texto
            string textoEnUnaLinea = texto.Replace('\r', ' ').Replace('\n', ' ');
            const int maximoCaracteres = 88;

            while (textoEnUnaLinea.Length > maximoCaracteres)
            {
                // Busca el último espacio antes del límite para cortar ahí
                int corte = textoEnUnaLinea.LastIndexOf(' ', maximoCaracteres);
                if (corte <= 0)
                {
                    // Si no hay ningún espacio (una palabra larga), se corta justo en el límite
                    corte = maximoCaracteres;
                }

                destino.Add(textoEnUnaLinea.Substring(0, corte));
                textoEnUnaLinea = textoEnUnaLinea.Substring(corte).TrimStart();
            }

            // Lo que queda (ya corto) se agrega como último renglón
            destino.Add(textoEnUnaLinea);
        }

        // EscaparTextoPdf: en un PDF, la barra \ y los paréntesis ( ) tienen un significado especial
        // Si aparecen dentro de un texto, hay que ponerles una barra adelante para que no rompan el archivo.

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

        // Auxiliar: junta tres arreglos de bytes en uno solo, uno detrás del otro
        private byte[] Concatenar(byte[] primero, byte[] segundo, byte[] tercero)
        {
            byte[] resultado = new byte[primero.Length + segundo.Length + tercero.Length];
            Buffer.BlockCopy(primero, 0, resultado, 0, primero.Length);
            Buffer.BlockCopy(segundo, 0, resultado, primero.Length, segundo.Length);
            Buffer.BlockCopy(tercero, 0, resultado, primero.Length + segundo.Length, tercero.Length);
            return resultado;
        }

        // Auxiliar: convierte un texto a bytes ASCII y lo escribe en el archivo que se está armando
        private void EscribirAscii(Stream destino, string texto)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(texto);      //ASCII es un estándar que representa caracteres mediante valores numéricos almacenados en bytes.
            destino.Write(bytes, 0, bytes.Length);
        }
    }
}