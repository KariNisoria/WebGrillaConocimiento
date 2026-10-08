namespace WebGrilla.DTOs
{
    public class EvaluacionDTO
    {
        public int IdEvaluacion { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        /// 0=Iniciada | 1=Finalizada | 2=Verificada | 3=Expirada
        public short Estado { get; set; } = 0;
        public int? IdRecursoSupervisor{ get; set; }
        public DateTime? FechaSupervision { get; set; }
        public int IdRecurso { get; set; }
        public int IdGrilla { get; set; }
        
        // Propiedades adicionales para mostrar información relacionada
        public string? NombreRecurso { get; set; }
        public string? NombreGrilla { get; set; }
        public string? NombreRecursoSupervisor { get; set; }
    }
}