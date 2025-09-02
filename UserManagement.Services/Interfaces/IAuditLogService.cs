using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Entities;

namespace UserManagement.Services.Interfaces;
public interface IAuditLogService
{
    Task<List<AuditLog>> GetAllAsync();
    Task<List<AuditLog>> GetLogsByEntityAsync(string entityName, long entityId);
}
