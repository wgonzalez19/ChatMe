namespace ChatMe.Infrastructure.Data.Repositories
{
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using ChatMe.Application.Shared.Queries;
    using ChatMe.Domain.Users;
    using ChatMe.Infrastructure.Data.Configurations;
    using ChatMe.Infrastructure.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDBContext applicationDBContext;

        public UsersRepository(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async Task<User> Add(User entity)
        {
            var added = await applicationDBContext
                .Users
                .AddAsync(
                    new DomainUserPersistence
                    {
                        Id = entity.Id,
                        Username = entity.Username,
                        Password = entity.Password
                    });

            return added.Entity;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return (await applicationDBContext.Users.ToListAsync()).Select(user => (User)user);
        }

        public async Task<IEnumerable<User>> GetRange(PageOptions pageOptions, Expression<Func<User, int>> order)
        {
            return (
                await applicationDBContext
                    .Users
                    .OrderBy(user => order)
                    .Skip(pageOptions.Page)
                    .Take(pageOptions.Size)
                    .ToListAsync())
                    .Select(user => (User)user);

        }

        public async Task<User> SingleOrDefault(Guid id)
        {
            return await applicationDBContext.Users.FindAsync(id);
        }

        public async Task<User> SingleOrDefault(string username)
        {
            return await applicationDBContext.Users.SingleOrDefaultAsync(user => user.Username.Equals(username));
        }
    }
}
