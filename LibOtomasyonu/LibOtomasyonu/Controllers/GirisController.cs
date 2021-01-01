using LibOtomasyonu.Data.HelperClass;
using LibOtomasyonu.Data.Model;
using LibOtomasyonu.Data.UnitOfWork;
using System;
using System.Web;
using System.Web.Mvc;

namespace LibOtomasyonu.Controllers
{
    public class GirisController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public GirisController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            if (Request.Cookies["uye"] != null) { return RedirectToAction("Index", "Kitap"); } //üye giriş yaptıysa /Giris/Index'e dönülemesin
                        
            return View();
        }

        [HttpPost]
        public JsonResult GirisKontrolJson(string email, string sifre, bool hatirla)
        {
            email = email.Trim();
            sifre = sifre.Trim();
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(sifre)) return Json("BosOlamaz");

            sifre = sifre.Sifrele();
            var uye = new Uye();

            try { uye = _unitOfWork.GetRepository<Uye>().Get(x => x.Mail == email && x.Sifre == sifre); }
            catch { }

            if (uye != null)
            { //üye null değilse çerez oluşturup üye bilgilerini tutucak, daha sonra giriş başarılı ise yönlendirme yapılacak
                HttpCookie cookie = new HttpCookie("uye"); //ismi uye
                cookie.Values.Add("Id", uye.Id.ToString()); //üyenin id'si
                cookie.Values.Add("Ad", uye.Ad); //üyenin adı
                cookie.Values.Add("Soyad", uye.Soyad); //üyenin soyadı
                cookie.Values.Add("YetkiId", uye.Yetki); //üyenin yetkisi

                if (hatirla == true) { cookie.Expires = DateTime.Now.AddDays(5); } //hatirla seçili ise 5 gün boyunca bellekte tut
                //seçili değilse çıkış yapıldığı andan itibaren cookie silinir, tekrar giriş yapılmalı.

                Response.Cookies.Add(cookie); //İşlemler başarılı ise Cookie eklenir

                return Json("1");
            }
            else return Json("Hata"); //Değerler gelmedi/üye yoksa giriş başarısız mesajı verilecek
        }

        public ActionResult CikisYap() 
        {
            var cookie = Response.Cookies["uye"];
            if (cookie != null) { cookie.Expires = DateTime.Now.AddDays(-1); }

            return RedirectToAction("Index"); //Giriş sayfasına yönlendirme
        }
    }
}