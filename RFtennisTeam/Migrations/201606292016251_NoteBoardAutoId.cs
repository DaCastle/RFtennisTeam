namespace RFtennisTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteBoardAutoId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.NoteBoard");
            AlterColumn("dbo.NoteBoard", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.NoteBoard", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.NoteBoard");
            AlterColumn("dbo.NoteBoard", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.NoteBoard", "Id");
        }
    }
}
