namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Systemadminmodeladded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemAdmins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(),
                        UserID = c.String(),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SystemAdmins", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.SystemAdmins", "GroupId", "dbo.Groups");
            DropIndex("dbo.SystemAdmins", new[] { "LocationId" });
            DropIndex("dbo.SystemAdmins", new[] { "GroupId" });
            DropTable("dbo.SystemAdmins");
        }
    }
}
