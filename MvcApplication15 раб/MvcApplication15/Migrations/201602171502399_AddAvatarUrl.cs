namespace MvcApplication15.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvatarUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDatas", "AvatarUrl", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.UserDatas", "AvatarUrl");
        } 
    }
}
