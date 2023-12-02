using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;

namespace T2NSHOP.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string Searchtext, int? page)
        {
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);

            ViewBag.Page = pageIndex;
            ViewBag.PageSize = pageSize;
            return View(items);
        }
        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Titel");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<string> icolor, List<int> rDefault, List<string> SeColor, List<string> SeSize, List<int> SeQuantity)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true,
                                Color = icolor[i]
                            });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false,
                                Color = icolor[i]
                            });
                        }
                    }
                }
                if (SeColor != null && SeColor.Count > 0)
                {
                    for (int i = 0; i < SeColor.Count; i++)
                    {

                        model.ProductAttris.Add(new ProductAttri
                        {
                            ProductId = model.Id,
                            Color = SeColor[i],
                            Alias = SeColor[i],
                            Size = SeSize[i],
                            Quantity = SeQuantity[i],
                        });

                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifedrDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.SeoTitel = model.Title;
                }

                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = T2NSHOP.Common.Filter.FilterChar(model.Title);
                }
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Titel");
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Titel");
            var items = db.Products.Find(id);
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var imgt = db.ProductImages.Where(x => x.ProductId == model.Id).ToList();
                foreach (var item in imgt)
                {
                    if (item.IsDefault == true)
                        model.Image = item.Image;
                }
                model.ModifedrDate = DateTime.Now;
                model.Alias = T2NSHOP.Common.Filter.FilterChar(model.Title);
                db.Products.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isSale = item.IsSale });
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
                        var obj = db.Products.Find(Convert.ToInt32(item));
                        db.Products.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }

}