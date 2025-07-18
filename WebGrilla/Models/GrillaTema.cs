using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGrilla.Models
{
    public class GrillaTema
    {
        [Key]
        public int IdGrillaTema { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public decimal Ponderacion { get; set; }
        // Fk
        public int IdGrilla { get; set; }
        [ForeignKey("IdGrilla")]
        public Grilla Grilla { get; set; }
        public int IdTema { get; set; }
        [ForeignKey("IdTema")]
        public Tema Tema { get; set; }
        //Relacion
        public ICollection<GrillaSubtema> GrillaSubtemas { get; set; }
    }
}
