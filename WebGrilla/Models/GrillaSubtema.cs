using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGrilla.Models
{
    public class GrillaSubtema
    {
        [Key]
        public int IdGrillaSubtema { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public decimal Ponderacion { get; set; }
        // Fk
        public int IdGrillaTema { get; set; }
        [ForeignKey("IdGrillaTema")]
        public GrillaTema GrillaTema { get; set; }
        public int IdSubtema { get; set; }
        [ForeignKey("IdSubtema")]
        public Subtema Subtema { get; set; }
        //Relacion
        public ICollection<ResultadoConocimiento> Resultados { get; set; }
    }
}
