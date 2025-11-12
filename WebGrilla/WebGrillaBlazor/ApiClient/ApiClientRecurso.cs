using System.Net;
using System.Net.Http.Json;
using WebGrilla.DTOs;

namespace WebGrillaBlazor.ApiClient
{
    /// <summary>
    /// Client for interacting with the Recurso API endpoints.
    /// </summary>
    public class ApiClientRecurso : ApiClientGeneric<RecursoDTO>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the ApiClientRecurso class.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for making API requests.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <exception cref="ArgumentNullException">Thrown when httpClient or configuration is null.</exception>
        public ApiClientRecurso(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, "api/Recurso")
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient), "HttpClient no fue inyectado.");
            }
        }

        /// <summary>
        /// Retrieves recursos by email address.
        /// </summary>
        /// <param name="correo">The email address to search for.</param>
        /// <returns>A list of recursos matching the email address.</returns>
        /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
        public async Task<List<RecursoDTO>> GetRecursoByCorreo(string correo)
        {
            if (string.IsNullOrEmpty(correo))
            {
                throw new ArgumentException("El correo electrónico no puede estar vacío.", nameof(correo));
            }

            var route = $"ByCorreoElectronico/{correo}";
            try
            {
                return await GetAsync(route);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Error al obtener recursos por correo: {ex.Message}", ex);
            }
        }
    }
}