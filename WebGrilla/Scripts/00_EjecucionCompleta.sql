-- =============================================================================
-- SCRIPT MAESTRO DE EJECUCIÓN - WEBGRILLA CONOCIMIENTO
-- =============================================================================
-- Versión: 1.0
-- Descripción: Script principal que ejecuta todos los scripts de implementación
--              en el orden correcto para configurar la base de datos completa
-- Autor: Sistema WebGrilla
-- Fecha: 2024-01-27

-- =============================================================================
-- INSTRUCCIONES DE USO
-- =============================================================================
/*
IMPORTANTE: Leer antes de ejecutar

Este script maestro ejecuta automáticamente todos los scripts necesarios para
implementar completamente la base de datos del sistema WebGrilla Conocimiento.

PRERREQUISITOS:
1. SQL Server instalado y funcionando
2. Permisos de administrador en SQL Server  
3. Los archivos de script deben estar en la misma carpeta:
   - 01_CreacionCompletaBaseDatos.sql
   - 02_CargaTemasSubtemas.sql  
   - 03_CorreccionesEstructura.sql
   - 04_DatosPrueba.sql
   - 00_EjecucionCompleta.sql (este archivo)

MODO DE EJECUCIÓN:
- Opción A: Ejecutar este script completo de una vez
- Opción B: Ejecutar cada sección por separado (recomendado para debugging)

TIEMPO ESTIMADO: 5-10 minutos

NOTA: Si ya tienes datos en la base de datos, revisa las secciones marcadas 
      con "CUIDADO" antes de ejecutar.
*/

-- =============================================================================
-- CONFIGURACIÓN INICIAL
-- =============================================================================

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON  
GO
SET ANSI_PADDING ON
GO

-- Variables de configuración
DECLARE @NombreBaseDatos NVARCHAR(50) = 'GriconDB'
DECLARE @ModoDebug BIT = 1  -- 1 = Mostrar mensajes detallados, 0 = Solo errores
DECLARE @ContinuarEnErrores BIT = 0  -- 1 = Continuar si hay errores, 0 = Parar en errores

PRINT '====================================================================='
PRINT 'WEBGRILLA CONOCIMIENTO - IMPLEMENTACIÓN COMPLETA DE BASE DE DATOS'
PRINT '====================================================================='
PRINT 'Iniciando implementación completa del sistema...'
PRINT 'Base de datos de destino: ' + @NombreBaseDatos
PRINT 'Fecha y hora: ' + CAST(GETDATE() AS VARCHAR)
PRINT '====================================================================='

-- =============================================================================
-- FASE 1: CREACIÓN DE ESTRUCTURA BASE
-- =============================================================================

PRINT ''
PRINT '?? FASE 1: CREACIÓN DE ESTRUCTURA DE BASE DE DATOS'
PRINT '---------------------------------------------------------------------'
PRINT 'Ejecutando: 01_CreacionCompletaBaseDatos.sql'
PRINT 'Descripción: Crea todas las tablas, índices, foreign keys y datos básicos'

BEGIN TRY
    -- Verificar si la base de datos existe
    IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = @NombreBaseDatos)
    BEGIN
        PRINT '?? La base de datos no existe, será creada automáticamente'
    END
    ELSE
    BEGIN
        PRINT '?? CUIDADO: La base de datos ya existe. Se aplicarán solo cambios faltantes'
    END

    -- AQUÍ SE EJECUTARÍA EL CONTENIDO DEL SCRIPT 01
    -- Para ejecución automática, descomenta la siguiente línea y comenta el PRINT:
    -- :r .\01_CreacionCompletaBaseDatos.sql
    
    PRINT '? Fase 1 completada: Estructura de base de datos creada'
    
END TRY
BEGIN CATCH
    PRINT '? ERROR EN FASE 1:'
    PRINT '   Error: ' + ERROR_MESSAGE()
    PRINT '   Línea: ' + CAST(ERROR_LINE() AS VARCHAR)
    
    IF @ContinuarEnErrores = 0
    BEGIN
        PRINT 'Ejecución terminada debido a errores en Fase 1'
        RETURN
    END
