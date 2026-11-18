# Correcciones sobre el proyecto original

> Informe completo en PDF: `Informe_de_cambios.pdf` (en la raíz del repositorio).

Se mantuvieron los aciertos: separación en capas, inyección por constructor,
`IRepository<T>` / `IService<T,DTO>`, el `PresupuestoRepository` Singleton en
memoria, la vista sin cálculos, el PDF sin librerías externas, el historial y
las validaciones del modelo.

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
- Los repositorios MySQL quedaron en el disco como referencia, pero ya no se
  compilan ni se usan: se sacaron del `.csproj` y `packages.config` quedó vacío.

## 5. Interruptor de guardado en la solapa izquierda

- Al final del menú lateral se agregó **Datos del catálogo** con un botón interruptor: **Usando memoria (no guardar)** / **Usando archivo local (guardar)**.
- `ServicioModoDatos` guarda el modo elegido y `RepositorioCatalogoConmutable<T>` delega cada operación al repositorio de memoria o al de archivo local. El cambio se aplica en caliente, sin reconstruir controllers, services ni formularios.
- Si hay un presupuesto en edición, se pide confirmación y se cierra ese formulario para no mezclar catálogos. Ya no hay prueba de conexión: guardar siempre funciona porque el destino es un archivo local.
- El cambio alcanza solo a los catálogos (salones, menús y servicios). Los presupuestos siguen en `PresupuestoRepository` (Singleton en memoria) durante la sesión.

## Verificación

Se compiló el proyecto con `dotnet build "proyecto integrador.sln"` sin paquetes NuGet: 0 errores y 0 advertencias. El `.exe` final no necesita DLLs de MySql. También se ejecutaron
pruebas rápidas de estimación, alta, historial, depuración de menús y generación
de PDF; los importes coinciden con los esperados. También se probó el
conmutador: con 3 salones en memoria, al pasar a archivo local el proxy creó el
XML con esos 3 salones, agregó uno nuevo (id 4), y al volver a memoria el proxy
siguió devolviendo los 3 originales. Al recrear el repositorio local leyó 4
salones desde el archivo, verificando que el guardado sobrevive al reinicio.
