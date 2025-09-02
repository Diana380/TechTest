using UserManagement.Data.Models;

namespace UserManagement.API.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }
    public required string Forename { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator UserListItemViewModel(User user) => new UserListItemViewModel
    {
        Id = user.Id,
        Forename = user.Forename,
        Surname = user.Surname,
        Email = user.Email,
        IsActive = user.IsActive
    };

}
