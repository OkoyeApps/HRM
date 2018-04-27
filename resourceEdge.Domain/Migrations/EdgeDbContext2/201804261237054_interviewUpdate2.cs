namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class interviewUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interviews", "FeedBack", c => c.String());
            AddColumn("dbo.Interviews", "FeedBackSummary", c => c.String());
            AddColumn("dbo.Interviews", "UseCount", c => c.Int(nullable: false));
            AddColumn("dbo.Interviews", "IsCompleted", c => c.Boolean());
            AddColumn("dbo.Interviews", "IsActive", c => c.Boolean());
            AlterColumn("dbo.Candidates", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Candidates", "Status", c => c.String());
            DropColumn("dbo.Interviews", "IsActive");
            DropColumn("dbo.Interviews", "IsCompleted");
            DropColumn("dbo.Interviews", "UseCount");
            DropColumn("dbo.Interviews", "FeedBackSummary");
            DropColumn("dbo.Interviews", "FeedBack");
        }
    }
}
