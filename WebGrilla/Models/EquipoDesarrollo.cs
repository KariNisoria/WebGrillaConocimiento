using System.ComponentModel.DataAnnotations;

namespace WebGrilla.Models
{
    public class EquipoDesarrollo
    {
        [Key]
        public int IdEquipoDesarrollo { get; set; }
        [Required]
        public string Nombre { get; set; }
        // Fk
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        // Relacion con Recursos
        public ICollection<Recurso> Recursos { get; set; }
    }
}
