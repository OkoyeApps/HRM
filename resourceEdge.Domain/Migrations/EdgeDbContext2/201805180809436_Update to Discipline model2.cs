namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetoDisciplinemodel2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DisciplinaryIncidents", name: "ConsequenceId", newName: "CorrectiveActionId");
            RenameIndex(table: "dbo.DisciplinaryIncidents", name: "IX_ConsequenceId", newName: "IX_CorrectiveActionId");
            DropColumn("dbo.DisciplinaryIncidents", "CorrectiveAction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisciplinaryIncidents", "CorrectiveAction", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.DisciplinaryIncidents", name: "IX_CorrectiveActionId", newName: "IX_ConsequenceId");
            RenameColumn(table: "dbo.DisciplinaryIncidents", name: "CorrectiveActionId", newName: "ConsequenceId");
        }
    }
}
