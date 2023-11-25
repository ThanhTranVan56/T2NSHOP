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
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(string Searchtext, int? page)
        {
            IEnumerable<Order> items = db.Orders.OrderByDescending(x => x.CreatedDate).ToList();
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.CustomerName.Contains(Searchtext) || x.Code.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;
            return View(items.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }
        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id);
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                if (trangthai == 3)
                {
                    item.StatusPayment = 3;
                }
                else {
                    if (trangthai == 4)
                    {
                        item.StatusPayment = 4;
                    }
                    else 
                    {
                        if(trangthai == 5)
                            item.StatusPayment = 5;
                    }
                }
                
                item.StatusOrder = trangthai;
                db.Entry(item).Property(x => x.StatusOrder).IsModified = true;
                db.Entry(item).Property(x => x.StatusPayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "UnSuccess", Success = false });
        }
    }
}