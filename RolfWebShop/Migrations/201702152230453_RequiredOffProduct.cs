namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredOffProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "Producer_ProducerId", "dbo.Producers");
            DropIndex("dbo.Products", new[] { "Category_CategoryId" });
            DropIndex("dbo.Products", new[] { "Producer_ProducerId" });
            AlterColumn("dbo.Products", "Category_CategoryId", c => c.Int());
            AlterColumn("dbo.Products", "Producer_ProducerId", c => c.Int());
            CreateIndex("dbo.Products", "Category_CategoryId");
            CreateIndex("dbo.Products", "Producer_ProducerId");
            AddForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Products", "Producer_ProducerId", "dbo.Producers", "ProducerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Producer_ProducerId", "dbo.Producers");
            DropForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Producer_ProducerId" });
            DropIndex("dbo.Products", new[] { "Category_CategoryId" });
            AlterColumn("dbo.Products", "Producer_ProducerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Category_CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "Producer_ProducerId");
            CreateIndex("dbo.Products", "Category_CategoryId");
            AddForeignKey("dbo.Products", "Producer_ProducerId", "dbo.Producers", "ProducerId", cascadeDelete: true);
            AddForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
