using CleanArchitecture.Application.Interfaces.Identity;
using Core.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService,
            IUserService userService,
            IConfiguration configuration)
        {
            _logger = logger;
            _authService = authService;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<AuthenticationResponse>> SignInAsync(SignInRequest request)
        {
            try
            {
                // Authenticate user and generate authentication token
                bool succeeded = await _authService.SignInAsync(request);

                if (!succeeded)
                {
                    // To do: display error messages
                    return Unauthorized();
                }

                //return Ok();
                //var user = await _userService.FindByEmailAsync(request.Email.ToString());
                //var token = GenerateJwtToken(user);

                //// Criar a resposta de autenticação com o token
                //var response = new AuthenticationResponse
                //{
                //    Token = token,
                //    // Você pode adicionar outras informações do usuário, se necessário
                //    UserId = user.Id,
                //    UserName = request.Email
                //};

                //// Retorna o token gerado
                //return Ok(response);
                var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var _issuer = _configuration["Jwt:Issuer"];
                var _audience = _configuration["Jwt:Audience"];

                var signinCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(2),
                    signingCredentials: signinCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new { Token = tokenString });


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthenticationResponse>> SignUpAsync(SignUpRequest request)
        {
            try
            {
                // Register new user
                AuthenticationResponse response = await _authService.SignUpAsync(request);

                if (response == null || !response.Succeeded)
                {
                    // To do: display error messages
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("signout")]
        public async Task<ActionResult> SignOutAsync()
        {
            // Log out user
            await _authService.SignOutAsync();

            return NoContent();
        }

        [HttpPost("reset")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            try
            {
                // Send password reset request for user
                AuthenticationResponse response = await _authService.ResetPasswordAsync(request);

                if (response == null || !response.Succeeded)
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("confirm")]
        public async Task<ActionResult> RegisterConfirmationAsync()
        {
            try
            {
                TokenResponse response = await _authService.GenerateEmailConfirmationAsync(User);

                if (response == null || !response.Succeeded)
                {
                    return BadRequest();
                }

                // Send email with token code

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("confirm")]
        public async Task<ActionResult> ConfirmEmailAsync(EmailConfirmationRequest request)
        {
            try
            {
                // Confirm email address of user
                await _authService.ConfirmEmailAsync(request);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/auth/whoami
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(User.Identity.Name);
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GenerateJwtToken(ApplicationUserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Email),
        // Adicione outras claims conforme necessário
    };

            // Define a data de ativação e expiração corretamente
            var now = DateTime.UtcNow;
            var expires = now.AddHours(1); // Token expira em 1 hora
            var notBefore = now; // Token pode ser usado imediatamente

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                NotBefore = notBefore, // Garante que o token será válido imediatamente
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }


}
