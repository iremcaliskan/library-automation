using LibOtomasyonu.Data.Model;
using LibOtomasyonu.Data.UnitOfWork;
using LibOtomasyonu.HelperClasses;
using System;
using System.Web.Mvc;

namespace LibOtomasyonu.Controllers
{
    [YetkiKontrolSistemi]
    //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
    //Sınıfın üzerine eklendiği zaman her Action için kontrol ediyor.
    public class UyeController : Controller
    {
        UnitOfWork unitOfWork;

        public UyeController()
        { //Yapıcı methodu
            unitOfWork = new UnitOfWork(); //unitOfWork nesnesi oluşturuldu
        }
        public ActionResult Index()
        {
            var uyeler = unitOfWork.GetRepository<Uye>().GetAll(); //üyelerin çekilmesi
            return View(uyeler);
        }

        public ActionResult Ekle() 
        {
            return View();
        }

        [HttpPost]
        public JsonResult EkleJson(string uyeAd, string uyeSoyad, string uyeTc, string uyeTel) 
        {
            if (!string.IsNullOrEmpty(uyeAd) && !string.IsNullOrEmpty(uyeSoyad))
            {
                Uye uye = new Uye();
                uye.Ad = uyeAd;
                uye.Soyad = uyeSoyad;
                uye.Tc = uyeTc;
                uye.Tel = uyeTel;
                uye.Ceza = 0;
                uye.KayitTarihi = DateTime.Now;
                unitOfWork.GetRepository<Uye>().Add(uye);
                var durum = unitOfWork.SaveChanges();
                if (durum > 0)
                    return Json("1");
                else 
                    return Json("0");
            }
            else
            {
                return Json("bosOlamaz");
            }
        }

        [HttpPost]
        public JsonResult SilJson(int uyeId)
        {
            unitOfWork.GetRepository<Uye>().Delete(uyeId);
            var durum = unitOfWork.SaveChanges();
            if (durum > 0) return Json("1");
            return Json("0");
        }

        public ActionResult Guncelle(int uyeId)
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            return View(uye);
        }

        [HttpPost]
        public JsonResult GuncelleJson(int uyeId, string uyeAd, string uyeSoyad, string uyeTc, string uyeTel)
        {
            if (!string.IsNullOrEmpty(uyeAd) && !string.IsNullOrEmpty(uyeSoyad))
            {
                var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
                uye.Ad = uyeAd;
                uye.Soyad = uyeSoyad;
                uye.Tc = uyeTc;
                uye.Tel = uyeTel;
                unitOfWork.GetRepository<Uye>().Update(uye);
                var durum = unitOfWork.SaveChanges();
                if (durum > 0)
                    return Json("1");
                else
                    return Json("0");
            }
            else
            {
                return Json("bosOlamaz");
            }
        }
    }
}