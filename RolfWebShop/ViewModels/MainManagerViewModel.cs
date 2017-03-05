using RolfWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolfWebShop.ViewModels
{
    public class MainManagerViewModel
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public MainManagerViewModel()
        {
            Products = _db.Products.ToList();
            Producers = _db.Producers.ToList();
            Categories = _db.Categories.ToList();
        }

        public List<Product> Products { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> ProducerToProduct { get; set; }



    }
}