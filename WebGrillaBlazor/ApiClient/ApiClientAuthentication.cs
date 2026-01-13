using System.Net.Http.Json;
using WebGrillaBlazor.DTOs;

namespace WebGrillaBlazor.ApiClient
{
    public class ApiClientAuthentication
    {
        private readonly HttpClient _httpClient;

        public ApiClientAuthentication(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Realizar login con email y número de documento
        /// </summary>
        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Authentication/login", loginRequest);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                    return result ?? new LoginResponseDTO { IsSuccess = false, Message = "Error al procesar la respuesta" };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new LoginResponseDTO 
                    { 
                        IsSuccess = false, 
                        Message = $"Error en el login: {response.StatusCode} - {errorContent}" 
                    };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponseDTO 
                { 
                    IsSuccess = false, 
                    Message = $"Error de conexión: {ex.Message}" 
                };
            }
        }

        /// <summary>
        /// Obtener usuario por email
        /// </summary>
        public async Task<RecursoSessionDTO?> GetUserByEmailAsync(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Authentication/user/{Uri.EscapeDataString(email)}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<RecursoSessionDTO>();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Validar credenciales
        /// </summary>
        public async Task<bool> ValidateCredentialsAsync(LoginRequestDTO loginRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Authentication/validate", loginRequest);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Obtener permisos de un rol
        /// </summary>
        public async Task<List<string>> GetRolePermissionsAsync(int idRol)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Authentication/permissions/{idRol}");
                
                if (response.IsSuccessStatusCode)
                {
                    var permissions = await response.Content.ReadFromJsonAsync<List<string>>();
                    return permissions ?? new List<string>();
                }
                return new List<string>();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Realizar logout
        /// </summary>
        public async Task<bool> LogoutAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("api/Authentication/logout", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}