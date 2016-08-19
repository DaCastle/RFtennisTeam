namespace RFtennisTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchResultsAutoId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MatchResults");
            AlterColumn("dbo.MatchResults", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MatchResults", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MatchResults");
            AlterColumn("dbo.MatchResults", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MatchResults", "Id");
        }
    }
}
