-- =============================================================================
-- SCRIPT COMPLETO DE IMPLEMENTACIÓN DE BASE DE DATOS - WEBGRILLA CONOCIMIENTO
-- =============================================================================
-- Versión: 1.0
-- Fecha: 2024-01-27
-- Descripción: Script completo para crear la base de datos del sistema de 
--              evaluación de conocimientos con todas las correcciones necesarias

USE [master]
GO

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'GriconDB')
BEGIN
    CREATE DATABASE [GriconDB]
    PRINT 'Base de datos GriconDB creada exitosamente'
END
GO

USE [GriconDB]
GO

-- =============================================================================
-- CONFIGURACIÓN INICIAL
-- =============================================================================

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================================================
-- CREACIÓN DE TABLAS
-- =============================================================================

-- Tabla: __EFMigrationsHistory (Control de migraciones de Entity Framework)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='__EFMigrationsHistory' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[__EFMigrationsHistory](
        [MigrationId] [nvarchar](150) NOT NULL,
        [ProductVersion] [nvarchar](32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
    )
    PRINT 'Tabla __EFMigrationsHistory creada'
END
GO

-- Tabla: TiposDocumentos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TiposDocumentos' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[TiposDocumentos](
        [IdTipoDocumento] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL,
        [Abreviacion] [nvarchar](10) NULL,
        CONSTRAINT [PK_TiposDocumentos] PRIMARY KEY CLUSTERED ([IdTipoDocumento] ASC)
    )
    PRINT 'Tabla TiposDocumentos creada'
END
GO

-- Tabla: Clientes
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clientes' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Clientes](
        [IdCliente] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([IdCliente] ASC)
    )
    PRINT 'Tabla Clientes creada'
END
GO

-- Tabla: Equipos (corregida de script original que tenía error en tipo de dato)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Equipos' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Equipos](
        [IdEquipoDesarrollo] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL, -- Corregido: era int, debe ser nvarchar
        [IdCliente] [int] NOT NULL,
        CONSTRAINT [PK_Equipos] PRIMARY KEY CLUSTERED ([IdEquipoDesarrollo] ASC)
    )
    PRINT 'Tabla Equipos creada'
END
GO

-- Tabla: Roles (corregida de script original que tenía error en tipo de dato)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Roles' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Roles](
        [IdRol] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL, -- Corregido: era int, debe ser nvarchar
        CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([IdRol] ASC)
    )
    PRINT 'Tabla Roles creada'
END
GO

-- Tabla: Temas
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Temas' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Temas](
        [IdTema] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL,
        [Orden] [int] NOT NULL,
        CONSTRAINT [PK_Temas] PRIMARY KEY CLUSTERED ([IdTema] ASC)
    )
    PRINT 'Tabla Temas creada'
END
GO

-- Tabla: Subtemas
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Subtemas' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Subtemas](
        [IdSubtema] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL,
        [Orden] [int] NOT NULL,
        [IdTema] [int] NOT NULL,
        CONSTRAINT [PK_Subtemas] PRIMARY KEY CLUSTERED ([IdSubtema] ASC)
    )
    PRINT 'Tabla Subtemas creada'
END
GO

-- Tabla: Grillas (con campos adicionales de migraciones recientes)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Grillas' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Grillas](
        [IdGrilla] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL,
        [Descripcion] [nvarchar](100) NOT NULL DEFAULT '',
        [Estado] [int] NOT NULL DEFAULT 0,
        [FechaVigencia] [datetime2](7) NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_Grillas] PRIMARY KEY CLUSTERED ([IdGrilla] ASC)
    )
    PRINT 'Tabla Grillas creada'
END
GO

-- Tabla: GrillaTemas
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='GrillaTemas' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[GrillaTemas](
        [IdGrillaTema] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL,
        [Ponderacion] [decimal](5, 2) NOT NULL,
        [Orden] [int] NOT NULL DEFAULT 0,
        [IdGrilla] [int] NOT NULL,
        [IdTema] [int] NOT NULL,
        CONSTRAINT [PK_GrillaTemas] PRIMARY KEY CLUSTERED ([IdGrillaTema] ASC)
    )
    PRINT 'Tabla GrillaTemas creada'
