namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationlocationIdadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobtitles", "LocationId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Jobtitles", "LocationId");
            AddForeignKey("dbo.Jobtitles", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobtitles", "LocationId", "dbo.Locations");
            DropIndex("dbo.Jobtitles", new[] { "LocationId" });
            DropColumn("dbo.Jobtitles", "LocationId");
        }
    }
}
