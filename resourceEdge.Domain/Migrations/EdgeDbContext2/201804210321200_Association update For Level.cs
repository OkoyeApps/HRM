namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssociationupdateForLevel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Levels", "GroupId");
            AddForeignKey("dbo.Levels", "GroupId", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Levels", "GroupId", "dbo.Groups");
            DropIndex("dbo.Levels", new[] { "GroupId" });
        }
    }
}
