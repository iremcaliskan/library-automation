using LibOtomasyonu.Data.Model;
using LibOtomasyonu.Data.UnitOfWork;
using LibOtomasyonu.HelperClasses;
using System.Web.Mvc;

namespace LibOtomasyonu.Controllers
{
    [YetkiKontrolSistemi]
    //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
    //Sınıfın üzerine eklendiği zaman her Action için kontrol eder, Action üzerine eklenirse Action kontol eder.
    public class YazarController : Controller
    {
        UnitOfWork unitOfWork;

        public YazarController() 
        { //Yapıcı methodu
            unitOfWork = new UnitOfWork(); //unitOfWork nesnesi oluşturuldu
        }

        public ActionResult Index()
        {
            //GetRepository() methoduna ulaşmak için
            var yazarlar = unitOfWork.GetRepository<Yazar>().GetAll();
            return View(yazarlar);
        }

        [HttpPost]
        public JsonResult EkleJson(string yzrAd)
        {
            Yazar yazar = new Yazar();
            yazar.Ad = yzrAd;
            var eklenenYazar = unitOfWork.GetRepository<Yazar>().Add(yazar);
            unitOfWork.SaveChanges();
            return Json(
                new
                {
                    Result = new
                    {
                        eklenenYazar.Id,
                        eklenenYazar.Ad,
                    },
                    JsonRequestBehavior.AllowGet
                });
        }

        [HttpPost]
        public JsonResult GuncelleJson(int yzrId, string yzrAd)
        {
            var yazar = unitOfWork.GetRepository<Yazar>().GetById(yzrId);
            yazar.Ad = yzrAd;
            var durum = unitOfWork.SaveChanges();
            if (durum > 0) return Json("1");
            return Json("0");
        }

        [HttpPost]
        public JsonResult SilJson(int yzrId)
        {
            unitOfWork.GetRepository<Yazar>().Delete(yzrId);
            var durum = unitOfWork.SaveChanges();
            if (durum > 0) return Json("1");
            return Json("0");
        }
    }
}