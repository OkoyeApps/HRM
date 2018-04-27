namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateforRequisitionCandidateDetails2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppraisalResults", "AppraisalInitializationId", c => c.Int(nullable: false));
            CreateIndex("dbo.AppraisalResults", "AppraisalInitializationId");
            AddForeignKey("dbo.AppraisalResults", "AppraisalInitializationId", "dbo.AppraisalInitializations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppraisalResults", "AppraisalInitializationId", "dbo.AppraisalInitializations");
            DropIndex("dbo.AppraisalResults", new[] { "AppraisalInitializationId" });
            DropColumn("dbo.AppraisalResults", "AppraisalInitializationId");
        }
    }
}
