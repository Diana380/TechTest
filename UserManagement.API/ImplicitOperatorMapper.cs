using UserManagement.API.Models.Users;
using UserManagement.Data.Models;

namespace UserManagement.API;

public class ImplicitOperatorMapper
{
    public static UserListItemViewModel Map(User user) => user;
}
