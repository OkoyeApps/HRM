namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class interviewUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Interviews", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Interviews", "ModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Interviews", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Interviews", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
