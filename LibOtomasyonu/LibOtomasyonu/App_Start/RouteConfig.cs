using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibOtomasyonu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Kategori", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SayfaBulunamadi",
                url: "{*url}",
                new { controller = "Hata", action = "SayfaBulunamadi"}
            ); //Altta kalmalı sürekli çalışmamalı
        }
    }
}
