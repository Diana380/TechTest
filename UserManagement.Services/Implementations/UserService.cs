using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Entities;
using UserManagement.Data.Models;
using UserManagement.Data.Repositories;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuditLogRepository _auditLog;

    public UserService(IUserRepository userRepository, IAuditLogRepository auditLog)
    {
        _userRepository = userRepository;
        _auditLog = auditLog;
    }

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public async Task<IEnumerable<User>> FilterByActive(bool isActive)
    {
        return await _userRepository.GetAll<User>().Where(x => x.IsActive == isActive).ToListAsync();
    }

    public IEnumerable<User> GetAllUsers() => _userRepository.GetAll<User>();

    public async Task<List<User>> GetAllUsersAsync()
        => await _userRepository.GetAll<User>().ToListAsync();

    public async Task<User?> GetUserByIdAsync(long userId)
        => await _userRepository.GetAll<User>().Where(x => x.Id == userId).FirstOrDefaultAsync();
    public async Task DeleteUserAsync(long userId)
    {
        var user = await _userRepository.GetAll<User>().Where(x => x.Id == userId).FirstAsync();
        await _userRepository.DeleteAsync(user);
        await _auditLog.LogAsync(new AuditLog
        {
            Action = "Delete",
            EntityName = "User",
            EntityId = user.Id,
            Details = $"Deleted user {user.Email}"
        });
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var existingUser = await _userRepository.GetAll<User>().Where(x => x.Id == user.Id).FirstAsync();
        var detailsMessage = $"Updated user {user.Email}. Changes: ";
        if (existingUser.Forename != user.Forename)
        {
            detailsMessage += $"Forename: '{existingUser.Forename}' to '{user.Forename}'; ";
        }
        if (existingUser.Surname != user.Surname)
        {
            detailsMessage += $"Surname: '{existingUser.Surname}' to '{user.Surname}'; ";
        }
        if (existingUser.Email != user.Email)
        {
            detailsMessage += $"Email: '{existingUser.Email}' to '{user.Email}'; ";
        }
        if (existingUser.IsActive != user.IsActive)
        {
            detailsMessage += $"IsActive: '{existingUser.IsActive}' to '{user.IsActive}'; ";
        }
        if (existingUser.DateOfBirth != user.DateOfBirth)
        {
            detailsMessage += $"Date of birth: '{existingUser.DateOfBirth}' to '{user.DateOfBirth}'; ";
        }
        var auditLog = (new AuditLog
        {
            Action = "Update",
            EntityName = "User",
            EntityId = user.Id,
            Details = detailsMessage
        });
        try
        {
            await _userRepository.UpdateAsync(user);
        }
        catch (Exception ex)
        {
            auditLog.Details += $" Update failed: {ex.Message}";
            await _auditLog.LogAsync(auditLog);
            throw;
        }

        await _auditLog.LogAsync(auditLog);
        return user;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _userRepository.CreateAsync(user);
        await _auditLog.LogAsync(new AuditLog
        {
            Action = "Create",
            EntityName = "User",
            EntityId = user.Id,
            Details = $"Created user {user.Email}"
        });
        return user;
    }
  
}
