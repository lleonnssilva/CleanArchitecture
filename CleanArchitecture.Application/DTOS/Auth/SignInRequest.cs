using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOS.Auth
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