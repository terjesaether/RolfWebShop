namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedPriceProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MyProperty", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "MyProperty");
        }
    }
}
