namespace WebGrilla.DTOs
{
    public class LoginResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public RecursoSessionDTO? Usuario { get; set; }
        public string Token { get; set; } = string.Empty; // Para futuras implementaciones con JWT
    }

    public class RecursoSessionDTO
    {
        public int IdRecurso { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public decimal NumeroDocumento { get; set; }
        public string PerfilSeguridad { get; set; } = string.Empty;
        
        // Información del Rol
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        
        // Información del Equipo
        public int IdEquipoDesarrollo { get; set; }
        public string NombreEquipo { get; set; } = string.Empty;
        
        // Permisos del usuario
        public List<string> Permisos { get; set; } = new List<string>();
    }
}