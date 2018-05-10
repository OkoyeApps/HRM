namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetoAppraisalInitialization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppraisalInitializations", "AppraisalModeId", c => c.Int(nullable: false, defaultValue:1));
            AddColumn("dbo.AppraisalInitializations", "AppraisalPeriodId", c => c.Int(nullable: false, defaultValue: 1));
            AddColumn("dbo.AppraisalInitializations", "AppraisalRatingId", c => c.Int(nullable: false, defaultValue: 1));
            AddColumn("dbo.AppraisalInitializations", "AppraisalStatusId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.AppraisalInitializations", "AppraisalModeId");
            CreateIndex("dbo.AppraisalInitializations", "AppraisalPeriodId");
            CreateIndex("dbo.AppraisalInitializations", "AppraisalRatingId");
            CreateIndex("dbo.AppraisalInitializations", "AppraisalStatusId");
            AddForeignKey("dbo.AppraisalInitializations", "AppraisalModeId", "dbo.AppraisalModes", "Id");
            AddForeignKey("dbo.AppraisalInitializations", "AppraisalPeriodId", "dbo.AppraisalPeriods", "Id");
            AddForeignKey("dbo.AppraisalInitializations", "AppraisalRatingId", "dbo.AppraisalRatings", "Id");
            AddForeignKey("dbo.AppraisalInitializations", "AppraisalStatusId", "dbo.AppraisalStatus", "Id");
            DropColumn("dbo.AppraisalInitializations", "AppraisalMode");
            DropColumn("dbo.AppraisalInitializations", "Period");
            DropColumn("dbo.AppraisalInitializations", "RatingType");
            DropColumn("dbo.AppraisalInitializations", "AppraisalStatus");
        }

        public override void Down()
        {
            AddColumn("dbo.AppraisalInitializations", "AppraisalStatus", c => c.Int(nullable: false));
            AddColumn("dbo.AppraisalInitializations", "RatingType", c => c.String());
            AddColumn("dbo.AppraisalInitializations", "Period", c => c.Int(nullable: false));
            AddColumn("dbo.AppraisalInitializations", "AppraisalMode", c => c.Int(nullable: false));
            DropForeignKey("dbo.AppraisalInitializations", "AppraisalStatusId", "dbo.AppraisalStatus");
            DropForeignKey("dbo.AppraisalInitializations", "AppraisalRatingId", "dbo.AppraisalRatings");
            DropForeignKey("dbo.AppraisalInitializations", "AppraisalPeriodId", "dbo.AppraisalPeriods");
            DropForeignKey("dbo.AppraisalInitializations", "AppraisalModeId", "dbo.AppraisalModes");
            DropIndex("dbo.AppraisalInitializations", new[] { "AppraisalStatusId" });
            DropIndex("dbo.AppraisalInitializations", new[] { "AppraisalRatingId" });
            DropIndex("dbo.AppraisalInitializations", new[] { "AppraisalPeriodId" });
            DropIndex("dbo.AppraisalInitializations", new[] { "AppraisalModeId" });
            DropColumn("dbo.AppraisalInitializations", "AppraisalStatusId");
            DropColumn("dbo.AppraisalInitializations", "AppraisalRatingId");
            DropColumn("dbo.AppraisalInitializations", "AppraisalPeriodId");
            DropColumn("dbo.AppraisalInitializations", "AppraisalModeId");
        }
    }
}
