using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebGrilla.Models;

namespace WebGrilla.DTOs
{
    public class RecursoDTO
    {
        public int IdRecurso { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int IdTipoDocumento { get; set; }
        public decimal NumeroDocumento { get; set; }
        public string CorreoElectronico { get; set; }
        public string PerfilSeguridad { get; set; }
        public int IdEquipoDesarrollo { get; set; }
        public int IdRol { get; set; }
    }
}
