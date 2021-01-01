using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOtomasyonu.Data.Model
{
    public class Kitap: BaseEntity //Id miras alınarak bu sınıfa katıldı
    {
        [Required] //Kitap adı zorunlu
        [Column(TypeName = "varchar")] //Tipi
        [MaxLength(50)] //Max. uzunluk 50
        public string Ad { get; set; }

        [Required] //Kitap sıra no zorunlu
        [Column(TypeName = "varchar")] //Tipi
        [MaxLength(20)] //Max. uzunluk 20
        public string SiraNo { get; set; }

        [Required] //Adet zorunlu
        public int Adet { get; set; }

        [Required] //Eklenme tarihi zorunlu
        public DateTime EklenmeTarihi { get; set; }

        [Required] //YazarId zorunlu
        public int YazarId { get; set; } //Hangi yazara ait
        public virtual Yazar Yazar { get; set; } // Kitabın yazar bilgileri
        public virtual List<Kategori> Kategoriler { get; set; } //Many to many ilişki olduğu için
    }
}
