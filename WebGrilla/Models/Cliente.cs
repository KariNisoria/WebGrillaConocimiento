using System.ComponentModel.DataAnnotations;

namespace WebGrilla.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        public string Nombre { get; set; }
        //Relacion con EquipoDesarrollo
        public ICollection<EquipoDesarrollo> Equipos { get; set; }
    }
}
