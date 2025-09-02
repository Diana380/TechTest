namespace UserManagement.Data.Migrations
{
    
    using System.Data.Entity.Migrations;
    
    public partial class CreateAuditLogDBEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Action = c.String(),
                        EntityName = c.String(),
                        EntityId = c.Long(nullable: false),
                        Details = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditLogs");
        }
    }
}
