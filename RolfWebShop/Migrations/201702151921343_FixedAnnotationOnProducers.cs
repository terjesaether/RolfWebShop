namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedAnnotationOnProducers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Producers", "Name", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Producers", "Name", c => c.String());
        }
    }
}
