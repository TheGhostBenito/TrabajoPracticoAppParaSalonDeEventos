# Correcciones sobre el proyecto original

> Informe completo en PDF: `Informe_de_cambios.pdf` (en la raíz del repositorio).

Se mantuvieron los aciertos: separación en capas, inyección por constructor,
`IRepository<T>` / `IService<T,DTO>`, el `PresupuestoRepository` Singleton con
caché en memoria, la vista sin cálculos, el PDF sin librerías externas, el
historial y las validaciones del modelo.

## 1. Herencia y polimorfismo en los ítems

- Nueva clase abstracta `models/ItemPresupuesto` con `CalcularSubtotal()` y
  `ObtenerDetalle()`.
- Subclases: `ItemSalon`, `ItemMenu` (conserva particularidades y precios por
  adulto/niño) e `ItemServicio` (catálogo o personalizado).
- `Presupuesto` guarda una sola `List<ItemPresupuesto>`. `TotalFinal` suma todos
  los ítems por polimorfismo. Las propiedades viejas (`CostoSalon`, `Menues`,
  `ServiciosAdicionales`, etc.) se derivan de esa lista para no romper la vista.
- `PresupuestoDto.Items` y el PDF recorren la misma lista polimórfica.

**Para agregar un ítem nuevo:** crear la subclase de `ItemPresupuesto`, su
`IConstructorItemPresupuesto` y su `IItemDtoMapper`, y registrarlos en
`FabricaPresupuestoService`. No se tocan `Presupuesto`, `PresupuestoService`,
los DTOs existentes ni el PDF.

## 2. PresupuestoService más chico y con menos responsabilidades

`PresupuestoService` quedó como orquestador (~250 líneas, antes tenía 753). Se extrajeron:

- `PresupuestoRequestValidator`: reglas generales.
- `ConstructorItemSalon` / `ConstructorItemMenu` / `ConstructorItemServicio`:
  preparación, validación y armado de cada tipo de ítem.
- `ContextoPresupuesto`: búsquedas en catálogos, incluidos ítems de un
  presupuesto que se está modificando.
- `PresupuestoCatalogosService`: listados disponibles para el formulario.
- `PresupuestoDtoMapper` + `IItemDtoMapper`: conversión a DTO.
- `FabricaPresupuestoService`: armado de todas las piezas.

Además, `CalcularEstimacion` usa el mismo modelo que se guarda, así que dejó de
haber dos fórmulas de cálculo distintas.

## 3. Interfaces

- `ISalonService`, `IMenuService`, `IServiciosAdicionalesService`,
  `IPresupuestoService` e `IPresupuestoPdfService` ahora se usan de verdad:
  controllers y services dependen de ellas.
- Todas están agregadas al `.csproj`.
- Se borraron `ISalonRepository`, `IMenuRepository`,
  `IServiciosAdicionalesRepository` e `IPresupuestoRepository`: duplicaban
  `IRepository<T>` (o tenían nombres de métodos incompatibles) y nadie las usaba.

## 4. Catálogos sin servidor (guardado local)

- Se eliminó la dependencia de MySQL: `App.config` solo tiene `ModoCatalogos`
  (`Memoria` por defecto / `Local`) y ya no hay cadena de conexión.
- `repository/RepositorioCatalogoLocal<T>` persiste cada catálogo en un archivo
  XML usando `System.Xml.Linq` (incluido en .NET). No hay servidor, XAMPP,
  driver de MySQL ni paquetes NuGet necesarios para guardar.
- Los archivos se crean en `%LOCALAPPDATA%\EventosPresupuestos`
  (`salones.xml`, `menues.xml`, `servicios.xml`) recién cuando se activa el modo
  guardado. Si no existen, se inicializan con los datos de ejemplo que ya
  tenía el proyecto.
- La escritura es atómica (se guarda un `.tmp` y se reemplaza el archivo). Si un
  archivo se corrompe, se renombra con fecha y la app arranca con los datos
  iniciales en lugar de fallar.
- Los archivos MySQL (`ConexionBD.cs` y los tres repositorios MySQL) se
  eliminaron del repositorio. No se compilan, no se usan y `packages.config`
  quedó vacío.

