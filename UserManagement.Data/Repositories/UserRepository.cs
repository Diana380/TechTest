using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dbContext;
    public UserRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => _dbContext.Set<TEntity>();
    public async Task CreateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Attach(entity);
        _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Attach(entity);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();

    }
}
