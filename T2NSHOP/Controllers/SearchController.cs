using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;

namespace T2NSHOP.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Search
        public ActionResult Index(string Searchtext)
        {
            if (!string.IsNullOrEmpty(Searchtext))
            {
                IEnumerable<Product> items = db.Products.ToList();
                items = items.Where(x => x.Title.Contains(Searchtext) || x.Alias.Contains(Searchtext));
                return View(items);
            }
            return View();
        }

        public ActionResult SearchProductCategory(string alias, int id, string Searchtext)
        {

            if (!string.IsNullOrEmpty(Searchtext))
            {
                if (id > 0)
                {
                    IEnumerable<Product> items = db.Products.Where(x => x.ProductCategoryID == id).ToList();
                    items = items.Where(x => x.Title.Contains(Searchtext) || x.Alias.Contains(Searchtext));
                    var cate = db.ProductCategories.Find(id);
                    if (cate != null)
                    {
                        ViewBag.CateName = cate.Titel;
                    }
                    ViewBag.CateId = id;
                    return View(items);
                }
            }
            if (id > 0)
            {
                var cate = db.ProductCategories.Find(id);
                if (cate != null)
                {
                    ViewBag.CateName = cate.Titel;
                }
                ViewBag.CateId = id;
            }
            return View();
        }

    }
}