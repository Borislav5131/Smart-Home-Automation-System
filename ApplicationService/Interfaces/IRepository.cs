namespace ApplicationService.Interfaces
{
    using System.Linq;

    public interface IRepository
    {
        Task Add<T>(T entity)
            where T : class;

        Task Remove<T>(T entity)
            where T : class;

        Task<IEnumerable<T>> All<T>()
            where T : class;

        Task<int> SaveChanges();
    }
}
