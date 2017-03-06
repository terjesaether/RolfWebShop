using RolfWebShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RolfWebShop.ViewModels;

namespace RolfWebShop.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FileOperations fileOps = new FileOperations();
        private string filePath = "/Images/Products/";

        // GET: StoreManager
        //[Route("StoreManager/Index")]
        public ActionResult Main()
        {
            var vm = new MainManagerViewModel();

            return View(vm);
        }

        public ActionResult ProductsIndex()
        {
            return View(db.Products.ToList());
        }
        // GET: StoreManager/Details/5
        public async Task<ActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Details
        public ActionResult ProducerDetails(int id)
        {
            var producer = db.Producers.Find(id);
            if (producer != null) return View(producer);
            return RedirectToAction("Main");
        }

        // GET: StoreManager/Create
        public ActionResult CreateProduct()
        {
            var vm = new CreateProductViewModel();
            vm.Product.Price = 99;
            //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            //ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name");
            return View(vm);
        }

        // POST: StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(CreateProductViewModel vm, string CategoryId, string ProducerId, string Price, string UnitId)
        {

            List<string> fileNames = new List<string>();
            var product = vm.Product;

            var httpRequest = System.Web.HttpContext.Current.Request;
            bool isSaved = true;
            CategoryId = Request.Form["Product.Category"];
            ProducerId = Request.Form["Product.Producer"];
            UnitId = Request.Form["Product.UnitId"];
            if (string.IsNullOrEmpty(UnitId))
            {
                UnitId = "1";
            }
            var newUnit = db.Units.Find(int.Parse(UnitId));

            product.Producer = db.Producers.Find(int.Parse(ProducerId));
            product.Category = db.Categories.Find(int.Parse(CategoryId));

            product.PriceToProducts = new List<PriceToProduct>
                    {
                        new PriceToProduct
                        {
                            Price = product.Price,
                            Date = DateTime.Now,
                            Product = product,
                            Unit = newUnit
                         }
                    };

            if (newUnit != null) product.Unit = newUnit.Name;

            string fileName = product.ProductName.Replace(" ", "_") + ".jpg";

            if (httpRequest.Files[0].ContentLength > 0)
            {
                isSaved = fileOps.SaveUploadedFile(httpRequest, filePath, fileName);
                product.MainImgUrl = filePath + fileName;
            }


            if (isSaved)
            {
                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Main", "StoreManager");
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            return RedirectToAction("Main");
        }

        // GET: StoreManager/Edit/5
        public ActionResult EditProduct(int? id = 1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.Category.CategoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name", product.Producer.ProducerId);
            PriceToProduct priceToProduct = product.PriceToProducts.LastOrDefault();
            if (priceToProduct != null)
                ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Name", priceToProduct.Unit.UnitId);

            return View(product);
        }

        // POST: StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product updatedProduct, string CategoryId, string ProducerId, string Price, string UnitId)
        {

            if (CategoryId == "")
            {
                CategoryId = "1";
            }
            if (ProducerId == "")
            {
                ProducerId = "1";
            }

            if (ModelState.IsValid)
            {
                var httpRequest = System.Web.HttpContext.Current.Request;
                bool isSaved = true;

                if (string.IsNullOrEmpty(UnitId))
                {
                    UnitId = "1";
                }

                var newUnit = db.Units.Find(int.Parse(UnitId));

                Product product = db.Products.Find(updatedProduct.ProductId);
                if (product != null)
                {
                    product.ProductName = updatedProduct.ProductName;
                    product.PriceToProducts = updatedProduct.PriceToProducts;
                    product.About = updatedProduct.About;
                    product.Offer = updatedProduct.Offer;

                    product.Producer = db.Producers.Find(int.Parse(ProducerId));
                    product.Category = db.Categories.Find(int.Parse(CategoryId));


                    PriceToProduct priceToProduct = product.PriceToProducts.LastOrDefault();
                    if (priceToProduct != null && updatedProduct.Price != priceToProduct.Price)
                    {
                        product.PriceToProducts.Add
                        (
                            new PriceToProduct
                            {
                                Price = updatedProduct.Price,
                                Date = DateTime.Now,
                                Product = product,
                                ProductId = product.ProductId,
                                Unit = newUnit
                            }
                        );
                    }


                    product.Price = decimal.Parse(Price);
                    if (newUnit != null) product.Unit = newUnit.Name;
                    string fileName = product.ProductName.Replace(" ", "_") + ".jpg";

                    if (httpRequest.Files[0].ContentLength > 0)
                    {
                        isSaved = fileOps.SaveUploadedFile(httpRequest, filePath, fileName);
                        product.MainImgUrl = filePath + fileName;
                    }

                    if (isSaved)
                    {
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Main");
                    }
                }
            }
            return View(updatedProduct);
        }


        public ActionResult EditCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Main");
        }

        [HttpGet]
        public ActionResult EditProducer(int id)
        {
            var producer = db.Producers.Find(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        [HttpPost]
        public ActionResult EditProducer(Producer producer)
        {

            if (ModelState.IsValid)
            {
                db.Entry(producer).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Main");
        }

        [HttpGet]
        public ActionResult CreateProducer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProducer(Producer producer)
        {
            if (ModelState.IsValid)
            {
                db.Producers.Add(producer);
                db.SaveChanges();
            }
            return RedirectToAction("Main");
        }

        public ActionResult OrdersIndex()
        {

            return View(db.OrderDetails.ToList());
        }

        public ActionResult ProducersIndex()
        {
            return View(db.Producers.ToList());
        }

        public ActionResult CategoriesIndex()
        {
            return View(db.Categories.ToList());
        }

        [HttpGet]
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            foreach (var item in db.Products.ToList())
            {
                if (item.Category.CategoryId == id.Value)
                {
                    ViewBag.ErrorMessage = "Kategori kan ikke slettes. " + item.ProductName + " er i den kategorien!";
                    var categories = db.Categories.ToList();
                    return View("CategoriesIndex", categories);
                }
            }

            var category = db.Categories.Find(id.Value);
            db.Entry(category).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Main");
        }

        // GET: StoreManager/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Product product = await db.Products.FindAsync(id);
            try
            {
                foreach (var item in db.PriceToProducts)
                {
                    if (product != null && product.ProductId == item.ProductId)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }

                if (product != null) db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Json(new { status = "success", message = "Det gikk fint! Slettet!" });
                //return RedirectToAction("Main");
            }
            catch (Exception)
            {
                return Json(new { status = "error", message = "Fikk ikke slettet produkt!" });
            }

        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);

            foreach (var item in db.PriceToProducts)
            {
                if (product.ProductId == item.ProductId)
                {
                    db.Entry(item).State = EntityState.Deleted;
                }
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Main");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}