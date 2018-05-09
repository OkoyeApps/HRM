namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateforgroupsforentititesleavereportmanagerrequestAsset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportManagers", "GroupId", c => c.Int(nullable: false));
            AddColumn("dbo.ReportManagers", "LocationId", c => c.Int(nullable: false));
            AddColumn("dbo.LeaveRequests", "GroupId", c => c.Int(nullable: false));
            AddColumn("dbo.LeaveRequests", "LocationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeaveRequests", "LocationId");
            DropColumn("dbo.LeaveRequests", "GroupId");
            DropColumn("dbo.ReportManagers", "LocationId");
            DropColumn("dbo.ReportManagers", "GroupId");
        }
    }
}
