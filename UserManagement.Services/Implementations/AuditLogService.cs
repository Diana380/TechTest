using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Entities;
using UserManagement.Data.Repositories;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Implementations;
public class AuditLogService: IAuditLogService
{
    private readonly IAuditLogRepository _auditLog;

    public AuditLogService(IAuditLogRepository auditLog)
    {
        _auditLog = auditLog;
    }
    public async Task<List<AuditLog>> GetAllAsync()
        => await _auditLog.GetAll().ToListAsync();

    public async Task<List<AuditLog>> GetLogsByEntityAsync(string entityName, long entityId)
        => await _auditLog.GetAll()
            .Where(x => x.EntityName == entityName && x.EntityId == entityId)
            .ToListAsync();
}
