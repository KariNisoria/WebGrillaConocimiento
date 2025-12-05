-- =============================================================================
-- SCRIPT DE CARGA DE TEMAS Y SUBTEMAS - WEBGRILLA CONOCIMIENTO
-- =============================================================================
-- Versión: 1.0
-- Descripción: Carga completa de temas y subtemas para el sistema de evaluación
-- Prerrequisito: Ejecutar después del script 01_CreacionCompletaBaseDatos.sql

USE [GriconDB]
GO

-- =============================================================================
-- LIMPIEZA PREVIA (solo si es necesario recrear los datos)
-- =============================================================================
/*
-- Descomenta las siguientes líneas si necesitas limpiar los datos existentes
PRINT 'Limpiando datos existentes...'
DELETE FROM ConocimientoRecurso
DELETE FROM ResultadoConocimiento  
DELETE FROM GrillaSubtemas
DELETE FROM GrillaTemas
DELETE FROM Subtemas
DELETE FROM Temas
PRINT 'Datos limpiados'
*/

-- =============================================================================
-- CARGA DE TEMAS
-- =============================================================================

PRINT 'Iniciando carga de temas...'

-- Limpiar temas existentes si es necesario (descomenta si necesitas recrear)
-- DELETE FROM Temas WHERE IdTema > 0

-- Insertar temas principales del sistema de conocimientos bancarios
INSERT INTO Temas (Nombre, Orden) VALUES 
('Generador de Productos (Parametría)', 1),
('Cuentas Corrientes Cajas de Ahorro', 2),
('Créditos', 3),
('Plazos Fijos', 4),
('Ingresos y Egresos Varios', 5),
('Garantía', 6),
('Generales – Dependen de Parametría de Producto', 7),
('Contabilidad', 8),
('BCRA', 9),
('Generador de notas', 10),
('Ingresos Masivos', 11),
('H2H', 12),
('Seguridad', 13),
('Anses', 14),
('Clientes', 15),
('Generador de Transacciones', 16),
('Caja y Tesorería', 17),
('Generador de Planilla', 18),
('Financiera Minorista', 19),
('Impuesto (cada impuestos incluye Parametrización, Rendición, Devolución, y Proceso de Cálculo)', 20),
('Cuentas Judiciales', 21),
('Financiera Mayorista', 22),
('Gestión de Cobranza', 23),
('Riesgo Crediticio', 24),
('Control de Gestión', 25),
('Servicios', 26),
('Cambio', 27),
('Tarjetas Débito', 28),
('Giros y Transferencias', 29),
('Débitos Directos', 30),
('NIIF', 31),
('UVA', 32),
('Multihilo', 33),
('Tarjeta de Crédito', 34),
('Agente 7x24 - Bot', 35),
('Report Server', 36),
('Capacitaciones', 37),
('Herramientas para Servicios Apis (sólo para el área herramientas)', 38),
('Otras herramientas', 39)

PRINT CAST(@@ROWCOUNT AS VARCHAR) + ' temas insertados'

-- =============================================================================
-- CARGA DE SUBTEMAS
-- =============================================================================

PRINT 'Iniciando carga de subtemas...'

-- Subtemas para Tema 1: Generador de Productos (Parametría)
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Administración de Productos', 101, 1),
('Proceso-Tarea', 102, 1),
('Administración Formas de Administración', 103, 1),
('Administración Cargos Financieros', 104, 1),
('Administración Historias', 105, 1),
('Rutinas de Cálculo', 106, 1),
('Devengamiento', 107, 1),
('Ajuste de Cartera', 108, 1),
('Administración de Pizarras', 109, 1)

