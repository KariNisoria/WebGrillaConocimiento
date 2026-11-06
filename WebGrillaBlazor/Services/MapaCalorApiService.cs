using System.Net.Http.Json;
using WebGrillaBlazor.DTOs.MapaCalor;
using WebGrillaBlazor.DTOs;

namespace WebGrillaBlazor.Services
{
    public class MapaCalorApiService
    {
        private readonly HttpClient _httpClient;

        public MapaCalorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MapaCalorDTO?> GetMapaCalorAsync(int idGrilla)
        {
            var resultado =
                await _httpClient.GetFromJsonAsync<ApiResultDTO<MapaCalorDTO>>(
                    $"api/MapaCalor/{idGrilla}"
                );

            if (resultado == null)
                return null;

            if (!resultado.IsSuccess)
            {
                Console.WriteLine(
                    $"Error MapaCalor: {resultado.Message}"
                );

                return null;
            }

            return resultado.Value;
        }

        public async Task<List<GrillaDTO>> GetGrillasAsync()
        {
            var grillas = await _httpClient
                .GetFromJsonAsync<List<GrillaDTO>>("api/Grilla");

            return grillas ?? new List<GrillaDTO>();
        }

        public async Task<List<RolDTO>> GetRolesAsync()
        {
            var roles = await _httpClient
                .GetFromJsonAsync<List<RolDTO>>("api/Rol");

            return roles ?? new List<RolDTO>();
        }

        public string ObtenerUrlExcel(int idGrilla, int idRol)
        {
            string ruta = $"api/MapaCalor/{idGrilla}/excel";

            // 0 representa Todos los roles
            if (idRol > 0)
            {
                ruta += $"?idRol={idRol}";
            }

            if (_httpClient.BaseAddress == null)
            {
                throw new InvalidOperationException(
                    "No está configurada la dirección de la API."
                );
            }

            return new Uri(_httpClient.BaseAddress, ruta).ToString();
        }
    }
}
