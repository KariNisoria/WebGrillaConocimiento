namespace WebGrillaBlazor.DTOs
{
    public class ConocimientoRecursoDTO
    {
        public int IdConocimientoRecurso { get; set; }
        public int ValorFuncional { get; set; }
        public int ValorTecnico { get; set; }
        public int? ValorFuncionalVerif { get; set; }
        public int? ValorTecnicoVerif { get; set; }
        public int IdRecurso { get; set; }
        public int IdGrilla { get; set; }
        public int IdSubtema { get; set; }
        public int IdEvaluacion { get; set; }
        
        // Propiedades adicionales para mostrar información relacionada
        public string? NombreRecurso { get; set; }
        public string? NombreSubtema { get; set; }
        public string? NombreTema { get; set; }
    }
}