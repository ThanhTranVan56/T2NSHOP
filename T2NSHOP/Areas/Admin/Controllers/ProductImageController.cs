using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;

namespace T2NSHOP.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            List<string> colorList = db.ProductImages
                .Where(x => x.ProductId == id && !string.IsNullOrEmpty(x.Color))
                .Select(x => x.Color)
                .Distinct()
                .ToList();
            ViewBag.colorList = colorList;
            var items = db.ProductImages.Where(x => x.ProductId == id).ToList();
            if(items != null)
            {
                return View(items);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddImage(int productId, string color, string url)
        {
            db.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Color = color,
                Image = url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImages.Find(id);
            db.ProductImages.Remove(item);
            db.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult DeleteAll(int id)
        {
            var items = db.ProductImages.Where(x => x.ProductId == id).ToList();
            foreach (var item in items)
            {
                db.ProductImages.Remove(item);
                db.SaveChanges();
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult IsDefault(int Prid, int id)
        {
            var items = db.ProductImages.Where(x => x.ProductId == Prid).ToList();
            if (items != null)
            {
                var item = db.ProductImages.Find(id);
                if (item.IsDefault == true)
                {
                    item.IsDefault = false;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    item.IsDefault = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    foreach (var itno in items)
                    {
                        if (itno.Id != item.Id && itno.IsDefault == true)
                        {
                            itno.IsDefault = false;
                            db.Entry(itno).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                    }
                    return Json(new { success = true });
                }
            }
            else
                return Json(new { success = false });
        }
    }
}