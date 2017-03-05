namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUnitsDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UnitId);
            
            AddColumn("dbo.PriceToProducts", "Unit_UnitId", c => c.Int());
            CreateIndex("dbo.PriceToProducts", "Unit_UnitId");
            AddForeignKey("dbo.PriceToProducts", "Unit_UnitId", "dbo.Units", "UnitId");
            DropColumn("dbo.PriceToProducts", "Unit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceToProducts", "Unit", c => c.Int(nullable: false));
            DropForeignKey("dbo.PriceToProducts", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.PriceToProducts", new[] { "Unit_UnitId" });
            DropColumn("dbo.PriceToProducts", "Unit_UnitId");
            DropTable("dbo.Units");
        }
    }
}
