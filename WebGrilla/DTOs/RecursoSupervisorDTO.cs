namespace WebGrilla.DTOs
{
    /// <summary>
    /// DTO para la gestión de relaciones supervisor-supervisado
    /// </summary>
    public class RecursoSupervisorDTO
    {
        public int IdRecursoSupervisor { get; set; }
        public int IdRecursoSupervisorAsignado { get; set; }
        public int IdRecursoSupervisado { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string? Observaciones { get; set; }
        
        // Información adicional para mostrar en la UI
        public string? NombreSupervisor { get; set; }
        public string? NombreSupervisado { get; set; }
        public string? CorreoSupervisor { get; set; }
        public string? CorreoSupervisado { get; set; }
        public string? RolSupervisor { get; set; }
        public string? RolSupervisado { get; set; }
    }

    /// <summary>
    /// DTO simplificado para listas desplegables
    /// </summary>
    public class RecursoSimpleDTO
    {
        public int IdRecurso { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string NombreRol { get; set; } = string.Empty;
        public string NombreEquipo { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para la vista de supervisión con recursos disponibles
    /// </summary>
    public class SupervisionViewDTO
    {
        public RecursoSimpleDTO Supervisor { get; set; } = new();
        public List<RecursoSimpleDTO> RecursosDisponibles { get; set; } = new();
        public List<RecursoSimpleDTO> RecursosSupervisados { get; set; } = new();
    }
}