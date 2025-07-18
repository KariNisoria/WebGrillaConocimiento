using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebGrillaBlazor.DTOs
{
    public class SubtemaDTO
    {
        public int IdSubtema { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public int IdTema { get; set; }

    }
}
