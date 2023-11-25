using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2NSHOP.Models;

namespace T2NSHOP.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(int? id)
        {
            var items = db.Products.ToList();
            if (id != null)
            {
                items = items.Where(x => x.ProductCategoryID == id).ToList();
            }
            return View(items);
        }
        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }

            return View(item);
        }
        public ActionResult Partial_DetailAttri(int id)
        {
            var item = db.Products.Find(id);
            var items = db.ProductAttris.Where(x => x.ProductId == id).ToList();
            ViewBag.ViewCount = item.ViewCount;
            ViewBag.Id = id;
            ViewBag.Price = item.Price;
            return PartialView(items);
        }
        public ActionResult Partial_DetailImage(int id)
        {
            var items = db.Products.SingleOrDefault(x => x.Id == id);
            return PartialView(items);
        }
        public ActionResult Partial_ProductCategory(int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryID == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Titel;
            }
            ViewBag.CateId = id;
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult ShowQuantity(int id, string color, string size)
        {
            if (color == "" && size == "")
            {
                var cart = db.ProductAttris.Where(x => x.ProductId == id).Select(p => p.Quantity).ToList();
                return Json(new { quantity = cart.Sum() });
            }
            else
            {
                if (color != "" && size == "")
                {
                    var cart = db.ProductAttris.Where(x => x.ProductId == id && x.Alias == color).Select(p => p.Quantity).ToList();
                    return Json(new { quantity = cart.Sum() });
                }
                else
                {
                    if (color == "" && size != "")
                    {
                        var cart = db.ProductAttris.Where(x => x.ProductId == id && x.Size == size).Select(p => p.Quantity).ToList();
                        return Json(new { quantity = cart.Sum() });
                    }
                    else
                    {
                        var cart = db.ProductAttris.Where(x => x.ProductId == id && x.Size == size && x.Alias == color).Select(p => p.Quantity).ToList();
                        return Json(new { quantity = cart.Sum() });
                    }
                }
            }
        }
        [HttpPost]
        public ActionResult GetSizesByColor(string color, int id)
        {
            var sizes = db.ProductAttris.Where(p => p.ProductId == id && p.Alias == color && p.Quantity > 0)
                .Select(p => p.Size)
                .Distinct()
                .ToList();
            return Json(new { Sizes = sizes });
        }

        public ActionResult ProductCategory(string alias, int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryID == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Titel;
            }
            ViewBag.CateId = id;
            return View(items);
        }
        public ActionResult Partial_ItemByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(15).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSale()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
    }
}