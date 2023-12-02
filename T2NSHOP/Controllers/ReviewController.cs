using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;

namespace T2NSHOP.Controllers
{
    public class ReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Partial_Review(int productId)
        {
            ViewBag.ProductId = productId;
            var item = new ReviewProduct();
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    var prouser = db.ProfileCustomers.FirstOrDefault(x => x.UserId == user.Id);
                    item.UserName = prouser.UserName;
                    item.Avata = prouser.Image;
                    item.Email = user.Email;
                    
                    return PartialView(item);
                }
                return PartialView();
            }
            return PartialView();
        }

        public ActionResult Partial_Load_Review(int productId)
        {
            var item = db.ReviewProducts.Where(x => x.ProductId == productId).OrderByDescending(x => x.id).ToList();
            if(item != null)
            {
                ViewBag.Count = item.Count;
                return PartialView(item);
            }
            return PartialView();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostReview(ReviewProduct req)
        {
            if (ModelState.IsValid)
            {
                req.Createdate = DateTime.Now;
                db.ReviewProducts.Add(req);
                db.SaveChanges();
                return Json(new { Success = true});
            }
            return Json(new { Success = false });
        }
    }
}