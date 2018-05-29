namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullablelocationIdforjob : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Jobtitles", new[] { "LocationId" });
            AlterColumn("dbo.Jobtitles", "LocationId", c => c.Int());
            CreateIndex("dbo.Jobtitles", "LocationId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Jobtitles", new[] { "LocationId" });
            AlterColumn("dbo.Jobtitles", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobtitles", "LocationId");
        }
    }
}
