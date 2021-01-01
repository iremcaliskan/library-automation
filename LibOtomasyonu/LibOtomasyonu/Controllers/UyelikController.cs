using LibOtomasyonu.Data.HelperClass;
using LibOtomasyonu.Data.Model;
using LibOtomasyonu.Data.UnitOfWork;
using LibOtomasyonu.HelperClasses;
using System;
using System.Web.Mvc;

namespace LibOtomasyonu.Controllers
{
    public class UyelikController : Controller
    {
        UnitOfWork unitOfWork;

        public UyelikController() //Yapıcı method
        {
            unitOfWork = new UnitOfWork();
        }

        [YetkiKontrolSistemi]
        //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
        //Sınıfın üzerine eklendiği zaman her Action için kontrol eder, Action üzerine eklenirse Action kontol eder.
        public ActionResult Index() //Index'e üyeler çekilir, yetkisi null olmayan üyeler çekilmeli
        {
            var uyeler = unitOfWork.GetRepository<Uye>().GetAll(x => x.Yetki != null);
            return View(uyeler); //View'a üyeler gönderilir
        }

        [YetkiKontrolSistemi]
        //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
        //Sınıfın üzerine eklendiği zaman her Action için kontrol eder, Action üzerine eklenirse Action kontol eder.
        public ActionResult Ekle() //Uyelik ekleye basıldığında bu Action'a gelir.
        {
            var uyeler = unitOfWork.GetRepository<Uye>().GetAll(x => x.Yetki == null); //Yetkisi null olanlar ekleneceği için
            return View(uyeler);
        }

        [YetkiKontrolSistemi]
        //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
        //Sınıfın üzerine eklendiği zaman her Action için kontrol eder, Action üzerine eklenirse Action kontol eder.
        [HttpPost]
        public JsonResult EkleJson(int uyeId, string mail, string parola, string parolaTekrar)
        {
            if (!string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(parola) && !string.IsNullOrEmpty(parolaTekrar))
            {
                if (parola == parolaTekrar)
                {
                    parola = Sifreleme.Sifrele(parola);
                    var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
                    uye.Mail = mail;
                    uye.Sifre = parola;
                    // 1 = admin(ful yetki), 2 = moderatör(üyelik controller'a ulaşamaz), 3 = izleyici(index sayfasını görür, işlem yapamaz)
                    uye.Yetki = "3";

                    unitOfWork.GetRepository<Uye>().Update(uye);
                    unitOfWork.SaveChanges();
                    return Json("1");
                }
                else return Json("parolaUyusmazligi");

            }
            else return Json("bosOlamaz");
        }

        [YetkiKontrolSistemi]
        //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
        //Sınıfın üzerine eklendiği zaman her Action için kontrol eder, Action üzerine eklenirse Action kontol eder.
        public ActionResult Guncelle(int uyeId) //Güncelleme kısmı
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId); //Alınan uyeId ye göre view çekilir
            return View(uye);
        }

        [YetkiKontrolSistemi]
        //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
        //Sınıfın üzerine eklendiği zaman her Action için kontrol eder, Action üzerine eklenirse Action kontol eder.
        [HttpPost]
        public JsonResult GuncelleJson(int uyeId, string mail, string parola, string parolaTekrar)
        {
            if (!string.IsNullOrEmpty(mail))
            {
                if (parola == parolaTekrar)
                {
                    parola = Sifreleme.Sifrele(parola);
                    var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
                    uye.Mail = mail;

                    if (!string.IsNullOrEmpty(parola)) { uye.Sifre = parola; }

                    unitOfWork.GetRepository<Uye>().Update(uye);
                    unitOfWork.SaveChanges();
                    return Json("1");
                }
                else return Json("parolaUyusmazligi");
            }
            else return Json("mailBosOlamaz");
        }

        [YetkiKontrolSistemi]
        //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
        //Sınıfın üzerine eklendiği zaman her Action için kontrol eder, Action üzerine eklenirse Action kontol eder.
        [HttpPost]
        public JsonResult SilJson(int uyeId)
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            unitOfWork.GetRepository<Uye>().Delete(uye);
            var durum = unitOfWork.SaveChanges();
            if (durum > 0)
                return Json("1");
            return Json("0");
        }

        [YetkiKontrolSistemi]
        //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
        //Sınıfın üzerine eklendiği zaman her Action için kontrol eder, Action üzerine eklenirse Action kontol eder.
        [HttpPost]
        public JsonResult YetkiAtaJson(int uyeId, string yetkiId) 
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            uye.Yetki = yetkiId;
            unitOfWork.GetRepository<Uye>().Update(uye);
            var durum = unitOfWork.SaveChanges();
            if (durum > 0)
                return Json("1");
          
            return Json("0");
        }

        [YetkiKontrolSistemi]
        //Hangi Controllerda kullanılması isteniyorsa attribute olarak eklenmeli, 
        //Sınıfın üzerine eklendiği zaman her Action için kontrol eder, Action üzerine eklenirse Action kontol eder.
        public ActionResult ProfilBilgilerimiGuncelle()
        {
            var uyeId = Convert.ToInt32(Request.Cookies["uye"]["Id"]);
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            return View(uye);
        }

        [HttpPost]
        //Yetki eklemedim, yetki farketmeksizin tüm kullanıcılar erişmeli
        public JsonResult ProfilBilgiGuncelleJson(string parola, string parolaTekrar, string tel)
        {
            if (parola == parolaTekrar)
            {
                var uyeId = Convert.ToInt32(Request.Cookies["uye"]["Id"]);
                var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);

                //  uye  .    Mail =     mail;
                uye.Tel = tel;

                if (!string.IsNullOrEmpty(parola))
                {
                    parola = Sifreleme.Sifrele(parola);
                    uye.Sifre = parola;
                }
               
                unitOfWork.GetRepository<Uye>().Update(uye);
                unitOfWork.SaveChanges();

                return Json("1");
            }
            else
            {
                return Json("parolaUyusmazligi");               
            }
        }
    }
}