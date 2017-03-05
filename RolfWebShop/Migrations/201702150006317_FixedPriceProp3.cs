namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedPriceProp3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "MyProperty", c => c.Int(nullable: false));
        }
    }
}
