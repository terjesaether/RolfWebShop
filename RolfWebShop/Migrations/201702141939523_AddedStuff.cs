namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceToProducts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PriceToProducts", "Unit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceToProducts", "Unit");
            DropColumn("dbo.PriceToProducts", "Price");
        }
    }
}
