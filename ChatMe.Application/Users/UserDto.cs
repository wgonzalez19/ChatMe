namespace ChatMe.Application.Users
{
    using ChatMe.Domain.Users;

    public class UserDto
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public static UserDto MapFromDomain(User user)
        {
            return new UserDto()
            {
                Id = user?.Id.ToString(),
                Username = user?.Username,
            };
        }
    }
}
