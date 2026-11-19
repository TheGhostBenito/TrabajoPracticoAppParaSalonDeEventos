(function () {
  "use strict";

  // Datos de ejemplo. "memoria" son los valores iniciales del proyecto;
  // "local" simula el archivo guardado (incluye un ítem extra por catálogo).
  var DATOS_BASE = {
    salones: [
      { id: 1, nombre: "Salón Imperio", ubicacion: "Centro", costoBase: 150 },
      { id: 2, nombre: "Salón Aurora", ubicacion: "Norte", costoBase: 120 },
      { id: 3, nombre: "Salón Rivera", ubicacion: "Oeste", costoBase: 95 }
    ],
    menues: [
      { id: 1, nombre: "Menú Clásico", costoAdulto: 28, costoNino: 14 },
      { id: 2, nombre: "Menú Premium", costoAdulto: 42, costoNino: 21 },
      { id: 3, nombre: "Menú Infantil", costoAdulto: 20, costoNino: 18 }
    ],
    servicios: [
      { id: 1, descripcion: "Música en vivo", costoDelServicio: 250 },
      { id: 2, descripcion: "Decoración", costoDelServicio: 180 },
      { id: 3, descripcion: "Catering especial", costoDelServicio: 320 }
    ]
  };

  var DATOS_LOCALES = {
    salones: DATOS_BASE.salones.concat([
      { id: 4, nombre: "Salón Jardín (guardado)", ubicacion: "Sur", costoBase: 88 }
    ]),
    menues: DATOS_BASE.menues.concat([
      { id: 4, nombre: "Menú Gourmet (guardado)", costoAdulto: 55, costoNino: 27 }
    ]),
    servicios: DATOS_BASE.servicios.concat([
      { id: 4, descripcion: "Fotografía (guardado)", costoDelServicio: 210 }
    ])
  };

  var CLAVE_MODO = "eventosDemo.modo";
  var CLAVE_HISTORIAL = "eventosDemo.presupuestos";

  var modo = leer(CLAVE_MODO, "memoria") === "local" ? "local" : "memoria";
  var historial = leer(CLAVE_HISTORIAL, []);
  if (!Array.isArray(historial)) { historial = []; }

  var fmt = new Intl.NumberFormat("es-AR", { style: "currency", currency: "ARS" });

  function leer(clave, porDefecto) {
    try {
      var valor = localStorage.getItem(clave);
      return valor === null ? porDefecto : JSON.parse(valor);
    } catch (error) {
      return porDefecto;
    }
  }

  function escribir(clave, valor) {
    try { localStorage.setItem(clave, JSON.stringify(valor)); } catch (error) { /* modo privado */ }
  }

  function catalogos() {
    return modo === "local" ? DATOS_LOCALES : DATOS_BASE;
  }

  function dinero(valor) {
    return fmt.format(isNaN(valor) ? 0 : valor);
  }

  function num(id, porDefecto) {
    var valor = parseFloat(document.getElementById(id).value);
    return isNaN(valor) ? porDefecto : valor;
  }

  function esc(texto) {
    return String(texto === undefined || texto === null ? "" : texto)
      .replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;")
      .replace(/"/g, "&quot;").replace(/'/g, "&#39;");
  }

  function toast(mensaje) {
    var elemento = document.getElementById("toast");
    elemento.textContent = mensaje;
    elemento.classList.add("show");
    window.clearTimeout(toast.timer);
    toast.timer = window.setTimeout(function () { elemento.classList.remove("show"); }, 2600);
  }

  function irA(nombre) {
    var botones = document.querySelectorAll(".tab-btn");
    for (var i = 0; i < botones.length; i++) {
      var activo = botones[i].getAttribute("data-tab") === nombre;
      botones[i].classList.toggle("active", activo);
      document.getElementById("panel-" + botones[i].getAttribute("data-tab")).classList.toggle("active", activo);
    }
  }

  function renderModo() {
    var esLocal = modo === "local";
    document.getElementById("modoSwitch").checked = esLocal;
    document.getElementById("modoTexto").innerHTML = esLocal
      ? "<b>Usando archivo local</b>guardar"
      : "<b>Usando memoria</b>no guardar";
    document.getElementById("modoHint").textContent = esLocal
      ? "Los catálogos se leen y escriben en XML."
      : "Los catálogos viven solo durante la sesión.";
    document.getElementById("statModo").textContent = esLocal ? "Archivo local" : "Memoria";

    var sufijo = esLocal ? "(modo archivo local)" : "(modo memoria: se reinicia al cerrar)";
    document.getElementById("subSalones").textContent = "Catálogo " + sufijo + ".";
    document.getElementById("subMenues").textContent = "Cada menú tiene precio por adulto y por niño " + sufijo + ".";
    document.getElementById("subServicios").textContent = "Se pueden tomar del catálogo o escribir uno personalizado " + sufijo + ".";
  }

  function renderTablas() {
    var datos = catalogos();

    document.getElementById("tablaSalones").innerHTML = datos.salones.map(function (s) {
      return "<tr><td>" + s.id + "</td><td>" + esc(s.nombre) + "</td><td>" + esc(s.ubicacion) +
        "</td><td class='num'>" + dinero(s.costoBase) + "</td></tr>";
    }).join("");

    document.getElementById("tablaMenues").innerHTML = datos.menues.map(function (m) {
      return "<tr><td>" + m.id + "</td><td>" + esc(m.nombre) + "</td><td class='num'>" +
        dinero(m.costoAdulto) + "</td><td class='num'>" + dinero(m.costoNino) + "</td></tr>";
    }).join("");

    document.getElementById("tablaServicios").innerHTML = datos.servicios.map(function (s) {
      return "<tr><td>" + s.id + "</td><td>" + esc(s.descripcion) +
        "</td><td class='num'>" + dinero(s.costoDelServicio) + "</td></tr>";
    }).join("");

    document.getElementById("statCatalogos").textContent =
      datos.salones.length + " · " + datos.menues.length + " · " + datos.servicios.length;
  }

  function renderFormulario() {
    var datos = catalogos();
    var salonActual = document.getElementById("selSalon").value;
    var menuActual = document.getElementById("selMenu").value;
    var serviciosMarcados = {};
    var viejos = document.querySelectorAll("input[name='servicio']");
    for (var i = 0; i < viejos.length; i++) {
      if (viejos[i].checked) { serviciosMarcados[viejos[i].value] = true; }
    }

    document.getElementById("selSalon").innerHTML = datos.salones.map(function (s) {
      return "<option value='" + s.id + "'>" + esc(s.nombre) + " — " + dinero(s.costoBase) + "/h</option>";
    }).join("");

    document.getElementById("selMenu").innerHTML = datos.menues.map(function (m) {
      return "<option value='" + m.id + "'>" + esc(m.nombre) + " · A " + dinero(m.costoAdulto) +
        " / N " + dinero(m.costoNino) + "</option>";
    }).join("");

    document.getElementById("listaServicios").innerHTML = datos.servicios.map(function (s) {
      return "<label><input type='checkbox' name='servicio' value='" + s.id + "'" +
        (serviciosMarcados[s.id] ? " checked" : "") + "> " + esc(s.descripcion) +
        " (" + dinero(s.costoDelServicio) + ")</label>";
    }).join("");

    var selSalon = document.getElementById("selSalon");
    var selMenu = document.getElementById("selMenu");
    if (salonActual) {
      selSalon.value = salonActual;
      if (selSalon.value !== salonActual) { selSalon.selectedIndex = 0; }
    }
    if (menuActual) {
      selMenu.value = menuActual;
      if (selMenu.value !== menuActual) { selMenu.selectedIndex = 0; }
    }
  }

  function renderHistorial() {
    var contenedor = document.getElementById("historialContenido");
    document.getElementById("statHistorial").textContent = String(historial.length);

    if (historial.length === 0) {
      contenedor.innerHTML = "<div class='empty'>Todavía no hay presupuestos guardados.<br>Armá uno en la pestaña “Presupuesto”.</div>";
      return;
    }

    contenedor.innerHTML = "<div class='table-wrap'><table class='data'><thead><tr>" +
      "<th>ID</th><th>Fecha</th><th>Contacto</th><th>Evento</th><th>Ítems</th><th class='num'>Total</th>" +
      "</tr></thead><tbody>" + historial.map(function (p) {
        return "<tr><td>#" + p.id + "</td><td>" + esc(p.fecha) + "</td><td>" + esc(p.contacto) +
          "</td><td>" + esc(p.evento) + "</td><td>" + p.items + "</td><td class='num'>" +
          dinero(p.total) + "</td></tr>";
      }).join("") + "</tbody></table></div>";
  }

  function calcular() {
    var datos = catalogos();
    var salon = datos.salones.filter(function (s) { return String(s.id) === document.getElementById("selSalon").value; })[0];
    var menu = datos.menues.filter(function (m) { return String(m.id) === document.getElementById("selMenu").value; })[0];
    var horas = Math.max(1, num("inpDuracion", 1));
    var adultos = Math.max(0, num("inpAdultos", 0));
    var ninos = Math.max(0, num("inpNinos", 0));
    var exclusion = document.getElementById("selExclusion").value;

    var salonSubtotal = salon ? salon.costoBase * horas : 0;
    var baseMenu = menu ? (adultos * menu.costoAdulto + ninos * menu.costoNino) : 0;
    var porcentaje = exclusion === "Ninguna" ? 0.10 : 0.05;
    var menuSubtotal = baseMenu * (1 + porcentaje);

    var serviciosSubtotal = 0;
    var cantidadServicios = 0;
    var marcados = document.querySelectorAll("input[name='servicio']:checked");
    for (var i = 0; i < marcados.length; i++) {
      var servicio = datos.servicios.filter(function (s) { return String(s.id) === marcados[i].value; })[0];
      if (servicio) { serviciosSubtotal += servicio.costoDelServicio; }
      cantidadServicios++;
    }

    var total = salonSubtotal + menuSubtotal + serviciosSubtotal;
    document.getElementById("totalValor").textContent = dinero(total);
    document.getElementById("desglose").textContent =
      "Salón " + dinero(salonSubtotal) + " · Menú " + dinero(menuSubtotal) +
      " · Servicios (" + cantidadServicios + ") " + dinero(serviciosSubtotal);

    return {
      total: total,
      items: (salon ? 1 : 0) + (baseMenu > 0 ? 1 : 0) + cantidadServicios,
      desglose: {
        salon: salonSubtotal,
        menu: menuSubtotal,
        servicios: serviciosSubtotal
      }
    };
  }

  function guardar() {
    var resultado = calcular();
    var contacto = document.getElementById("inpContacto").value.trim() || "Sin nombre";
    var evento = document.getElementById("inpEvento").value.trim() || "Evento";
    var proximoId = 1;
    for (var i = 0; i < historial.length; i++) {
      if (historial[i].id >= proximoId) { proximoId = historial[i].id + 1; }
    }

    historial.unshift({
      id: proximoId,
      fecha: new Date().toLocaleString("es-AR"),
      contacto: contacto,
      evento: evento,
      items: resultado.items,
      total: resultado.total,
      desglose: resultado.desglose
    });

    escribir(CLAVE_HISTORIAL, historial);
    renderHistorial();
    irA("historial");
    toast("Presupuesto #" + proximoId + " guardado en el historial");
  }

  function init() {
    var botones = document.querySelectorAll(".tab-btn");
    for (var i = 0; i < botones.length; i++) {
      botones[i].addEventListener("click", function () {
        irA(this.getAttribute("data-tab"));
      });
    }

    var enlaces = document.querySelectorAll("[data-go]");
    for (var j = 0; j < enlaces.length; j++) {
      enlaces[j].addEventListener("click", function () { irA(this.getAttribute("data-go")); });
    }

    document.getElementById("modoSwitch").addEventListener("change", function () {
      modo = this.checked ? "local" : "memoria";
      escribir(CLAVE_MODO, modo);
      renderModo();
      renderTablas();
      renderFormulario();
      calcular();
      toast(modo === "local" ? "Catálogos leyendo desde archivo local" : "Catálogos en memoria");
    });

    var ids = ["selSalon", "inpDuracion", "selMenu", "inpAdultos", "inpNinos", "selExclusion"];
    ids.forEach(function (id) {
      document.getElementById(id).addEventListener("input", calcular);
      document.getElementById(id).addEventListener("change", calcular);
    });
    document.getElementById("listaServicios").addEventListener("change", calcular);

    document.getElementById("btnGuardar").addEventListener("click", guardar);
    document.getElementById("btnLimpiarHistorial").addEventListener("click", function () {
      if (window.confirm("¿Borrar todos los presupuestos guardados en esta demo?")) {
        historial = [];
        escribir(CLAVE_HISTORIAL, historial);
        renderHistorial();
        toast("Historial de la demo borrado");
      }
    });

    renderModo();
    renderTablas();
    renderFormulario();
    renderHistorial();
    calcular();
  }

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", init);
  } else {
    init();
  }
})();
