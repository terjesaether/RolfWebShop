namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainImgUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MainImgUrl", c => c.String());
            DropColumn("dbo.Products", "ImgUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImgUrl", c => c.String());
            DropColumn("dbo.Products", "MainImgUrl");
        }
    }
}
