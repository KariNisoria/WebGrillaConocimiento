namespace WebGrilla.DTOs.MapaCalor
{
    public class MapaCalorDTO
    {
        public int IdGrilla { get; set; }
        public string NombreGrilla { get; set; } = string.Empty;
        public List<MapaCalorTemaDTO> Temas { get; set; } = new();
        public List<MapaCalorRecursoDTO> Recursos { get; set; } = new();
        public List<MapaCalorValorDTO> Valores { get; set; } = new();
    }
}
