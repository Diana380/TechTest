using UserManagement.API.Models.Users;
using UserManagement.Data.Entities;
using UserManagement.Data.Models;

namespace UserManagement.API;

public class ImplicitOperatorMapper
{
    public static UserListItemViewModel Map(User user) => user;
    public static UserAuditLogcs Map(AuditLog log) => log;
}
