﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    Task<IEnumerable<User>> FilterByActive(bool isActive);
    IEnumerable<User> GetAll();
    Task<List<User>> GetAllAsync();
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);

}
