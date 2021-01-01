using LibOtomasyonu.HelperClasses;
using LibOtomasyonu.Tasks.Triggers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibOtomasyonu
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CezaArttirmaDusurmeTrigger.Baslat(); //CezaArttirmaDusurmeTrigger.cs Baslat() methodu eklenir, proje baþladýðý anda buraya girilir ve trigger baþlatýlýr.            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Hatayý Yakala
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;
            if (httpException != null)
            {
                //log bilgileri
                LogInfo log = new LogInfo
                {
                    Url = Request.Url.ToString(),
                    HataMesaji = httpException.Message,
                    EklenmeTarihi = DateTime.Now,
                    Ip = KullaniciIpAdres.KullaniciIpBul(),
                    Tarayici = Request.Browser.Browser,
                    Dil = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].Substring(0, 2)
                };

                switch (httpException.GetHttpCode())
                {
                    case 403:
                        Response.Redirect("/Hata/ErisimIzniYok");
                        break;
                    case 404:
                        Response.Redirect("/Hata/SayfaBulunamadi");
                        break;
                    case 500:
                        Response.Redirect("/Hata/SunucuHatasý");
                        break;
                    default:
                        Response.Redirect("/Hata/SayfaBulunamadi");
                        break;
                }
                Server.ClearError();
            }        
        }
    }
}
