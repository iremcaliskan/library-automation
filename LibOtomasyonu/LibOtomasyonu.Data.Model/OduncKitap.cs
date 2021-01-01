using System;
using System.ComponentModel.DataAnnotations;

namespace LibOtomasyonu.Data.Model
{
    public class OduncKitap: BaseEntity //Id miras alınarak bu sınıfa katıldı
    {
        [Required] //KitapId zorunlu
        public int KitapId { get; set; }

        [Required] //UyeId zorunlu
        public int UyeId { get; set; }

        [Required] //Alınış tarihi zorunlu
        public DateTime AlisTarihi { get; set; }

        [Required] //Getirileceği tarih zorunlu
        public DateTime GetirecegiTarihi { get; set; }
        public DateTime? GetirdigiTarih { get; set; } //null olabilir

        public virtual Uye Uye { get; set; } //Üye verileri
        public virtual Kitap Kitap { get; set; } //Kitap verileri
    }
}