END
GO

-- Tabla: GrillaSubtemas (con campos adicionales)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='GrillaSubtemas' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[GrillaSubtemas](
        [IdGrillaSubtema] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](max) NOT NULL,
        [Ponderacion] [decimal](5, 2) NOT NULL,
        [Orden] [int] NOT NULL DEFAULT 0,
        [Descripcion] [nvarchar](500) NULL,
        [IdGrillaTema] [int] NOT NULL,
        [IdSubtema] [int] NOT NULL,
        CONSTRAINT [PK_GrillaSubtemas] PRIMARY KEY CLUSTERED ([IdGrillaSubtema] ASC)
    )
    PRINT 'Tabla GrillaSubtemas creada'
END
GO

-- Tabla: Recursos (con campos adicionales de migraciones recientes)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Recursos' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Recursos](
        [IdRecurso] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL,
        [Apellido] [nvarchar](100) NOT NULL,
        [Apellidos] [nvarchar](200) NULL, -- Campo adicional para múltiples apellidos
        [FechaIngreso] [datetime2](7) NOT NULL,
        [IdTipoDocumento] [int] NOT NULL, -- Corregido: referencia correcta a TipoDocumento
        [NumeroDocumento] [decimal](17, 0) NOT NULL,
        [CorreoElectronico] [nvarchar](255) NOT NULL DEFAULT '',
        [PerfilSeguridad] [nvarchar](100) NOT NULL DEFAULT '',
        [IdEquipoDesarrollo] [int] NOT NULL,
        [IdRol] [int] NOT NULL,
        [IdGrilla] [int] NULL, -- Grilla asignada al recurso (puede ser null)
        CONSTRAINT [PK_Recursos] PRIMARY KEY CLUSTERED ([IdRecurso] ASC)
    )
    PRINT 'Tabla Recursos creada'
END
GO

-- Tabla: Evaluacion (con relaciones correctas)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Evaluacion' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Evaluacion](
        [IdEvaluacion] [int] IDENTITY(1,1) NOT NULL,
        [Descripcion] [nvarchar](100) NOT NULL,
        [FechaInicio] [datetime2](7) NOT NULL,
        [FechaFin] [datetime2](7) NOT NULL,
        [IdRecurso] [int] NOT NULL, -- Recurso principal de la evaluación
        [IdGrilla] [int] NOT NULL,  -- Grilla utilizada en la evaluación
        CONSTRAINT [PK_Evaluacion] PRIMARY KEY CLUSTERED ([IdEvaluacion] ASC)
    )
    PRINT 'Tabla Evaluacion creada'
END
GO

-- Tabla: ConocimientoRecurso (tabla principal para evaluaciones)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ConocimientoRecurso' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[ConocimientoRecurso](
        [IdConocimientoRecurso] [int] IDENTITY(1,1) NOT NULL,
        [ValorFuncional] [int] NOT NULL DEFAULT 0,
        [ValorTecnico] [int] NOT NULL DEFAULT 0,
        [ValorFuncionalVerif] [int] NULL, -- Valores de verificación (pueden ser null)
        [ValorTecnicoVerif] [int] NULL,
        [IdRecurso] [int] NOT NULL,
        [IdGrilla] [int] NOT NULL,
        [IdSubtema] [int] NOT NULL,
        [IdEvaluacion] [int] NOT NULL,
        CONSTRAINT [PK_ConocimientoRecurso] PRIMARY KEY CLUSTERED ([IdConocimientoRecurso] ASC)
    )
    PRINT 'Tabla ConocimientoRecurso creada'
END
GO

-- Tabla: ResultadoConocimiento (para resultados consolidados)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ResultadoConocimiento' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[ResultadoConocimiento](
        [IdResultadoConocimiento] [int] IDENTITY(1,1) NOT NULL,
        [ValorFuncional] [decimal](5, 2) NOT NULL,
        [ValorTecnico] [decimal](5, 2) NOT NULL,
        [IdRecurso] [int] NOT NULL,
        [IdGrilla] [int] NOT NULL,
        [IdEvaluacion] [int] NOT NULL,
        [IdSubtema] [int] NOT NULL,
        [Id_Subtema] [int] NOT NULL, -- Campo legacy (mantener compatibilidad)
        CONSTRAINT [PK_ResultadoConocimiento] PRIMARY KEY CLUSTERED ([IdResultadoConocimiento] ASC)
    )
    PRINT 'Tabla ResultadoConocimiento creada'
