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

        public async Task<ConocimientoRecursoDTO?> GetByIdAsync(int id)
        {
            try
            {
                Console.WriteLine($"GET by Id: {_endpoint}/{id}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/{id}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ConocimientoRecursoDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetByIdAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ConocimientoRecursoDTO>> GetConocimientosPorEvaluacionYRecursoAsync(int idEvaluacion, int idRecurso)
        {
            try
            {
                Console.WriteLine($"GET conocimientos: {_endpoint}/evaluacion/{idEvaluacion}/recurso/{idRecurso}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/evaluacion/{idEvaluacion}/recurso/{idRecurso}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                var conocimientos = JsonSerializer.Deserialize<List<ConocimientoRecursoDTO>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<ConocimientoRecursoDTO>();

                Console.WriteLine($"Conocimientos cargados: {conocimientos.Count}");
                return conocimientos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetConocimientosPorEvaluacionYRecursoAsync: {ex.Message}");
                return new List<ConocimientoRecursoDTO>();
            }
        }

        public async Task<ConocimientoRecursoDTO> CreateAsync(ConocimientoRecursoDTO conocimiento)
        {
            try
            {
                Console.WriteLine($"POST create: {_endpoint}");
                var jsonContent = JsonSerializer.Serialize(conocimiento);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}{_endpoint}", content);
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ConocimientoRecursoDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? conocimiento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<ConocimientoRecursoDTO> UpdateAsync(int id, ConocimientoRecursoDTO conocimiento)
        {
            try
            {
                Console.WriteLine($"PUT update: {_endpoint}/{id}");
                var jsonContent = JsonSerializer.Serialize(conocimiento);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"{_baseUrl}{_endpoint}/{id}", content);
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ConocimientoRecursoDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? conocimiento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<ConocimientoRecursoDTO> CrearOActualizarAsync(ConocimientoRecursoDTO conocimiento)
        {
            try
            {
                Console.WriteLine($"POST crear-o-actualizar: {_endpoint}/crear-o-actualizar");
                var jsonContent = JsonSerializer.Serialize(conocimiento);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}{_endpoint}/crear-o-actualizar", content);
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ConocimientoRecursoDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? conocimiento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearOActualizarAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ConocimientoRecursoDTO>> GuardarMultiplesAsync(List<ConocimientoRecursoDTO> conocimientos)
        {
            try
            {
                Console.WriteLine($"POST guardar-multiples: {_endpoint}/guardar-multiples - {conocimientos.Count} items");
                var jsonContent = JsonSerializer.Serialize(conocimientos);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}{_endpoint}/guardar-multiples", content);
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ConocimientoRecursoDTO>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? conocimientos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GuardarMultiplesAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Console.WriteLine($"DELETE: {_endpoint}/{id}");
                var response = await _httpClient.DeleteAsync($"{_baseUrl}{_endpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<decimal> GetPorcentajeCompletitudSubtemasAsync(int idGrillaTema)
        {
            try
            {
                Console.WriteLine($"GET completitud-subtemas: {_endpoint}/completitud-subtemas/{idGrillaTema}");
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

        public async Task<Dictionary<int, decimal>> GetCompletitudSubtemasPorGrillaAsync(int idGrilla)
        {
            try
            {
                Console.WriteLine($"GET completitud-grilla: {_endpoint}/completitud-grilla/{idGrilla}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/completitud-grilla/{idGrilla}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Dictionary<int, decimal>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new Dictionary<int, decimal>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetCompletitudSubtemasPorGrillaAsync: {ex.Message}");
                return new Dictionary<int, decimal>();
            }
        }
    }
}