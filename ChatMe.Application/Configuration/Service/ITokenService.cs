namespace ChatMe.Application.Configuration.Service
{
    using ChatMe.Application.Users;

    public interface ITokenService
    {
        string Key { get; }

        string Issuer { get; }

        string Audience { get; }

        string BuildToken(UserDto user);

        bool ValidateToken(string token);
    }
}
