namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequistiongroupIdadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisitions", "groupId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requisitions", "groupId");
        }
    }
}
