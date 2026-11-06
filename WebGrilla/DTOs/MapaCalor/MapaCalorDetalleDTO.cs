namespace WebGrilla.DTOs.MapaCalor
{
    public class MapaCalorDetalleDTO
    {
        public int IdGrillaTema { get; set; }

        public int IdTema { get; set; }

        public int IdSubtema { get; set; }

        public int IdRecurso { get; set; }

        public int IdEvaluacion { get; set; }

        public decimal PonderacionSubtema { get; set; }

        public decimal? ValorFuncionalVerif { get; set; }

        public decimal? ValorTecnicoVerif { get; set; }
    }
}
