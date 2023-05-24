namespace ApplicationService.Interfaces
{
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository
    {
        Task Add<T>(T entity)
            where T : class;

        Task Remove<T>(T entity)
            where T : class;

        Task<IEnumerable<T>> All<T>()
            where T : class;

        Task<IEnumerable<T>> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties)
            where T : class;

        Task<int> SaveChanges();
    }
}
