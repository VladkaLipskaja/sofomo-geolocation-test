namespace Sofomo.Entities
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly GeolocationDBContext _dbContext;

        public Repository(GeolocationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ValueTask<T?> GetAsync(string ip)
        {
            return _dbContext.Set<T>().FindAsync(ip);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}