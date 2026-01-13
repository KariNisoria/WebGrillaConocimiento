using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGrilla.Models
{
    /// <summary>
    /// Modelo para gestionar los permisos por rol
    /// Esto permite definir qué funcionalidades puede acceder cada rol
    /// </summary>
    public class RolPermiso
    {
        [Key]
        public int IdRolPermiso { get; set; }
        
        [Required]
        public int IdRol { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CodigoPermiso { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Descripcion { get; set; }
        
        [Required]
        public bool Activo { get; set; } = true;
        
        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("IdRol")]
        public virtual Rol? Rol { get; set; }
    }
}