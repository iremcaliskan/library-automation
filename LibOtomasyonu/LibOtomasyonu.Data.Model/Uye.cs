using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOtomasyonu.Data.Model
{
    public class Uye: BaseEntity //Id miras alınarak bu sınıfa katıldı
    {
        [Required] //Uye adı zorunlu
        [Column(TypeName = "varchar")] //Tipi
        [MaxLength(50)] //Max. uzunluk 50
        public string Ad { get; set; }

        [Required] //Uye soyadı zorunlu
        [Column(TypeName = "varchar")] //Tipi
        [MaxLength(50)] //Max. uzunluk 50
        public string Soyad { get; set; }

        [Column(TypeName = "char")] //Tipi
        [MaxLength(11), MinLength(11)] //Uzunluk 11
        public string Tc { get; set; }

        [Column(TypeName = "char")] //Tipi
        [MaxLength(11), MinLength(11)] //Uzunluk 11
        public string Tel { get; set; }

        [Required] //Kayıt tarihi zorunlu
        public DateTime KayitTarihi { get; set; }

        [Column(TypeName = "nvarchar")] //Tipi
                                  //Nvarchar tıpkı Varchar gibi içinde bulunan değerin uzunluğuna göre 
                                  //bellekte yer tutar. Unicode karakterler bellekte daha fazla yer tutar.
                                  //Unicode desteğine ihtiyaç duyulduğunda nvarchar kullanılır.
       
        [MaxLength(100)] //Max. uzunluk 100
        public string Mail { get; set; }

        [Column(TypeName = "char")] //Tipi
        [MaxLength(32), MinLength(32)] //Max. uzunluk 32
        public string Sifre { get; set; }

        [Required] //Başta 0 kayıtlı olucak.
        public int Ceza { get; set; } //Ceza alacağı gün sayısı

        [Column(TypeName = "char")] //Tipi
        [MaxLength(1), MinLength(1)] //1 değere sahip
        public string Yetki { get; set; }

        public virtual List<OduncKitap> OduncKitaplar { get; set; }
    }
}
