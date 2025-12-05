-- =============================================================================
-- SCRIPT DE CORRECCIONES Y ACTUALIZACIONES - WEBGRILLA CONOCIMIENTO
-- =============================================================================
-- Versión: 1.0
-- Descripción: Aplica correcciones de estructura según las migraciones más recientes
-- Prerrequisito: Ejecutar después de 01_CreacionCompletaBaseDatos.sql

USE [GriconDB]
GO

-- =============================================================================
-- VERIFICACIÓN Y CORRECCIÓN DE ESTRUCTURA DE TABLAS
-- =============================================================================

PRINT 'Iniciando correcciones de estructura...'

-- 1. Corrección en tabla Equipos: Verificar que Nombre sea nvarchar, no int
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
          WHERE TABLE_NAME = 'Equipos' AND COLUMN_NAME = 'Nombre' AND DATA_TYPE = 'int')
BEGIN
    PRINT 'Corrigiendo tipo de dato en Equipos.Nombre...'
    
    -- Agregar columna temporal
    ALTER TABLE Equipos ADD NombreTemp nvarchar(100) NULL
    
    -- Actualizar con valores por defecto
    UPDATE Equipos SET NombreTemp = 'Equipo ' + CAST(IdEquipoDesarrollo AS VARCHAR)
    
    -- Eliminar columna original
    ALTER TABLE Equipos DROP COLUMN Nombre
    
    -- Renombrar columna temporal
    EXEC sp_rename 'Equipos.NombreTemp', 'Nombre', 'COLUMN'
    
    -- Hacer NOT NULL
    ALTER TABLE Equipos ALTER COLUMN Nombre nvarchar(100) NOT NULL
    
    PRINT 'Tipo de dato en Equipos.Nombre corregido'
END

-- 2. Corrección en tabla Roles: Verificar que Nombre sea nvarchar, no int
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
          WHERE TABLE_NAME = 'Roles' AND COLUMN_NAME = 'Nombre' AND DATA_TYPE = 'int')
BEGIN
    PRINT 'Corrigiendo tipo de dato en Roles.Nombre...'
    
    -- Agregar columna temporal
    ALTER TABLE Roles ADD NombreTemp nvarchar(100) NULL
    
    -- Actualizar con valores por defecto
    UPDATE Roles SET NombreTemp = 'Rol ' + CAST(IdRol AS VARCHAR)
    
    -- Eliminar columna original
    ALTER TABLE Roles DROP COLUMN Nombre
    
    -- Renombrar columna temporal
    EXEC sp_rename 'Roles.NombreTemp', 'Nombre', 'COLUMN'
    
    -- Hacer NOT NULL
    ALTER TABLE Roles ALTER COLUMN Nombre nvarchar(100) NOT NULL
    
    PRINT 'Tipo de dato en Roles.Nombre corregido'
END

-- 3. Agregar campos faltantes en GrillaTemas
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'GrillaTemas' AND COLUMN_NAME = 'Orden')
BEGIN
    ALTER TABLE GrillaTemas ADD Orden int NOT NULL DEFAULT 0
    PRINT 'Campo Orden agregado a GrillaTemas'
END

-- 4. Agregar campos faltantes en GrillaSubtemas
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'GrillaSubtemas' AND COLUMN_NAME = 'Orden')
BEGIN
    ALTER TABLE GrillaSubtemas ADD Orden int NOT NULL DEFAULT 0
    PRINT 'Campo Orden agregado a GrillaSubtemas'
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'GrillaSubtemas' AND COLUMN_NAME = 'Descripcion')
BEGIN
    ALTER TABLE GrillaSubtemas ADD Descripcion nvarchar(500) NULL
    PRINT 'Campo Descripcion agregado a GrillaSubtemas'
END

-- 5. Agregar campo Descripcion en Subtemas si no existe
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'Subtemas' AND COLUMN_NAME = 'Descripcion')
BEGIN
    ALTER TABLE Subtemas ADD Descripcion nvarchar(500) NULL
    PRINT 'Campo Descripcion agregado a Subtemas'
END

-- 6. Verificar que tabla TiposDocumentos tenga Abreviacion
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'TiposDocumentos' AND COLUMN_NAME = 'Abreviacion')
BEGIN
    ALTER TABLE TiposDocumentos ADD Abreviacion nvarchar(10) NULL
    PRINT 'Campo Abreviacion agregado a TiposDocumentos'
END

-- 7. Verificar campos adicionales en Recursos
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'Recursos' AND COLUMN_NAME = 'Apellidos')
BEGIN
    ALTER TABLE Recursos ADD Apellidos nvarchar(200) NULL
    PRINT 'Campo Apellidos agregado a Recursos'
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'Recursos' AND COLUMN_NAME = 'CorreoElectronico')
BEGIN
    ALTER TABLE Recursos ADD CorreoElectronico nvarchar(255) NOT NULL DEFAULT ''
    PRINT 'Campo CorreoElectronico agregado a Recursos'
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'Recursos' AND COLUMN_NAME = 'PerfilSeguridad')
BEGIN
    ALTER TABLE Recursos ADD PerfilSeguridad nvarchar(100) NOT NULL DEFAULT ''
    PRINT 'Campo PerfilSeguridad agregado a Recursos'
END

-- 8. Verificar que ValorFuncionalVerif y ValorTecnicoVerif permitan NULL
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
          WHERE TABLE_NAME = 'ConocimientoRecurso' AND COLUMN_NAME = 'ValorFuncionalVerif' AND IS_NULLABLE = 'NO')
