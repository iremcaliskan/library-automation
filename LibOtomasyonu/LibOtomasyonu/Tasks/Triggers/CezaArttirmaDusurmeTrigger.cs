using LibOtomasyonu.Tasks.Jobs;
using Quartz;
using Quartz.Impl;

namespace LibOtomasyonu.Tasks.Triggers
{
    public class CezaArttirmaDusurmeTrigger
    {
        public static void Baslat() 
        {
            //Zamanlayıcıyı Oluşturma
            IScheduler zamanlayici = StdSchedulerFactory.GetDefaultScheduler();
            //Zamanlayıcıyı çalıştırma
            if (!zamanlayici.IsStarted)
                zamanlayici.Start();
            //Tetiklenecek görev
            IJobDetail gorev = JobBuilder.Create<CezaArttirmaDusurmeJob>().Build();

            //Tetikleyici oluşturma
            ICronTrigger tetikleyici = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("CezaArttirmaDusurmeJob", "null") //Çalıştırılacak görevin adı
                // * işareti herhangibir koşul yok demektir, ? ise geçerli bir değer tanımlanmadığında kullanılır
                .WithCronSchedule("0 00 22 * * ? *") //Günün hangi saatinde çalışacak saniye/dakika/saate/günler/ay/haftanın günleri/yıl
                .Build(); //Tetikleyiciyi aktif etme

            //Zamanlayıcıya görevi ve tetikleyiciyi tanıtma
            zamanlayici.ScheduleJob(gorev, tetikleyici);
        }
    }
}