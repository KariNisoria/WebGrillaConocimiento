-- Script para crear la tabla RolPermiso y configurar permisos del sistema
-- Este script debe ejecutarse después de la configuración de autenticación

USE [WebGrillaConocimiento]
GO

-- Crear tabla RolPermiso si no existe
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RolPermisos' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[RolPermisos](
        [IdRolPermiso] [int] IDENTITY(1,1) NOT NULL,
        [IdRol] [int] NOT NULL,
        [CodigoPermiso] [nvarchar](100) NOT NULL,
        [Descripcion] [nvarchar](200) NULL,
        [Activo] [bit] NOT NULL DEFAULT 1,
        [FechaCreacion] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT [PK_RolPermisos] PRIMARY KEY CLUSTERED ([IdRolPermiso] ASC),
        CONSTRAINT [FK_RolPermisos_Roles] FOREIGN KEY([IdRol]) REFERENCES [dbo].[Roles] ([IdRol])
    )
    
    PRINT 'Tabla RolPermisos creada exitosamente'
END
ELSE
BEGIN
    PRINT 'La tabla RolPermisos ya existe'
END
GO

-- Limpiar permisos existentes para reconfigurar
DELETE FROM RolPermisos
PRINT 'Permisos anteriores limpiados'

-- Obtener IDs de roles
DECLARE @AdminId INT = (SELECT IdRol FROM Roles WHERE Nombre = 'Administrador')
DECLARE @ManagerId INT = (SELECT IdRol FROM Roles WHERE Nombre = 'Manager')
DECLARE @EvaluadorId INT = (SELECT IdRol FROM Roles WHERE Nombre = 'Evaluador')
DECLARE @DesarrolladorId INT = (SELECT IdRol FROM Roles WHERE Nombre = 'Desarrollador')

-- ====================================
-- PERMISOS PARA ADMINISTRADOR (COMPLETO)
-- ====================================
INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion) VALUES
-- Recursos
(@AdminId, 'RECURSOS_READ', 'Consultar recursos/personas'),
(@AdminId, 'RECURSOS_WRITE', 'Crear/editar recursos/personas'),
(@AdminId, 'RECURSOS_DELETE', 'Eliminar recursos/personas'),

-- Roles
(@AdminId, 'ROLES_READ', 'Consultar roles'),
(@AdminId, 'ROLES_WRITE', 'Crear/editar roles'),
(@AdminId, 'ROLES_DELETE', 'Eliminar roles'),

-- Grillas
(@AdminId, 'GRILLAS_READ', 'Consultar grillas de conocimiento'),
(@AdminId, 'GRILLAS_WRITE', 'Crear/editar grillas de conocimiento'),
(@AdminId, 'GRILLAS_DELETE', 'Eliminar grillas de conocimiento'),

-- Temas
(@AdminId, 'TEMAS_READ', 'Consultar temas'),
(@AdminId, 'TEMAS_WRITE', 'Crear/editar temas'),
(@AdminId, 'TEMAS_DELETE', 'Eliminar temas'),

-- Subtemas
(@AdminId, 'SUBTEMAS_READ', 'Consultar subtemas'),
(@AdminId, 'SUBTEMAS_WRITE', 'Crear/editar subtemas'),
(@AdminId, 'SUBTEMAS_DELETE', 'Eliminar subtemas'),

-- Evaluaciones
(@AdminId, 'EVALUACIONES_READ', 'Consultar evaluaciones'),
(@AdminId, 'EVALUACIONES_WRITE', 'Crear/editar evaluaciones'),
(@AdminId, 'EVALUACIONES_DELETE', 'Eliminar evaluaciones'),
(@AdminId, 'EVALUACIONES_GLOBALES_READ', 'Ver estadísticas globales'),

-- Clientes
(@AdminId, 'CLIENTES_READ', 'Consultar clientes'),
(@AdminId, 'CLIENTES_WRITE', 'Crear/editar clientes'),
(@AdminId, 'CLIENTES_DELETE', 'Eliminar clientes'),

-- Equipos
(@AdminId, 'EQUIPOS_READ', 'Consultar equipos'),
(@AdminId, 'EQUIPOS_WRITE', 'Crear/editar equipos'),
(@AdminId, 'EQUIPOS_DELETE', 'Eliminar equipos'),

-- Tipos de documento
(@AdminId, 'TIPOS_DOCUMENTO_READ', 'Consultar tipos de documento'),
(@AdminId, 'TIPOS_DOCUMENTO_WRITE', 'Crear/editar tipos de documento'),
(@AdminId, 'TIPOS_DOCUMENTO_DELETE', 'Eliminar tipos de documento'),

-- Permisos especiales
(@AdminId, 'PERMISOS_ADMIN', 'Administrar permisos del sistema'),
(@AdminId, 'CONFIGURACION_SISTEMA', 'Configurar parámetros del sistema');

