using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOS.Auth
{
    public class SignUpRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}