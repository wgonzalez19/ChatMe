namespace ChatMe.Application.Configuration.Persistence.Repositories
{
    using ChatMe.Application.Shared.Queries;
    using ChatMe.Domain.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IMessagesRepository
    {
        Task<IEnumerable<Message>> GetAll();

        Task<Message> SingleOrDefault(Guid id);

        Task<IEnumerable<Message>> GetRange(PageOptions pageOptions, Expression<Func<Message, int>> order);

        Task<Message> Add(Message entity);
    }
}
