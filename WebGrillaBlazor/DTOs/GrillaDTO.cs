namespace WebGrillaBlazor.DTOs
{
    public class GrillaDTO
    {
        public int IdGrilla { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaVigencia { get; set; }
        public int Estado { get; set; }
    }
}
