using System.ComponentModel.DataAnnotations;

namespace WebGrilla.Models
{
    public class TipoDocumento
    {
        [Required]
        public int IdTipoDocumento { get; set; }
        [Required]
        public string Nombre { get; set; }
        // Relacion ...
        public ICollection<Recurso> Recursos { get; set; }
    }
}
