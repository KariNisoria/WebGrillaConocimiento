using System.ComponentModel.DataAnnotations;

namespace WebGrilla.Models
{
    public class Grilla
    {
        [Key]
        public int IdGrilla { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaVigencia { get; set; }
        public int Estado { get; set; }
        
        //Relaciones
        public ICollection<GrillaTema> GrillaTemas { get; set; } = new List<GrillaTema>();
        public ICollection<ResultadoConocimiento> Resultados { get; set; } = new List<ResultadoConocimiento>();
        public ICollection<ConocimientoRecurso> Conocimientos { get; set; } = new List<ConocimientoRecurso>();
        public ICollection<Evaluacion> Evaluaciones { get; set; } = new List<Evaluacion>();
    }
}
