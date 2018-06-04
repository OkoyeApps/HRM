namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatetoemployeemodelasssociation : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.Employees", "empID");
            RenameColumn("dbo.Employees", "empID", "ID");
            RenameColumn("dbo.Employees", "businessunitId", "BusinessunitId");
            RenameColumn("dbo.Employees", "jobtitleId", "JobTitleId");
            RenameColumn("dbo.Employees", "positionId", "PositionId");
            //RenameColumn("dbo.Employees", "jobtitleId", "JobTitleId");
            DropColumn("dbo.Employees", "reportingManager1");
            DropColumn("dbo.Employees", "reportingManager2");
            DropIndex("dbo.Employees", new[] { "businessunitId" });
            //DropPrimaryKey("dbo.Employees");
            //AddColumn("dbo.Employees", "ID", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.Employees", "ID");
            CreateIndex("dbo.Employees", "BusinessunitId");
            CreateIndex("dbo.Employees", "JobTitleId");
            CreateIndex("dbo.Employees", "PositionId");
            AddForeignKey("dbo.Employees", "JobTitleId", "dbo.Jobtitles", "Id");
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id");
            //AddForeignKey("dbo.Employees", "BusinessunitId", "dbo.BusinessUnits", "Id");

        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "reportingManager2", c => c.String());
            AddColumn("dbo.Employees", "reportingManager1", c => c.String());
            AddColumn("dbo.Employees", "empID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Employees", "JobTitleId", "dbo.Jobtitles");
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropIndex("dbo.Employees", new[] { "JobTitleId" });
            DropIndex("dbo.Employees", new[] { "BusinessunitId" });
            DropPrimaryKey("dbo.Employees");
            DropColumn("dbo.Employees", "ID");
            AddPrimaryKey("dbo.Employees", "empID");
            CreateIndex("dbo.Employees", "businessunitId");
        }
    }
}