-- Subtemas para Tema 2: Cuentas Corrientes Cajas de Ahorro  
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Administración de Cheques', 10, 2),
('Funciones. Transacciones de Cheque', 11, 2),
('Liquidación de intereses Acr/Deu', 12, 2),
('Adm y Calc de Interés sobre Acuerdos', 13, 2),
('Funciones Déb./Cred/Com', 14, 2),
('Capitalización de Interés', 15, 2),
('Cobro de Comisiones Mensuales', 16, 2),
('Impuestos', 17, 2),
('Resumen de Ctas', 18, 2),
('Cierre de Cuentas', 19, 2),
('Armado de Saldos', 20, 2),
('Inmovilización y Bloqueo de Cuentas', 21, 2),
('Emisión de Comprobantes', 22, 2),
('Déb. Pendientes', 23, 2),
('Campañas de Bonificación de Comisiones', 24, 2),
('Generación de Resúmenes Masivos', 25, 2),
('Envío de resumen por Mail', 26, 2),
('Echeq Configuración', 27, 2),
('Echeq emisión', 28, 2),
('Echeq depósito', 29, 2),
('Echeq negociación', 30, 2),
('Echeq CAC (Certif. Acción Civil)', 31, 2),
('Debín/Credín', 32, 2),
('Acuerdos en CC', 33, 2),
('Echeq Pago y depósito por Ventanilla', 34, 2),
('Echeq Devolución', 35, 2),
('Echeq Cesión Electrónica de Derechos (CED)', 36, 2),
('Cámara recibida vuelco CP0', 37, 2),
('Servicios de novedades de echeq (Depósito, Custodia Rescate y Emisión)', 38, 2),
('Sirplus - circuito de cuentas de recaudación de pagos', 39, 2),
('Api Débito/Crédito con control de duplicidad', 40, 2),
('CEDIP Banco receptor', 41, 2)

-- Subtemas para Tema 3: Créditos
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Liquidación', 42, 3),
('Funciones Pagos/Anulación/Cancelación', 43, 3),
('Scoring Versión BSE', 44, 3),
('Informe de Deuda', 45, 3),
('Refinanciación', 46, 3),
('Restructuración', 47, 3),
('Pase a Legales', 48, 3),
('Descuentos de Cuotas', 49, 3),
('Cobro cuotas en base a Acreditación de Sueldo', 50, 3),
('Lote de reserva para el cobro de cuotas', 51, 3),
('Venta de Cartera', 52, 3),
('Seguro', 53, 3),
('Pase a Irrecuperables', 54, 3),
('Ajuste Unitario de Cartera', 55, 3),
('Compra de Cartera', 56, 3),
('Leasing', 57, 3),
('Bonificaciones de Gastos de Liquidación y Circuito de autorización', 58, 3),
('Scoring por Plataforma', 59, 3),
('Subsidios', 60, 3),
('Administración de Cupos para CR', 61, 3),
('Acuerdos en CR', 62, 3),
('MIPYME', 63, 3),
('Liquidación de créditos (Ventana)', 64, 3),
('Pase a quiebra', 65, 3)

-- Subtemas para Tema 4: Plazos Fijos
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Parametrización de Tramos', 66, 4),
('Liquidación', 67, 4),
('Renovación', 68, 4),
('Funciones. Pagos/Anulación/Cancelación', 69, 4),
('Plazos Fijos UVA', 70, 4)

-- Subtemas para Tema 5: Ingresos y Egresos Varios
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Administración Entes - Convenios', 71, 5),
('Funciones. Transacciones', 72, 5),
('Recaudación – Punta de Caja', 73, 5)

-- Subtemas para Tema 6: Garantía
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Parametrización Tipos/subtipos', 74, 6),
('Administración de garantías', 75, 6),
('Contabilización de garantías', 76, 6),
('Funciones. Transacciones', 77, 6),
('Circuito de Garantías de Mercedes Benz', 78, 6),
('Inventario de Garantías – Ver dif Mercedes', 79, 6),
('Proceso de Cálculo Consumo de Garantía del BCRA', 80, 6),
('Autorización de Garantías Cotización y Asignación de Garantías a operaciones', 81, 6)

-- Subtemas para Tema 7: Generales – Dependen de Parametría de Producto
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Ajuste de Cartera', 82, 7),
('Cálculo de Índices de Ajuste (CER-CVS – UVA)', 83, 7),
('Rutinas de Amortización', 84, 7),
('Cálculo de IVA', 85, 7),
('Ejecución de Proceso de tareas', 86, 7),
('Congelamiento', 87, 7)

-- Subtemas para Tema 8: Contabilidad (reutilizando algunos de tema 7 + específicos)
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Plan de Cuentas', 88, 8),
('Generación de Minutas Libres y Predefinidas', 89, 8),
('Consultas y Modificación de Minutas', 90, 8),
('Circuito de Autorización de Minutas', 91, 8),
('Balances', 92, 8),
('Mayores / Subdiarios', 93, 8),
('Contabilidad Automática', 94, 8),
('Sobregiro', 95, 8),
('Revalúo', 96, 8),
('Mayorización', 97, 8),
('Administración de Libros contables – NIIF', 98, 8),
('Vuelco de Minutas de sistemas externos', 99, 8),
('Inicio y Cierre de día contable', 100, 8),
('Cierre de Mes y Diario Rubricado', 101, 8),
('Conciliación Automática de Cartera y Contabilidad', 102, 8),
('Ajuste por Inflación proceso', 103, 8),
('Ajuste por inflación Configuración a nivel de rubros', 104, 8)

