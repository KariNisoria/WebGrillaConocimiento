using WebGrilla.DTOs;

namespace WebGrilla.Services
{
    public interface IEvaluacionService
    {
        Task<List<EvaluacionDTO>> GetAllAsync();
        Task<EvaluacionDTO?> GetByIdAsync(int id);
        Task<EvaluacionDTO> CreateAsync(EvaluacionDTO evaluacionDto);
        Task<EvaluacionDTO> CreateAsync(EvaluacionDTO evaluacionDto, bool generarConocimientosAutomatico);
        Task<EvaluacionDTO> UpdateAsync(int id, EvaluacionDTO evaluacionDto);
        Task<bool> DeleteAsync(int id);
        Task<EvaluacionDTO?> GetEvaluacionActivaAsync();
        Task<EvaluacionDTO?> GetEvaluacionActivaPorRecursoAsync(int idRecurso);
        Task<List<EvaluacionDTO>> GetEvaluacionesPorRecursoAsync(int idRecurso);
        Task<List<EvaluacionDTO>> GetEvaluacionesPorSupervisionAsync(int idSupervisor);
        Task<List<EvaluacionDTO>> GetEvaluacionesPorRecursoYSupervisionAsync(int idRecurso);
        Task<EvaluacionDTO> IniciarEvaluacionParaRecursoAsync(int idRecurso, int idGrilla, string descripcion);
        Task<List<ConocimientoRecursoDTO>> GenerarConocimientosTemporalesAsync(int idEvaluacion, int idRecurso, int idGrilla);
        
        // Métodos para evaluación global
        Task<List<EvaluacionDTO>> CrearEvaluacionGlobalAsync(int idGrilla, string descripcion, DateTime fechaFin);
        Task<List<EvaluacionDTO>> GetEvaluacionesGlobalesAsync();
        Task<List<EvaluacionDTO>> GetEvaluacionesPorGrillaYPeriodoAsync(int idGrilla, DateTime fechaInicio, DateTime fechaFin);
        Task<EvaluacionGlobalResumenDTO> GetResumenEvaluacionGlobalAsync(int idGrilla, DateTime fechaInicio, DateTime fechaFin);
    }
}