BEGIN
    ALTER TABLE ConocimientoRecurso ALTER COLUMN ValorFuncionalVerif int NULL
    PRINT 'Campo ValorFuncionalVerif cambiado a nullable'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
          WHERE TABLE_NAME = 'ConocimientoRecurso' AND COLUMN_NAME = 'ValorTecnicoVerif' AND IS_NULLABLE = 'NO')
BEGIN
    ALTER TABLE ConocimientoRecurso ALTER COLUMN ValorTecnicoVerif int NULL
    PRINT 'Campo ValorTecnicoVerif cambiado a nullable'
END

PRINT 'Correcciones de estructura completadas'
GO

-- =============================================================================
-- ACTUALIZACIÓN DE DATOS EXISTENTES
-- =============================================================================

PRINT 'Actualizando datos existentes...'

-- Actualizar órden en temas si están todos en 0
UPDATE Temas SET Orden = IdTema WHERE Orden = 0

-- Actualizar órden en subtemas si están todos en 0
UPDATE Subtemas SET Orden = IdSubtema WHERE Orden = 0

-- Actualizar orden en GrillaTemas si existen datos
IF EXISTS (SELECT * FROM GrillaTemas WHERE Orden = 0)
BEGIN
    WITH CTE AS (
        SELECT IdGrillaTema, ROW_NUMBER() OVER (PARTITION BY IdGrilla ORDER BY IdGrillaTema) as NuevoOrden
        FROM GrillaTemas WHERE Orden = 0
    )
    UPDATE gt SET Orden = c.NuevoOrden
    FROM GrillaTemas gt
    INNER JOIN CTE c ON gt.IdGrillaTema = c.IdGrillaTema
    PRINT 'Orden actualizado en GrillaTemas'
END

-- Actualizar orden en GrillaSubtemas si existen datos
IF EXISTS (SELECT * FROM GrillaSubtemas WHERE Orden = 0)
BEGIN
    WITH CTE AS (
        SELECT IdGrillaSubtema, ROW_NUMBER() OVER (PARTITION BY IdGrillaTema ORDER BY IdGrillaSubtema) as NuevoOrden
        FROM GrillaSubtemas WHERE Orden = 0
    )
    UPDATE gs SET Orden = c.NuevoOrden
    FROM GrillaSubtemas gs
    INNER JOIN CTE c ON gs.IdGrillaSubtema = c.IdGrillaSubtema
    PRINT 'Orden actualizado en GrillaSubtemas'
END

PRINT 'Actualización de datos completada'
GO

-- =============================================================================
-- VERIFICACIÓN FINAL DE FOREIGN KEYS
-- =============================================================================

-- Verificar que todas las FK necesarias existan

-- FK que podrían faltar según el análisis
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Evaluacion_Recursos_IdRecurso')
BEGIN
    ALTER TABLE [dbo].[Evaluacion] ADD CONSTRAINT [FK_Evaluacion_Recursos_IdRecurso] 
    FOREIGN KEY([IdRecurso]) REFERENCES [dbo].[Recursos] ([IdRecurso]) ON DELETE CASCADE
    PRINT 'FK Evaluacion -> Recursos agregada'
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Evaluacion_Grillas_IdGrilla')
BEGIN
    ALTER TABLE [dbo].[Evaluacion] ADD CONSTRAINT [FK_Evaluacion_Grillas_IdGrilla] 
    FOREIGN KEY([IdGrilla]) REFERENCES [dbo].[Grillas] ([IdGrilla]) ON DELETE CASCADE
    PRINT 'FK Evaluacion -> Grillas agregada'
END

-- Verificar campos IdRecurso e IdGrilla en tabla Evaluacion
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'Evaluacion' AND COLUMN_NAME = 'IdRecurso')
BEGIN
    ALTER TABLE Evaluacion ADD IdRecurso int NOT NULL DEFAULT 1
    PRINT 'Campo IdRecurso agregado a Evaluacion'
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'Evaluacion' AND COLUMN_NAME = 'IdGrilla')
BEGIN
    ALTER TABLE Evaluacion ADD IdGrilla int NOT NULL DEFAULT 1
    PRINT 'Campo IdGrilla agregado a Evaluacion'
END

PRINT 'Verificación de Foreign Keys completada'
GO

-- =============================================================================
-- INSERCIÓN DE REGISTROS DE HISTORIAL DE MIGRACIONES
-- =============================================================================

-- Insertar registros de migraciones para que EF no trate de aplicarlas nuevamente
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20240906010128_Inicial', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20240906010128_Inicial')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20240906020619_Primera', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20240906020619_Primera')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20240906021736_Segunda', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20240906021736_Segunda')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20240906184014_Tercera', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20240906184014_Tercera')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20240906222242_Cuarta', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20240906222242_Cuarta')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20250127000000_CorregirEvaluacionGrillaRecurso', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20250127000000_CorregirEvaluacionGrillaRecurso')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20250307201327_quinta', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20250307201327_quinta')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20250308233038_Sexta', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20250308233038_Sexta')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20250310191812_septima', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20250310191812_septima')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20250314193611_octava', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20250314193611_octava')

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20250315233037_novena', '6.0.0')
WHERE NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20250315233037_novena')

PRINT 'Historial de migraciones actualizado'
GO

PRINT '====================================================================='
PRINT 'CORRECCIONES Y ACTUALIZACIONES COMPLETADAS'
PRINT '====================================================================='
PRINT 'La base de datos ha sido actualizada con todas las correcciones'
PRINT 'necesarias según las migraciones más recientes.'
PRINT '====================================================================='
GO