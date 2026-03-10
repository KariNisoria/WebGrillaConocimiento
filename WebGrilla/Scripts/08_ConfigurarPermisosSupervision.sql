-- Script para crear y configurar los permisos de Supervisión
-- Ejecutar este script en la base de datos después de tener la tabla RolPermisos creada

USE [WebGrillaConocimiento] -- Cambiar por el nombre de tu base de datos
GO

PRINT 'Iniciando configuración de permisos de SUPERVISION...'

-- Verificar que exista la tabla RolPermisos
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolPermisos]') AND type in (N'U'))
BEGIN
    PRINT 'ERROR: La tabla RolPermisos no existe. Ejecutar primero el script 06_CrearTablaRolPermisos.sql'
    RETURN
END

-- Verificar que existan los roles básicos
IF NOT EXISTS (SELECT 1 FROM Roles WHERE Nombre = 'Administrador')
BEGIN
    PRINT 'ERROR: Los roles básicos no existen. Verificar la configuración de roles.'
    RETURN
END

PRINT 'Agregando permisos de SUPERVISION...'

-- PERMISOS DE SUPERVISION PARA ADMINISTRADOR
-- El administrador tiene acceso completo
INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion, Activo)
SELECT 
    r.IdRol, 
    'SUPERVISION_READ', 
    'Permite consultar relaciones supervisor-supervisado', 
    1
FROM Roles r 
WHERE r.Nombre = 'Administrador'
AND NOT EXISTS (
    SELECT 1 FROM RolPermisos rp 
    WHERE rp.IdRol = r.IdRol AND rp.CodigoPermiso = 'SUPERVISION_READ'
);

INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion, Activo)
SELECT 
    r.IdRol, 
    'SUPERVISION_WRITE', 
    'Permite crear, modificar y eliminar relaciones supervisor-supervisado', 
    1
FROM Roles r 
WHERE r.Nombre = 'Administrador'
AND NOT EXISTS (
    SELECT 1 FROM RolPermisos rp 
    WHERE rp.IdRol = r.IdRol AND rp.CodigoPermiso = 'SUPERVISION_WRITE'
);

INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion, Activo)
SELECT 
    r.IdRol, 
    'SUPERVISION_DELETE', 
    'Permite eliminar permanentemente relaciones de supervisión', 
    1
FROM Roles r 
WHERE r.Nombre = 'Administrador'
AND NOT EXISTS (
    SELECT 1 FROM RolPermisos rp 
    WHERE rp.IdRol = r.IdRol AND rp.CodigoPermiso = 'SUPERVISION_DELETE'
);

-- PERMISOS DE SUPERVISION PARA MANAGER
-- El manager puede gestionar supervisiones pero no eliminar permanentemente
INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion, Activo)
SELECT 
    r.IdRol, 
    'SUPERVISION_READ', 
    'Permite consultar relaciones supervisor-supervisado', 
    1
FROM Roles r 
WHERE r.Nombre = 'Manager'
AND NOT EXISTS (
    SELECT 1 FROM RolPermisos rp 
    WHERE rp.IdRol = r.IdRol AND rp.CodigoPermiso = 'SUPERVISION_READ'
);

INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion, Activo)
SELECT 
    r.IdRol, 
    'SUPERVISION_WRITE', 
    'Permite crear y modificar relaciones supervisor-supervisado', 
    1
FROM Roles r 
WHERE r.Nombre = 'Manager'
AND NOT EXISTS (
    SELECT 1 FROM RolPermisos rp 
    WHERE rp.IdRol = r.IdRol AND rp.CodigoPermiso = 'SUPERVISION_WRITE'
);

-- PERMISOS DE SUPERVISION PARA EVALUADOR
-- El evaluador solo puede consultar para entender la estructura organizacional
INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion, Activo)
SELECT 
    r.IdRol, 
    'SUPERVISION_READ', 
    'Permite consultar relaciones supervisor-supervisado para evaluaciones', 
    1
FROM Roles r 
WHERE r.Nombre = 'Evaluador'
AND NOT EXISTS (
    SELECT 1 FROM RolPermisos rp 
    WHERE rp.IdRol = r.IdRol AND rp.CodigoPermiso = 'SUPERVISION_READ'
);

-- PERMISOS DE SUPERVISION PARA DESARROLLADOR
-- El desarrollador no tiene acceso a supervisión por defecto
-- (Se puede agregar solo SUPERVISION_READ si es necesario)

PRINT 'Configuración de permisos de SUPERVISION completada.'

-- Mostrar resumen de permisos asignados
PRINT 'Resumen de permisos de SUPERVISION asignados:'

SELECT 
    r.Nombre as Rol,
    rp.CodigoPermiso as Permiso,
    rp.Descripcion,
    CASE WHEN rp.Activo = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado
FROM RolPermisos rp
INNER JOIN Roles r ON r.IdRol = rp.IdRol
WHERE rp.CodigoPermiso LIKE 'SUPERVISION%'
ORDER BY r.Nombre, rp.CodigoPermiso;

-- Verificar que todos los roles principales tengan al menos SUPERVISION_READ
PRINT 'Verificación final:'

DECLARE @rolesConSupervision TABLE (Rol NVARCHAR(100), TieneAcceso BIT)

INSERT INTO @rolesConSupervision (Rol, TieneAcceso)
SELECT 
    r.Nombre,
    CASE 
        WHEN EXISTS (
            SELECT 1 FROM RolPermisos rp 
            WHERE rp.IdRol = r.IdRol 
            AND rp.CodigoPermiso = 'SUPERVISION_READ' 
            AND rp.Activo = 1
        ) THEN 1 
        ELSE 0 
    END
FROM Roles r
WHERE r.Nombre IN ('Administrador', 'Manager', 'Evaluador', 'Desarrollador');

SELECT 
    Rol,
    CASE 
        WHEN TieneAcceso = 1 THEN '? Configurado' 
        ELSE '? Sin configurar' 
    END as EstadoSupervision
FROM @rolesConSupervision;

PRINT 'Script de configuración de permisos de SUPERVISION ejecutado correctamente.'