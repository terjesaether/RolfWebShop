using RolfWebShop.Models;
using System.Collections.Generic;

namespace RolfWebShop.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}