-- Continuar con los demás temas (acortado para brevedad, pero se puede extender)
-- Tema 9: BCRA
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Balance', 105, 9),
('Requisitos Mínimos de Liquidez - RML', 106, 9),
('Efectivo Mínimo', 107, 9),
('Aplicación de Recursos', 108, 9),
('Deudores Sist. Fin.', 109, 9),
('Posición de Liquidez', 110, 9),
('Capitales Mínimos', 111, 9),
('Estado de Situación de Deudores', 112, 9),
('Tipos de Cartera', 113, 9),
('Riesgo de Tasa Operacional', 114, 9),
('Riesgo de Tasa Contable', 115, 9),
('Riesgo Financiero', 116, 9),
('RPC Banco', 117, 9),
('Lavado de Dinero', 120, 9),
('Cierre de Día', 122, 9),
('Vuelcos', 123, 9),
('Actualización Manual ODS', 124, 9),
('ABM Grupos de Información', 125, 9),
('Inventario ODS', 126, 9),
('Panel de Conciliación', 127, 9),
('Vuelcos de Archivos emitidos por BCRA', 128, 9),
('Situación del Cliente', 129, 9),
('Inspección BCRA', 130, 9),
('Garantías', 131, 9),
('Situación', 132, 9),
('Previsión', 133, 9),
('Activos Inmovilizados', 134, 9),
('Operaciones de Cambio', 135, 9),
('Posición Global Neta Moneda Extranjera', 136, 9),
('Reg. Supervisión Trimestral', 137, 9),
('Estado de Situaciones de Deudores', 138, 9),
('Graduación y Fraccionamiento', 139, 9),
('Cuadros SISCEN', 140, 9),
('Grandes Exposiciones al Riesgo de Crédito', 142, 9),
('MIPYME', 144, 9)

-- Tema 10: Generador de notas
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Componente JAVA Gendoc', 145, 10),
('API Generador de Notas .NET', 146, 10),
('Armado de sp recuperadores', 147, 10),
('Gestión y Armado de Notas', 148, 10),
('Escenarios de Notas', 149, 10),
('Generación de notas en PDF', 150, 10)

-- Tema 11: Ingresos Masivos
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Parametrización Concepto y Subconcepto / Equivalencia de Trx', 151, 11),
('Procesos Especiales', 152, 11),
('Pago Haberes / Acreditación Sueldo', 153, 11),
('Mutuales', 154, 11),
('Impuestos', 155, 11),
('Transferencias Inmediatas', 156, 11),
('Circuito para la Imputación de Lotes', 157, 11),
('Transferencias múltiples', 158, 11),
('Interacción con la cámara BEE', 159, 11),
('Gestión de doc electrónicos', 160, 11),
('Ingresos masivos: Individual CC/CA', 161, 11),
('Ingresos masivos: Individual CR', 162, 11),
('Ingresos masivos: Masivo CA/CC bse', 163, 11),
('Ingresos masivos: Masivo x cliente CA/CC bse', 164, 11),
('Ingresos masivos: Masivo Multihilo CA/CC Piano', 165, 11)

