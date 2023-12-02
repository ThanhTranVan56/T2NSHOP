using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;
using static T2NSHOP.FilterConfig;

namespace T2NSHOP.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Profile
        public ActionResult Index()
        {
            var iuser = User.Identity.GetUserId();
            ViewBag.IdUser = iuser;
            var item = db.ProfileCustomers.FirstOrDefault(x => x.UserId == iuser);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                var profileid = new ProfileCustomer
                {
                    UserId = iuser,
                    DateOfBirth = DateTime.ParseExact("05/06/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    TypePaymentVN = 0
                };
                db.ProfileCustomers.Add(profileid);
                db.SaveChanges();
                var items = db.ProfileCustomers.FirstOrDefault(x => x.UserId == iuser);
                return View(items);
            }
            
        }

        public ActionResult Address()
        {
            var iuser = User.Identity.GetUserId();
            ViewBag.IdUser = iuser;
            var item = db.ProfileCustomers.FirstOrDefault(x => x.UserId == iuser);
            if (item != null)
            {
                return View(item);
            }
            return View();
        }

        public ActionResult Purchase()
        {
            var iuser = User.Identity.GetUserId();
            ViewBag.IdUser = iuser;
            var item = db.ProfileCustomers.FirstOrDefault(x => x.UserId == iuser);
            if (item != null)
            {
                return View(item);
            }
            return View();
        }

        public ActionResult Partial_List_Order()
        {
            var iuser = User.Identity.GetUserId();
            IEnumerable<Order> items = db.Orders.Where(x => x.CustomerId == iuser)
                                                .OrderByDescending(x => x.CreatedDate)
                                                .ToList();
            if (items != null && items.Any())
            {
                return PartialView(items);
            }
            return PartialView();
        }
        public ActionResult Partial_Item_OrderDetail(string code)
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.Code == code).ToList();
            if (cart != null && cart.Any())
            {
                return PartialView(cart);
            }
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public ActionResult AddProfile(string id, string Name, string Gender, DateTime DateOfBirth)
        {
            var checkpro = db.ProfileCustomers.FirstOrDefault(x => x.UserId == id);
            if (checkpro != null)
            {
                checkpro.UserName = Name;
                checkpro.Gender = Gender;
                checkpro.DateOfBirth = DateOfBirth;
                db.Entry(checkpro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });

        }
        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public ActionResult SaveImage(string imageUrl)
        {
            if (imageUrl != null)
            {
                var iuser = User.Identity.GetUserId();
                var checkpro = db.ProfileCustomers.FirstOrDefault(x => x.UserId == iuser);
                checkpro.Image = imageUrl;
                db.Entry(checkpro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public ActionResult UpdateIsDefual(int id)
        {
            if (id != 0)
            {
                var checkAdd = db.AddressCustomers.FirstOrDefault(x => x.Id == id);

                var checkPro = db.AddressCustomers.Where(x => x.ProfileId == checkAdd.ProfileId).ToList();
                foreach (var item in checkPro)
                {
                    item.IsDefault = false;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                checkAdd.IsDefault = true;
                db.Entry(checkAdd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public ActionResult AddAddress(string Id, string Name, string Phone, string Address, string AddressDt)
        {
            var iuser = User.Identity.GetUserId();
            var item = db.ProfileCustomers.FirstOrDefault(x => x.UserId == iuser);
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            if (item != null)
            {
                if (item.AddressCustomers.Count() > 0)
                {
                    db.ProfileCustomers.Attach(item);
                    item.AddressCustomers.Add(new AddressCustomer
                    {
                        ProfileId = item.Id,
                        Name = Name,
                        UserId = user.Id,
                        Address = Address,
                        AddressDetail = AddressDt,
                        Email = user.Email,
                        PhoneNumber = Phone,
                        IsDefault = false
                    });
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { message = "Success", Success = true });
                }
                else
                {
                    db.ProfileCustomers.Attach(item);
                    item.AddressCustomers.Add(new AddressCustomer
                    {
                        ProfileId = item.Id,
                        Name = Name,
                        UserId = user.Id,
                        Address = Address,
                        AddressDetail = AddressDt,
                        Email = user.Email,
                        PhoneNumber = Phone,
                        IsDefault = true
                    });
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { message = "Success", Success = true });
                }
            }
            return Json(new { message = "UnSuccess", Success = false });
        }
        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public ActionResult UploadPayment(int TypePayment, int id)
        {
            var item = db.ProfileCustomers.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                db.ProfileCustomers.Attach(item);
                item.TypePayment = TypePayment;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });

        }
        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public ActionResult UploadPaymentVN(int TypePaymenVN, int id)
        {
            var item = db.ProfileCustomers.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                db.ProfileCustomers.Attach(item);
                item.TypePaymentVN = TypePaymenVN;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });

        }
        [HttpGet]
        public ActionResult LoadAddress(int id)
        {
            var address = db.AddressCustomers.Find(id);
            if (address != null)
            {
                return Json(new
                {
                    Id = address.Id,
                    Name = address.Name,
                    PhoneNumber = address.PhoneNumber,
                    Address = address.Address,
                    AddressDetail = address.AddressDetail
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public ActionResult EditAddress(int Id, string Name, string Phone, string AddressDt)
        {
            var address = db.AddressCustomers.FirstOrDefault(x => x.Id == Id);
            if (address != null)
            {
                address.Name = Name;
                address.PhoneNumber = Phone;
                address.AddressDetail = AddressDt;
                db.Entry(address).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true });
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult DeleteAddress(int id)
        {
            var address = db.AddressCustomers.FirstOrDefault(x => x.Id == id);
            if (address != null)
            {
                db.AddressCustomers.Remove(address);
                db.SaveChanges();
                return Json(new { Success = true });
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}