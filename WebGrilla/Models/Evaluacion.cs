using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGrilla.Models
{
    public class Evaluacion
    {
        [Key]
        public int IdEvaluacion { get; set; }
        [Required]
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        
        // FK para asociar evaluación a un recurso y grilla específicos
        public int IdRecurso { get; set; }
        public int IdGrilla { get; set; }
        
        [ForeignKey("IdRecurso")]
        public Recurso? Recurso { get; set; }
        [ForeignKey("IdGrilla")]
        public Grilla? Grilla { get; set; }
        
        // Relaciones
        public ICollection<ResultadoConocimiento> Resultados { get; set; } = new List<ResultadoConocimiento>();
        public ICollection<ConocimientoRecurso> Conocimientos { get; set; } = new List<ConocimientoRecurso>();
    }
}
