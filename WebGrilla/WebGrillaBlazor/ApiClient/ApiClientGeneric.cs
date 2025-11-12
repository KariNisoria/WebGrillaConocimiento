using System.Net.Http.Json;

namespace WebGrillaBlazor.ApiClient
{
    public class ApiClientGeneric<T> where T : class
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _endpoint;

        // Constructor original
        public ApiClientGeneric(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _endpoint = string.Empty;
        }

        // Nuevo constructor con endpoint
        public ApiClientGeneric(HttpClient httpClient, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        // GET all
        public async Task<List<T>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<T>>(_endpoint) ?? new List<T>();
        }

        // GET by ID
        public async Task<T> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<T>(@$"{_endpoint}/{id}");
        }

        // POST
        public async Task<T> CreateAsync(T entity)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // PUT
        public async Task UpdateAsync(int id, T entity)
        {
            var response = await _httpClient.PutAsJsonAsync(@$"{_endpoint}/{id}", entity);
            response.EnsureSuccessStatusCode();
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(@$"{_endpoint}/{id}");
            response.EnsureSuccessStatusCode();
        }

        // GET with custom route
        public async Task<List<T>> GetAsync(string route)
        {
            return await _httpClient.GetFromJsonAsync<List<T>>(@$"{_endpoint}/{route}") ?? new List<T>();
        }

        // Configuración futura para JWT
        public void SetJwtToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
