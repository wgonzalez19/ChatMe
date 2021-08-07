namespace ChatMe.Infrastructure.Middlewares.Security
{
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Application.Users;
    using ChatMe.Domain.Exceptions;
    using ChatMe.Domain.Users;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class JwtMiddleware
    {
        private const string authorizationHeaderName = "Authorization";
        private const char whiteSpace = (char)32;

        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;
        private readonly ITokenService tokenService;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration, ITokenService tokenService)
        {
            this.next = next;
            this.configuration = configuration;
            this.tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers[authorizationHeaderName].FirstOrDefault()?.Split(whiteSpace).Last();

            if (token is not null) 
                AttachUserToContext(context, token);

            await next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            Byte[] key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

            SecurityToken validatedToken = this.tokenService.ValidateToken(token);

            Throw.When<RestException>(validatedToken is null, "Invalid Token", HttpStatusCode.Unauthorized);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

            string userId = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            string username = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

            context.Items[nameof(User)] = new UserDto() { Id = userId, Username = username };

            return;
        }
    }
}
