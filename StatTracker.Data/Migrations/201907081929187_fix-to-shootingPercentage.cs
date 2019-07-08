namespace StatTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixtoshootingPercentage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerStat", "ShootingPercentage", c => c.Double(nullable: false));
            AlterColumn("dbo.PlayerStat", "Goals", c => c.Double(nullable: false));
            AlterColumn("dbo.PlayerStat", "Shots", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlayerStat", "Shots", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PlayerStat", "Goals", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PlayerStat", "ShootingPercentage");
        }
    }
}
