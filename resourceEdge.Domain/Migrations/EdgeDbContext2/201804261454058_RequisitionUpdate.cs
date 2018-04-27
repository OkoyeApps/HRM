namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisitions", "JobTitleId", c => c.Int());
            DropColumn("dbo.Requisitions", "JobTitle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requisitions", "JobTitle", c => c.Int());
            DropColumn("dbo.Requisitions", "JobTitleId");
        }
    }
}
