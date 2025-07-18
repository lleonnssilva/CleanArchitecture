namespace CleanArchitecture.Infrastructure.Identity.Models
{
    public class AuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserCreateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
