namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppraisalQuestionGroupIdandDepartmentidUpdateandunitId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AppraisalQuestions", new[] { "QuestionId" });
            AddColumn("dbo.AppraisalQuestions", "GeneralQuestionId", c => c.Int());
            AddColumn("dbo.AppraisalQuestions", "BusinessUnitId", c => c.Int());
            AddColumn("dbo.AppraisalQuestions", "DepartmentQuestionId", c => c.Int());
            AlterColumn("dbo.AppraisalQuestions", "QuestionId", c => c.Int());
            CreateIndex("dbo.AppraisalQuestions", "QuestionId");
            CreateIndex("dbo.AppraisalQuestions", "GeneralQuestionId");
            AddForeignKey("dbo.AppraisalQuestions", "GeneralQuestionId", "dbo.GeneralQuestions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppraisalQuestions", "GeneralQuestionId", "dbo.GeneralQuestions");
            DropIndex("dbo.AppraisalQuestions", new[] { "GeneralQuestionId" });
            DropIndex("dbo.AppraisalQuestions", new[] { "QuestionId" });
            AlterColumn("dbo.AppraisalQuestions", "QuestionId", c => c.Int(nullable: false));
            DropColumn("dbo.AppraisalQuestions", "DepartmentQuestionId");
            DropColumn("dbo.AppraisalQuestions", "BusinessUnitId");
            DropColumn("dbo.AppraisalQuestions", "GeneralQuestionId");
            CreateIndex("dbo.AppraisalQuestions", "QuestionId");
        }
    }
}
