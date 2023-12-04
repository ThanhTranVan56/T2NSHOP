using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;
using T2NSHOP.Common;

namespace T2NSHOP.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Search

        public ActionResult Index()
        {  
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Searchtext)
        {
            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchText = RemoveSpecialCharacter.RemoveSpecialCharacters(Searchtext);
                if (searchText != Searchtext)
                {
                    ViewBag.Error = "Vui lòng không nhập kí tự đặc biệt";
                    return View();
                }
                else
                {
                    IEnumerable<Product> items = db.Products.ToList();
                    items = items.Where(x => x.Title.Contains(searchText) || x.Alias.Contains(searchText));
                    ViewBag.Notification = "Kết quả tìm kiếm cho từ khoá '" + searchText + "' : " + items.Count() + " giá trị";
                    return View(items);
                }
            }
            return View();
        }
        
        public ActionResult SearchProductCategory(string alias, int id)
        {
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Titel;
            }
            ViewBag.CateId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchProductCategory(int id, string Searchtext)
        {
            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchText = RemoveSpecialCharacter.RemoveSpecialCharacters(Searchtext);
                if (searchText != Searchtext)
                {
                    ViewBag.Error = "Vui lòng không nhập kí tự đặc biệt";
                    return View();
                }
                else
                {
                    IEnumerable<Product> items = db.Products.Where(x => x.ProductCategoryID == id).ToList();
                    items = items.Where(x => x.Title.Contains(searchText) || x.Alias.Contains(searchText));
                    var cate = db.ProductCategories.Find(id);
                    if (cate != null)
                    {
                        ViewBag.CateName = cate.Titel;
                        ViewBag.Notification = "Kết quả tìm kiếm cho từ khoá '" + searchText + "' : " + items.Count() + " giá trị";
                    }
                    ViewBag.CateId = id;
                    return View(items);
                }
            }
            else
            {
                return View();
            }
        }

    }
}