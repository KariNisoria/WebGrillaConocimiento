-- Script para configurar el sistema de autenticación y permisos básicos
-- Este script debe ejecutarse después de la migración principal

USE [WebGrillaConocimiento]
GO

-- Crear algunos roles básicos si no existen
IF NOT EXISTS (SELECT 1 FROM Roles WHERE Nombre = 'Administrador')
BEGIN
    INSERT INTO Roles (Nombre) VALUES ('Administrador')
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE Nombre = 'Manager')
BEGIN
    INSERT INTO Roles (Nombre) VALUES ('Manager')
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE Nombre = 'Evaluador')
BEGIN
    INSERT INTO Roles (Nombre) VALUES ('Evaluador')
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE Nombre = 'Desarrollador')
BEGIN
    INSERT INTO Roles (Nombre) VALUES ('Desarrollador')
END

-- Crear algunos tipos de documento básicos si no existen
IF NOT EXISTS (SELECT 1 FROM TipoDocumentos WHERE Nombre = 'DNI')
BEGIN
    INSERT INTO TipoDocumentos (Nombre) VALUES ('DNI')
END

IF NOT EXISTS (SELECT 1 FROM TipoDocumentos WHERE Nombre = 'Cédula')
BEGIN
    INSERT INTO TipoDocumentos (Nombre) VALUES ('Cédula')
END

IF NOT EXISTS (SELECT 1 FROM TipoDocumentos WHERE Nombre = 'Pasaporte')
BEGIN
    INSERT INTO TipoDocumentos (Nombre) VALUES ('Pasaporte')
END

-- Crear algunos equipos básicos si no existen
IF NOT EXISTS (SELECT 1 FROM EquipoDesarrollos WHERE Nombre = 'Desarrollo')
BEGIN
    INSERT INTO EquipoDesarrollos (Nombre) VALUES ('Desarrollo')
END

IF NOT EXISTS (SELECT 1 FROM EquipoDesarrollos WHERE Nombre = 'Testing')
BEGIN
    INSERT INTO EquipoDesarrollos (Nombre) VALUES ('Testing')
END

IF NOT EXISTS (SELECT 1 FROM EquipoDesarrollos WHERE Nombre = 'Arquitectura')
BEGIN
    INSERT INTO EquipoDesarrollos (Nombre) VALUES ('Arquitectura')
END

-- Crear usuario administrador por defecto
DECLARE @IdRolAdmin INT = (SELECT IdRol FROM Roles WHERE Nombre = 'Administrador')
DECLARE @IdTipoDocDNI INT = (SELECT IdTipoDocumento FROM TipoDocumentos WHERE Nombre = 'DNI')
DECLARE @IdEquipoDesarrollo INT = (SELECT IdEquipoDesarrollo FROM EquipoDesarrollos WHERE Nombre = 'Desarrollo')

IF NOT EXISTS (SELECT 1 FROM Recursos WHERE CorreoElectronico = 'admin@censys.com')
BEGIN
    INSERT INTO Recursos (
        Nombre, 
        Apellido, 
        FechaIngreso, 
        IdTipoDocumento, 
        NumeroDocumento, 
        CorreoElectronico, 
        PerfilSeguridad, 
        IdEquipoDesarrollo, 
        IdRol
    ) VALUES (
        'Administrador',
        'Sistema',
        GETDATE(),
        @IdTipoDocDNI,
        12345678,
        'admin@censys.com',
        'admin',
        @IdEquipoDesarrollo,
        @IdRolAdmin
    )
    
    PRINT 'Usuario administrador creado:'
    PRINT 'Email: admin@censys.com'
    PRINT 'DNI: 12345678'
END

-- Crear usuario manager por defecto
DECLARE @IdRolManager INT = (SELECT IdRol FROM Roles WHERE Nombre = 'Manager')

IF NOT EXISTS (SELECT 1 FROM Recursos WHERE CorreoElectronico = 'manager@censys.com')
BEGIN
    INSERT INTO Recursos (
        Nombre, 
        Apellido, 
        FechaIngreso, 
        IdTipoDocumento, 
        NumeroDocumento, 
        CorreoElectronico, 
        PerfilSeguridad, 
        IdEquipoDesarrollo, 
        IdRol
    ) VALUES (
        'Manager',
        'Sistema',
        GETDATE(),
        @IdTipoDocDNI,
        87654321,
        'manager@censys.com',
        'manager',
        @IdEquipoDesarrollo,
        @IdRolManager
    )
    
    PRINT 'Usuario manager creado:'
    PRINT 'Email: manager@censys.com'
    PRINT 'DNI: 87654321'
END

-- Crear usuario evaluador por defecto
DECLARE @IdRolEvaluador INT = (SELECT IdRol FROM Roles WHERE Nombre = 'Evaluador')

IF NOT EXISTS (SELECT 1 FROM Recursos WHERE CorreoElectronico = 'evaluador@censys.com')
BEGIN
    INSERT INTO Recursos (
        Nombre, 
        Apellido, 
        FechaIngreso, 
        IdTipoDocumento, 
        NumeroDocumento, 
        CorreoElectronico, 
        PerfilSeguridad, 
        IdEquipoDesarrollo, 
        IdRol
    ) VALUES (
        'Evaluador',
        'Test',
        GETDATE(),
        @IdTipoDocDNI,
        11111111,
        'evaluador@censys.com',
        'evaluator',
        @IdEquipoDesarrollo,
        @IdRolEvaluador
    )
    
    PRINT 'Usuario evaluador creado:'
    PRINT 'Email: evaluador@censys.com'
    PRINT 'DNI: 11111111'
END

-- Crear usuario desarrollador por defecto
DECLARE @IdRolDesarrollador INT = (SELECT IdRol FROM Roles WHERE Nombre = 'Desarrollador')

IF NOT EXISTS (SELECT 1 FROM Recursos WHERE CorreoElectronico = 'dev@censys.com')
BEGIN
    INSERT INTO Recursos (
        Nombre, 
        Apellido, 
        FechaIngreso, 
        IdTipoDocumento, 
        NumeroDocumento, 
        CorreoElectronico, 
        PerfilSeguridad, 
        IdEquipoDesarrollo, 
        IdRol
    ) VALUES (
        'Desarrollador',
        'Test',
        GETDATE(),
        @IdTipoDocDNI,
        22222222,
        'dev@censys.com',
        'developer',
        @IdEquipoDesarrollo,
        @IdRolDesarrollador
    )
    
    PRINT 'Usuario desarrollador creado:'
    PRINT 'Email: dev@censys.com'
    PRINT 'DNI: 22222222'
END

PRINT ''
PRINT '=========================================='
PRINT 'CONFIGURACIÓN DE AUTENTICACIÓN COMPLETADA'
PRINT '=========================================='
PRINT ''
PRINT 'Usuarios de prueba creados:'
PRINT '• Administrador: admin@censys.com / DNI: 12345678'
PRINT '• Manager: manager@censys.com / DNI: 87654321'  
PRINT '• Evaluador: evaluador@censys.com / DNI: 11111111'
PRINT '• Desarrollador: dev@censys.com / DNI: 22222222'
PRINT ''
PRINT 'Sistema de permisos configurado por roles:'
PRINT '• Administrador: Acceso completo a todo el sistema'
PRINT '• Manager: Gestión de recursos, grillas y evaluaciones'
PRINT '• Evaluador: Acceso a evaluaciones y consultas'
PRINT '• Desarrollador: Solo consultas básicas'
PRINT '=========================================='

GO