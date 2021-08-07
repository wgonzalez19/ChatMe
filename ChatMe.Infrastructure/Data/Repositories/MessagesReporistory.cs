namespace ChatMe.Infrastructure.Data.Repositories
{
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using ChatMe.Application.Shared.Queries;
    using ChatMe.Domain.Messages;
    using ChatMe.Infrastructure.Data.Configurations;
    using ChatMe.Infrastructure.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class MessagesReporistory : IMessagesRepository
    {
        private readonly ApplicationDBContext applicationDBContext;

        public MessagesReporistory(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async Task<Message> Add(Message entity)
        {
            var added = await applicationDBContext
                .Messages
                .AddAsync(
                    new DomainMessagePersistence
                    {
                        Id = entity.Id,
                        MessageText = entity.MessageText,
                        Timestamp = entity.Timestamp,
                        User = new DomainUserPersistence 
                        {
                            Id = entity.User.Id,
                            Username = entity.User.Username,
                            Password = entity.User.Password,
                        },
                    });

            return added.Entity;
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return (await applicationDBContext.Messages.ToListAsync()).Select(message => (Message)message);
        }

        public async Task<IEnumerable<Message>> GetRange(PageOptions pageOptions, Expression<Func<Message, int>> order)
        {
            return (
                await applicationDBContext
                    .Messages
                    .OrderBy(user => order)
                    .Skip(pageOptions.Page)
                    .Take(pageOptions.Size)
                    .ToListAsync())
                    .Select(message => (Message)message);
        }

        public async Task<Message> SingleOrDefault(Guid id)
        {
            return await applicationDBContext.Messages.FindAsync(id);
        }
    }
}
