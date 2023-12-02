using System.Linq;
using System.Web.Mvc;
using T2NSHOP.Models;

namespace T2NSHOP.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = db.Categories.OrderBy(x => x.Possition).ToList();
            return PartialView("_MenuTop", items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuLeft", items);
        }
        public ActionResult MenuLeftSearch(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuLeftSearch", items);
        }
        public ActionResult MenuArrivals()
        {
            var items = db.ProductCategories.Take(7).ToList();
            return PartialView("_MenuArrivals", items);
        }
    }
}