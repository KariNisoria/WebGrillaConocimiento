using System.Net.Http.Json;
using WebGrillaBlazor.DTOs;

namespace WebGrillaBlazor.ApiClient
{
    public class ApiClientSubtema
    {
        private readonly HttpClient _httpClient;

        public ApiClientSubtema(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener un subtema por IdSubtema
        public async Task<SubtemaDTO> GetSubtemaByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/subtema/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Devolver null si el subtema no existe
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SubtemaDTO>();
        }


        // Obtener los subtemas por IdTema
        public async Task<List<SubtemaDTO>> GetSubtemasByIdTemaAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/subtema/byidtema/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<SubtemaDTO>>() ?? new List<SubtemaDTO>();
        }

        // Obtener los subtemas 
        public async Task<List<SubtemaDTO>> GetAllSubtemas()
        {
            var response = await _httpClient.GetAsync($"api/subtema/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<SubtemaDTO>>() ?? new List<SubtemaDTO>();
        }

        // Crear un nuevo tema
        public async Task<SubtemaDTO> AddSubtemaAsync(SubtemaDTO subtema)
        {
            var response = await _httpClient.PostAsJsonAsync("api/subtema", subtema);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SubtemaDTO>();
        }

        // Actualizar un tema existente
        public async Task UpdateSubtemaAsync(int id, SubtemaDTO subtema)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/subtema/{id}", subtema);
            response.EnsureSuccessStatusCode();
        }

        // Eliminar un tema
        public async Task DeleteSubtemaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/subtema/{id}");
            response.EnsureSuccessStatusCode();
        }

    }

}