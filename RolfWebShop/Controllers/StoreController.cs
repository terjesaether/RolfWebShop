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

            var category = _db.Categories.Include("Products").Single(c => c.CategoryId == id.Value);

            if (id != null)
            {
                var find = _db.Categories.Find(id.Value);
                if (find != null) ViewBag.Category = find.Name;
            }
            return View(category);
        }

        // GET: Details
        public ActionResult ProducerDetails(int id)
        {
            var producer = _db.Producers.Find(id);
            if (producer != null) return View(producer);
            return RedirectToAction("Index");
        }

        // GET: Categories
        public ActionResult Categories()
        {
            var categories = _db.Categories.ToList();
            return View(categories);
        }
    }
}