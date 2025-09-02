using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Models;
using UserManagement.Data.Repositories;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _dataAccess;
    public UserService(IUserRepository dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public async Task<IEnumerable<User>> FilterByActive(bool isActive)
    {
        return await _dataAccess.GetAll<User>().Where(x => x.IsActive==isActive).ToListAsync();       
    }

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public async Task<List<User>> GetAllAsync()
       => await _dataAccess.GetAll<User>().ToListAsync();

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _dataAccess.GetAll<User>().Where(x => x.Id == userId).FirstAsync();
        await _dataAccess.DeleteAsync(user);
    }
    public async Task<User> UpdateUserAsync(User user)
    {
        await _dataAccess.UpdateAsync(user);
        return user;
    }
    public async Task<User> CreateUserAsync(User user)
    {
        await _dataAccess.CreateAsync(user);
        return user;
    }
}
