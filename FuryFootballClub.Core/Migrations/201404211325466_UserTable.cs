namespace FuryFootballClub.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.User",
                    c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccessToken = c.String(),
                        PrimaryEmail = c.String(),
                        LastLogin = c.DateTime(),
                    })
                    .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
