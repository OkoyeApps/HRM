namespace resourceEdge.webUi.Migrations.ApplicationDbConntextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationAndGroupForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserfullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "GroupId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "LocationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LocationId");
            DropColumn("dbo.AspNetUsers", "GroupId");
            DropColumn("dbo.AspNetUsers", "UserfullName");
        }
    }
}
