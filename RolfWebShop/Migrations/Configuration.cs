namespace RolfWebShop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RolfWebShop.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            var spekemat = new Category { Name = "Spekemat" };
            var fenalaar = new Category { Name = "Fenalår" };
            var gaver = new Category { Name = "Gaver" };
            var polser = new Category { Name = "Pølser" };

            context.Categories.AddOrUpdate(
              p => p.Name,
              spekemat,
              fenalaar,
              gaver,
              polser

            );

            context.Products.AddOrUpdate(
              p => p.ProductName,
              new Product { ProductName = "Fenalår fra Valdres", Producer = new Producer { Name = "Valdres Fena A/S" }, About = "Godt Fenalår fra Valdres" }
            );

        }
    }
}
