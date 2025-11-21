-- Script SQL para aplicar los cambios de la migración CorregirEvaluacionGrillaRecurso
-- Ejecutar este script en la base de datos

-- 1. Agregar IdRecurso a la tabla Evaluacion
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Evaluacion' AND COLUMN_NAME = 'IdRecurso')
BEGIN
    ALTER TABLE Evaluacion 
    ADD IdRecurso int NOT NULL DEFAULT 1;
    PRINT 'Columna IdRecurso agregada a tabla Evaluacion';
    
    -- Crear índice para la foreign key
    CREATE INDEX IX_Evaluacion_IdRecurso ON Evaluacion (IdRecurso);
    
    -- Agregar foreign key constraint
    ALTER TABLE Evaluacion
    ADD CONSTRAINT FK_Evaluacion_Recursos_IdRecurso 
    FOREIGN KEY (IdRecurso) REFERENCES Recursos(IdRecurso) ON DELETE CASCADE;
    PRINT 'Foreign key IdRecurso creada en tabla Evaluacion';
END

-- 2. Agregar IdGrilla a la tabla Evaluacion
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Evaluacion' AND COLUMN_NAME = 'IdGrilla')
BEGIN
    ALTER TABLE Evaluacion 
    ADD IdGrilla int NOT NULL DEFAULT 1;
    PRINT 'Columna IdGrilla agregada a tabla Evaluacion';
    
    -- Crear índice para la foreign key
    CREATE INDEX IX_Evaluacion_IdGrilla ON Evaluacion (IdGrilla);
    
    -- Agregar foreign key constraint
    ALTER TABLE Evaluacion
    ADD CONSTRAINT FK_Evaluacion_Grillas_IdGrilla 
    FOREIGN KEY (IdGrilla) REFERENCES Grillas(IdGrilla) ON DELETE CASCADE;
    PRINT 'Foreign key IdGrilla creada en tabla Evaluacion';
END

-- 3. Quitar IdGrilla de la tabla Recursos si existe (el recurso no tiene grilla asignada permanentemente)
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Recursos' AND COLUMN_NAME = 'IdGrilla')
BEGIN
    -- Quitar foreign key constraint si existe
    IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Recursos_Grillas_IdGrilla')
    BEGIN
        ALTER TABLE Recursos DROP CONSTRAINT FK_Recursos_Grillas_IdGrilla;
        PRINT 'Foreign key FK_Recursos_Grillas_IdGrilla eliminada';
    END
    
    -- Quitar índice si existe
    IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Recursos_IdGrilla')
    BEGIN
        DROP INDEX IX_Recursos_IdGrilla ON Recursos;
        PRINT 'Índice IX_Recursos_IdGrilla eliminado';
    END
    
    -- Quitar columna
    ALTER TABLE Recursos DROP COLUMN IdGrilla;
    PRINT 'Columna IdGrilla eliminada de tabla Recursos';
END

-- 4. Actualizar datos existentes
-- Para esto necesitamos saber qué grilla y qué recurso usar por defecto
-- Puedes modificar estos valores según tus datos reales

DECLARE @PrimeraGrillaId INT = (SELECT TOP 1 IdGrilla FROM Grillas ORDER BY IdGrilla);
DECLARE @PrimerRecursoId INT = (SELECT TOP 1 IdRecurso FROM Recursos ORDER BY IdRecurso);

PRINT 'Primera grilla encontrada: ' + CAST(@PrimeraGrillaId AS VARCHAR);
PRINT 'Primer recurso encontrado: ' + CAST(@PrimerRecursoId AS VARCHAR);

-- Actualizar evaluaciones existentes que tengan valores por defecto
UPDATE Evaluacion 
SET 
    IdGrilla = CASE WHEN IdGrilla = 1 THEN @PrimeraGrillaId ELSE IdGrilla END,
    IdRecurso = CASE WHEN IdRecurso = 1 THEN @PrimerRecursoId ELSE IdRecurso END
WHERE IdGrilla = 1 OR IdRecurso = 1;

PRINT 'Datos existentes actualizados';

-- 5. Mostrar el estado actual de las evaluaciones
SELECT 
    IdEvaluacion,
    Descripcion,
    IdRecurso,
    IdGrilla,
    FechaInicio,
    FechaFin
FROM Evaluacion;

PRINT 'Migración aplicada correctamente';

-- Explicación del diseño:
-- Una evaluación (Evaluacion) se crea para un recurso específico (IdRecurso) usando una grilla específica (IdGrilla)
-- El recurso NO tiene una grilla asignada permanentemente
-- La grilla se selecciona al momento de crear la evaluación para ese recurso