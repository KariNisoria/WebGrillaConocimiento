using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoService _service;
        public TipoDocumentoController(ITipoDocumentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocumentoDTO>>> GetAllTipoDocumento() 
        { 
            var resultado = await _service.GetAllTipoDocumento();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDocumentoDTO>> GetTipoDocumentoById(int id)
        {
            var resultado = await _service.GetTipoDocumentoById(id); 
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<TipoDocumentoDTO>> AddTipoDocumento([FromBody]TipoDocumentoDTO tipodocumento)
        {
            var resultado = await _service.AddTipoDocumento(tipodocumento);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return CreatedAtAction(nameof(GetTipoDocumentoById), new { id = resultado.value.IdTipoDocumento }, resultado.value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTipoDocumento(int id)
        {
            var resultado = await _service.DeleteTipoDocumento(id);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTipoDocumento(int id, [FromBody] TipoDocumentoDTO tipoDocumento)
        {
            var resultado = await _service.UpdateTipoDocumento(id, tipoDocumento);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }
    }
}
