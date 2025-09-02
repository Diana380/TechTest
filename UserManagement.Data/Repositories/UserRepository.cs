using System;
using System.Data.Entity;
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
    public async Task UpdateAsync(Models.User user)
    {
        if (_dbContext.Users == null)
            throw new InvalidOperationException("Users DbSet is not initialized.");

        var existingUser = await _dbContext.Users.Where(x => x.Id == user.Id).FirstAsync();

        existingUser.Forename = user.Forename;
        existingUser.Surname = user.Surname;
        existingUser.Email = user.Email;
        existingUser.DateOfBirth = user.DateOfBirth;
        existingUser.IsActive = user.IsActive;

        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Attach(entity);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();

    }
}
