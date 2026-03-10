using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGrilla.Models
{
    /// <summary>
    /// Modelo que representa la relación muchos a muchos entre supervisor y supervisado
    /// </summary>
    public class RecursoSupervisor
    {
        [Key]
        public int IdRecursoSupervisor { get; set; }

        [Required]
        public int IdRecursoSupervisorAsignado { get; set; }

        [Required]
        public int IdRecursoSupervisado { get; set; }

        [Required]
        public DateTime FechaAsignacion { get; set; }

        [Required]
        public bool Activo { get; set; } = true;

        public DateTime? FechaBaja { get; set; }

        public string? Observaciones { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdRecursoSupervisorAsignado")]
        public Recurso? RecursoSupervisorAsignado { get; set; }

        [ForeignKey("IdRecursoSupervisado")]
        public Recurso? RecursoSupervisado { get; set; }
    }
}