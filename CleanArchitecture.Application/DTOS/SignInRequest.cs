using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class SignInRequest
    {
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("rememberMe")]
        public bool RememberMe { get; set; }
    }
}