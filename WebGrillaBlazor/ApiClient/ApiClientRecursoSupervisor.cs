using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WebGrillaBlazor.DTOs;

namespace WebGrillaBlazor.ApiClient
{
    public class ApiClientRecursoSupervisor
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/RecursoSupervisor";

        public ApiClientRecursoSupervisor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtener todas las relaciones supervisor-supervisado
        /// </summary>
        public async Task<IEnumerable<RecursoSupervisorDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<RecursoSupervisorDTO>>() ?? new List<RecursoSupervisorDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener relaciones supervisor-supervisado: {ex.Message}");
                return new List<RecursoSupervisorDTO>();
            }
        }

        /// <summary>
        /// Obtener todos los recursos disponibles para supervisión
        /// </summary>
        public async Task<IEnumerable<RecursoSimpleDTO>> GetRecursosDisponiblesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/recursos-disponibles");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<RecursoSimpleDTO>>() ?? new List<RecursoSimpleDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener recursos disponibles: {ex.Message}");
                return new List<RecursoSimpleDTO>();
            }
        }

        /// <summary>
        /// Obtener la vista de supervisión para un supervisor específico
        /// </summary>
        public async Task<SupervisionViewDTO?> GetSupervisionViewAsync(int idSupervisor)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/supervision/{idSupervisor}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<SupervisionViewDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener vista de supervisión: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Asignar un supervisado a un supervisor
        /// </summary>
        public async Task<bool> AsignarSupervisadoAsync(int idSupervisor, int idSupervisado, string? observaciones = null)
        {
            try
            {
                var request = new AsignarSupervisadoRequest
                {
                    IdSupervisor = idSupervisor,
                    IdSupervisado = idSupervisado,
                    Observaciones = observaciones
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/asignar", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al asignar supervisado: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Desasignar un supervisado de un supervisor
        /// </summary>
        public async Task<bool> DesasignarSupervisadoAsync(int idSupervisor, int idSupervisado)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/desasignar/{idSupervisor}/{idSupervisado}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desasignar supervisado: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Verificar si existe una relación supervisor-supervisado
        /// </summary>
        public async Task<bool> ExisteRelacionAsync(int idSupervisor, int idSupervisado)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/existe-relacion/{idSupervisor}/{idSupervisado}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<bool>();
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar relación: {ex.Message}");
                return false;
            }
        }
    }
}