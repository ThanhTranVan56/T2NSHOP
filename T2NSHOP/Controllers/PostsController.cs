using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2NSHOP.Models;

namespace T2NSHOP.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Article
        public ActionResult Index(string alias)
        {
            var item = db.Posts.FirstOrDefault(x => x.Alias == alias);
            if(item != null)
            {
                return View(item);
            }
            return View();
        }
    }
}