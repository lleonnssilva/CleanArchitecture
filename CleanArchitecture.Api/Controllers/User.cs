using System.Security.Claims;

namespace CleanArchitecture.Api.Controllers
{
    internal class User
    {
        public ClaimsIdentity? Username { get; internal set; }
        public ClaimsIdentity? Email { get; internal set; }
    }
}