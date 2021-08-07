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
            DomainMessagePersistence entityToAdd = new()
            {
                Id = entity.Id,
                MessageText = entity.MessageText,
                Timestamp = entity.Timestamp,
                UserId = entity.User.Id
            };

            var added = await applicationDBContext
                .Messages
                .AddAsync(entityToAdd);

            _ = await applicationDBContext.SaveChangesAsync();

            return added.Entity;
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return (await applicationDBContext.Messages.ToListAsync()).Select(message => (Message)message);
        }

        public async Task<IEnumerable<Message>> GetRange(PageOptions pageOptions)
        {
            IEnumerable<DomainMessagePersistence> orderedList = await applicationDBContext
                    .Messages
                    .Include("User")
                    .OrderByDescending(order => order.Timestamp)
                    .ToListAsync();

            IEnumerable<DomainMessagePersistence> pagedList = orderedList.Take(pageOptions.Size);

            return pagedList.Select(message => (Message)message);
        }

        public async Task<Message> SingleOrDefault(Guid id)
        {
            return await applicationDBContext.Messages.FindAsync(id);
        }
    }
}
