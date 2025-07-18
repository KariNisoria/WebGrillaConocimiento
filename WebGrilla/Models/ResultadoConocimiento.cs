using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGrilla.Models
{
    public class ResultadoConocimiento
    {
        [Key]
        public int IdResultadoConocimiento { get; set; }
        [Required]
        public decimal ValorFuncional { get; set; }
        [Required]
        public decimal ValorTecnico { get; set; }

        /*fk*/
        public int IdRecurso { get; set; }
        public int IdGrilla { get; set; }
        public int IdEvaluacion { get; set; }
        public int IdSubtema { get; set; }

        //Relacion
        [ForeignKey("IdRecurso")]
        public Recurso Recurso { get; set; }
        [ForeignKey("IdGrilla")]
        public Grilla Grilla { get; set; }
        [ForeignKey("IdEvaluacion")]
        public Evaluacion Evaluacion { get; set; }
        [ForeignKey("Id_Subtema")]
        public GrillaSubtema GrillaSubtema { get; set;}
    }
}
