using System.Linq;

namespace UserManagement.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dbContext;
    public UserRepository(DataContext dbContext )
    {
        _dbContext = dbContext;
    }
    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => _dbContext.Set<TEntity>();
    public void Create<TEntity>(TEntity entity) where TEntity : class
        {
        _dbContext.Set<TEntity>().Add(entity);
        _dbContext.SaveChanges();
    }
    public void Update<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Attach(entity);
        _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        _dbContext.SaveChanges();
    }
    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Attach(entity);
        _dbContext.Set<TEntity>().Remove(entity);
        _dbContext.SaveChanges();
    }
}
