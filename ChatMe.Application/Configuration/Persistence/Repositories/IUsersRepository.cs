namespace ChatMe.Application.Configuration.Persistence.Repositories
{
    using ChatMe.Application.Shared.Queries;
    using ChatMe.Domain.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> SingleOrDefault(Guid id);

        Task<User> SingleOrDefault(string username);

        Task<IEnumerable<User>> GetRange(PageOptions pageOptions, Expression<Func<User, int>> order);

        Task<User> Add(User entity);
    }
}
