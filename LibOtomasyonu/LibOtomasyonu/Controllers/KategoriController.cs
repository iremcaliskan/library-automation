using LibOtomasyonu.Data.Model;
using LibOtomasyonu.Data.UnitOfWork;
using LibOtomasyonu.HelperClasses;
using System.Web.Mvc;

namespace LibOtomasyonu.Controllers
{
    [YetkiKontrolSistemi] 
    //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
    //Sınıfın üzerine eklendiği zaman her Action için kontrol ediyor.
    public class KategoriController : Controller
    {
        UnitOfWork unitOfWork;

        public KategoriController() 
        { //Yapıcı methodu
            unitOfWork = new UnitOfWork(); //unitOfWork nesnesi oluşturuldu
        }

        public ActionResult Index()
        {
            //GetRepository() methoduna ulaşmak için
            var kategoriler = unitOfWork.GetRepository<Kategori>().GetAll();
            return View(kategoriler);
        }

        [HttpPost]
        public JsonResult EkleJson(string ktgAd) 
        {
            Kategori ktgri = new Kategori();
            ktgri.Ad = ktgAd;
            var eklenenKtg = unitOfWork.GetRepository<Kategori>().Add(ktgri);
            unitOfWork.SaveChanges();
            return Json(
                new {
                    Result = new 
                    {
                        eklenenKtg.Id,
                        eklenenKtg.Ad,
                    }, JsonRequestBehavior.AllowGet
                });
        }

        [HttpPost]
        public JsonResult GuncelleJson(int ktgId, string ktgAd) 
        {
            var kategori = unitOfWork.GetRepository<Kategori>().GetById(ktgId);
            kategori.Ad = ktgAd;
            var durum = unitOfWork.SaveChanges();
            if (durum > 0) return Json("1");
            return Json("0");
        }

        [HttpPost]
        public JsonResult SilJson(int ktgId)
        {           
            unitOfWork.GetRepository<Kategori>().Delete(ktgId);
            var durum = unitOfWork.SaveChanges();
            if (durum > 0) return Json("1");
            return Json("0");
        }
    }
}