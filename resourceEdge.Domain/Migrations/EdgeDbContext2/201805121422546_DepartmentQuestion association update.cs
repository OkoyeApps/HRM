namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentQuestionassociationupdate : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AppraisalQuestions", "DepartmentQuestionId");
            AddForeignKey("dbo.AppraisalQuestions", "DepartmentQuestionId", "dbo.GeneralQuestions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppraisalQuestions", "DepartmentQuestionId", "dbo.GeneralQuestions");
            DropIndex("dbo.AppraisalQuestions", new[] { "DepartmentQuestionId" });
        }
    }
}
