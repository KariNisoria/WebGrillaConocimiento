using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.DTOs;
using WebGrilla.Models;

namespace WebGrilla.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(AppDbContext context, ILogger<AuthenticationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            try
            {
                _logger.LogInformation($"Intento de login para email: {loginRequest.Email}");

                // Validar credenciales
                var isValid = await ValidateCredentialsAsync(loginRequest.Email, loginRequest.NumeroDocumento);
                
                if (!isValid)
                {
                    _logger.LogWarning($"Credenciales inválidas para email: {loginRequest.Email}");
                    return new LoginResponseDTO 
                    { 
                        IsSuccess = false, 
                        Message = "Email o número de documento incorrectos" 
                    };
                }

                // Obtener información del usuario
                var usuario = await GetUsuarioByEmailAsync(loginRequest.Email);
                
                if (usuario == null)
                {
                    _logger.LogError($"Usuario no encontrado para email: {loginRequest.Email}");
                    return new LoginResponseDTO 
                    { 
                        IsSuccess = false, 
                        Message = "Usuario no encontrado" 
                    };
                }

                // Obtener permisos del usuario
                usuario.Permisos = await GetPermisosUsuarioAsync(usuario.IdRol);

                _logger.LogInformation($"Login exitoso para usuario: {usuario.Nombre} {usuario.Apellido} (ID: {usuario.IdRecurso})");

                return new LoginResponseDTO
                {
                    IsSuccess = true,
                    Message = "Login exitoso",
                    Usuario = usuario,
                    Token = GenerateSessionToken(usuario) // Para futuras implementaciones
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error durante el login para email: {loginRequest.Email}");
                return new LoginResponseDTO 
                { 
                    IsSuccess = false, 
                    Message = "Error interno del servidor" 
                };
            }
        }

        public async Task<bool> ValidateCredentialsAsync(string email, decimal numeroDocumento)
        {
            try
            {
                var recurso = await _context.Recursos
                    .FirstOrDefaultAsync(r => r.CorreoElectronico.ToLower() == email.ToLower() 
                                           && r.NumeroDocumento == numeroDocumento);
                
                return recurso != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating credentials for email: {email}");
                return false;
            }
        }

        public async Task<RecursoSessionDTO?> GetUsuarioByEmailAsync(string email)
        {
            try
            {
                var recurso = await _context.Recursos
                    .Include(r => r.Rol)
                    .Include(r => r.EquipoDesarrollo)
                    .FirstOrDefaultAsync(r => r.CorreoElectronico.ToLower() == email.ToLower());

                if (recurso == null)
                    return null;

                return new RecursoSessionDTO
                {
                    IdRecurso = recurso.IdRecurso,
                    Nombre = recurso.Nombre,
                    Apellido = recurso.Apellido,
                    CorreoElectronico = recurso.CorreoElectronico,
                    NumeroDocumento = recurso.NumeroDocumento,
                    PerfilSeguridad = recurso.PerfilSeguridad,
                    IdRol = recurso.IdRol,
                    NombreRol = recurso.Rol?.Nombre ?? "Sin Rol",
                    IdEquipoDesarrollo = recurso.IdEquipoDesarrollo,
                    NombreEquipo = recurso.EquipoDesarrollo?.Nombre ?? "Sin Equipo"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user by email: {email}");
                return null;
            }
        }

        public async Task<List<string>> GetPermisosUsuarioAsync(int idRol)
        {
            try
            {
                // Obtener permisos desde la base de datos
                var permisos = await _context.RolPermisos
                    .Where(rp => rp.IdRol == idRol && rp.Activo)
                    .Select(rp => rp.CodigoPermiso)
                    .ToListAsync();

                // Si no hay permisos en BD, usar sistema de fallback por rol
                if (!permisos.Any())
                {
                    _logger.LogWarning($"No se encontraron permisos en BD para rol ID: {idRol}, usando permisos por defecto");
                    return await GetPermisosDefaultPorRol(idRol);
                }

                _logger.LogInformation($"Cargados {permisos.Count} permisos desde BD para rol ID: {idRol}");
                return permisos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting permissions for role ID: {idRol}");
                // En caso de error, usar sistema de fallback
                return await GetPermisosDefaultPorRol(idRol);
            }
        }

        /// <summary>
        /// Sistema de permisos por defecto como fallback
        /// </summary>
        private async Task<List<string>> GetPermisosDefaultPorRol(int idRol)
        {
            var permisos = new List<string>();

            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.IdRol == idRol);
            if (rol == null) return permisos;

            // Definir permisos por rol como fallback (mantener compatibilidad)
            switch (rol.Nombre.ToLower())
            {
                case "administrador":
                case "admin":
                    permisos.AddRange(new[]
                    {
                        "RECURSOS_READ", "RECURSOS_WRITE", "RECURSOS_DELETE",
                        "ROLES_READ", "ROLES_WRITE", "ROLES_DELETE",
                        "GRILLAS_READ", "GRILLAS_WRITE", "GRILLAS_DELETE",
                        "TEMAS_READ", "TEMAS_WRITE", "TEMAS_DELETE",
                        "SUBTEMAS_READ", "SUBTEMAS_WRITE", "SUBTEMAS_DELETE",
                        "EVALUACIONES_READ", "EVALUACIONES_WRITE", "EVALUACIONES_DELETE",
                        "EVALUACIONES_GLOBALES_READ",
                        "CLIENTES_READ", "CLIENTES_WRITE", "CLIENTES_DELETE",
                        "EQUIPOS_READ", "EQUIPOS_WRITE", "EQUIPOS_DELETE",
                        "TIPOS_DOCUMENTO_READ", "TIPOS_DOCUMENTO_WRITE", "TIPOS_DOCUMENTO_DELETE",
                        "PERMISOS_ADMIN", "CONFIGURACION_SISTEMA"
                    });
                    break;

                case "manager":
                case "gerente":
                case "líder":
                case "lider":
                    permisos.AddRange(new[]
                    {
                        "RECURSOS_READ", "RECURSOS_WRITE",
                        "ROLES_READ",
                        "GRILLAS_READ", "GRILLAS_WRITE",
                        "TEMAS_READ", "TEMAS_WRITE",
                        "SUBTEMAS_READ", "SUBTEMAS_WRITE",
                        "EVALUACIONES_READ", "EVALUACIONES_WRITE",
                        "EVALUACIONES_GLOBALES_READ",
                        "CLIENTES_READ", "CLIENTES_WRITE",
                        "EQUIPOS_READ"
                    });
                    break;

                case "evaluador":
                    permisos.AddRange(new[]
                    {
                        "RECURSOS_READ",
                        "GRILLAS_READ",
                        "TEMAS_READ",
                        "SUBTEMAS_READ",
                        "EVALUACIONES_READ", "EVALUACIONES_WRITE",
                        "EVALUACIONES_GLOBALES_READ"
                    });
                    break;

                case "desarrollador":
                case "developer":
                case "analista":
                    permisos.AddRange(new[]
                    {
                        "RECURSOS_READ",
                        "GRILLAS_READ",
                        "TEMAS_READ",
                        "SUBTEMAS_READ",
                        "EVALUACIONES_READ",
                        "EVALUACIONES_GLOBALES_READ"
                    });
                    break;

                default:
                    // Rol sin permisos específicos - solo lectura básica
                    permisos.AddRange(new[]
                    {
                        "RECURSOS_READ",
                        "EVALUACIONES_READ"
                    });
                    break;
            }

            return permisos;
        }

        public async Task LogoutAsync()
        {
            // Para futuras implementaciones con JWT o session tokens
            await Task.CompletedTask;
            _logger.LogInformation("User logged out");
        }

        private string GenerateSessionToken(RecursoSessionDTO usuario)
        {
            // Para futuras implementaciones con JWT
            // Por ahora retornamos un token simple
            return $"{usuario.IdRecurso}_{DateTime.UtcNow.Ticks}";
        }
    }
}