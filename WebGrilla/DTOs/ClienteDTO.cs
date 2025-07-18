using System.ComponentModel.DataAnnotations;
using WebGrilla.Models;

namespace WebGrilla.DTOs
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
    }
}
