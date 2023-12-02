using System;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace T2NSHOP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ValidateAntiForgeryTokenOnAllPosts());
        }
        public class ValidateAntiForgeryTokenOnAllPosts : AuthorizeAttribute
        {
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                var request = filterContext.HttpContext.Request;
                // Only validate POSTs  
                if (request.HttpMethod == WebRequestMethods.Http.Post)
                {
                    var formTokenValue = request.Form["__RequestVerificationToken"];
                    if (request.IsAjaxRequest() && string.IsNullOrEmpty(formTokenValue))
                    {
                        var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];
                        var cookieValue = antiForgeryCookie != null ? antiForgeryCookie.Value : null;
                        AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                    }
                    else
                    {
                        new ValidateAntiForgeryTokenAttribute().OnAuthorization(filterContext);
                    }
                }
            }
        }
    }
    
}
