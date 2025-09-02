using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Entities;

namespace UserManagement.Data.Repositories;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly DataContext _context;
    public AuditLogRepository(DataContext context) => _context = context;

    public async Task LogAsync(AuditLog log)
    {
        System.ArgumentNullException.ThrowIfNull(log);

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }
    public IQueryable<AuditLog> GetAll()
    {
        return _context.AuditLogs.AsQueryable();
    }
}
