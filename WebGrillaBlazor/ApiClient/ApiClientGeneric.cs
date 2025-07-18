using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebGrillaBlazor.ApiClient
{
    public class ApiClientGeneric<T> where T : class
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _endpoint;

        public ApiClientGeneric(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]);

            // Procesar el nombre de la entidad
            string entityName = typeof(T).Name;

            // 1. Eliminar el sufijo "DTO" (si existe)
            if (entityName.EndsWith("DTO", StringComparison.OrdinalIgnoreCase))
            {
                entityName = entityName.Substring(0, entityName.Length - 3);
            }

            // 2. Asegurar primera letra mayúscula
            if (!string.IsNullOrEmpty(entityName))
            {
                entityName = char.ToUpper(entityName[0]) + entityName.Substring(1).ToLower();
            }

            _endpoint = $"api/{entityName}"; // Ej: "api/Cliente"

            Console.WriteLine($"baseurl :{_httpClient.BaseAddress}");
            Console.WriteLine($"endpoint :{_endpoint}");
        }

        // GET all
        public async Task<List<T>> GetAllAsync()
        {
            Console.WriteLine($"GET all: {_endpoint}");
            var response = await _httpClient.GetAsync(_endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<T>>();
        }

        // GET by ID
        public async Task<T> GetByIdAsync(int id)
        {
            string endpoint = $"{_endpoint}/{id}";
            Console.WriteLine($"GET by Id: {endpoint}");
            var response = await _httpClient.GetAsync(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Devolver null si el objeto no existe
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // POST
        public async Task<T> CreateAsync(T entity)
        {
            string endpoint = _endpoint;
            Console.WriteLine($"POST: {endpoint}");
            var response = await _httpClient.PostAsJsonAsync(endpoint, entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // PUT
        public async Task UpdateAsync(int id, T entity)
        {
            string endpoint = $"{_endpoint}/{id}";
            Console.WriteLine($"PUT: {endpoint}");
            var response = await _httpClient.PutAsJsonAsync(endpoint, entity);
            response.EnsureSuccessStatusCode();
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            string endpoint = $"{_endpoint}/{id}";
            Console.WriteLine($"DELETE: {endpoint}");
            var response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();
        }

        // Configuración futura para JWT
        public void SetJwtToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