END
GO

-- =============================================================================
-- CREACIÓN DE ÍNDICES
-- =============================================================================

-- Índices para Foreign Keys y consultas frecuentes
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Equipos_IdCliente')
    CREATE INDEX IX_Equipos_IdCliente ON Equipos (IdCliente)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Subtemas_IdTema')
    CREATE INDEX IX_Subtemas_IdTema ON Subtemas (IdTema)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_GrillaTemas_IdGrilla')
    CREATE INDEX IX_GrillaTemas_IdGrilla ON GrillaTemas (IdGrilla)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_GrillaTemas_IdTema')
    CREATE INDEX IX_GrillaTemas_IdTema ON GrillaTemas (IdTema)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_GrillaSubtemas_IdGrillaTema')
    CREATE INDEX IX_GrillaSubtemas_IdGrillaTema ON GrillaSubtemas (IdGrillaTema)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_GrillaSubtemas_IdSubtema')
    CREATE INDEX IX_GrillaSubtemas_IdSubtema ON GrillaSubtemas (IdSubtema)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Recursos_IdTipoDocumento')
    CREATE INDEX IX_Recursos_IdTipoDocumento ON Recursos (IdTipoDocumento)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Recursos_IdEquipoDesarrollo')
    CREATE INDEX IX_Recursos_IdEquipoDesarrollo ON Recursos (IdEquipoDesarrollo)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Recursos_IdRol')
    CREATE INDEX IX_Recursos_IdRol ON Recursos (IdRol)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Recursos_IdGrilla')
    CREATE INDEX IX_Recursos_IdGrilla ON Recursos (IdGrilla)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Evaluacion_IdRecurso')
    CREATE INDEX IX_Evaluacion_IdRecurso ON Evaluacion (IdRecurso)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Evaluacion_IdGrilla')
    CREATE INDEX IX_Evaluacion_IdGrilla ON Evaluacion (IdGrilla)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ConocimientoRecurso_IdRecurso')
    CREATE INDEX IX_ConocimientoRecurso_IdRecurso ON ConocimientoRecurso (IdRecurso)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ConocimientoRecurso_IdGrilla')
    CREATE INDEX IX_ConocimientoRecurso_IdGrilla ON ConocimientoRecurso (IdGrilla)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ConocimientoRecurso_IdSubtema')
    CREATE INDEX IX_ConocimientoRecurso_IdSubtema ON ConocimientoRecurso (IdSubtema)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ConocimientoRecurso_IdEvaluacion')
    CREATE INDEX IX_ConocimientoRecurso_IdEvaluacion ON ConocimientoRecurso (IdEvaluacion)

PRINT 'Índices creados exitosamente'
GO

-- =============================================================================
-- CREACIÓN DE FOREIGN KEYS
-- =============================================================================

-- FK: Equipos -> Clientes
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Equipos_Clientes_IdCliente')
BEGIN
    ALTER TABLE [dbo].[Equipos] ADD CONSTRAINT [FK_Equipos_Clientes_IdCliente] 
    FOREIGN KEY([IdCliente]) REFERENCES [dbo].[Clientes] ([IdCliente]) ON DELETE CASCADE
    PRINT 'FK Equipos -> Clientes creada'
END

-- FK: Subtemas -> Temas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Subtemas_Temas_IdTema')
BEGIN
    ALTER TABLE [dbo].[Subtemas] ADD CONSTRAINT [FK_Subtemas_Temas_IdTema] 
    FOREIGN KEY([IdTema]) REFERENCES [dbo].[Temas] ([IdTema]) ON DELETE NO ACTION
    PRINT 'FK Subtemas -> Temas creada'
END

