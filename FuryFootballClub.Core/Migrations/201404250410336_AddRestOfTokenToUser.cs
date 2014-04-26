namespace FuryFootballClub.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRestOfTokenToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "RefreshToken", c => c.String());
            AddColumn("dbo.User", "TokenScope", c => c.String());
            AddColumn("dbo.User", "TokenType", c => c.String());
            AddColumn("dbo.User", "TokenIssued", c => c.DateTime());
            AddColumn("dbo.User", "TokenExpiresInSeconds", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "TokenExpiresInSeconds");
            DropColumn("dbo.User", "TokenIssued");
            DropColumn("dbo.User", "TokenType");
            DropColumn("dbo.User", "TokenScope");
            DropColumn("dbo.User", "RefreshToken");
        }
    }
}
