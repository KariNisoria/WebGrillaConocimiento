using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGrilla.Models
{
    public class Subtema
    {
        [Key]
        public int IdSubtema { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Orden { get; set; }
        // Fk
        public int IdTema { get; set; }
        [ForeignKey("IdTema")]
        public Tema Tema { get; set; }
        //Relaciones
        public ICollection<GrillaSubtema> GrillaSubtemas { get; set; }

    }
}
