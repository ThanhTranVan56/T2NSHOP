using System.Web.Mvc;
using System.Web.Routing;

namespace T2NSHOP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
                name: "search",
                url: "tim-kiem",
                defaults: new { controller = "Search", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
               name: "searchcategoryproduct",
               url: "tim-kiem/{alias}-{id}",
               defaults: new { controller = "Search", action = "SearchProductCategory", id = UrlParameter.Optional },
               namespaces: new[] { "T2NSHOP.Controllers" }
           );
            routes.MapRoute(
                name: "introduce",
                url: "gioi-thieu",
                defaults: new { controller = "Introduce", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
                name: "promotion",
                url: "khuyen-mai",
                defaults: new { controller = "Promotion", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
               name: "CheckOut",
               url: "thanh-toan",
               defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
               namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
               name: "Shopping",
               url: "gio-hang",
               defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
                name: "CategoryProduct",
                url: "danh-muc-san-pham/{alias}-{id}",
                defaults: new { controller = "Products", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
                name: "detailProduct",
                url: "chi-tiet/{alias}-p{id}",
                defaults: new { controller = "Products", action = "Detail", alias = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
                name: "Products",
                url: "san-pham",
                defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
                name: "DetailNew",
                url: "{alias}-n{id}",
                defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
               name: "NewsList",
               url: "tin-tuc",
               defaults: new { controller = "News", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "T2NSHOP.Controllers" }
           );
            routes.MapRoute(
                name: "BaiViet",
                url: "post/{alias}",
                defaults: new { controller = "Article", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "T2NSHOP.Controllers" }
            );
        }
    }
}
