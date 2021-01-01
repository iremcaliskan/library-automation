using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOtomasyonu.Data.Model
{
    public class Kategori: BaseEntity //Id miras alınarak bu sınıfa katıldı
    {
        [Required] //Kategori adı zorunlu
        [Column(TypeName ="varchar")] //Tipi
        [MaxLength(50)] //Max. uzunluk 50
        public string Ad { get; set; }
        public virtual List<Kitap> Kitaplar { get; set; } //Many to many ilişki olduğu için
    }
}
