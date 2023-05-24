namespace ApplicationService.Repositories
{
    using ApplicationService.Interfaces;
    using Data.Context;
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    public class Repository : IRepository
    {
        private readonly DbContext dbContext;

        public Repository(SHASContext context)
        {
            dbContext = context;
        }

        private DbSet<T> DbSet<T>()
            where T : class
        {
            return dbContext.Set<T>();
        }

        public async Task Add<T>(T entity)
            where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }

        public async Task<IEnumerable<T>> All<T>()
            where T : class
        {
            return await DbSet<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties)
            where T : class
        {
            IQueryable<T> query = DbSet<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public Task Remove<T>(T entity) 
            where T : class
        {
            DbSet<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
