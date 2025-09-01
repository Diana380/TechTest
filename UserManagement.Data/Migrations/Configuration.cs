namespace UserManagement.Data.Migrations;
using System.Data.Entity.Migrations;
using UserManagement.Models;

internal sealed class Configuration: DbMigrationsConfiguration<UserManagement.Data.DataContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = false;
    }
    protected override void Seed(DataContext context)
    {
        context.Users.AddOrUpdate(new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true });
        context.Users.AddOrUpdate(new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true });
        context.Users.AddOrUpdate(new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false });
        context.Users.AddOrUpdate(new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true });
        context.Users.AddOrUpdate(new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true });
        context.Users.AddOrUpdate(new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true });
        context.Users.AddOrUpdate(new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false });
        context.Users.AddOrUpdate(new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false });
        context.Users.AddOrUpdate(new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false });
        context.Users.AddOrUpdate(new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true });
        context.Users.AddOrUpdate(new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true });


    }

}
