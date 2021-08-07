namespace ChatMe.Application.Users.LoginUser
{
    using ChatMe.Application.Configuration.Commands;
    using ChatMe.Application.Configuration.Persistence;
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Domain.Exceptions;
    using ChatMe.Domain.Users;
    using ChatMe.Resources;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            this.unitOfWork = unitOfWork;
            this.tokenService = tokenService;
        }

        public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User userToLogin = new(request.Username, request.Password);

            User registerUser = await this.unitOfWork.UsersRepository.SingleOrDefault(request.Username);

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
