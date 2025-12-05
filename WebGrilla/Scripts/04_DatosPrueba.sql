-- =============================================================================
-- SCRIPT DE DATOS DE PRUEBA - WEBGRILLA CONOCIMIENTO
-- =============================================================================
-- Versión: 1.0
-- Descripción: Carga datos de prueba para probar el sistema
-- Prerrequisito: Ejecutar después de los scripts 01, 02 y 03

USE [GriconDB]
GO

-- =============================================================================
-- CONFIGURACIÓN DE DATOS DE PRUEBA
-- =============================================================================

PRINT 'Iniciando carga de datos de prueba...'

-- Insertar datos adicionales solo si no existen
-- Clientes de prueba
IF NOT EXISTS (SELECT * FROM Clientes WHERE Nombre = 'Banco Ejemplo S.A.')
BEGIN
    INSERT INTO Clientes (Nombre) VALUES 
        ('Banco Ejemplo S.A.'),
        ('Financiera Regional'),
        ('Cooperativa de Crédito'),
        ('Instituto Bancario')
    PRINT 'Clientes de prueba insertados'
END

-- Equipos de desarrollo
IF NOT EXISTS (SELECT * FROM Equipos WHERE Nombre = 'Equipo Core Banking')
BEGIN
    INSERT INTO Equipos (Nombre, IdCliente) VALUES 
        ('Equipo Core Banking', 1),
        ('Equipo Digital', 1),
        ('Equipo Riesgo', 1),
        ('Equipo Contable', 1),
        ('Equipo QA', 1)
    PRINT 'Equipos de prueba insertados'
END

-- Roles adicionales
IF (SELECT COUNT(*) FROM Roles) <= 5
BEGIN
    INSERT INTO Roles (Nombre) VALUES 
        ('Gerente de Proyecto'),
        ('Analista Senior'),
        ('Consultor Funcional'),
        ('Administrador de Sistemas'),
        ('Especialista en BCRA')
    PRINT 'Roles adicionales insertados'
END

-- Recursos de prueba
IF NOT EXISTS (SELECT * FROM Recursos WHERE Nombre = 'Juan')
BEGIN
    INSERT INTO Recursos (Nombre, Apellido, Apellidos, FechaIngreso, IdTipoDocumento, NumeroDocumento, 
                         CorreoElectronico, PerfilSeguridad, IdEquipoDesarrollo, IdRol, IdGrilla) 
    VALUES 
        ('Juan', 'Pérez', 'Pérez García', GETDATE(), 1, 12345678, 'juan.perez@banco.com', 'Usuario', 1, 1, 1),
        ('María', 'González', 'González López', GETDATE(), 1, 87654321, 'maria.gonzalez@banco.com', 'Supervisor', 2, 2, 1),
        ('Carlos', 'Rodríguez', 'Rodríguez Silva', GETDATE(), 1, 11223344, 'carlos.rodriguez@banco.com', 'Admin', 1, 3, 1),
        ('Ana', 'Martínez', 'Martínez Ruiz', GETDATE(), 1, 55667788, 'ana.martinez@banco.com', 'Usuario', 3, 2, 1),
        ('Luis', 'García', 'García Fernández', GETDATE(), 1, 99887766, 'luis.garcia@banco.com', 'Tester', 5, 4, 1)
    PRINT 'Recursos de prueba insertados'
END

-- Grillas adicionales de prueba
IF (SELECT COUNT(*) FROM Grillas) = 1
BEGIN
    INSERT INTO Grillas (Nombre, Descripcion, Estado, FechaVigencia) VALUES 
        ('Grilla Desarrolladores Senior', 'Evaluación para desarrolladores con +3 años de experiencia', 1, GETDATE()),
        ('Grilla Analistas Funcionales', 'Evaluación específica para analistas funcionales bancarios', 1, GETDATE()),
        ('Grilla Especialistas BCRA', 'Evaluación especializada en normativas BCRA', 1, GETDATE())
    PRINT 'Grillas adicionales de prueba insertadas'
END

