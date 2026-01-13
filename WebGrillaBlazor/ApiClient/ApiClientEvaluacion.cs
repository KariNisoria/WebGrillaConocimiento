using System.Text.Json;
using WebGrillaBlazor.DTOs;

namespace WebGrillaBlazor.ApiClient
{
    public class ApiClientEvaluacion
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _endpoint;

        public ApiClientEvaluacion(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7093/";
            _endpoint = "api/Evaluacion";
            
            Console.WriteLine($"baseurl :{_baseUrl}");
            Console.WriteLine($"endpoint :{_endpoint}");
        }

        public async Task<List<EvaluacionDTO>> GetAllAsync()
        {
            try
            {
                Console.WriteLine($"GET all: {_endpoint}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<EvaluacionDTO>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<EvaluacionDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAllAsync: {ex.Message}");
                return new List<EvaluacionDTO>();
            }
        }

        public async Task<EvaluacionDTO?> GetByIdAsync(int id)
        {
            try
            {
                Console.WriteLine($"GET by Id: {_endpoint}/{id}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/{id}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EvaluacionDTO>(jsonString, new JsonSerializerOptions
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

        public async Task<EvaluacionDTO?> GetEvaluacionActivaAsync()
        {
            try
            {
                Console.WriteLine($"GET evaluacion activa: {_endpoint}/activa");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/activa");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                    
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EvaluacionDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetEvaluacionActivaAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ConocimientoRecursoDTO>> GenerarConocimientosTemporalesAsync(int idEvaluacion, int idRecurso, int idGrilla)
        {
            try
            {
                Console.WriteLine($"GET conocimientos temporales: {_endpoint}/{idEvaluacion}/conocimientos-temporales/{idRecurso}/{idGrilla}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/{idEvaluacion}/conocimientos-temporales/{idRecurso}/{idGrilla}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ConocimientoRecursoDTO>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<ConocimientoRecursoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GenerarConocimientosTemporalesAsync: {ex.Message}");
                return new List<ConocimientoRecursoDTO>();
            }
        }

        public async Task<List<EvaluacionDTO>> GetEvaluacionesGlobalesAsync()
        {
            try
            {
                Console.WriteLine($"GET evaluaciones globales: {_endpoint}/globales");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/globales");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<EvaluacionDTO>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<EvaluacionDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetEvaluacionesGlobalesAsync: {ex.Message}");
                return new List<EvaluacionDTO>();
            }
        }

        public async Task<List<EvaluacionDTO>> GetEvaluacionesPorGrillaAsync(int idGrilla, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                var queryParams = new List<string>();
                if (fechaInicio.HasValue)
                    queryParams.Add($"fechaInicio={fechaInicio.Value:yyyy-MM-ddTHH:mm:ss}");
                if (fechaFin.HasValue)
                    queryParams.Add($"fechaFin={fechaFin.Value:yyyy-MM-ddTHH:mm:ss}");
                
                var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
                
                Console.WriteLine($"GET evaluaciones por grilla: {_endpoint}/por-grilla/{idGrilla}{queryString}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/por-grilla/{idGrilla}{queryString}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<EvaluacionDTO>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<EvaluacionDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetEvaluacionesPorGrillaAsync: {ex.Message}");
                return new List<EvaluacionDTO>();
            }
        }

        public async Task<EvaluacionGlobalResumenDTO?> GetResumenEvaluacionGlobalAsync(int idGrilla, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                var queryParams = new List<string>();
                if (fechaInicio.HasValue)
                    queryParams.Add($"fechaInicio={fechaInicio.Value:yyyy-MM-ddTHH:mm:ss}");
                if (fechaFin.HasValue)
                    queryParams.Add($"fechaFin={fechaFin.Value:yyyy-MM-ddTHH:mm:ss}");
                
                var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
                
                Console.WriteLine($"GET resumen evaluacion global: {_endpoint}/resumen-global/{idGrilla}{queryString}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{_endpoint}/resumen-global/{idGrilla}{queryString}");
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EvaluacionGlobalResumenDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetResumenEvaluacionGlobalAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<List<EvaluacionDTO>> CrearEvaluacionGlobalAsync(int idGrilla, string descripcion, DateTime fechaFin)
        {
            try
            {
                Console.WriteLine($"POST evaluacion global: {_endpoint}/global");
                
                var request = new
                {
                    IdGrilla = idGrilla,
                    Descripcion = descripcion,
                    FechaFin = fechaFin
                };

                var jsonContent = JsonSerializer.Serialize(request);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}{_endpoint}/global", content);
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<EvaluacionDTO>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<EvaluacionDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearEvaluacionGlobalAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<EvaluacionDTO> IniciarEvaluacionParaRecursoAsync(int idRecurso, int idGrilla, string descripcion)
        {
            try
            {
                Console.WriteLine($"POST iniciar evaluacion: {_endpoint}/iniciar-para-recurso");
                
                var request = new
                {
                    IdRecurso = idRecurso,
                    IdGrilla = idGrilla,
                    Descripcion = descripcion
                };

                var jsonContent = JsonSerializer.Serialize(request);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}{_endpoint}/iniciar-para-recurso", content);
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EvaluacionDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new EvaluacionDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en IniciarEvaluacionParaRecursoAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<EvaluacionDTO> CreateAsync(EvaluacionDTO evaluacion)
        {
            try
            {
                Console.WriteLine($"POST: {_endpoint}");
                var jsonContent = JsonSerializer.Serialize(evaluacion);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}{_endpoint}", content);
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EvaluacionDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new EvaluacionDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<EvaluacionDTO> UpdateAsync(int id, EvaluacionDTO evaluacion)
        {
            try
            {
                Console.WriteLine($"PUT: {_endpoint}/{id}");
                var jsonContent = JsonSerializer.Serialize(evaluacion);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"{_baseUrl}{_endpoint}/{id}", content);
                response.EnsureSuccessStatusCode();
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EvaluacionDTO>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new EvaluacionDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateAsync: {ex.Message}");
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
    }
}