using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;

namespace T2NSHOP.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_Subcrice()
        {
            return PartialView();
        }
        public ActionResult Partial_ImageUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var Prouser = db.ProfileCustomers.FirstOrDefault(x => x.UserId == userId);
                if (Prouser != null && Prouser.Image != null)
                {
                    ViewBag.Image = Prouser.Image;
                    ViewBag.Name = Prouser.Name;
                }

                return PartialView();
            }
            return PartialView();

        }
        public ActionResult Partial_ProduceCategorySearch()
        {
            var items = db.ProductCategories.ToList();
            if (items != null)
            {
                return PartialView(items);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Subscribe(Subscribe req)
        {
            if (ModelState.IsValid)
            {
                db.Subscribes.Add(new Subscribe { Email = req.Email, CreateDate = DateTime.Now });
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return View("Partial_Subcrice", req);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}