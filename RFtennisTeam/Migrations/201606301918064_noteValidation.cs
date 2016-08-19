namespace RFtennisTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noteValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NoteBoard", "note", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NoteBoard", "note", c => c.String());
        }
    }
}
