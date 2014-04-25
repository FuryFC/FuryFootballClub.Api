namespace FuryFootballClub.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchFixture",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Competition = c.String(),
                        HomeTeam = c.String(),
                        AwayTeam = c.String(),
                        Field = c.String(),
                        MatchTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MatchFixture");
        }
    }
}