-- ====================================
-- PERMISOS PARA MANAGER
-- ====================================
INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion) VALUES
-- Recursos (gestión limitada)
(@ManagerId, 'RECURSOS_READ', 'Consultar recursos/personas'),
(@ManagerId, 'RECURSOS_WRITE', 'Crear/editar recursos/personas'),

-- Roles (solo lectura)
(@ManagerId, 'ROLES_READ', 'Consultar roles'),

-- Grillas (gestión completa)
(@ManagerId, 'GRILLAS_READ', 'Consultar grillas de conocimiento'),
(@ManagerId, 'GRILLAS_WRITE', 'Crear/editar grillas de conocimiento'),

-- Temas y subtemas
(@ManagerId, 'TEMAS_READ', 'Consultar temas'),
(@ManagerId, 'TEMAS_WRITE', 'Crear/editar temas'),
(@ManagerId, 'SUBTEMAS_READ', 'Consultar subtemas'),
(@ManagerId, 'SUBTEMAS_WRITE', 'Crear/editar subtemas'),

-- Evaluaciones
(@ManagerId, 'EVALUACIONES_READ', 'Consultar evaluaciones'),
(@ManagerId, 'EVALUACIONES_WRITE', 'Crear/editar evaluaciones'),
(@ManagerId, 'EVALUACIONES_GLOBALES_READ', 'Ver estadísticas globales'),

-- Clientes
(@ManagerId, 'CLIENTES_READ', 'Consultar clientes'),
(@ManagerId, 'CLIENTES_WRITE', 'Crear/editar clientes'),

-- Equipos (solo lectura)
(@ManagerId, 'EQUIPOS_READ', 'Consultar equipos');

-- ====================================
-- PERMISOS PARA EVALUADOR
-- ====================================
INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion) VALUES
-- Recursos (solo lectura)
(@EvaluadorId, 'RECURSOS_READ', 'Consultar recursos/personas'),

-- Grillas y temas (solo lectura)
(@EvaluadorId, 'GRILLAS_READ', 'Consultar grillas de conocimiento'),
(@EvaluadorId, 'TEMAS_READ', 'Consultar temas'),
(@EvaluadorId, 'SUBTEMAS_READ', 'Consultar subtemas'),

-- Evaluaciones (gestión completa)
(@EvaluadorId, 'EVALUACIONES_READ', 'Consultar evaluaciones'),
(@EvaluadorId, 'EVALUACIONES_WRITE', 'Crear/editar evaluaciones'),
(@EvaluadorId, 'EVALUACIONES_GLOBALES_READ', 'Ver estadísticas globales');

-- ====================================
-- PERMISOS PARA DESARROLLADOR
-- ====================================
INSERT INTO RolPermisos (IdRol, CodigoPermiso, Descripcion) VALUES
-- Solo permisos básicos de consulta
(@DesarrolladorId, 'RECURSOS_READ', 'Consultar recursos/personas'),
(@DesarrolladorId, 'GRILLAS_READ', 'Consultar grillas de conocimiento'),
(@DesarrolladorId, 'TEMAS_READ', 'Consultar temas'),
(@DesarrolladorId, 'SUBTEMAS_READ', 'Consultar subtemas'),
(@DesarrolladorId, 'EVALUACIONES_READ', 'Consultar evaluaciones propias'),
(@DesarrolladorId, 'EVALUACIONES_GLOBALES_READ', 'Ver estadísticas globales');

PRINT ''
PRINT '=========================================='
PRINT 'CONFIGURACIÓN DE PERMISOS COMPLETADA'
PRINT '=========================================='
PRINT ''
PRINT 'Permisos configurados por rol:'
PRINT '• Administrador: ' + CAST((SELECT COUNT(*) FROM RolPermisos WHERE IdRol = @AdminId) AS VARCHAR) + ' permisos'
PRINT '• Manager: ' + CAST((SELECT COUNT(*) FROM RolPermisos WHERE IdRol = @ManagerId) AS VARCHAR) + ' permisos'  
PRINT '• Evaluador: ' + CAST((SELECT COUNT(*) FROM RolPermisos WHERE IdRol = @EvaluadorId) AS VARCHAR) + ' permisos'
PRINT '• Desarrollador: ' + CAST((SELECT COUNT(*) FROM RolPermisos WHERE IdRol = @DesarrolladorId) AS VARCHAR) + ' permisos'
PRINT ''
PRINT 'Para gestionar permisos:'
PRINT '1. Ejecuta este script para configurar permisos base'
PRINT '2. Usa consultas SQL para modificar permisos específicos'  
PRINT '3. En el futuro se agregará interfaz web para gestión'
PRINT '=========================================='

-- Consulta para ver todos los permisos por rol
SELECT 
    r.Nombre as Rol,
    rp.CodigoPermiso,
    rp.Descripcion,
    rp.Activo,
    rp.FechaCreacion
FROM RolPermisos rp
INNER JOIN Roles r ON r.IdRol = rp.IdRol
ORDER BY r.Nombre, rp.CodigoPermiso

GO