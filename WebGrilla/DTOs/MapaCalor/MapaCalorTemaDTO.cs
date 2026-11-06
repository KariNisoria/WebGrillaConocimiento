namespace WebGrilla.DTOs.MapaCalor
{
    public class MapaCalorTemaDTO
    {
        public int IdGrillaTema { get; set; }
        public int IdTema { get; set; }
        public string NombreTema{ get; set; } = string.Empty;
        public int Orden { get; set; }
        public decimal PonderacionTema { get; set; }
    }
}
