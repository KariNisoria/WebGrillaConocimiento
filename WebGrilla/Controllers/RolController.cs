using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;    
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDTO>>> GetAllRoles()
        {
            var resultado = await _rolService.GetAllRol();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolDTO>> GetRolById(int id)
        {
            var resultado = await _rolService.GetRolById(id);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<RolDTO>> AddRol([FromBody] RolDTO rol)
        {
            var resultado = await _rolService.AddRol(rol);
            if (!resultado.isSuccess) 
            { 
                return BadRequest(resultado.message);   
            }
            return CreatedAtAction(nameof(GetRolById), new { id = resultado.value.IdRol }, resultado.value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRol(int id, [FromBody] RolDTO rol)
        {
            var resultado = await _rolService.UpdateRol(id, rol);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult> DeleteRol(int id)
        {
            var resultado = await _rolService.DeleteRol(id);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }
    }
}
