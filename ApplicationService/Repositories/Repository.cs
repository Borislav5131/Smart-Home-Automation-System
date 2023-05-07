namespace ApplicationService.Repositories
{
    using ApplicationService.Interfaces;
    using Data.Context;
    using Microsoft.EntityFrameworkCore;

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
