(function () {
  "use strict";

  var CORREO_PREDETERMINADO = "alteffjoaquin@gmail.com";

  function toast(mensaje) {
    var elemento = document.getElementById("toast");
    elemento.textContent = mensaje;
    elemento.classList.add("show");
    window.clearTimeout(toast.timer);
    toast.timer = window.setTimeout(function () { elemento.classList.remove("show"); }, 2600);
  }

  function copiar(correo) {
    function ok() { toast("Correo copiado al portapapeles"); }
    function error() { toast("No se pudo copiar: seleccioná el correo manualmente"); }

    if (navigator.clipboard && window.isSecureContext) {
      navigator.clipboard.writeText(correo).then(ok).catch(function () { fallback(); });
    } else {
      fallback();
    }

    function fallback() {
      var aux = document.createElement("textarea");
      aux.value = correo;
      aux.setAttribute("readonly", "");
      aux.style.position = "fixed";
      aux.style.opacity = "0";
      document.body.appendChild(aux);
      aux.select();
      try {
        document.execCommand("copy") ? ok() : error();
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
    var destino = document.getElementById("cf-destino").value || CORREO_PREDETERMINADO;
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
    var url = "mailto:" + destino +
      "?subject=" + encodeURIComponent(asunto) +
      "&body=" + encodeURIComponent(cuerpo);

    window.location.href = url;
    toast("Abriendo tu cliente de correo…");
  }

  var botones = document.querySelectorAll("[data-copiar]");
  for (var i = 0; i < botones.length; i++) {
    botones[i].addEventListener("click", function () {
      copiar(this.getAttribute("data-copiar"));
    });
  }

  document.getElementById("formularioContacto").addEventListener("submit", enviar);
})();
