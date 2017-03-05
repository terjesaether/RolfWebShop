namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedUnit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Unit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Unit");
        }
    }
}