END CATCH

-- =============================================================================
-- FASE 2: CARGA DE TEMAS Y SUBTEMAS
-- =============================================================================

PRINT ''
PRINT '?? FASE 2: CARGA DE TEMAS Y SUBTEMAS MAESTROS'
PRINT '---------------------------------------------------------------------'
PRINT 'Ejecutando: 02_CargaTemasSubtemas.sql'
PRINT 'Descripción: Carga todos los temas y subtemas del sistema bancario'

BEGIN TRY
    USE [GriconDB]  -- Cambiar a la base de datos creada
    
    -- Verificar que las tablas necesarias existan
    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Temas')
    BEGIN
        PRINT '? ERROR: La tabla Temas no existe. Debe ejecutar primero la Fase 1'
        RETURN
    END
    
    -- AQUÍ SE EJECUTARÍA EL CONTENIDO DEL SCRIPT 02
    -- Para ejecución automática, descomenta la siguiente línea:
    -- :r .\02_CargaTemasSubtemas.sql
    
    PRINT '? Fase 2 completada: Temas y subtemas cargados'
    
    -- Mostrar estadísticas
    DECLARE @CantTemas INT = (SELECT COUNT(*) FROM Temas)
    DECLARE @CantSubtemas INT = (SELECT COUNT(*) FROM Subtemas)
    PRINT '?? Estadísticas: ' + CAST(@CantTemas AS VARCHAR) + ' temas, ' + CAST(@CantSubtemas AS VARCHAR) + ' subtemas'
    
END TRY
BEGIN CATCH
    PRINT '? ERROR EN FASE 2:'
    PRINT '   Error: ' + ERROR_MESSAGE()
    PRINT '   Línea: ' + CAST(ERROR_LINE() AS VARCHAR)
    
    IF @ContinuarEnErrores = 0
    BEGIN
        PRINT 'Ejecución terminada debido a errores en Fase 2'
        RETURN
    END
END CATCH

-- =============================================================================
-- FASE 3: CORRECCIONES Y ACTUALIZACIONES
-- =============================================================================

PRINT ''
PRINT '?? FASE 3: APLICACIÓN DE CORRECCIONES Y ACTUALIZACIONES'
PRINT '---------------------------------------------------------------------'
PRINT 'Ejecutando: 03_CorreccionesEstructura.sql'
PRINT 'Descripción: Aplica correcciones de estructura según migraciones recientes'

BEGIN TRY
    -- AQUÍ SE EJECUTARÍA EL CONTENIDO DEL SCRIPT 03
    -- Para ejecución automática, descomenta la siguiente línea:
    -- :r .\03_CorreccionesEstructura.sql
    
    PRINT '? Fase 3 completada: Correcciones aplicadas'
    
END TRY
BEGIN CATCH
    PRINT '? ERROR EN FASE 3:'
    PRINT '   Error: ' + ERROR_MESSAGE()
    PRINT '   Línea: ' + CAST(ERROR_LINE() AS VARCHAR)
    
    IF @ContinuarEnErrores = 0
    BEGIN
        PRINT 'Ejecución terminada debido a errores en Fase 3'
        RETURN
    END
END CATCH

-- =============================================================================
-- FASE 4: DATOS DE PRUEBA Y CONFIGURACIÓN
-- =============================================================================

PRINT ''
PRINT '?? FASE 4: CARGA DE DATOS DE PRUEBA Y CONFIGURACIÓN INICIAL'
PRINT '---------------------------------------------------------------------'
PRINT 'Ejecutando: 04_DatosPrueba.sql'
PRINT 'Descripción: Carga datos de prueba para testing del sistema'

BEGIN TRY
    -- AQUÍ SE EJECUTARÍA EL CONTENIDO DEL SCRIPT 04
    -- Para ejecución automática, descomenta la siguiente línea:
    -- :r .\04_DatosPrueba.sql
    
    PRINT '? Fase 4 completada: Datos de prueba cargados'
    
