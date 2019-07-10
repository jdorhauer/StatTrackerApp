namespace StatTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPKAndPPPercent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamStat", "PowerPlayPercentage", c => c.Double(nullable: false));
            AddColumn("dbo.TeamStat", "PenaltyKillPercentage", c => c.Double(nullable: false));
            AlterColumn("dbo.TeamStat", "PowerPlays", c => c.Double(nullable: false));
            AlterColumn("dbo.TeamStat", "PowerPlayGoals", c => c.Double(nullable: false));
            AlterColumn("dbo.TeamStat", "PenaltyKills", c => c.Double(nullable: false));
            AlterColumn("dbo.TeamStat", "PenaltyKillGoalsAgainst", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeamStat", "PenaltyKillGoalsAgainst", c => c.Int(nullable: false));
            AlterColumn("dbo.TeamStat", "PenaltyKills", c => c.Int(nullable: false));
            AlterColumn("dbo.TeamStat", "PowerPlayGoals", c => c.Int(nullable: false));
            AlterColumn("dbo.TeamStat", "PowerPlays", c => c.Int(nullable: false));
            DropColumn("dbo.TeamStat", "PenaltyKillPercentage");
            DropColumn("dbo.TeamStat", "PowerPlayPercentage");
        }
    }
}