-- FK: GrillaTemas -> Grillas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_GrillaTemas_Grillas_IdGrilla')
BEGIN
    ALTER TABLE [dbo].[GrillaTemas] ADD CONSTRAINT [FK_GrillaTemas_Grillas_IdGrilla] 
    FOREIGN KEY([IdGrilla]) REFERENCES [dbo].[Grillas] ([IdGrilla]) ON DELETE CASCADE
    PRINT 'FK GrillaTemas -> Grillas creada'
END

-- FK: GrillaTemas -> Temas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_GrillaTemas_Temas_IdTema')
BEGIN
    ALTER TABLE [dbo].[GrillaTemas] ADD CONSTRAINT [FK_GrillaTemas_Temas_IdTema] 
    FOREIGN KEY([IdTema]) REFERENCES [dbo].[Temas] ([IdTema]) ON DELETE CASCADE
    PRINT 'FK GrillaTemas -> Temas creada'
END

-- FK: GrillaSubtemas -> GrillaTemas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_GrillaSubtemas_GrillaTemas_IdGrillaTema')
BEGIN
    ALTER TABLE [dbo].[GrillaSubtemas] ADD CONSTRAINT [FK_GrillaSubtemas_GrillaTemas_IdGrillaTema] 
    FOREIGN KEY([IdGrillaTema]) REFERENCES [dbo].[GrillaTemas] ([IdGrillaTema]) ON DELETE CASCADE
    PRINT 'FK GrillaSubtemas -> GrillaTemas creada'
END

-- FK: GrillaSubtemas -> Subtemas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_GrillaSubtemas_Subtemas_IdSubtema')
BEGIN
    ALTER TABLE [dbo].[GrillaSubtemas] ADD CONSTRAINT [FK_GrillaSubtemas_Subtemas_IdSubtema] 
    FOREIGN KEY([IdSubtema]) REFERENCES [dbo].[Subtemas] ([IdSubtema]) ON DELETE CASCADE
    PRINT 'FK GrillaSubtemas -> Subtemas creada'
END

-- FK: Recursos -> TipoDocumento
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Recursos_TiposDocumentos_IdTipoDocumento')
BEGIN
    ALTER TABLE [dbo].[Recursos] ADD CONSTRAINT [FK_Recursos_TiposDocumentos_IdTipoDocumento] 
    FOREIGN KEY([IdTipoDocumento]) REFERENCES [dbo].[TiposDocumentos] ([IdTipoDocumento]) ON DELETE CASCADE
    PRINT 'FK Recursos -> TiposDocumentos creada'
END

-- FK: Recursos -> Equipos
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Recursos_Equipos_IdEquipoDesarrollo')
BEGIN
    ALTER TABLE [dbo].[Recursos] ADD CONSTRAINT [FK_Recursos_Equipos_IdEquipoDesarrollo] 
    FOREIGN KEY([IdEquipoDesarrollo]) REFERENCES [dbo].[Equipos] ([IdEquipoDesarrollo]) ON DELETE CASCADE
    PRINT 'FK Recursos -> Equipos creada'
END

-- FK: Recursos -> Roles
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Recursos_Roles_IdRol')
BEGIN
    ALTER TABLE [dbo].[Recursos] ADD CONSTRAINT [FK_Recursos_Roles_IdRol] 
    FOREIGN KEY([IdRol]) REFERENCES [dbo].[Roles] ([IdRol]) ON DELETE CASCADE
    PRINT 'FK Recursos -> Roles creada'
END

-- FK: Recursos -> Grillas (opcional)
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Recursos_Grillas_IdGrilla')
BEGIN
    ALTER TABLE [dbo].[Recursos] ADD CONSTRAINT [FK_Recursos_Grillas_IdGrilla] 
    FOREIGN KEY([IdGrilla]) REFERENCES [dbo].[Grillas] ([IdGrilla]) ON DELETE SET NULL
    PRINT 'FK Recursos -> Grillas creada'
END

-- FK: Evaluacion -> Recursos
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Evaluacion_Recursos_IdRecurso')
BEGIN
    ALTER TABLE [dbo].[Evaluacion] ADD CONSTRAINT [FK_Evaluacion_Recursos_IdRecurso] 
    FOREIGN KEY([IdRecurso]) REFERENCES [dbo].[Recursos] ([IdRecurso]) ON DELETE CASCADE
    PRINT 'FK Evaluacion -> Recursos creada'
