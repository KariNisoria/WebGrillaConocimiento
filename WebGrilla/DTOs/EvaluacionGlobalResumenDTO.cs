namespace WebGrilla.DTOs
{
    public class EvaluacionGlobalResumenDTO
    {
        public int IdGrilla { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int TotalEvaluaciones { get; set; }
        public int EvaluacionesCompletadas { get; set; }
        public int EvaluacionesPendientes { get; set; }
        public decimal PorcentajeCompletitud { get; set; }
        public List<EvaluacionDTO> Evaluaciones { get; set; } = new();
        public string? NombreGrilla { get; set; }
    }
}