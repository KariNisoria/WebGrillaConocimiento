namespace WebGrilla.DTOs.MapaCalor
{
    public class MapaCalorRecursoDTO
    {
        public int IdRecurso { get; set; }

        public int IdEvaluacion { get; set; }

        public string NombreRecurso { get; set; } = string.Empty;

        public int? IdRol { get; set; }
    }
}
