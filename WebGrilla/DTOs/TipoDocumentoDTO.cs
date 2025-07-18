using System.ComponentModel.DataAnnotations;
using WebGrilla.Models;

namespace WebGrilla.DTOs
{
    public class TipoDocumentoDTO
    {
        public int IdTipoDocumento { get; set; }
        public string Nombre { get; set; }
    }
}
