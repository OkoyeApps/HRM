namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Birthdayandammouncementmodeladded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Createdon = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Birthdays",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        DateofBirth = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Birthdays", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Birthdays", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Announcements", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Announcements", "GroupId", "dbo.Groups");
            DropIndex("dbo.Birthdays", new[] { "LocationId" });
            DropIndex("dbo.Birthdays", new[] { "GroupId" });
            DropIndex("dbo.Announcements", new[] { "LocationId" });
            DropIndex("dbo.Announcements", new[] { "GroupId" });
            DropTable("dbo.Birthdays");
            DropTable("dbo.Announcements");
        }
    }
}
