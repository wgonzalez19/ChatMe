namespace ChatMe.Infrastructure.Data.Repositories
{
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using ChatMe.Application.Shared.Queries;
    using ChatMe.Infrastructure.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        private readonly ApplicationDBContext applicationDBContext;

        public Repository(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var added = await applicationDBContext.Set<TEntity>().AddAsync(entity);

            return added.Entity;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await applicationDBContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetRange(PageOptions pageOptions, Expression<Func<TEntity, int>> order)
        {
            return await applicationDBContext
                .Set<TEntity>()
                .OrderBy(order)
                .Skip(pageOptions.Page)
                .Take(pageOptions.Size)
                .ToListAsync();
        }

        public async Task<TEntity> SingleOrDefault(Guid id)
        {
            return await applicationDBContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await applicationDBContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
    }
}
