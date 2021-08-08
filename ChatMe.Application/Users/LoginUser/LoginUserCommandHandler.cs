namespace ChatMe.Application.Users.LoginUser
{
    using ChatMe.Application.Configuration.Commands;
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Domain.Exceptions;
    using ChatMe.Domain.Users;
    using ChatMe.Resources;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenDto>
    {
        private readonly IUsersRepository usersRepository;
        private readonly ITokenService tokenService;

        public LoginUserCommandHandler(IUsersRepository usersRepository, ITokenService tokenService)
        {
            this.usersRepository = usersRepository;
            this.tokenService = tokenService;
        }

        public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            Throw.When<RestException>(
                string.IsNullOrEmpty(request?.Username) || string.IsNullOrEmpty(request?.Password),
                ExceptionMessage.INVALID_USER_PASSWORD,
                HttpStatusCode.BadRequest);

            User userToLogin = new(request.Username, request.Password);

            User registerUser = await this.usersRepository.SingleOrDefault(request.Username);

            Throw.When<RestException>(
                registerUser is null,
                ExceptionMessage.INVALID_USER_PASSWORD,
                HttpStatusCode.Unauthorized);

            Throw.When<RestException>(
                !registerUser.ValidatePassword(userToLogin.Password),
                ExceptionMessage.INVALID_USER_PASSWORD,
                HttpStatusCode.Unauthorized);

            string token = this.tokenService.BuildToken(UserDto.MapFromDomain(registerUser));

            return new TokenDto { Token = token };
        }
    }
}