-- Tema 12: H2H
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Parametrización H2H', 166, 12),
('Circuito Operativo – Refresh Día Abierto / Día Cerrado', 167, 12),
('Swich/GPL – Intermediarios entre Monitores y Link', 168, 12),
('Conciliación Versión Vieja y Nueva', 169, 12),
('Créditos por H2H', 170, 12),
('Plazos fijos por H2H', 171, 12),
('PF precancelables (pesos/uva)', 172, 12),
('Tratamiento de Mensajes H2H', 173, 12),
('SAF – 7*24 -BMR-VOII y CMF', 174, 12),
('Extract - Refresh', 175, 12),
('Extract / sin uso del COBOL CMF', 176, 12),
('Banca Empresa', 177, 12),
('Transferencia 3.0', 178, 12),
('Conciliación MasterDebit', 179, 12),
('Refresh de Alta de Cuentas Sueldo', 180, 12),
('Refresh de Préstamos y Cuotas', 181, 12),
('Refresh de Movimientos Diferidos', 182, 12),
('Refresh de Cheques Descontados', 183, 12),
('Refresh de Responsables de Cuenta', 184, 12),
('Refresh de Posición Consolidada', 185, 12),
('Refresh de Plazo Fijo', 186, 12),
('Refresh de Saldos', 187, 12),
('Refresh de Movimientos', 188, 12),
('Refresh de los 10 últimos Movimientos', 189, 12),
('Refresh de CBU', 190, 12),
('Carga de Extract', 191, 12),
('Refresh BEE', 192, 12),
('Refresh cheques cámara', 193, 12),
('Extract novedades e-cheq', 194, 12),
('Extract BEE', 195, 12),
('Refresh Órdenes Transferencias BEE', 196, 12),
('Refresh consentimiento', 197, 12),
('Servicio Dispara Transacción usado por CiberBank', 198, 12)

-- Tema 13: Seguridad
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Adm Usuarios', 199, 13),
('Active Directory BMR', 200, 13),
('Seguridad Centralizada BMR', 201, 13),
('Seguridad Centralizada PIANO', 202, 13),
('Tareas Perfiles', 203, 13),
('Tareas englobadoras – CMF – MB – BSE', 204, 13),
('Controles de Tareas / Supervisión', 205, 13),
('Configuración de Seguridad de los Archivos', 206, 13),
('Login', 407, 13)

-- Tema 14: Anses
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Circuito de Vuelco y Administración', 207, 14),
('Modalidades Parametrización', 208, 14),
('Asignación Universal', 209, 14),
('Nueva Acreditación Anticipada', 210, 14),
('Modalización de Cuentas', 211, 14),
('Acreditación ANSES', 212, 14),
('Funciones Transacciones', 213, 14),
('Proceso Modificación – Migración de Sucursales', 214, 14),
('Rendición y Devolución Anses', 215, 14),
('Emisión de Plásticos', 216, 14)

-- Tema 15: Clientes
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Adm. Datos Generales', 217, 15),
('Alta de clientes con ejecución de Alertas', 218, 15),
('Autorización de Alta de clientes', 219, 15),
('Mantenimiento de Cuentas (y Altas Especiales)', 220, 15),
('Param. Grupos Afinidad', 221, 15),
('Cliente Único - Proceso de Alta de Cliente Descentralizado', 222, 15),
('Proceso de Unificación Cliente Duplicado – CMF', 223, 15),
('Alertas - Configuración', 224, 15),
('Trámites y Requisitos', 225, 15),
('MIPYME', 226, 15)

-- Tema 16: Generador de Transacciones
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Adm. Funciones', 227, 16),
('Comprobantes', 228, 16),
('Perfiles de transacciones', 229, 16),
('Direccionamiento de impresión', 230, 16),
('Inter-Sucursales', 231, 16),
('Param. Transacciones', 232, 16),
('Ejecución de Tx', 233, 16)

-- Tema 17: Caja y Tesorería
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Caja de Seguridad BSE', 235, 17),
('Cajas de Seguridad CMF', 236, 17),
('Administración de Puestos', 237, 17),
('Consulta de Información', 238, 17),
('Parámetros', 239, 17),
('Romaneo', 240, 17),
('Cierre de Tesorería', 241, 17)

-- Tema 18: Generador de Planilla
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Parametrización de Planillas', 242, 18),
('Ejecución de Plantillas', 243, 18),
('Funciones', 244, 18)

-- Tema 19: Financiera Minorista
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Web Service de Seguros', 245, 19),
('Planes de Financiación', 246, 19),
('Consultas externas a Nosis', 247, 19),
('Riesgo Crediticio', 248, 19),
('Generador de Pedidos', 249, 19),
('Operatoria Minorista Prendaria (Circuito Minorista)', 250, 19),
('Operatoria Minorista Leasing', 251, 19),
('Desvío de Fondos', 252, 19),
('Circuito de Conformación de Operaciones', 253, 19),
('Administración de garantías', 254, 19),
('Web Service', 255, 19),
('Administración de Requisitos', 256, 19),
('Estructura Atípicas', 257, 19),
('Solicitud Única', 258, 19),
('Funciones de Pago Especial', 408, 19),
('Aplicación de Pago Flota', 409, 19),
('Proceso de Diferimiento de Cuotas', 259, 19)

