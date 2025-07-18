using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAllClientes()
        {
            var resultado = await _clienteService.GetAllCliente();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClienteById(int id) 
        { 
            var resultado = await _clienteService.GetClienteById(id);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> AddCliente([FromBody]ClienteDTO clienteDTO)
        {
            var resultado = await _clienteService.AddCliente(clienteDTO);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }

            return CreatedAtAction(nameof(GetClienteById), new {id = resultado.value.IdCliente}, resultado.value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCliente(int id, [FromBody]ClienteDTO cliente)
        {
            var resultado = await _clienteService.UpdateCliente(id, cliente);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            var resultado = await _clienteService.DeleteCliente(id);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }

    }
}
