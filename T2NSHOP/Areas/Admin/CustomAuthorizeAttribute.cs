using Microsoft.AspNet.Identity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace T2NSHOP.Areas.Admin
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You are not authorized to access this page");
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Please login to continue");
            }
            //if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You are not authorized to access this page ");
            //    // Logout user
            //    filterContext.HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            //    // Redirect to login page
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            //    {
            //        area = "Admin",
            //        controller = "Account",
            //        action = "Login"
            //    }));
            //}
            //else
            //{
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            //    {
            //        area = "Admin",
            //        controller = "Account",
            //        action = "Login"
            //    }));
            //    //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Please login to continue");
            //}
        }
    }
}