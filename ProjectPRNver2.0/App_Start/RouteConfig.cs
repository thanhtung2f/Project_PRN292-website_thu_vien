using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectPRNver2._0
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Product Detail",
               url: "chi-tiet/sanpham={id}",
               defaults: new { controller = "BookItem", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "ProjectPRN.Controllers" }
           );
            routes.MapRoute(
               name: "Register",
               url: "dang-ky",
               defaults: new { controller = "Register", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "ProjectPRN.Controllers" }
           );

            routes.MapRoute(
              name: "Author Detail",
              url: "detail/tacgia={id}",
              defaults: new { controller = "BookAuthor", action = "detailAuthor", id = UrlParameter.Optional },
              namespaces: new[] { "ProjectPRN.Controllers" }
            );

            routes.MapRoute(
               name: "Add cart",
               url: "them-gio-hang",
               defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
               namespaces: new[] { "ProjectPRN.Controllers" }
           );

            routes.MapRoute(
               name: "Payment",
               url: "thanh-toan",
               defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
               namespaces: new[] { "ProjectPRN.Controllers" }
           );

            routes.MapRoute(
               name: "Payment success",
               url: "hoan-thanh",
               defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
               namespaces: new[] { "ProjectPRN.Controllers" }
           );

            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "ProjectPRN.Controllers" }
           );
            routes.MapRoute(
                name: "User Detail",
                url: "nguoi-dung/id={id}",
                defaults: new { controller = "UserDetail", action = "UserDetail", id = UrlParameter.Optional },
                namespaces: new[] { "ProjectPRN.Controllers" }
            );
            routes.MapRoute(
                name: "Wish List",
                url: "yeu-thich/id={id}",
                defaults: new { controller = "WishList", action = "WishList", id = UrlParameter.Optional },
                namespaces: new[] { "ProjectPRN.Controllers" }
            );
            routes.MapRoute(
                name: "Order Detail",
                url: "chi-tiet-don-hang/id={id}",
                defaults: new { controller = "OrderView", action = "OrderView", id = UrlParameter.Optional },
                namespaces: new[] { "ProjectPRN.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
