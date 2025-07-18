using System.ComponentModel.DataAnnotations;

namespace WebGrilla.Models
{
    public class Tema
    {
        [Key]
        public int IdTema { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Orden { get; set; }
        //Relacion
        public ICollection<Subtema> Subtemas { get; set; }
        public ICollection<GrillaTema> GrillaTemas { get; set; }
    }
}
