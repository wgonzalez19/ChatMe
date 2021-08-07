namespace ChatMe.Application.Users.LoginUser
{
    using ChatMe.Application.Configuration.Commands;

    public class LoginUserCommand : CommandBase<TokenDto>
    {
        public LoginUserCommand(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Username { get; }

        public string Password { get; }

    }
}
