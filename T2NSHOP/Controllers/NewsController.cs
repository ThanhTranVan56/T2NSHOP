using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;

namespace T2NSHOP.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index(int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = db.News.OrderByDescending(x => x.CreatedDate);
            if (items != null)
            {
                var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                items = items.ToPagedList(pageIndex, pageSize);
                ViewBag.PageSize = pageSize;
                ViewBag.Page = page;
                return View(items);
            }
            return View();
        }
        public ActionResult Detail(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                return View(item);
            }
            return View();
        }
        public ActionResult Partial_News_Home()
        {
            var items = db.News.Take(3).ToList();
            if (items != null)
            {
                return PartialView(items);
            }
            return PartialView();
        }
    }
}