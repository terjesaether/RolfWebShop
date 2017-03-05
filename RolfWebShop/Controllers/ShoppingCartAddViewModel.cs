namespace RolfWebShop.Controllers
{
    internal class ShoppingCartAddViewModel
    {
        public int CartCount { get; set; }
        public decimal CartTotal { get; set; }

        public int ItemCount { get; set; }
        public string Message { get; set; }
    }
}