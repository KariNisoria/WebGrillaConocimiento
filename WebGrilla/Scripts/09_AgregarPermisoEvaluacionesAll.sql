-- ===============================================
-- Script: Agregar Permiso EVALUACIONES_ALL
-- Descripción: Permiso para ver todas las evaluaciones del sistema
-- Fecha: 2025-01-28
-- ===============================================

USE [WebGrillaConocimiento];
GO

-- Verificar si el permiso ya existe antes de insertarlo
IF NOT EXISTS (SELECT 1 FROM [RolPermiso] WHERE [CodigoPermiso] = 'EVALUACIONES_ALL')
BEGIN
    INSERT INTO [RolPermiso] ([IdRol], [CodigoPermiso], [Descripcion], [Activo], [FechaCreacion])
    VALUES 
    (1, 'EVALUACIONES_ALL', 'Permite ver todas las evaluaciones del sistema (sin restricciones de supervisión)', 1, GETUTCDATE()),
    (2, 'EVALUACIONES_ALL', 'Permite ver todas las evaluaciones del sistema (sin restricciones de supervisión)', 1, GETUTCDATE());
    
    PRINT '? Permiso EVALUACIONES_ALL agregado correctamente para Admin y Supervisor.';
END
ELSE
BEGIN
    PRINT '?? El permiso EVALUACIONES_ALL ya existe en la base de datos.';
END

-- Verificar los permisos creados
SELECT 
    r.Nombre as 'Rol',
    rp.CodigoPermiso as 'Código Permiso',
    rp.Descripcion as 'Descripción',
    rp.Activo as 'Activo',
    rp.FechaCreacion as 'Fecha Creación'
FROM [RolPermiso] rp
INNER JOIN [Rol] r ON rp.IdRol = r.IdRol
WHERE rp.CodigoPermiso = 'EVALUACIONES_ALL'
ORDER BY r.Nombre;

PRINT '? Script completado exitosamente.';