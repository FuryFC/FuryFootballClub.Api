namespace FuryFootballClub.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NothingHopefully : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "AccessToken", c => c.String());
            AlterColumn("dbo.User", "PrimaryEmail", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "PrimaryEmail", c => c.String(maxLength: 4000));
            AlterColumn("dbo.User", "AccessToken", c => c.String(maxLength: 4000));
        }
    }
}
