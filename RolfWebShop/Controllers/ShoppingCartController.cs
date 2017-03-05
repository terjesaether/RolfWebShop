using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RolfWebShop.Models;
using RolfWebShop.ViewModels;

namespace RolfWebShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(AddToCart addTocart)
        {

            int id = addTocart.id;
            int number = addTocart.number;

            // Retrieve the product from the database
            var addedProduct = storeDB.Products
                .Single(p => p.ProductId == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            for (int i = 0; i < number; i++)
            {
                cart.AddToCart(addedProduct);
            }

            // Display the confirmation message
            var results = new ShoppingCartAddViewModel
            {
                Message = Server.HtmlEncode(addedProduct.ProductName) +
                    " har blitt lagt til.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),

            };

            // Go back to the main store page for more shopping
            //return RedirectToAction("Index");
            return Json(results);
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the product to display confirmation
            //string productName = storeDB.Carts
            // .Single(item => item.ProductId == id).Product.ProductName;

            string productName = storeDB.Carts.Where(i => i.ProductId == id).FirstOrDefault().Product.ProductName;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) +
                    " er fjernet fra handlekurven.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            ViewData["CartTotal"] = cart.GetTotal();
            return PartialView("CartSummary");
        }
    }
}