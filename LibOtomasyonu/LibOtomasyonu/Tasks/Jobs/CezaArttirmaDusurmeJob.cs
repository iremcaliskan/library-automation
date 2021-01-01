using LibOtomasyonu.Data.Model;
using LibOtomasyonu.Data.UnitOfWork;
using Quartz;
using System;

namespace LibOtomasyonu.Tasks.Jobs
{
    public class CezaArttirmaDusurmeJob : IJob
    {
        UnitOfWork unitOfWork;
        public CezaArttirmaDusurmeJob() //Yapıcı Method
        {
            unitOfWork = new UnitOfWork();
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                CezaArttir();
                CezaDusur();
                unitOfWork.SaveChanges();
            }
            catch { }
        }

        void CezaArttir()
        {
            //Kitabı getirmediyse yani null ise ve Getiriceği tarihi geçmiş ise Ceza Artıcak
            var oduncKitaplar = unitOfWork.GetRepository<OduncKitap>().GetAll(x => x.GetirdigiTarih == null && DateTime.Now > x.GetirecegiTarihi);
            foreach (var oduncKitap in oduncKitaplar)
            {
                oduncKitap.Uye.Ceza += 1;
                unitOfWork.GetRepository<Uye>().Update(oduncKitap.Uye);
            }
        }

        void CezaDusur()
        {
            //Kitabı getirdiyse yani null değil ise ve Üyenin Cezası 0'dan büyükse Ceza Düşücek
            var oduncKitaplar = unitOfWork.GetRepository<OduncKitap>().GetAll(x => x.GetirdigiTarih != null && x.Uye.Ceza > 0);
            foreach (var oduncKitap in oduncKitaplar)
            {
                oduncKitap.Uye.Ceza -= 1;
                unitOfWork.GetRepository<Uye>().Update(oduncKitap.Uye);
            }
        }
    }
}