using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGrilla.Models
{
    public class ConocimientoRecurso
    {
        [Key]
        public int IdConocimientoRecurso { get; set; }
        [Required]
        public int ValorFuncional { get; set; }
        [Required]
        public int ValorTecnico { get; set; }
        [Required]
        public int ValorFuncionalVerif { get; set; }
        [Required]
        public int ValorTecnicoVerif { get; set; }
        //Fk
        public int IdRecurso { get; set; }
        public int IdGrilla { get; set; }
        public int IdSubtema { get; set; }
        public int IdEvaluacion { get; set; }
        //Relaciones
        [ForeignKey("IdRecurso")]
        public Recurso Recurso { get; set; }
        [ForeignKey("IdGrilla")]
        public Grilla Grilla { get; set; }
        [ForeignKey("IdSubtema")]
        public Subtema Subtema { get; set; }
        [ForeignKey("IdEvaluacion")]
        public Evaluacion Evaluacion { get; set; }


    }
}
