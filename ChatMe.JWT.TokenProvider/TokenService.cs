namespace ChatMe.JWT.TokenProvider
{
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Application.Users;
    using ChatMe.Domain.Extensions.String;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Key => this.configuration["Jwt:Key"];

        public string Issuer => this.configuration["Jwt:Issuer"];

        public string Audience => throw new NotImplementedException();

        public string BuildToken(UserDto user)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Key));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken tokenDescriptor = new( 
                Issuer,
                Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(configuration["Jwt:ExpirationMinutes"].ToDouble()),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public bool ValidateToken(string token)
        {
            Byte[] secret = Encoding.UTF8.GetBytes(Key);
            SymmetricSecurityKey securityKey = new(secret);
            JwtSecurityTokenHandler tokenHandler = new();

            TokenValidationParameters parameters = new()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = Issuer,
                IssuerSigningKey = securityKey,
            };

            try
            {
                tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
            }
            catch
            {

                return false;
            }

            return true;
        }
    }
}
