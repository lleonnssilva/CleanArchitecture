using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOS.Auth
{
    public class AuthenticationResponse
    {
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }
        
        [JsonPropertyName("errors")]
        public Dictionary<string, string> Errors { get; set; }

        public string Token { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
