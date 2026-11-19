# Presupuestos para Eventos

Aplicación WinForms en C# para armar presupuestos de eventos con salón, menús y servicios adicionales, calcular totales, guardar el historial en el equipo y exportar a PDF.

## Ejecutar desde Visual Studio

**Requisitos:** Windows, Visual Studio 2019 o 2022 con la carga de trabajo *Desarrollo de escritorio con .NET* y .NET Framework 4.8 (incluido en Windows 10/11).

1. Clonar o descargar este repositorio.
2. Abrir `proyecto integrador.sln`.
3. Presionar **F5** o elegir *Iniciar depuración*.

No hay paquetes NuGet que restaurar, ni MySQL, XAMPP o servidores externos. El proyecto ya apunta a .NET Framework 4.8.

## Sin compilar

En la página de GitHub Pages hay un ZIP con el ejecutable listo para Windows:

```text
https://theghostbenito.github.io/TrabajoPracticoAppParaSalonDeEventos/
```

## Estructura

```text
models/       entidades del dominio e ítems polimórficos
dto/          objetos de transferencia entre capas
Service/      interfaces y lógica de negocio
controller/   adaptadores entre la UI y los servicios
repository/   persistencia en memoria y XML local
pantallas/    formularios WinForms
docs/         sitio de GitHub Pages, documentación y descargas
```

## Datos guardados

Los archivos se crean en:

```text
%LOCALAPPDATA%\EventosPresupuestos\
```

- `salones.xml`, `menues.xml`, `servicios.xml`: catálogos (según el interruptor “Datos del catálogo”).
- `presupuestos.xml`: historial de presupuestos (siempre).

## Contacto

Para consultas sobre versiones personalizadas o modificaciones concretas:

```text
alteffjoaquin@gmail.com
tutuluna580@gmail.com
```
