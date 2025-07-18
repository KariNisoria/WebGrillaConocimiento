using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGrilla.Models
{
    public class Recurso
    {
        [Key]
        public int IdRecurso { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public DateTime FechaIngreso { get; set; }
        [Required]
        public int IdTipoDocumento { get; set; }
        [Column(TypeName = "decimal(17, 0)")]
        [Required]
        public decimal NumeroDocumento { get; set; }
        // Nuevo campo para el correo electrónico
        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string CorreoElectronico { get; set; }
        [Required]
        public string PerfilSeguridad { get; set; }
        // fk ...
        public int IdEquipoDesarrollo { get; set; }
        public int IdRol { get; set; }
        [ForeignKey("IdEquipoDesarrollo")]
        public EquipoDesarrollo EquipoDesarrollo { get; set; }
        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }
        [ForeignKey("IdTipoDocumento")]
        public TipoDocumento TipoDocumento { get; set; }
        // Relacion con...
        public ICollection<ResultadoConocimiento> Resultados { get; set; }
        public ICollection<ConocimientoRecurso> Conocimientos { get; set; }

    }
}