## 5. Interruptor de guardado en la solapa izquierda

- Al final del menú lateral se agregó **Datos del catálogo** con un botón interruptor: **Usando memoria (no guardar)** / **Usando archivo local (guardar)**.
- `ServicioModoDatos` guarda el modo elegido y `RepositorioCatalogoConmutable<T>` delega cada operación al repositorio de memoria o al de archivo local. El cambio se aplica en caliente, sin reconstruir controllers, services ni formularios.
- Si hay un presupuesto en edición, se pide confirmación y se cierra ese formulario para no mezclar catálogos. Ya no hay prueba de conexión: guardar siempre funciona porque el destino es un archivo local.
- El cambio alcanza solo a los catálogos (salones, menús y servicios). Los presupuestos se guardan siempre en local, sin importar la posición del interruptor.

## 6. Historial de presupuestos en archivo local

- `repository/PresupuestoRepository.cs` sigue siendo un Singleton e
  `IRepository<Presupuesto>`, pero ahora es híbrido: mantiene la caché en memoria
  y, además, lee y escribe `presupuestos.xml` en
  `%LOCALAPPDATA%\EventosPresupuestos`. No cambió su API pública, así que el
  service, los controllers, las vistas y el PDF no se modificaron.
- Al crear, modificar o eliminar un presupuesto se actualiza el archivo. El
  nuevo id se calcula como `máximo(id) + 1`, por lo que no se repiten números al
  reiniciar la aplicación.
- `repository/MapeadorPresupuestoXml.cs` (archivo nuevo) serializa el grafo
  completo: datos del evento, contacto, porcentajes, `RutaPdf` y la lista
  polimórfica de ítems con un discriminador `tipo` (`Salon`, `Menu`,
  `Servicio`). Se agregó al `.csproj`.
- Cada presupuesto guarda un **snapshot** del salón, menú y servicio usados
  (nombre, descripción, precios y particularidad), no solo sus ids. Así, si
  después cambia o se borra un catálogo, los presupuestos viejos conservan su
  detalle y su total original.
- Para tipos de ítem aún no mapeados existe un fallback `ItemGenerico` que
  conserva descripción, detalle e importe.
- Se reutilizó `CatalogosLocales.ObtenerRutaArchivo(...)` para que catálogos y
  presupuestos compartan carpeta. La escritura es atómica (`.tmp` + reemplazo)
  y, si el XML se corrompe, se guarda una copia `.error-yyyyMMddHHmmss` y se
  arranca con el historial vacío en lugar de fallar.
- Este guardado **no está atado al interruptor**: los catálogos pueden estar en
  memoria o en archivo, pero el historial de presupuestos siempre persiste.

## Verificación

Se compiló el proyecto con `dotnet build "proyecto integrador.sln" --no-restore`:
0 errores y 0 advertencias. El `.exe` final no necesita DLLs de MySql. También se
ejecutaron pruebas rápidas de estimación, alta, historial, depuración de menús y
generación de PDF; los importes coinciden con los esperados. También se probó el
conmutador: con 3 salones en memoria, al pasar a archivo local el proxy creó el
XML con esos 3 salones, agregó uno nuevo (id 4), y al volver a memoria el proxy
siguió devolviendo los 3 originales. Al recrear el repositorio local leyó 4
salones desde el archivo, verificando que el guardado sobrevive al reinicio.

La persistencia del historial se verificó con una ruta temporal (sin tocar los
datos reales): `id=1 total=734 items=3 exclusion=Celiaco contacto=Ana
archivo=True trasBorrar=0`. Es decir: se creó el presupuesto con sus tres ítems
polimórficos, se recreó el repositorio leyendo desde el XML (conservando total,
particularidad y contacto) y, tras eliminarlo y volver a cargar, el historial
quedó vacío.

El proyecto apunta ahora a **.NET Framework 4.8** (incluido en Windows 10/11),
así que se abre y ejecuta desde Visual Studio sin descargar runtimes ni paquetes.
Además se generó `docs/assets/descargas/PresupuestosEventos-v1.0-Windows.zip`
con el `.exe` de Release, su `.config` y un `LEEME.txt`, listo para publicar y
descargar desde GitHub Pages.
