using System.ComponentModel.DataAnnotations;

namespace WebGrilla.DTOs
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El documento es requerido")]
        [Range(1000000, 999999999999, ErrorMessage = "El número de documento debe ser válido")]
        public decimal NumeroDocumento { get; set; }
    }
}