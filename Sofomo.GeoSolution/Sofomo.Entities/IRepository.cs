namespace Sofomo.Entities
{
    public interface IRepository<T> where T : class
    {
        ValueTask<T?> GetAsync(string ip);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
    }
}