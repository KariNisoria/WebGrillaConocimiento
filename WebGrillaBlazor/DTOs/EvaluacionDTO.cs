namespace WebGrillaBlazor.DTOs
{
    public class EvaluacionDTO
    {
        public int IdEvaluacion { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdRecurso { get; set; }
        public int IdGrilla { get; set; }
        
        // Propiedades adicionales para mostrar información relacionada
        public string? NombreRecurso { get; set; }
        public string? NombreGrilla { get; set; }
    }
}