END

-- FK: Evaluacion -> Grillas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Evaluacion_Grillas_IdGrilla')
BEGIN
    ALTER TABLE [dbo].[Evaluacion] ADD CONSTRAINT [FK_Evaluacion_Grillas_IdGrilla] 
    FOREIGN KEY([IdGrilla]) REFERENCES [dbo].[Grillas] ([IdGrilla]) ON DELETE CASCADE
    PRINT 'FK Evaluacion -> Grillas creada'
END

-- FK: ConocimientoRecurso -> Recursos
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ConocimientoRecurso_Recursos_IdRecurso')
BEGIN
    ALTER TABLE [dbo].[ConocimientoRecurso] ADD CONSTRAINT [FK_ConocimientoRecurso_Recursos_IdRecurso] 
    FOREIGN KEY([IdRecurso]) REFERENCES [dbo].[Recursos] ([IdRecurso]) ON DELETE CASCADE
    PRINT 'FK ConocimientoRecurso -> Recursos creada'
END

-- FK: ConocimientoRecurso -> Grillas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ConocimientoRecurso_Grillas_IdGrilla')
BEGIN
    ALTER TABLE [dbo].[ConocimientoRecurso] ADD CONSTRAINT [FK_ConocimientoRecurso_Grillas_IdGrilla] 
    FOREIGN KEY([IdGrilla]) REFERENCES [dbo].[Grillas] ([IdGrilla]) ON DELETE CASCADE
    PRINT 'FK ConocimientoRecurso -> Grillas creada'
END

-- FK: ConocimientoRecurso -> Subtemas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ConocimientoRecurso_Subtemas_IdSubtema')
BEGIN
    ALTER TABLE [dbo].[ConocimientoRecurso] ADD CONSTRAINT [FK_ConocimientoRecurso_Subtemas_IdSubtema] 
    FOREIGN KEY([IdSubtema]) REFERENCES [dbo].[Subtemas] ([IdSubtema]) ON DELETE CASCADE
    PRINT 'FK ConocimientoRecurso -> Subtemas creada'
END

-- FK: ConocimientoRecurso -> Evaluacion
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ConocimientoRecurso_Evaluacion_IdEvaluacion')
BEGIN
    ALTER TABLE [dbo].[ConocimientoRecurso] ADD CONSTRAINT [FK_ConocimientoRecurso_Evaluacion_IdEvaluacion] 
    FOREIGN KEY([IdEvaluacion]) REFERENCES [dbo].[Evaluacion] ([IdEvaluacion]) ON DELETE CASCADE
    PRINT 'FK ConocimientoRecurso -> Evaluacion creada'
END

-- FK: ResultadoConocimiento -> Recursos
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ResultadoConocimiento_Recursos_IdRecurso')
BEGIN
    ALTER TABLE [dbo].[ResultadoConocimiento] ADD CONSTRAINT [FK_ResultadoConocimiento_Recursos_IdRecurso] 
    FOREIGN KEY([IdRecurso]) REFERENCES [dbo].[Recursos] ([IdRecurso]) ON DELETE CASCADE
    PRINT 'FK ResultadoConocimiento -> Recursos creada'
END

-- FK: ResultadoConocimiento -> Grillas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ResultadoConocimiento_Grillas_IdGrilla')
BEGIN
    ALTER TABLE [dbo].[ResultadoConocimiento] ADD CONSTRAINT [FK_ResultadoConocimiento_Grillas_IdGrilla] 
    FOREIGN KEY([IdGrilla]) REFERENCES [dbo].[Grillas] ([IdGrilla]) ON DELETE CASCADE
    PRINT 'FK ResultadoConocimiento -> Grillas creada'
END

-- FK: ResultadoConocimiento -> Evaluacion
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ResultadoConocimiento_Evaluacion_IdEvaluacion')
BEGIN
    ALTER TABLE [dbo].[ResultadoConocimiento] ADD CONSTRAINT [FK_ResultadoConocimiento_Evaluacion_IdEvaluacion] 
    FOREIGN KEY([IdEvaluacion]) REFERENCES [dbo].[Evaluacion] ([IdEvaluacion]) ON DELETE CASCADE
    PRINT 'FK ResultadoConocimiento -> Evaluacion creada'
