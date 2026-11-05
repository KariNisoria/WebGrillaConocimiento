using System.Text.Json.Serialization;

namespace WebGrillaBlazor.DTOs.MapaCalor
{
    public class ApiResultDTO<T>
    {
        [JsonPropertyName("value")]
        public T? Value { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}
