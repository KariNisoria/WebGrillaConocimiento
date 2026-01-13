# Resumen de Cambios: Evaluaciones sin Generación Automática de ConocimientoRecurso

## Problema Original
Cuando se iniciaba una evaluación para un recurso y una grilla, se generaban y guardaban automáticamente todos los registros de ConocimientoRecurso para cada subtema de la grilla en la base de datos.

## Solución Implementada
Se modificó el comportamiento para que:

1. **El registro de Evaluación se guarde inmediatamente** cuando se crea la evaluación
2. **Los registros de ConocimientoRecurso se generen automáticamente pero temporalmente** (no se guardan en BD hasta que el usuario decida hacerlo)

## Cambios Realizados

### 1. EvaluacionService.cs
- **Nuevo método sobrecargado `CreateAsync(EvaluacionDTO evaluacionDto, bool generarConocimientosAutomatico)`**
  - Permite controlar si se generan conocimientos automáticamente o no
  - Por defecto, NO se generan automáticamente (`generarConocimientosAutomatico = false`)

- **Nuevo método `GenerarConocimientosTemporalesAsync(int idEvaluacion, int idRecurso, int idGrilla)`**
  - Genera una lista de ConocimientoRecursoDTO temporal para todos los subtemas de una grilla
  - Los registros tienen `IdConocimientoRecurso = 0` (indicando que no están guardados en BD)
  - No se persisten en la base de datos

- **Modificado `IniciarEvaluacionParaRecursoAsync()`**
  - Ahora llama a `CreateAsync(evaluacionDto, false)` para NO generar conocimientos automáticamente

### 2. EvaluacionController.cs
- **Nuevo endpoint `GET /{idEvaluacion}/conocimientos-temporales/{idRecurso}/{idGrilla}`**
  - Permite generar conocimientos temporales para una evaluación específica
  - Retorna lista de ConocimientoRecursoDTO sin persistir en BD

### 3. ApiClientEvaluacion.cs
- **Nuevo método `GenerarConocimientosTemporalesAsync(int idEvaluacion, int idRecurso, int idGrilla)`**
  - Cliente HTTP para llamar al nuevo endpoint
  - Maneja la generación de conocimientos temporales desde el frontend

### 4. FormularioEvaluacion.razor
- **Modificado `CargarConocimientos()`**
  - Primero intenta cargar conocimientos desde la BD
  - Si no encuentra registros, genera conocimientos temporales automáticamente
  - Los conocimientos temporales se muestran en la UI pero no se persisten hasta que el usuario los guarde

## Flujo Actual

1. **Crear Evaluación**: Se crea solo el registro de Evaluación en la BD
2. **Acceder a FormularioEvaluacion**: 
   - Si no existen ConocimientoRecurso en BD para esa evaluación, se generan automáticamente de forma temporal
   - Se muestran todos los subtemas de la grilla con valores (0,0) 
   - Los registros temporales tienen `IdConocimientoRecurso = 0`
3. **Editar Valores**: El usuario puede modificar los valores funcionales y técnicos
4. **Guardar**: 
   - Solo cuando el usuario hace clic en "Guardar" se persisten los registros en la BD
   - Los registros obtienen un `IdConocimientoRecurso > 0` al guardarse

## Beneficios

- ? **Mejor rendimiento**: No se crean registros innecesarios en BD al iniciar evaluación
- ? **Control del usuario**: El usuario decide cuándo guardar los registros
- ? **Experiencia de usuario mejorada**: Se puede trabajar con valores temporales antes de confirmar
- ? **Menos ruido en BD**: Solo se guardan registros cuando realmente se necesitan
- ? **Compatibilidad**: Mantiene toda la funcionalidad existente

## Verificación

El sistema ahora funciona de la siguiente manera:

1. **Iniciar Evaluación** ? Solo se crea el registro de Evaluación
2. **Acceder a evaluación de un recurso** ? Se generan ConocimientoRecurso temporales automáticamente
3. **Usuario modifica valores** ? Los cambios se mantienen en memoria
4. **Usuario hace clic en "Guardar"** ? Los registros se persisten en la BD con IDs reales

Esto cumple exactamente con tu requerimiento: 
- ? Se genera un registro para la tabla de Evaluación inmediatamente
- ? Se generan los IdConocimientoRecurso automáticamente para todos los subtemas
- ? NO se graban en la base de datos hasta que el usuario elija grabar esos registros