namespace resourceEdge.Domain.Migrations.EdgeDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginTableUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logins", "SessionID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logins", "SessionID");
        }
    }
}
