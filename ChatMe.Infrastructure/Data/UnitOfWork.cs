namespace ChatMe.Infrastructure.Data
{
    using ChatMe.Application.Configuration.Persistence;
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using ChatMe.Infrastructure.Data.Context;
    using ChatMe.Infrastructure.Data.Repositories;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext applicationDBContext;

        public UnitOfWork(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public IMessagesRepository MessagesRepository => new MessagesReporistory(applicationDBContext);

        public IUsersRepository UsersRepository => new UsersRepository(applicationDBContext);

        public async Task<int> Complete() => await this.applicationDBContext.SaveChangesAsync();

        public void Dispose() => this.applicationDBContext.Dispose();
    }
}
