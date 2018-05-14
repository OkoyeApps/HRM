namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class httpmethodforactivitylog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityLogs", "HttpMethod", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActivityLogs", "HttpMethod");
        }
    }
}
