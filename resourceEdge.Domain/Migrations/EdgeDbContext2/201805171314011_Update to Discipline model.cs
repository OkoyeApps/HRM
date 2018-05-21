namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetoDisciplinemodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DisciplinaryIncidents", "RaisedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DisciplinaryIncidents", "RaisedBy", c => c.Int(nullable: false));
        }
    }
}
