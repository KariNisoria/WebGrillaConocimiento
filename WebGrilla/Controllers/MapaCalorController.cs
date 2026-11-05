using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs.MapaCalor;
using WebGrilla.Services;
using WebGrilla.Utils;

namespace WebGrilla.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapaCalorController : ControllerBase
    {
        private readonly IMapaCalorService _service;
        private readonly MapaCalorExcelService _excelService;

        public MapaCalorController(
            IMapaCalorService service,
            MapaCalorExcelService excelService)
        {
            _service = service;
            _excelService = excelService;
        }

        [HttpGet("{idGrilla:int}")]
        public async Task<ActionResult<Result<MapaCalorDTO>>> GetMapaCalor(
            int idGrilla)
        {
            var resultado = await _service.GetMapaCalor(idGrilla);

            return Ok(resultado);
        }

        [HttpGet("{idGrilla:int}/excel")]
        public async Task<IActionResult> ExportarExcel(
        int idGrilla,
        [FromQuery] int? idRol)
        {
            var resultado = await _service.GetMapaCalor(idGrilla);

            if (!resultado.isSuccess || resultado.value == null)
            {
                return BadRequest(resultado.message);
            }
            var nombreRol = await _excelService.ObtenerNombreRolAsync(idRol);
            var archivo = _excelService.GenerarExcel(
                resultado.value,
                idRol,
                nombreRol
            );

            string nombreArchivo = $"MapaCalor_Grilla{idGrilla}.xlsx";

            return File(
                archivo,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                nombreArchivo
            );
        }
    }
}
