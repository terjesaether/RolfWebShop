using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RolfWebShop.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required, Display(Name = "Produktnavn"), MaxLength(100)]
        public string ProductName { get; set; }

        //[Required, Display(Name = "Produsent")]
        //public string Producer { get; set; }

        [Display(Name = "Om")]
        public string About { get; set; }

        public string MainImgUrl { get; set; }

        [Required, Display(Name = "Pris")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Display(Name = "Enhet")]
        public string Unit { get; set; }

        //public decimal Price
        //{
        //    get
        //    {

        //        if (PriceToProducts.Count > 0)
        //        {
        //            return PriceToProducts.FindLast(p => p.ProductId == ProductId).Price;
        //        }
        //        return 0;
        //    }
        //}

        public decimal GetPrice()
        {
            return PriceToProducts.FindLast(p => p.ProductId == ProductId).Price;
        }


        [Display(Name = "Tilbud (ja/nei)")]
        public bool Offer { get; set; }

        [Display(Name = "Kategori")]
        public virtual Category Category { get; set; }

        [Display(Name = "Produsent")]
        public virtual Producer Producer { get; set; }

        public virtual List<PriceToProduct> PriceToProducts { get; set; }
    }
}