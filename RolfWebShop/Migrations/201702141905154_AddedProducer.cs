namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProducer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        ProducerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Mail = c.String(),
                        ContactPerson = c.String(),
                        Comment = c.String(),
                        About = c.String(),
                    })
                .PrimaryKey(t => t.ProducerId);
            
            AddColumn("dbo.Products", "Producer_ProducerId", c => c.Int());
            CreateIndex("dbo.Products", "Producer_ProducerId");
            AddForeignKey("dbo.Products", "Producer_ProducerId", "dbo.Producers", "ProducerId");
            DropColumn("dbo.Products", "Producer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Producer", c => c.String(nullable: false));
            DropForeignKey("dbo.Products", "Producer_ProducerId", "dbo.Producers");
            DropIndex("dbo.Products", new[] { "Producer_ProducerId" });
            DropColumn("dbo.Products", "Producer_ProducerId");
            DropTable("dbo.Producers");
        }
    }
}
