namespace WebGrillaBlazor.DTOs
{
    public class GrillaTemaDTO
    {
        public int IdGrillaTema { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Ponderacion { get; set; }
        public int Orden { get; set; }
        public int IdTema { get; set; }
        public int IdGrilla { get; set; }
    }
}