-- Asignar algunos temas a las grillas de prueba
IF NOT EXISTS (SELECT * FROM GrillaTemas WHERE IdGrilla = 2)
BEGIN
    -- Grilla Desarrolladores Senior - Temas técnicos
    INSERT INTO GrillaTemas (Nombre, Ponderacion, Orden, IdGrilla, IdTema) VALUES
        ('H2H', 25.0, 1, 2, 12),
        ('Multihilo', 20.0, 2, 2, 33),
        ('Seguridad', 20.0, 3, 2, 13),
        ('Generador de Transacciones', 15.0, 4, 2, 16),
        ('Report Server', 20.0, 5, 2, 36)

    -- Grilla Analistas Funcionales - Temas funcionales
    INSERT INTO GrillaTemas (Nombre, Ponderacion, Orden, IdGrilla, IdTema) VALUES
        ('Contabilidad', 30.0, 1, 3, 8),
        ('Créditos', 25.0, 2, 3, 3),
        ('Cuentas Corrientes Cajas de Ahorro', 25.0, 3, 3, 2),
        ('Clientes', 20.0, 4, 3, 15)

    -- Grilla Especialistas BCRA - Temas BCRA
    INSERT INTO GrillaTemas (Nombre, Ponderacion, Orden, IdGrilla, IdTema) VALUES
        ('BCRA', 50.0, 1, 4, 9),
        ('Riesgo Crediticio', 25.0, 2, 4, 24),
        ('NIIF', 15.0, 3, 4, 31),
        ('Lavado de Dinero', 10.0, 4, 4, 9)

    PRINT 'Temas asignados a grillas de prueba'
END

-- Asignar algunos subtemas a los temas de las grillas
IF NOT EXISTS (SELECT * FROM GrillaSubtemas WHERE IdGrillaTema > 1)
BEGIN
    -- Subtemas para H2H en grilla desarrolladores
    DECLARE @IdGrillaTemaH2H INT = (SELECT IdGrillaTema FROM GrillaTemas WHERE IdGrilla = 2 AND IdTema = 12)
    IF @IdGrillaTemaH2H IS NOT NULL
    BEGIN
        INSERT INTO GrillaSubtemas (Nombre, Ponderacion, Orden, IdGrillaTema, IdSubtema) VALUES
            ('Parametrización H2H', 30.0, 1, @IdGrillaTemaH2H, 166),
            ('Tratamiento de Mensajes H2H', 40.0, 2, @IdGrillaTemaH2H, 173),
            ('Extract - Refresh', 30.0, 3, @IdGrillaTemaH2H, 175)
    END

    -- Subtemas para Contabilidad en grilla analistas
    DECLARE @IdGrillaTemaCont INT = (SELECT IdGrillaTema FROM GrillaTemas WHERE IdGrilla = 3 AND IdTema = 8)
    IF @IdGrillaTemaCont IS NOT NULL
    BEGIN
        INSERT INTO GrillaSubtemas (Nombre, Ponderacion, Orden, IdGrillaTema, IdSubtema) VALUES
            ('Plan de Cuentas', 25.0, 1, @IdGrillaTemaCont, 88),
            ('Balances', 25.0, 2, @IdGrillaTemaCont, 92),
            ('Contabilidad Automática', 30.0, 3, @IdGrillaTemaCont, 94),
            ('Mayorización', 20.0, 4, @IdGrillaTemaCont, 97)
    END

    PRINT 'Subtemas asignados a grillas de prueba'
END

-- Evaluación de prueba
IF NOT EXISTS (SELECT * FROM Evaluacion WHERE Descripcion = 'Evaluación de Prueba Q1 2024')
BEGIN
    INSERT INTO Evaluacion (Descripcion, FechaInicio, FechaFin, IdRecurso, IdGrilla) VALUES
        ('Evaluación de Prueba Q1 2024', DATEADD(day, -30, GETDATE()), DATEADD(day, 30, GETDATE()), 1, 2)
    PRINT 'Evaluación de prueba insertada'
END

-- Algunos conocimientos de muestra
IF NOT EXISTS (SELECT * FROM ConocimientoRecurso)
BEGIN
    DECLARE @IdEvaluacion INT = (SELECT IdEvaluacion FROM Evaluacion WHERE Descripcion = 'Evaluación de Prueba Q1 2024')
    IF @IdEvaluacion IS NOT NULL
    BEGIN
        INSERT INTO ConocimientoRecurso (ValorFuncional, ValorTecnico, ValorFuncionalVerif, ValorTecnicoVerif,
                                       IdRecurso, IdGrilla, IdSubtema, IdEvaluacion) VALUES
            (4, 3, NULL, NULL, 1, 2, 166, @IdEvaluacion),  -- Parametrización H2H
            (3, 4, NULL, NULL, 1, 2, 173, @IdEvaluacion),  -- Tratamiento de Mensajes H2H
            (2, 3, NULL, NULL, 1, 2, 175, @IdEvaluacion)   -- Extract - Refresh
        PRINT 'Conocimientos de muestra insertados'
    END
