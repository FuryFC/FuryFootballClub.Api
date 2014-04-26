namespace FuryFootballClub.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserClaimKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserClaim", "User_Id", "dbo.User");
            DropIndex("dbo.UserClaim", new[] { "User_Id" });
            RenameColumn(table: "dbo.UserClaim", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.UserClaim", "UserId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.UserClaim");
            AddPrimaryKey("dbo.UserClaim", new[] { "UserId", "Key" });
            CreateIndex("dbo.UserClaim", "UserId");
            AddForeignKey("dbo.UserClaim", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropPrimaryKey("dbo.UserClaim");
            AddPrimaryKey("dbo.UserClaim", "Key");
            AlterColumn("dbo.UserClaim", "UserId", c => c.Guid());
            RenameColumn(table: "dbo.UserClaim", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.UserClaim", "User_Id");
            AddForeignKey("dbo.UserClaim", "User_Id", "dbo.User", "Id");
        }
    }
}
