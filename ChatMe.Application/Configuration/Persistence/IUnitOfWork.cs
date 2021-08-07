namespace ChatMe.Application.Configuration.Persistence
{
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        public IMessagesRepository MessagesRepository { get; }

        public IUsersRepository UsersRepository { get; }

        Task<int> Complete();
    }
}
