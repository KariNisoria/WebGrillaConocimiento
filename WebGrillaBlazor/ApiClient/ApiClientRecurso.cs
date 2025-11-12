using System.Net;
using System.Net.Http.Json;
using WebGrillaBlazor.DTOs;

namespace WebGrillaBlazor.ApiClient
{
    public class ApiClientRecurso : ApiClientGeneric<RecursoDTO>
    {
        private readonly IConfiguration _configuration;

        public ApiClientRecurso(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient), "HttpClient no fue inyectado.");
            }

            Console.WriteLine($"ApiClienteRecurso()-> Constructor {_endpoint}");
        }

        public async Task<List<RecursoDTO>> GetRecursoByCorreo(string correo)
        {

            var baseUrl = _httpClient.BaseAddress;
            var direccion = $"{_endpoint}/ByCorreoElectronico/{correo}";

            Console.WriteLine($"GetRecursoByCorreo -> {baseUrl}");
            Console.WriteLine($"GetRecursoByCorreo -> {direccion}");

            var response = await _httpClient.GetAsync(direccion);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<RecursoDTO>>();
        }
    }
}
