namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewdetailUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interviews", "CreatedBy", c => c.String());
            AddColumn("dbo.Interviews", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Interviews", "ModifiedBy", c => c.String());
            AddColumn("dbo.Interviews", "ModifiedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interviews", "ModifiedOn");
            DropColumn("dbo.Interviews", "ModifiedBy");
            DropColumn("dbo.Interviews", "CreatedOn");
            DropColumn("dbo.Interviews", "CreatedBy");
        }
    }
}
