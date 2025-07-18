using System.ComponentModel.DataAnnotations;
using WebGrilla.Models;

namespace WebGrilla.DTOs
{
    public class EquipoDesarrolloDTO
    {
        public int IdEquipoDesarrollo { get; set; }
        public string Nombre { get; set; }
        public int IdCliente { get; set; }
    }
}
