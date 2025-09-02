using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Entities;

namespace UserManagement.Data.Repositories;

public interface IAuditLogRepository
{
    Task LogAsync(AuditLog log);
    IQueryable<AuditLog> GetAll();
}
