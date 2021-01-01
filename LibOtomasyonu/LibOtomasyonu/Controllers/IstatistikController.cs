using LibOtomasyonu.Data.Model;
using LibOtomasyonu.Data.UnitOfWork;
using System;
using System.Web.Mvc;

namespace LibOtomasyonu.Controllers
{
    public class IstatistikController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public IstatistikController()
        {
            _unitOfWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            ViewBag.KategoriSayisi = _unitOfWork.GetRepository<Kategori>().Count(); //Kategori sayısı bulunur.
            ViewBag.YazarSayisi = _unitOfWork.GetRepository<Yazar>().Count(); //Yazar sayısı bulunur.
            ViewBag.KitapSayisi = _unitOfWork.GetRepository<Kitap>().Count(); //Kitap sayısı bulunur.
            ViewBag.TeslimEdilenKitapSayisi = _unitOfWork.GetRepository<OduncKitap>().Count(x => x.GetirdigiTarih == null); // Ödünç alınan kitap sayısı
            ViewBag.TeslimAlinanKitapSayisi = _unitOfWork.GetRepository<OduncKitap>().Count(x => x.GetirdigiTarih != null); // Getirilen kitap sayısı
            var sonBirHafta = DateTime.Now.AddDays(-6); //İçinde bulunan günden dolayı -6
            ViewBag.SonHaftaTeslimEdilenKitapSayisi = _unitOfWork.GetRepository<OduncKitap>().Count(x => x.AlisTarihi > sonBirHafta);
            ViewBag.SonHaftaTeslimAlinanKitapSayisi = _unitOfWork.GetRepository<OduncKitap>().Count(x => x.GetirdigiTarih != null && x.GetirdigiTarih > sonBirHafta);
            return View();
        }
    }
}