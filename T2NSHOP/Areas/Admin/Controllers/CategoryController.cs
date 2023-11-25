using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;

namespace T2NSHOP.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }

            IEnumerable<Categogy> items = db.Categories.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.Alias.Contains(Searchtext) || x.Titel.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.Page = pageIndex;
            ViewBag.PageSize = pageSize;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Categogy model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifedrDate = DateTime.Now;
                model.Alias = T2NSHOP.Common.Filter.FilterChar(model.Titel);
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = db.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categogy model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(model);
                model.ModifedrDate = DateTime.Now;
                model.Alias = T2NSHOP.Common.Filter.FilterChar(model.Titel);
                db.Entry(model).Property(x => x.Titel).IsModified = true;
                db.Entry(model).Property(x => x.Decription).IsModified = true;
                db.Entry(model).Property(x => x.Alias).IsModified = true;
                db.Entry(model).Property(x => x.SeoDecription).IsModified = true;
                db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(model).Property(x => x.SeoTitel).IsModified = true;
                db.Entry(model).Property(x => x.Possition).IsModified = true;
                db.Entry(model).Property(x => x.ModifedrDate).IsModified = true;
                db.Entry(model).Property(x => x.ModifierBy).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Categories.Find(id);
            if (item != null)
            {
                //var DeleteItem = db.Categories.Attach(item);
                db.Categories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Categories.Find(Convert.ToInt32(item));
                        db.Categories.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult IsActive(int id)
        {
            var item = db.Categories.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
    }
}