namespace ChatMe.Application.Configuration.Persistence.Repositories
{
    using ChatMe.Application.Shared.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> 
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> SingleOrDefault(Guid id);

        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetRange(PageOptions pageOptions, Expression<Func<TEntity, int>> order);

        Task<TEntity> Add(TEntity entity);
    }
}
