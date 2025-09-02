using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Models.Users;
using UserManagement.Data.Models;
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
        var items = await _userService.GetAllAsync();
        var result = items.Select(ImplicitOperatorMapper.Map).ToList();
        return Ok(items);
    }
    [HttpGet]
    [Route("GetActive/{isActive}")]
    public async Task<IActionResult> GetActiveUsers(bool isActive)
    {
        var items = await _userService.FilterByActive(isActive);
        var result = items.Select(ImplicitOperatorMapper.Map).ToList();
        return Ok(items);
    }
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateAsync([FromBody] UserListItemViewModel user)
    {
        if (user == null)
            return BadRequest("User data is required.");


        var newUser = new User
        {
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive
        };
        var createdUser = await _userService.CreateUserAsync(newUser);
        var ceatedUser = ImplicitOperatorMapper.Map(createdUser);
   
        return CreatedAtAction(nameof(GetAll), createdUser);
    }     
}
