using UserManagement.Data.Entities;

namespace UserManagement.API.Models.Users;

public class UserAuditLogcs
{
    public long Id { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public long EntityId { get; set; }
    public string? Details { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static implicit operator UserAuditLogcs(AuditLog auditLog) => new UserAuditLogcs
    {
        Id = auditLog.Id,
        Action = auditLog.Action,
        EntityName = auditLog.EntityName,
        EntityId = auditLog.EntityId,
        Details = auditLog.Details,
        Timestamp = auditLog.Timestamp

    };
}

