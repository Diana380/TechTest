using UserManagement.Data.Models;
using System.Data.Entity;
using UserManagement.Data.Entities;



namespace UserManagement.Data;

public class DataContext : DbContext
{
    public DataContext() : base("DataContext")
    {
    }

    public DbSet<User>? Users { get; set; }
    public virtual required DbSet<AuditLog> AuditLogs { get; set; }
}
