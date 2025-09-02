using UserManagement.Data.Models;
using System.Data.Entity;



namespace UserManagement.Data;

public class DataContext : DbContext
{
    public DataContext() : base("DataContext")
    {
    }

    public DbSet<User>? Users { get; set; }
}
