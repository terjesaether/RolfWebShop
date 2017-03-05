using RolfWebShop.Models;
using System.Linq;
using System.Web.Mvc;

namespace RolfWebShop.Controllers
{
    public class StoreController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        // GET: Browse
        public ActionResult Browse(int? id)
        {
            //var products = _db.Categories.Include("Products").Single(s => s.CategoryId == id.Value);
            //var products = _db.Products
            //.Where(p => p.Category.CategoryId == id.Value)
            //.ToList();

            var category = _db.Categories.Include("Products").Where(c => c.CategoryId == id.Value).Single();

            ViewBag.Category = _db.Categories.Find(id.Value).Name;
            return View(category);
        }

        // GET: Details
        public ActionResult Details()
        {
            return View();
        }

        // GET: Categories
        public ActionResult Categories()
        {
            var categories = _db.Categories.ToList();
            return View(categories);
        }
    }
}