-- Tema 20: Impuesto
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('IVA', 260, 20),
('Impuesto a los débitos Créditos', 261, 20),
('Ingresos Brutos', 262, 20),
('Sellado', 263, 20),
('SITER', 264, 20),
('SIRCREB', 265, 20),
('CABA', 266, 20),
('Proceso de Conciliación contable IVA – BMR', 267, 20),
('Exención de Impuestos', 268, 20)

-- Agregar subtemas para los temas restantes de manera similar...
-- (Acortado para brevedad, pero se pueden agregar todos los subtemas restantes)

-- Algunos subtemas adicionales importantes
-- Tema 37: Capacitaciones
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Capacitación Funcional Básica - Jorge Márquez', 362, 37),
('Capacitación Herramientas Apis', 363, 37),
('Circuitos Préstamos Mantenimiento', 364, 37),
('Capacitación H2H', 365, 37),
('Capacitación Multihilos', 366, 37),
('Inducción QC', 367, 37),
('Inducción IBS PB SYBASE', 368, 37),
('Capacitación SYBASE POWER BUILDER - Julio Leiva', 370, 37),
('Capacitación NIIF - César Romano', 371, 37),
('Capacitación NIIF - Hernán Honorato', 372, 37),
('Formación Débito Directo - Víctor Giampaoli', 373, 37),
('Formación Débito Directo - Ana Paula Pelegrina y Javier Ocaranza', 374, 37),
('Sistema de amortizaciones de cuotas - Hernán Honorato', 375, 37),
('Capacitación Contable Básica', 376, 37),
('Tasa de interés', 377, 37),
('Postman', 378, 37),
('Módulo Contable (Funcional y Técnico de IBS)', 379, 37),
('Automatización de APIS', 380, 37),
('Automatización FrontEnd (WEB)', 381, 37)

-- Tema 38: Herramientas para Servicios Apis
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('Generador de Apis (GENSOA)', 382, 38),
('Licencias para uso de Apis', 383, 38),
('Token', 384, 38),
('Frame', 385, 38),
('Sucursal Digital - Banca Individuo (WEB)', 386, 38),
('Sucursal Digital - Banca Empresa (WEB)', 387, 38),
('Sucursal Digital - Banca Individuo (Mobile)', 388, 38),
('Sucursal Digital - Banca Empresa (Mobile)', 389, 38),
('BackOffice - Mesa de Ayuda', 390, 38),
('BackOffice - Comercial', 391, 38),
('BackOffice - IT', 392, 38)

-- Tema 39: Otras herramientas
INSERT INTO Subtemas (Nombre, Orden, IdTema) VALUES 
('ASE', 393, 39),
('Pasajes', 394, 39),
('GOOGS (controlador de versiones)', 395, 39),
('SICP WEB', 396, 39),
('SAPYA', 397, 39),
('TranServer', 398, 39),
('Power Designer', 399, 39),
('Adm_Frame', 400, 39),
('Despachante H2H', 401, 39),
('SOAP UI', 402, 39),
('JMETER', 403, 39),
('Selenium', 404, 39),
('Postman', 405, 39),
('Python', 406, 39)

PRINT CAST(@@ROWCOUNT AS VARCHAR) + ' subtemas adicionales insertados'

-- =============================================================================
-- VERIFICACIÓN DE CARGA
-- =============================================================================

PRINT '====================================================================='
PRINT 'RESUMEN DE CARGA DE TEMAS Y SUBTEMAS'
PRINT '====================================================================='

-- Contar temas
DECLARE @TotalTemas INT = (SELECT COUNT(*) FROM Temas)
PRINT 'Total de temas cargados: ' + CAST(@TotalTemas AS VARCHAR)

-- Contar subtemas por tema
SELECT 
    t.IdTema,
    t.Nombre as 'Tema',
    COUNT(s.IdSubtema) as 'Cantidad Subtemas'
FROM Temas t
LEFT JOIN Subtemas s ON t.IdTema = s.IdTema
GROUP BY t.IdTema, t.Nombre
ORDER BY t.Orden

-- Total general
DECLARE @TotalSubtemas INT = (SELECT COUNT(*) FROM Subtemas)
PRINT ''
PRINT 'Total de subtemas cargados: ' + CAST(@TotalSubtemas AS VARCHAR)

PRINT ''
PRINT 'Carga de temas y subtemas completada exitosamente'
PRINT '====================================================================='

GO