(function () {
  "use strict";

  var CORREO = "alteffjoaquin@gmail.com";

  function toast(mensaje) {
    var elemento = document.getElementById("toast");
    elemento.textContent = mensaje;
    elemento.classList.add("show");
    window.clearTimeout(toast.timer);
    toast.timer = window.setTimeout(function () { elemento.classList.remove("show"); }, 2600);
  }

  function copiar() {
    function ok() { toast("Correo copiado al portapapeles"); }
    function error() { toast("No se pudo copiar: seleccioná el correo manualmente"); }

    if (navigator.clipboard && window.isSecureContext) {
      navigator.clipboard.writeText(CORREO).then(ok).catch(function () { fallback(); });
    } else {
      fallback();
    }

    function fallback() {
      var aux = document.createElement("textarea");
      aux.value = CORREO;
      aux.setAttribute("readonly", "");
      aux.style.position = "fixed";
      aux.style.opacity = "0";
      document.body.appendChild(aux);
      aux.select();
      try {
        var copiado = document.execCommand("copy");
        copiado ? ok() : error();
      } catch (e) {
        error();
      }
      document.body.removeChild(aux);
    }
  }

  function enviar(evento) {
    evento.preventDefault();

    var nombre = document.getElementById("cf-nombre").value.trim();
    var correo = document.getElementById("cf-correo").value.trim();
    var tipo = document.getElementById("cf-tipo").value;
    var mensaje = document.getElementById("cf-mensaje").value.trim();

    var cuerpo = [
      "Hola:",
      "",
      mensaje,
      "",
      "---",
      "Nombre: " + nombre,
      "Correo: " + correo,
      "Modificación solicitada: " + tipo
    ].join("\n");

    var asunto = "Consulta sobre versión personalizada — " + tipo;
    var url = "mailto:" + CORREO +
      "?subject=" + encodeURIComponent(asunto) +
      "&body=" + encodeURIComponent(cuerpo);

    window.location.href = url;
    toast("Abriendo tu cliente de correo…");
  }

  document.getElementById("copiarCorreo").addEventListener("click", copiar);
  document.getElementById("formularioContacto").addEventListener("submit", enviar);
})();
