using CleanArchitecture.Infrastructure.Identity.Models;
using CleanArchitecture.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = await _authService.Autenticate(authenticationRequest);
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserCreateResponse>> Register(UserCreateRequest request)
        {
            var userCreateResponse = await _authService.RegisterAsync(request);
            if (userCreateResponse == null) return BadRequest();
            return userCreateResponse;
        }
    }


}
