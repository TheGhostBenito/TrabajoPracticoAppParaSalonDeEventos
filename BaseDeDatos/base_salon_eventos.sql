-- =====================================================
--  Base de datos: salon_eventos
--  Sistema de presupuestos para eventos
--  Para importar en phpMyAdmin (pestaña "Importar")
-- =====================================================

-- Crea la base de datos (si no existe) y la deja seleccionada
CREATE DATABASE IF NOT EXISTS salon_eventos
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

USE salon_eventos;

-- -----------------------------------------------------
-- Tabla SALON  (clase Salon)
-- -----------------------------------------------------
CREATE TABLE salon (
  id         INT AUTO_INCREMENT PRIMARY KEY,
  nombre     VARCHAR(100)  NOT NULL,
  ubicacion  VARCHAR(150)  NOT NULL,
  costo_base DECIMAL(10,2) NOT NULL
) ENGINE=InnoDB;

-- -----------------------------------------------------
-- Tabla MENU_COMIDA  (clase MenuDeComida)
-- -----------------------------------------------------
CREATE TABLE menu_comida (
  id               INT AUTO_INCREMENT PRIMARY KEY,
  nombre           VARCHAR(100)  NOT NULL,
  costo_por_adulto DECIMAL(10,2) NOT NULL,
  costo_por_nino   DECIMAL(10,2) NOT NULL
) ENGINE=InnoDB;

-- -----------------------------------------------------
-- Tabla SERVICIO_ADICIONAL  (clase ServiciosAdicionales)
-- -----------------------------------------------------
CREATE TABLE servicio_adicional (
  id          INT AUTO_INCREMENT PRIMARY KEY,
  descripcion VARCHAR(150)  NOT NULL,
  monto_fijo  DECIMAL(10,2) NOT NULL
) ENGINE=InnoDB;

-- -----------------------------------------------------
-- Tabla PRESUPUESTO  (clases Presupuesto + Evento)
-- Los datos del evento (horas, adultos, niños) van acá
-- porque cada presupuesto tiene un solo evento.
-- Los totales NO se guardan: el programa los calcula.
-- -----------------------------------------------------
CREATE TABLE presupuesto (
  id                                      INT AUTO_INCREMENT PRIMARY KEY,
  fecha_creacion                          DATETIME      NOT NULL,
  fecha_evento                            DATE          NOT NULL,
  hora_inicio                             TIME          NOT NULL,
  nombre_contacto                         VARCHAR(100)  NOT NULL,
  email_contacto                          VARCHAR(100)  NOT NULL,
  telefono_contacto                       VARCHAR(30)   NOT NULL,
  ubicacion_evento                        VARCHAR(150)  NOT NULL,
  observaciones                           TEXT          NULL,
  salon_id                                INT           NOT NULL,
  duracion_horas                          INT           NOT NULL,
  cantidad_adultos                        INT           NOT NULL,
  cantidad_ninos                          INT           NOT NULL,
  porcentaje_adicional_menu_principal     DECIMAL(5,2)  NOT NULL DEFAULT 0,
  porcentaje_adicional_menu_personalizado DECIMAL(5,2)  NOT NULL DEFAULT 0,
  ruta_pdf                                VARCHAR(255)  NULL,
  FOREIGN KEY (salon_id) REFERENCES salon(id)
) ENGINE=InnoDB;

-- -----------------------------------------------------
-- Tabla DETALLE_MENU  (clase DetalleDeMenus)
-- Cada fila: un menú elegido dentro de un presupuesto.
-- "exclusion" es el enum TipoDeExclusionDeMenu.
-- -----------------------------------------------------
CREATE TABLE detalle_menu (
  id               INT AUTO_INCREMENT PRIMARY KEY,
  presupuesto_id   INT NOT NULL,
  menu_id          INT NOT NULL,
  cantidad_adultos INT NOT NULL,
  cantidad_ninos   INT NOT NULL,
  exclusion ENUM('Ninguna','Celiaco','Vegetariano','Diabetico','Vegano')
            NOT NULL DEFAULT 'Ninguna',
  FOREIGN KEY (presupuesto_id) REFERENCES presupuesto(id) ON DELETE CASCADE,
  FOREIGN KEY (menu_id)        REFERENCES menu_comida(id)
) ENGINE=InnoDB;

-- -----------------------------------------------------
-- Tabla PRESUPUESTO_SERVICIO
-- Servicios adicionales de un presupuesto.
-- Guarda la descripción y el monto de ese momento.
-- servicio_id queda vacío (NULL) si el servicio fue
-- escrito a mano y no sale del listado.
-- -----------------------------------------------------
CREATE TABLE presupuesto_servicio (
  id             INT AUTO_INCREMENT PRIMARY KEY,
  presupuesto_id INT NOT NULL,
  servicio_id    INT NULL,
  descripcion    VARCHAR(150)  NOT NULL,
  monto          DECIMAL(10,2) NOT NULL,
  FOREIGN KEY (presupuesto_id) REFERENCES presupuesto(id) ON DELETE CASCADE,
  FOREIGN KEY (servicio_id)    REFERENCES servicio_adicional(id) ON DELETE SET NULL
) ENGINE=InnoDB;

-- -----------------------------------------------------
-- Datos de ejemplo (los mismos que ya usa el programa)
-- -----------------------------------------------------
INSERT INTO salon (nombre, ubicacion, costo_base) VALUES
  ('Salon A', 'Sin definir', 100.00),
  ('Salon B', 'Sin definir', 150.00),
  ('Salon C', 'Sin definir', 200.00);

INSERT INTO menu_comida (nombre, costo_por_adulto, costo_por_nino) VALUES
  ('Menu A', 50.00, 30.00),
  ('Menu B', 75.00, 45.00),
  ('Menu C', 100.00, 60.00);
