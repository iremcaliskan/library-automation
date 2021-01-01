using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOtomasyonu.Data.Model
{
    public class Yazar: BaseEntity //Id miras alınarak bu sınıfa katıldı
    {
        [Required] //Yazar adı zorunlu
        [Column(TypeName = "nvarchar")] //Tipi
        [MaxLength(100)] //Max. uzunluk 50
        public string Ad { get; set; }

        public virtual List<Kitap> Kitaplar { get; set; }

        // "-" hata alınmaması için
        public static implicit operator Yazar(string v)
        {
            throw new NotImplementedException();
        }
    }
}