END

PRINT 'Datos de prueba cargados exitosamente'
GO

-- =============================================================================
-- VERIFICACIÓN FINAL DEL SISTEMA
-- =============================================================================

PRINT '====================================================================='
PRINT 'VERIFICACIÓN FINAL DEL SISTEMA'
PRINT '====================================================================='

-- Mostrar resumen de datos
SELECT 'Clientes' as Tabla, COUNT(*) as Total FROM Clientes
UNION ALL
SELECT 'Equipos', COUNT(*) FROM Equipos
UNION ALL
SELECT 'Roles', COUNT(*) FROM Roles
UNION ALL
SELECT 'TiposDocumentos', COUNT(*) FROM TiposDocumentos
UNION ALL
SELECT 'Recursos', COUNT(*) FROM Recursos
UNION ALL
SELECT 'Temas', COUNT(*) FROM Temas
UNION ALL
SELECT 'Subtemas', COUNT(*) FROM Subtemas
UNION ALL
SELECT 'Grillas', COUNT(*) FROM Grillas
UNION ALL
SELECT 'GrillaTemas', COUNT(*) FROM GrillaTemas
UNION ALL
SELECT 'GrillaSubtemas', COUNT(*) FROM GrillaSubtemas
UNION ALL
SELECT 'Evaluaciones', COUNT(*) FROM Evaluacion
UNION ALL
SELECT 'Conocimientos', COUNT(*) FROM ConocimientoRecurso
ORDER BY Tabla

-- Verificar integridad referencial
PRINT ''
PRINT 'Verificando integridad referencial...'

-- Recursos sin equipo
IF EXISTS (SELECT * FROM Recursos r LEFT JOIN Equipos e ON r.IdEquipoDesarrollo = e.IdEquipoDesarrollo WHERE e.IdEquipoDesarrollo IS NULL)
BEGIN
    PRINT 'ADVERTENCIA: Existen recursos sin equipo asignado'
    SELECT r.IdRecurso, r.Nombre, r.Apellido FROM Recursos r LEFT JOIN Equipos e ON r.IdEquipoDesarrollo = e.IdEquipoDesarrollo WHERE e.IdEquipoDesarrollo IS NULL
END
ELSE
    PRINT 'OK: Todos los recursos tienen equipo asignado'

-- Equipos sin cliente
IF EXISTS (SELECT * FROM Equipos e LEFT JOIN Clientes c ON e.IdCliente = c.IdCliente WHERE c.IdCliente IS NULL)
BEGIN
    PRINT 'ADVERTENCIA: Existen equipos sin cliente asignado'
END
ELSE
    PRINT 'OK: Todos los equipos tienen cliente asignado'

-- GrillaTemas sin ponderación completa al 100%
SELECT 
    g.IdGrilla,
    g.Nombre,
    SUM(gt.Ponderacion) as PonderacionTotal,
    CASE 
        WHEN SUM(gt.Ponderacion) = 100 THEN 'OK'
        WHEN SUM(gt.Ponderacion) > 100 THEN 'EXCEDE 100%'
        WHEN SUM(gt.Ponderacion) < 100 THEN 'FALTA COMPLETAR'
        ELSE 'SIN TEMAS'
    END as Estado
FROM Grillas g
LEFT JOIN GrillaTemas gt ON g.IdGrilla = gt.IdGrilla
GROUP BY g.IdGrilla, g.Nombre
ORDER BY g.IdGrilla

PRINT ''
PRINT '====================================================================='
PRINT 'SISTEMA LISTO PARA USO'
PRINT '====================================================================='
PRINT ''
PRINT 'La base de datos ha sido configurada completamente con:'
PRINT '- Estructura de tablas completa'
PRINT '- Relaciones y constraints'
PRINT '- Datos maestros (temas y subtemas)'
PRINT '- Datos de prueba para testing'
PRINT ''
PRINT 'Próximos pasos:'
PRINT '1. Configurar cadena de conexión en la aplicación'
PRINT '2. Verificar que Entity Framework pueda conectarse'
PRINT '3. Probar la aplicación con los datos de prueba'
PRINT '4. Configurar usuarios y roles de base de datos si es necesario'
PRINT ''
PRINT 'Datos de prueba disponibles:'
PRINT '- Usuario: juan.perez@banco.com (ID: 1)'
PRINT '- Evaluación activa: "Evaluación de Prueba Q1 2024"'
PRINT '- Grillas configuradas con temas y subtemas'
PRINT '====================================================================='

GO