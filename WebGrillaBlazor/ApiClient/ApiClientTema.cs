using System.Net.Http.Json;
using WebGrillaBlazor.DTOs;
using WebGrillaBlazor.Utils;

namespace WebGrillaBlazor.ApiClient { 
    public class ApiClientTema
    {
        private readonly HttpClient _httpClient;

        public ApiClientTema(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener todos los temas
        public async Task<List<TemaDTO>> GetAllTemasAsync()
        {
            var response = await _httpClient.GetAsync("api/tema");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TemaDTO>>() ?? new List<TemaDTO>();
        }

        // Obtener un tema por ID
        public async Task<TemaDTO> GetTemaByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/tema/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Devolver null si el tema no existe
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TemaDTO>();
        }

        // Crear un nuevo tema
        public async Task<TemaDTO> AddTemaAsync(TemaDTO tema)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tema", tema);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TemaDTO>();
        }

        // Actualizar un tema existente
        public async Task UpdateTemaAsync(int id, TemaDTO tema)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tema/{id}", tema);
            response.EnsureSuccessStatusCode();
        }

        // Eliminar un tema
        public async Task<Result<TemaDTO>> DeleteTemaAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/tema/{id}");

                // Si la respuesta es exitosa (2xx)
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<TemaDTO>();
                    return Result<TemaDTO>.Success(content);
                }

                // Si hay error (4xx o 5xx)
                var error = await response.Content.ReadAsStringAsync();
                return Result<TemaDTO>.Failure(error);

            }
            catch (Exception ex)
            {

                // Errores de red (p.ej., API no disponible)
                return Result<TemaDTO>.Failure($"Error de comunicación: {ex.Message}");
            }
        }
    }

}