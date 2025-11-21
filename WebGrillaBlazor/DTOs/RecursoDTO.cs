namespace WebGrillaBlazor.DTOs
{
    public class RecursoDTO
    {
        public int IdRecurso { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public int IdTipoDocumento { get; set; }
        public decimal NumeroDocumento { get; set; }
        public string CorreoElectronico { get; set; } = string.Empty;
        public string PerfilSeguridad { get; set; } = string.Empty;
        public int IdEquipoDesarrollo { get; set; }
        public int IdRol { get; set; }
        public int? IdGrilla { get; set; }
        
        // Propiedades adicionales para mostrar información relacionada
        public string? NombreEquipo { get; set; }
        public string? NombreRol { get; set; }
        public string? NombreTipoDocumento { get; set; }
    }

}
