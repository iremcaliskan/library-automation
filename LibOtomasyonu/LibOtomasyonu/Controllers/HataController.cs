using System.Web.Mvc;

namespace LibOtomasyonu.Controllers
{
    // 403 :: Sayfaya erişim izni yok
    // 404 :: Sayfa bulunamadı 
    // 500 :: Sunucuda hata oluşması, istek karşılanamadı hatası
    public class HataController : Controller
    {
       //404, 400
        public ActionResult SayfaBulunamadi()
        {
            return View();
        }

        //403
        public ActionResult ErisimIzniYok()
        {
            return View();
        }

        //500
        public ActionResult SunucuHatası()
        {
            return View();
        }
    }
}