using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebGrilla.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required]
        public string Nombre { get; set; }
        //Relaciones
        public ICollection<Recurso> Recursos { get; set; }
    }
}
