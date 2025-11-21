namespace WebGrilla.DTOs
{
    public class GrillaSubtemaDTO
    {
        public int IdGrillaSubtema { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Ponderacion { get; set; }
        public int IdGrillaTema { get; set; }
        public int IdSubtema { get; set; }
    }
}