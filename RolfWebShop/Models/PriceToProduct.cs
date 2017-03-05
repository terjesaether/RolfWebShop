using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolfWebShop.Models
{
    public class PriceToProduct
    {
        public int PriceToProductId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }

    //public enum Unit
    //{
    //    stk = 1,
    //    kg = 2
    //}
}