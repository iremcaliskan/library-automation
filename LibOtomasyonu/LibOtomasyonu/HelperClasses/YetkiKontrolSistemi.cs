using LibOtomasyonu.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibOtomasyonu.HelperClasses
{
    public class YetkiKontrolSistemi : ActionFilterAttribute
    {
        private readonly UnitOfWork _unitOfWork; //bağlantı alınır

        public YetkiKontrolSistemi() //Yapıcı metot oluşturulur
        {
            _unitOfWork = new UnitOfWork();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies["uye"]; //Cookie filterContext parametresi ile alınır.
            if (cookie == null)
            {
                //cookie null ise GirisController'ın Index Action'a yönlendirilir.
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { 
                    { "controller", "Giris" }, { "action", "Index" } 
                });
            }
            else
            {
                var yetki = cookie["YetkiId"];
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                //null değil ise yetki kontrolü
                if (yetki == "2") //Moderatör ise
                {
                    if (controllerName == "Uyelik")                   
                        //Üyelik Kontrolcüsüne erişmemesi için Kitap Index'ine atlansın.
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "controller", "Kitap" }, { "action", "Index" }
                        });                    
                }
                else if (yetki == "3") //İzleyici ise
                {
                    if ((controllerName == "Uyelik" && actionName == "Index")|| 
                        (controllerName == "Uye" && actionName == "Index") || 
                        actionName != "Index")
                        //Üyelik Kontrolcüsüne erişmemesi, actionName Index değilse Kitap Index'ine atlansın.
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "controller", "Kitap" }, { "action", "Index" }
                        });                  
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}