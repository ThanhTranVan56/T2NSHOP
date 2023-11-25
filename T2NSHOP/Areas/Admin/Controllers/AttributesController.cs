using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;

namespace T2NSHOP.Areas.Admin.Controllers
{
    public class AttributesController : Controller
    {
        // GET: Admin/Attributes
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int id)
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var items = db.ProductAttris.Where(x => x.ProductId == id).ToList();
            if (items == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tile = db.Products.Find(id).Title;
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<ProductAttri> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                {
                    item.Alias = T2NSHOP.Common.Filter.FilterChar(item.Color);
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                return View(model);
            }
            return View(model);
        }
    }
}