using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebGrilla.Models;

namespace WebGrilla.DTOs
{
    public class TipoDocumentoDTO
    {
        public int IdTipoDocumento { get; set; }
        
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;
        
        [JsonPropertyName("abreviacion")]
        public string Abreviacion { get; set; } = string.Empty;
    }
}
