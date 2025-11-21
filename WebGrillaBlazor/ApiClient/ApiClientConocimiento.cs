using System.Text.Json;
using WebGrillaBlazor.DTOs;

namespace WebGrillaBlazor.ApiClient
{
    public class ApiClientConocimiento
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _endpoint;

        public ApiClientConocimiento(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7093/";
            _endpoint = "api/ConocimientoRecurso";
            
            Console.WriteLine($"baseurl :{_baseUrl}");
            Console.WriteLine($"endpoint :{_endpoint}");
        }

        public async Task<List<ConocimientoRecursoDTO>> GetAllAsync()
        {
            try
            {
                Console.WriteLine($"GET all: {_endpoint}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ConocimientoRecursoDTO>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<ConocimientoRecursoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAllAsync: {ex.Message}");
                return new List<ConocimientoRecursoDTO>();
            }
        }

        public async Task<List<ConocimientoRecursoDTO>> GetConocimientosPorEvaluacionYRecursoAsync(int idEvaluacion, int idRecurso)
        {
            try
            {
                Console.WriteLine($"GET conocimientos evaluacion/recurso: {_endpoint}/evaluacion/{idEvaluacion}/recurso/{idRecurso}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/evaluacion/{idEvaluacion}/recurso/{idRecurso}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ConocimientoRecursoDTO>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<ConocimientoRecursoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetConocimientosPorEvaluacionYRecursoAsync: {ex.Message}");
                return new List<ConocimientoRecursoDTO>();
            }
        }

        public async Task<decimal> GetPorcentajeCompletitudSubtemasAsync(int idGrillaTema)
        {
            try
            {
                Console.WriteLine($"GET completitud subtemas: {_endpoint}/completitud-subtemas/{idGrillaTema}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/completitud-subtemas/{idGrillaTema}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<decimal>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetPorcentajeCompletitudSubtemasAsync: {ex.Message}");
                return 0;
            }
        }

        public async Task<ConocimientoRecursoDTO> UpdateAsync(int id, ConocimientoRecursoDTO conocimiento)
        {
            try
            {
                Console.WriteLine($"PUT: {_endpoint}/{id}");
                var jsonContent = JsonSerializer.Serialize(conocimiento);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"{_baseUrl}{_endpoint}/{id}", content);
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ConocimientoRecursoDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new ConocimientoRecursoDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateAsync: {ex.Message}");
                throw;
            }
        }
    }
}