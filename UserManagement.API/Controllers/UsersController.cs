using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Models.Users;
using UserManagement.Services.Domain.Interfaces;


namespace UserManagement.API.Controllers;


[Route("users")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        // Simulate async operation if _userService.GetAll() is synchronous
        var items = await Task.Run(() =>
            _userService.GetAll().Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive
            }).ToList()
        );

        return Ok(items);
    }
}
