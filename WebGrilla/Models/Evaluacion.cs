using System.ComponentModel.DataAnnotations;

namespace WebGrilla.Models
{
    public class Evaluacion
    {
        [Key]
        public int IdEvaluacion { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        // Fk
        // Relacion
        public ICollection<ResultadoConocimiento> Resultados { get; set; }
        public ICollection<ConocimientoRecurso> Conocimientos { get; set; }
    }
}