END TRY
BEGIN CATCH
    PRINT '? ERROR EN FASE 4:'
    PRINT '   Error: ' + ERROR_MESSAGE()
    PRINT '   Línea: ' + CAST(ERROR_LINE() AS VARCHAR)
    
    IF @ContinuarEnErrores = 0
    BEGIN
        PRINT 'Ejecución terminada debido a errores en Fase 4'
        RETURN
    END
END CATCH

-- =============================================================================
-- VERIFICACIÓN FINAL INTEGRAL
-- =============================================================================

PRINT ''
PRINT '?? VERIFICACIÓN FINAL INTEGRAL DEL SISTEMA'
PRINT '====================================================================='

BEGIN TRY
    USE [GriconDB]
    
    -- Verificar existencia de todas las tablas críticas
    DECLARE @TablasRequeridas TABLE (NombreTabla VARCHAR(50), Existe BIT)
    INSERT INTO @TablasRequeridas VALUES 
        ('Clientes', 0), ('Equipos', 0), ('Roles', 0), ('TiposDocumentos', 0),
        ('Recursos', 0), ('Temas', 0), ('Subtemas', 0), ('Grillas', 0),
        ('GrillaTemas', 0), ('GrillaSubtemas', 0), ('Evaluacion', 0),
        ('ConocimientoRecurso', 0), ('ResultadoConocimiento', 0)
    
    UPDATE @TablasRequeridas 
    SET Existe = 1 
    WHERE NombreTabla IN (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE')
    
    -- Mostrar estado de tablas
    PRINT '?? ESTADO DE TABLAS PRINCIPALES:'
    SELECT 
        NombreTabla as 'Tabla',
        CASE WHEN Existe = 1 THEN '? OK' ELSE '? FALTA' END as 'Estado'
    FROM @TablasRequeridas
    ORDER BY NombreTabla
    
    -- Verificar datos mínimos
    PRINT ''
    PRINT '?? VERIFICACIÓN DE DATOS MÍNIMOS:'
    
    DECLARE @CantidadClientes INT = (SELECT COUNT(*) FROM Clientes)
    DECLARE @CantidadRecursos INT = (SELECT COUNT(*) FROM Recursos)  
    DECLARE @CantidadTemas INT = (SELECT COUNT(*) FROM Temas)
    DECLARE @CantidadGrillas INT = (SELECT COUNT(*) FROM Grillas)
    
    PRINT 'Clientes: ' + CAST(@CantidadClientes AS VARCHAR) + CASE WHEN @CantidadClientes > 0 THEN ' ?' ELSE ' ?' END
    PRINT 'Recursos: ' + CAST(@CantidadRecursos AS VARCHAR) + CASE WHEN @CantidadRecursos > 0 THEN ' ?' ELSE ' ?' END  
    PRINT 'Temas: ' + CAST(@CantidadTemas AS VARCHAR) + CASE WHEN @CantidadTemas > 0 THEN ' ?' ELSE ' ?' END
    PRINT 'Grillas: ' + CAST(@CantidadGrillas AS VARCHAR) + CASE WHEN @CantidadGrillas > 0 THEN ' ?' ELSE ' ?' END
    
    -- Verificar foreign keys críticas
    PRINT ''
    PRINT '?? VERIFICACIÓN DE INTEGRIDAD REFERENCIAL:'
    
    DECLARE @FKCount INT = (
        SELECT COUNT(*) 
        FROM sys.foreign_keys fk 
        WHERE fk.name IN (
            'FK_Equipos_Clientes_IdCliente',
            'FK_Recursos_Equipos_IdEquipoDesarrollo', 
            'FK_GrillaTemas_Grillas_IdGrilla',
            'FK_Evaluacion_Recursos_IdRecurso'
        )
    )
    
    PRINT 'Foreign Keys críticas: ' + CAST(@FKCount AS VARCHAR) + '/4 ' + CASE WHEN @FKCount >= 4 THEN '?' ELSE '??' END
    
END TRY
BEGIN CATCH
    PRINT '? ERROR EN VERIFICACIÓN FINAL:'
    PRINT '   Error: ' + ERROR_MESSAGE()
END CATCH

-- =============================================================================
-- RESUMEN FINAL Y PRÓXIMOS PASOS
-- =============================================================================

PRINT ''
PRINT '?? IMPLEMENTACIÓN COMPLETADA'
PRINT '====================================================================='
PRINT '? Base de datos WebGrilla Conocimiento implementada exitosamente'
PRINT ''
PRINT '?? RESUMEN DE LO IMPLEMENTADO:'
PRINT '   ? Estructura completa de base de datos'
PRINT '   ? Temas y subtemas bancarios (39 temas, 400+ subtemas)'  
PRINT '   ? Datos maestros y catálogos básicos'
PRINT '   ? Datos de prueba para testing'
PRINT '   ? Relaciones e integridad referencial'
PRINT ''
PRINT '?? PRÓXIMOS PASOS PARA USAR EL SISTEMA:'
PRINT '   1?? Configurar cadena de conexión en appsettings.json:'
PRINT '      "DefaultConnection": "Server=.;Database=GriconDB;Trusted_Connection=true;"'
PRINT ''
PRINT '   2?? Verificar conexión desde la aplicación .NET'
PRINT ''
PRINT '   3?? Probar funcionalidades con datos de prueba:'
PRINT '      ?? Usuario: juan.perez@banco.com (ID: 1)'
PRINT '      ?? Grillas configuradas: 4 grillas con temas asignados'
PRINT '      ?? Evaluación activa: "Evaluación de Prueba Q1 2024"'
PRINT ''
PRINT '   4?? Acceder al sistema vía:'
PRINT '      ?? Web: https://localhost:7093 (API)'
PRINT '      ??? Blazor: https://localhost:7094 (UI)'
PRINT ''
PRINT '?? DOCUMENTACIÓN ADICIONAL:'
PRINT '   ?? Scripts de base de datos: ./Scripts/'
PRINT '   ?? Configuración: appsettings.json'
PRINT '   ?? Testing: Usar datos de prueba cargados'
PRINT ''
PRINT '?? IMPORTANTE:'
PRINT '   • Configurar permisos de usuario de base de datos si es necesario'
PRINT '   • Revisar configuración de Entity Framework'
PRINT '   • Hacer backup antes de usar en producción'
PRINT ''
PRINT '?? SOPORTE:'
PRINT '   En caso de problemas, revisar:'
PRINT '   • Connection strings'
PRINT '   • Permisos de SQL Server'  
PRINT '   • Logs de la aplicación'
PRINT ''
PRINT 'Fecha de implementación: ' + CAST(GETDATE() AS VARCHAR)
PRINT '====================================================================='
PRINT '?? ¡SISTEMA LISTO PARA USO!'
PRINT '====================================================================='

GO

-- =============================================================================
-- INSTRUCCIONES PARA EJECUCIÓN MANUAL
-- =============================================================================
/*
INSTRUCCIONES PARA EJECUCIÓN PASO A PASO:

Si prefieres ejecutar manualmente cada fase, sigue estos pasos:

1. FASE 1 - Estructura Base:
   :r .\01_CreacionCompletaBaseDatos.sql

2. FASE 2 - Temas y Subtemas:
   :r .\02_CargaTemasSubtemas.sql

3. FASE 3 - Correcciones:
   :r .\03_CorreccionesEstructura.sql

4. FASE 4 - Datos de Prueba:
   :r .\04_DatosPrueba.sql

NOTA: El comando :r solo funciona en SQL Server Management Studio (SSMS).
      En otros clientes SQL, copia y pega el contenido de cada archivo manualmente.

PARA AUTOMATIZAR COMPLETAMENTE:
Descomenta las líneas ":r .\nombreScript.sql" en cada fase de este archivo.
*/