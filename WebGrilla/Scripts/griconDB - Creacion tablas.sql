USE [GriconDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConocimientoRecurso]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConocimientoRecurso](
	[IdConocimientoRecurso] [int] IDENTITY(1,1) NOT NULL,
	[ValorFuncional] [int] NOT NULL,
	[ValorTecnico] [int] NOT NULL,
	[ValorFuncionalVerif] [int] NOT NULL,
	[ValorTecnicoVerif] [int] NOT NULL,
	[IdRecurso] [int] NOT NULL,
	[IdGrilla] [int] NOT NULL,
	[IdSubtema] [int] NOT NULL,
	[IdEvaluacion] [int] NOT NULL,
 CONSTRAINT [PK_ConocimientoRecurso] PRIMARY KEY CLUSTERED 
(
	[IdConocimientoRecurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipos]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipos](
	[IdEquipoDesarrollo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [int] NOT NULL,
	[IdCliente] [int] NOT NULL,
 CONSTRAINT [PK_Equipos] PRIMARY KEY CLUSTERED 
(
	[IdEquipoDesarrollo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluacion]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluacion](
	[IdEvaluacion] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[FechaInicio] [datetime2](7) NOT NULL,
	[FechaFin] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Evaluacion] PRIMARY KEY CLUSTERED 
(
	[IdEvaluacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grillas]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grillas](
	[IdGrilla] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Grillas] PRIMARY KEY CLUSTERED 
(
	[IdGrilla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrillaSubtemas]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrillaSubtemas](
	[IdGrillaSubtema] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Ponderacion] [decimal](5, 2) NOT NULL,
	[IdGrillaTema] [int] NOT NULL,
	[IdSubtema] [int] NOT NULL,
 CONSTRAINT [PK_GrillaSubtemas] PRIMARY KEY CLUSTERED 
(
	[IdGrillaSubtema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrillaTemas]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrillaTemas](
	[IdGrillaTema] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Ponderacion] [decimal](5, 2) NOT NULL,
	[IdGrilla] [int] NOT NULL,
	[IdTema] [int] NOT NULL,
 CONSTRAINT [PK_GrillaTemas] PRIMARY KEY CLUSTERED 
(
	[IdGrillaTema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recursos]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recursos](
	[IdRecurso] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellido] [nvarchar](100) NOT NULL,
	[FechaIngreso] [datetime2](7) NOT NULL,
	[TipoDocumento] [int] NOT NULL,
	[NumeroDocumento] [decimal](17, 0) NOT NULL,
	[IdEquipoDesarrollo] [int] NOT NULL,
	[IdRol] [int] NOT NULL,
 CONSTRAINT [PK_Recursos] PRIMARY KEY CLUSTERED 
(
	[IdRecurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResultadoConocimiento]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResultadoConocimiento](
	[IdResultadoConocimiento] [int] IDENTITY(1,1) NOT NULL,
	[ValorFuncional] [decimal](5, 2) NOT NULL,
	[ValorTecnico] [decimal](5, 2) NOT NULL,
	[IdRecurso] [int] NOT NULL,
	[IdGrilla] [int] NOT NULL,
	[IdEvaluacion] [int] NOT NULL,
	[IdSubtema] [int] NOT NULL,
	[Id_Subtema] [int] NOT NULL,
 CONSTRAINT [PK_ResultadoConocimiento] PRIMARY KEY CLUSTERED 
(
	[IdResultadoConocimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [int] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subtemas]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subtemas](
	[IdSubtema] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Orden] [int] NOT NULL,
	[IdTema] [int] NOT NULL,
 CONSTRAINT [PK_Subtemas] PRIMARY KEY CLUSTERED 
(
	[IdSubtema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Temas]    Script Date: 09/09/2024 6:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Temas](
	[IdTema] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Orden] [int] NOT NULL,
 CONSTRAINT [PK_Temas] PRIMARY KEY CLUSTERED 
(
	[IdTema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Recursos] ADD  DEFAULT ((0)) FOR [IdRol]
GO
ALTER TABLE [dbo].[ConocimientoRecurso]  WITH CHECK ADD  CONSTRAINT [FK_ConocimientoRecurso_Evaluacion_IdEvaluacion] FOREIGN KEY([IdEvaluacion])
REFERENCES [dbo].[Evaluacion] ([IdEvaluacion])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConocimientoRecurso] CHECK CONSTRAINT [FK_ConocimientoRecurso_Evaluacion_IdEvaluacion]
GO
ALTER TABLE [dbo].[ConocimientoRecurso]  WITH CHECK ADD  CONSTRAINT [FK_ConocimientoRecurso_Grillas_IdGrilla] FOREIGN KEY([IdGrilla])
REFERENCES [dbo].[Grillas] ([IdGrilla])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConocimientoRecurso] CHECK CONSTRAINT [FK_ConocimientoRecurso_Grillas_IdGrilla]
GO
ALTER TABLE [dbo].[ConocimientoRecurso]  WITH CHECK ADD  CONSTRAINT [FK_ConocimientoRecurso_Recursos_IdRecurso] FOREIGN KEY([IdRecurso])
REFERENCES [dbo].[Recursos] ([IdRecurso])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConocimientoRecurso] CHECK CONSTRAINT [FK_ConocimientoRecurso_Recursos_IdRecurso]
GO
ALTER TABLE [dbo].[ConocimientoRecurso]  WITH CHECK ADD  CONSTRAINT [FK_ConocimientoRecurso_Subtemas_IdSubtema] FOREIGN KEY([IdSubtema])
REFERENCES [dbo].[Subtemas] ([IdSubtema])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConocimientoRecurso] CHECK CONSTRAINT [FK_ConocimientoRecurso_Subtemas_IdSubtema]
GO
ALTER TABLE [dbo].[Equipos]  WITH CHECK ADD  CONSTRAINT [FK_Equipos_Clientes_IdCliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Clientes] ([IdCliente])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Equipos] CHECK CONSTRAINT [FK_Equipos_Clientes_IdCliente]
GO
ALTER TABLE [dbo].[GrillaSubtemas]  WITH CHECK ADD  CONSTRAINT [FK_GrillaSubtemas_GrillaTemas_IdGrillaTema] FOREIGN KEY([IdGrillaTema])
REFERENCES [dbo].[GrillaTemas] ([IdGrillaTema])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GrillaSubtemas] CHECK CONSTRAINT [FK_GrillaSubtemas_GrillaTemas_IdGrillaTema]
GO
ALTER TABLE [dbo].[GrillaSubtemas]  WITH CHECK ADD  CONSTRAINT [FK_GrillaSubtemas_Subtemas_IdSubtema] FOREIGN KEY([IdSubtema])
REFERENCES [dbo].[Subtemas] ([IdSubtema])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GrillaSubtemas] CHECK CONSTRAINT [FK_GrillaSubtemas_Subtemas_IdSubtema]
GO
ALTER TABLE [dbo].[GrillaTemas]  WITH CHECK ADD  CONSTRAINT [FK_GrillaTemas_Grillas_IdGrilla] FOREIGN KEY([IdGrilla])
REFERENCES [dbo].[Grillas] ([IdGrilla])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GrillaTemas] CHECK CONSTRAINT [FK_GrillaTemas_Grillas_IdGrilla]
GO
ALTER TABLE [dbo].[GrillaTemas]  WITH CHECK ADD  CONSTRAINT [FK_GrillaTemas_Temas_IdTema] FOREIGN KEY([IdTema])
REFERENCES [dbo].[Temas] ([IdTema])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GrillaTemas] CHECK CONSTRAINT [FK_GrillaTemas_Temas_IdTema]
GO
ALTER TABLE [dbo].[Recursos]  WITH CHECK ADD  CONSTRAINT [FK_Recursos_Equipos_IdEquipoDesarrollo] FOREIGN KEY([IdEquipoDesarrollo])
REFERENCES [dbo].[Equipos] ([IdEquipoDesarrollo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Recursos] CHECK CONSTRAINT [FK_Recursos_Equipos_IdEquipoDesarrollo]
GO
ALTER TABLE [dbo].[Recursos]  WITH CHECK ADD  CONSTRAINT [FK_Recursos_Roles_IdRol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([IdRol])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Recursos] CHECK CONSTRAINT [FK_Recursos_Roles_IdRol]
GO
ALTER TABLE [dbo].[ResultadoConocimiento]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoConocimiento_Evaluacion_IdEvaluacion] FOREIGN KEY([IdEvaluacion])
REFERENCES [dbo].[Evaluacion] ([IdEvaluacion])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ResultadoConocimiento] CHECK CONSTRAINT [FK_ResultadoConocimiento_Evaluacion_IdEvaluacion]
GO
ALTER TABLE [dbo].[ResultadoConocimiento]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoConocimiento_Grillas_IdGrilla] FOREIGN KEY([IdGrilla])
REFERENCES [dbo].[Grillas] ([IdGrilla])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ResultadoConocimiento] CHECK CONSTRAINT [FK_ResultadoConocimiento_Grillas_IdGrilla]
GO
ALTER TABLE [dbo].[ResultadoConocimiento]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoConocimiento_GrillaSubtemas_IdSubtema] FOREIGN KEY([IdSubtema])
REFERENCES [dbo].[GrillaSubtemas] ([IdGrillaSubtema])
GO
ALTER TABLE [dbo].[ResultadoConocimiento] CHECK CONSTRAINT [FK_ResultadoConocimiento_GrillaSubtemas_IdSubtema]
GO
ALTER TABLE [dbo].[ResultadoConocimiento]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoConocimiento_Recursos_IdRecurso] FOREIGN KEY([IdRecurso])
REFERENCES [dbo].[Recursos] ([IdRecurso])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ResultadoConocimiento] CHECK CONSTRAINT [FK_ResultadoConocimiento_Recursos_IdRecurso]
GO
ALTER TABLE [dbo].[Subtemas]  WITH CHECK ADD  CONSTRAINT [FK_Subtemas_Temas_IdTema] FOREIGN KEY([IdTema])
REFERENCES [dbo].[Temas] ([IdTema])
GO
ALTER TABLE [dbo].[Subtemas] CHECK CONSTRAINT [FK_Subtemas_Temas_IdTema]
GO
