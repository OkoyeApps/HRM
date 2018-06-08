namespace resourceEdge.webUi.Migrations.ApplicationDbConntextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentitymodelupdatedwithnullablegroupandlocationId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "GroupId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "LocationId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LocationId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "GroupId", c => c.Int(nullable: false));
        }
    }
}
