namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidategroupIdadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "GroupId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "GroupId");
        }
    }
}
