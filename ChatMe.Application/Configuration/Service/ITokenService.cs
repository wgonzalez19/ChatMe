namespace ChatMe.Application.Configuration.Service
{
    using ChatMe.Application.Users;
    using Microsoft.IdentityModel.Tokens;

    public interface ITokenService
    {
        string Key { get; }

        string Issuer { get; }

        string Audience { get; }

        string BuildToken(UserDto user);

        SecurityToken ValidateToken(string token);
    }
}
