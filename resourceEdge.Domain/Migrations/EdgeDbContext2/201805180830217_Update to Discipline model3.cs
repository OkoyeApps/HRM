namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetoDisciplinemodel3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplinaryIncidents", "GroupId", c => c.Int(nullable: false, defaultValue:1));
            AddColumn("dbo.DisciplinaryIncidents", "LocationId", c => c.Int(nullable: false, defaultValue:1));
            CreateIndex("dbo.DisciplinaryIncidents", "GroupId");
            CreateIndex("dbo.DisciplinaryIncidents", "LocationId");
            AddForeignKey("dbo.DisciplinaryIncidents", "GroupId", "dbo.Groups", "Id");
            AddForeignKey("dbo.DisciplinaryIncidents", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisciplinaryIncidents", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.DisciplinaryIncidents", "GroupId", "dbo.Groups");
            DropIndex("dbo.DisciplinaryIncidents", new[] { "LocationId" });
            DropIndex("dbo.DisciplinaryIncidents", new[] { "GroupId" });
            DropColumn("dbo.DisciplinaryIncidents", "LocationId");
            DropColumn("dbo.DisciplinaryIncidents", "GroupId");
        }
    }
}
