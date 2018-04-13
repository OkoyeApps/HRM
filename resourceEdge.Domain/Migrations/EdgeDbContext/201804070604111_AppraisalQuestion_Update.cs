namespace resourceEdge.Domain.Migrations.EdgeDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppraisalQuestion_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppraisalQuestions", "EditCount", c => c.Int());
            AddColumn("dbo.AppraisalQuestions", "IsSubmitted", c => c.Boolean());
            AddColumn("dbo.AppraisalQuestions", "IsAccepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppraisalQuestions", "IsAccepted");
            DropColumn("dbo.AppraisalQuestions", "IsSubmitted");
            DropColumn("dbo.AppraisalQuestions", "EditCount");
        }
    }
}
