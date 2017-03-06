using RolfWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RolfWebShop.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        const string PromoCode = "FREE";
        //
        // GET: /Checkout/AddressAndPayment
        [HttpGet]
        public ActionResult AddressAndPayment()
        {
            var order = storeDB.Orders.FirstOrDefault(o => o.Username == User.Identity.Name);

            if (order != null)
            {
                return View(order);
            }
            return View();

        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                //if (string.Equals(values["PromoCode"], PromoCode,
                //    StringComparison.OrdinalIgnoreCase) == false)
                //{
                //    return View(order);
                //}
                //else
                //{
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                //Save Order
                storeDB.Orders.Add(order);
                storeDB.SaveChanges();
                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete",
                    new { id = order.OrderId });
                //}
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                // TODO-sende mail
                //SendMail(id);
                try
                {
                    SendSimpleMail(id);
                }
                catch (Exception)
                {
                    return View("MailError");
                }

                //var cartList = new List<OrderDetail>();
                //foreach (var item in storeDB.OrderDetails)
                //{
                //    if (item.OrderId == id)
                //    {
                //        cartList.Add(item);
                //    }
                //}

                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        private async Task<bool> SendMail(int orderId)
        {
            var fromName = "Rolfs Webshop";
            var fromEmail = "Rolfs Webshop";
            var cart = new List<OrderDetail>();
            foreach (var item in storeDB.OrderDetails)
            {
                if (item.OrderId == orderId)
                {
                    cart.Add(item);
                }
            }
            var order = "";

            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("recipient@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("sender@outlook.com");  // replace with valid value
            message.Subject = "Your email subject";
            message.Body = string.Format(body, fromName, fromEmail, order);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "user@outlook.com",  // replace with valid value
                    Password = "password"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                return true;
            }

            //return await false;
        }

        private void SendSimpleMail(int orderId)
        {
            var body = "Ny ordre har kommet inn. Ordre nummer " + orderId.ToString();
            var message = new MailMessage();
            message.To.Add(new MailAddress("terjesather@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("terje@sonicrage.net");  // replace with valid value

            message.Subject = "Ny Ordre!";
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "terje@sonicrage.net",  // replace with valid value
                    Password = "qvtr3xaztQ6"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "sonicrage.net";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.Send(message);

            }
        }


    }
}