END

-- FK: ResultadoConocimiento -> GrillaSubtemas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ResultadoConocimiento_GrillaSubtemas_IdSubtema')
BEGIN
    ALTER TABLE [dbo].[ResultadoConocimiento] ADD CONSTRAINT [FK_ResultadoConocimiento_GrillaSubtemas_IdSubtema] 
    FOREIGN KEY([IdSubtema]) REFERENCES [dbo].[GrillaSubtemas] ([IdGrillaSubtema]) ON DELETE NO ACTION
    PRINT 'FK ResultadoConocimiento -> GrillaSubtemas creada'
END

PRINT 'Foreign Keys creadas exitosamente'
GO

-- =============================================================================
-- INSERCIÓN DE DATOS BÁSICOS (SEEDS)
-- =============================================================================

-- Insertar tipos de documento básicos
IF NOT EXISTS (SELECT * FROM TiposDocumentos)
BEGIN
    INSERT INTO TiposDocumentos (Nombre, Abreviacion) VALUES 
        ('Documento Nacional de Identidad', 'DNI'),
        ('Cédula de Identidad', 'CI'),
        ('Pasaporte', 'PAS'),
        ('Cédula de Extranjería', 'CE')
    PRINT 'Tipos de documento insertados'
END

-- Insertar cliente básico
IF NOT EXISTS (SELECT * FROM Clientes)
BEGIN
    INSERT INTO Clientes (Nombre) VALUES ('Cliente Default')
    PRINT 'Cliente default insertado'
END

-- Insertar equipo básico
IF NOT EXISTS (SELECT * FROM Equipos)
BEGIN
    INSERT INTO Equipos (Nombre, IdCliente) VALUES ('Equipo Default', 1)
    PRINT 'Equipo default insertado'
END

-- Insertar rol básico
IF NOT EXISTS (SELECT * FROM Roles)
BEGIN
    INSERT INTO Roles (Nombre) VALUES 
        ('Desarrollador'),
        ('Analista'),
        ('Arquitecto'),
        ('Tester'),
        ('DevOps')
    PRINT 'Roles básicos insertados'
END

-- Insertar grilla básica
IF NOT EXISTS (SELECT * FROM Grillas)
BEGIN
    INSERT INTO Grillas (Nombre, Descripcion, Estado, FechaVigencia) VALUES 
        ('Grilla General', 'Grilla de evaluación general', 1, GETDATE())
    PRINT 'Grilla básica insertada'
END

PRINT 'Datos básicos insertados exitosamente'
GO

-- =============================================================================
-- VERIFICACIÓN FINAL
-- =============================================================================

-- Mostrar resumen de tablas creadas
SELECT 
    TABLE_NAME as 'Tabla',
    (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = t.TABLE_NAME) as 'Columnas'
FROM INFORMATION_SCHEMA.TABLES t 
WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'dbo'
ORDER BY TABLE_NAME

-- Mostrar foreign keys creadas
SELECT 
    fk.name AS 'Foreign Key',
    tp.name AS 'Tabla Padre',
    tr.name AS 'Tabla Referenciada'
FROM sys.foreign_keys fk
    INNER JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
    INNER JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
ORDER BY fk.name

PRINT '====================================================================='
PRINT 'IMPLEMENTACIÓN DE BASE DE DATOS COMPLETADA EXITOSAMENTE'
PRINT '====================================================================='
PRINT 'La base de datos GriconDB ha sido creada con todas las tablas,'
PRINT 'índices, foreign keys y datos básicos necesarios para el sistema'
PRINT 'de evaluación de conocimientos.'
PRINT ''
PRINT 'Siguientes pasos recomendados:'
PRINT '1. Ejecutar el script de carga de temas y subtemas'
PRINT '2. Configurar usuarios y permisos de base de datos'
PRINT '3. Verificar la conexión desde la aplicación'
PRINT '====================================================================='
GO