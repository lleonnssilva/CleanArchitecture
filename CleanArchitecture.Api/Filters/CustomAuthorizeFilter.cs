using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Api.Filters
{
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly string _requiredRole;

        public CustomAuthorizeFilter(string requiredRole = null)
        {
            _requiredRole = requiredRole; // Você pode passar uma role que o usuário precisa para acessar a rota
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Obtendo o cabeçalho de autorização
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authorizationHeader.StartsWith("Bearer ") ? authorizationHeader.Substring("Bearer ".Length).Trim() : null;

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                // Validação do token JWT
                var key = Encoding.UTF8.GetBytes("sua_chave_secreta"); // Chave secreta usada para assinar o token

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var userRole = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (_requiredRole != null && userRole != _requiredRole)
                {
                    context.Result = new ForbidResult(); // Se o usuário não tem a role necessária
                    return;
                }

                // Você pode adicionar mais validações aqui (ex: verificar claims adicionais)

            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
