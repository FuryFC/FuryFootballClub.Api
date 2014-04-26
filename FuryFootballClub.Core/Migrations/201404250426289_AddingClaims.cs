namespace FuryFootballClub.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingClaims : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClaim", "User_Id", "dbo.User");
            DropIndex("dbo.UserClaim", new[] { "User_Id" });
            DropTable("dbo.UserClaim");
        }
    }
}
