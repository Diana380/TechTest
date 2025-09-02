namespace UserManagement.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddDateOfBirthColumnToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DateOfBirth");
        }
    }
}
