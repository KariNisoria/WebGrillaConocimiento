using System.ComponentModel.DataAnnotations;

namespace WebGrilla.Models
{
    public class TipoDocumento
    {
        [Required]
        public int IdTipoDocumento { get; set; }
        [Required]
        public string Nombre { get; set; }

        public string Abreviacion { get; set; }

        public ICollection<Recurso> Recursos { get; set; }
    }
}
