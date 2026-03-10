-- Script para crear la tabla RecursosSupervisores
-- Ejecutar este script en la base de datos

USE [WebGrillaConocimiento] -- Cambiar por el nombre de tu base de datos
GO

-- Crear la tabla RecursosSupervisores
CREATE TABLE [dbo].[RecursosSupervisores](
    [IdRecursoSupervisor] [int] IDENTITY(1,1) NOT NULL,
    [IdRecursoSupervisorAsignado] [int] NOT NULL,
    [IdRecursoSupervisado] [int] NOT NULL,
    [FechaAsignacion] [datetime2](7) NOT NULL,
    [Activo] [bit] NOT NULL DEFAULT ((1)),
    [FechaBaja] [datetime2](7) NULL,
    [Observaciones] [nvarchar](500) NULL,
    
    CONSTRAINT [PK_RecursosSupervisores] PRIMARY KEY CLUSTERED ([IdRecursoSupervisor] ASC),
    
    CONSTRAINT [FK_RecursosSupervisores_Recursos_IdRecursoSupervisorAsignado] 
        FOREIGN KEY([IdRecursoSupervisorAsignado])
        REFERENCES [dbo].[Recursos] ([IdRecurso])
        ON DELETE NO ACTION,
    
    CONSTRAINT [FK_RecursosSupervisores_Recursos_IdRecursoSupervisado] 
        FOREIGN KEY([IdRecursoSupervisado])
        REFERENCES [dbo].[Recursos] ([IdRecurso])
        ON DELETE NO ACTION
);
GO

-- Crear índice único para evitar duplicados
CREATE UNIQUE NONCLUSTERED INDEX [IX_RecursoSupervisor_Unique] 
ON [dbo].[RecursosSupervisores] ([IdRecursoSupervisorAsignado] ASC, [IdRecursoSupervisado] ASC);
GO

-- Crear índice para el campo IdRecursoSupervisado
CREATE NONCLUSTERED INDEX [IX_RecursosSupervisores_IdRecursoSupervisado] 
ON [dbo].[RecursosSupervisores] ([IdRecursoSupervisado] ASC);
GO

-- Insertar registros de ejemplo para pruebas (opcional)
-- Asegurate de que existan recursos con estos IDs en tu tabla Recursos
/*
INSERT INTO [dbo].[RecursosSupervisores] 
([IdRecursoSupervisorAsignado], [IdRecursoSupervisado], [FechaAsignacion], [Activo], [Observaciones])
VALUES 
(1, 2, GETDATE(), 1, 'Asignación inicial de prueba'),
(1, 3, GETDATE(), 1, 'Segundo supervisado para el mismo supervisor'),
(4, 5, GETDATE(), 1, 'Relación supervisor-supervisado adicional');
*/

PRINT 'Tabla RecursosSupervisores creada exitosamente';