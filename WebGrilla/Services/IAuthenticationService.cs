using WebGrilla.DTOs;

namespace WebGrilla.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest);
        Task<RecursoSessionDTO?> GetUsuarioByEmailAsync(string email);
        Task<bool> ValidateCredentialsAsync(string email, decimal numeroDocumento);
        Task<List<string>> GetPermisosUsuarioAsync(int idRol);
        Task LogoutAsync();
    }
}