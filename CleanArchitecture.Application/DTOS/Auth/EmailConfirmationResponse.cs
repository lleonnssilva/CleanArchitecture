using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOS.Auth
{
    public class TokenResponse : AuthenticationResponse
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}