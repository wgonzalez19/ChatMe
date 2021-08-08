namespace ChatMe.UnitTests
{
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Application.Users;
    using ChatMe.Application.Users.LoginUser;
    using ChatMe.Domain.Exceptions;
    using ChatMe.Domain.Users;
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;
    using System.Threading;
    using System.Threading.Tasks;
    
    [TestFixture]
    public class LoginUserTests
    {
        [Test]
        public async Task LoginUser_WhenUserExists_IsSuccessful()
        {
            // Arrange
            LoginUserCommand loginUserCommand = new("Will", "ClaveSegura1");
            User userInDB = new("Will", "jpPI/BQPEbRdb6hJhlA2kwlqdbA=");

            var userRepositoryMock = new Mock<IUsersRepository>();
            var tokenServiceMock = new Mock<ITokenService>();

            userRepositoryMock.Setup(mock => mock.SingleOrDefault(It.IsAny<string>())).ReturnsAsync(userInDB);
            tokenServiceMock.Setup(mock => mock.BuildToken(It.IsAny<UserDto>())).Returns("Token");

            LoginUserCommandHandler commandHandler = new(userRepositoryMock.Object, tokenServiceMock.Object);

            // Act
            TokenDto result = await commandHandler.Handle(loginUserCommand, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task LoginUser_WhenUserDontExists_ThrowsAnException()
        {
            // Arrange
            LoginUserCommand loginUserCommand = new("Will", "ClaveSegura1");

            var userRepositoryMock = new Mock<IUsersRepository>();
            var tokenServiceMock = new Mock<ITokenService>();

            userRepositoryMock.Setup(mock => mock.SingleOrDefault(It.IsAny<string>())).Returns(Task.FromResult<User>(null));
            tokenServiceMock.Setup(mock => mock.BuildToken(It.IsAny<UserDto>())).Returns("Token");

            LoginUserCommandHandler commandHandler = new(userRepositoryMock.Object, tokenServiceMock.Object);

            // Act and Assert
            FluentActions.Invoking(() => commandHandler.Handle(loginUserCommand, CancellationToken.None))
                .Should()
                .Throw<RestException>();
        }

        [Test]
        public async Task LoginUser_WhenPasswordDontMatch_ThrowsAnException()
        {
            // Arrange
            LoginUserCommand loginUserCommand = new("Will", "Password");
            User userInDB = new("Will", "jpPI/BQPEbRdb6hJhlA2kwlqdbA=");

            var userRepositoryMock = new Mock<IUsersRepository>();
            var tokenServiceMock = new Mock<ITokenService>();

            userRepositoryMock.Setup(mock => mock.SingleOrDefault(It.IsAny<string>())).ReturnsAsync(userInDB);
            tokenServiceMock.Setup(mock => mock.BuildToken(It.IsAny<UserDto>())).Returns("Token");

            LoginUserCommandHandler commandHandler = new(userRepositoryMock.Object, tokenServiceMock.Object);

            // Act and Assert
            FluentActions.Invoking(() => commandHandler.Handle(loginUserCommand, CancellationToken.None))
                .Should()
                .Throw<RestException>();
        }

        [Test]
        public async Task LoginUser_WhenCommandRequestPasswordIsInvalid_ThrowsAnException()
        {
            // Arrange
            LoginUserCommand loginUserCommand = new("Will", string.Empty);
            User userInDB = new("Will", "jpPI/BQPEbRdb6hJhlA2kwlqdbA=");

            var userRepositoryMock = new Mock<IUsersRepository>();
            var tokenServiceMock = new Mock<ITokenService>();

            userRepositoryMock.Setup(mock => mock.SingleOrDefault(It.IsAny<string>())).ReturnsAsync(userInDB);
            tokenServiceMock.Setup(mock => mock.BuildToken(It.IsAny<UserDto>())).Returns("Token");

            LoginUserCommandHandler commandHandler = new(userRepositoryMock.Object, tokenServiceMock.Object);

            // Act and Assert
            FluentActions.Invoking(() => commandHandler.Handle(loginUserCommand, CancellationToken.None))
                .Should()
                .Throw<RestException>();
        }

        [Test]
        public async Task LoginUser_WhenCommandRequestUserNameIsInvalid_ThrowsAnException()
        {
            // Arrange
            LoginUserCommand loginUserCommand = new(string.Empty, "Password");
            User userInDB = new("Will", "jpPI/BQPEbRdb6hJhlA2kwlqdbA=");

            var userRepositoryMock = new Mock<IUsersRepository>();
            var tokenServiceMock = new Mock<ITokenService>();

            userRepositoryMock.Setup(mock => mock.SingleOrDefault(It.IsAny<string>())).ReturnsAsync(userInDB);
            tokenServiceMock.Setup(mock => mock.BuildToken(It.IsAny<UserDto>())).Returns("Token");

            LoginUserCommandHandler commandHandler = new(userRepositoryMock.Object, tokenServiceMock.Object);

            // Act and Assert
            FluentActions.Invoking(() => commandHandler.Handle(loginUserCommand, CancellationToken.None))
                .Should()
                .Throw<RestException>();
        }

        [Test]
        public async Task LoginUser_WhenCommandRequestIsInvalid_ThrowsAnException()
        {
            // Arrange
            LoginUserCommand loginUserCommand = new(string.Empty, string.Empty);
            User userInDB = new("Will", "jpPI/BQPEbRdb6hJhlA2kwlqdbA=");

            var userRepositoryMock = new Mock<IUsersRepository>();
            var tokenServiceMock = new Mock<ITokenService>();

            userRepositoryMock.Setup(mock => mock.SingleOrDefault(It.IsAny<string>())).ReturnsAsync(userInDB);
            tokenServiceMock.Setup(mock => mock.BuildToken(It.IsAny<UserDto>())).Returns("Token");

            LoginUserCommandHandler commandHandler = new(userRepositoryMock.Object, tokenServiceMock.Object);

            // Act and Assert
            FluentActions.Invoking(() => commandHandler.Handle(loginUserCommand, CancellationToken.None))
                .Should()
                .Throw<RestException>();
        }
    }
}
