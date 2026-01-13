using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Login del usuario con email y número de documento
        /// </summary>
        /// <param name="loginRequest">Credenciales de login</param>
        /// <returns>Respuesta del login con información del usuario</returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new LoginResponseDTO 
                    { 
                        IsSuccess = false, 
                        Message = "Datos de entrada inválidos" 
                    });
                }

                var result = await _authService.LoginAsync(loginRequest);
                
                if (!result.IsSuccess)
                {
                    return Unauthorized(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login process");
                return StatusCode(500, new LoginResponseDTO 
                { 
                    IsSuccess = false, 
                    Message = "Error interno del servidor" 
                });
            }
        }

        /// <summary>
        /// Obtener información de usuario por email
        /// </summary>
        /// <param name="email">Email del usuario</param>
        /// <returns>Información del usuario</returns>
        [HttpGet("user/{email}")]
        public async Task<ActionResult<RecursoSessionDTO>> GetUserByEmail(string email)
        {
            try
            {
                var user = await _authService.GetUsuarioByEmailAsync(email);
                
                if (user == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user by email: {email}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Validar credenciales de usuario
        /// </summary>
        /// <param name="loginRequest">Credenciales para validar</param>
        /// <returns>True si las credenciales son válidas</returns>
        [HttpPost("validate")]
        public async Task<ActionResult<bool>> ValidateCredentials([FromBody] LoginRequestDTO loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(false);
                }

                var isValid = await _authService.ValidateCredentialsAsync(loginRequest.Email, loginRequest.NumeroDocumento);
                return Ok(isValid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating credentials");
                return StatusCode(500, false);
            }
        }

        /// <summary>
        /// Obtener permisos de un rol
        /// </summary>
        /// <param name="idRol">ID del rol</param>
        /// <returns>Lista de permisos</returns>
        [HttpGet("permissions/{idRol}")]
        public async Task<ActionResult<List<string>>> GetRolePermissions(int idRol)
        {
            try
            {
                var permissions = await _authService.GetPermisosUsuarioAsync(idRol);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting permissions for role: {idRol}");
                return StatusCode(500, new List<string>());
            }
        }

        /// <summary>
        /// Logout del usuario
        /// </summary>
        /// <returns>Confirmación del logout</returns>
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _authService.LogoutAsync();
                return Ok(new { message = "Logout exitoso" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return StatusCode(500, "Error durante el logout");
            }
        }
    }
}