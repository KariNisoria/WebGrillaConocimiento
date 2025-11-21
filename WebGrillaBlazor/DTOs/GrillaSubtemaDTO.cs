namespace WebGrillaBlazor.DTOs
{
    public class GrillaSubtemaDTO
    {
        public int IdGrillaSubtema { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Ponderacion { get; set; }
        public int Orden { get; set; }
        public int IdGrillaTema { get; set; }
        public int IdSubtema { get; set; }
        public string? Descripcion { get; set; }
    }
}