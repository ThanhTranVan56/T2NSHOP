using System.Linq;
using System.Web.Mvc;
using T2NSHOP.Models;

namespace T2NSHOP.Controllers
{
    public class IntroduceController : Controller
    {
        // GET: Introduce
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var item = db.Posts.First();
            if (item != null)
            {
                return View(item);
            }
            return View();
        }
    }
}