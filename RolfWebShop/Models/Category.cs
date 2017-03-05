using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RolfWebShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name = "Kategorinavn")]
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}