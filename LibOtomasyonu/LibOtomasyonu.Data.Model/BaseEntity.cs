using System.ComponentModel.DataAnnotations;

namespace LibOtomasyonu.Data.Model
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; } //Id diğer sınıflar tarafından miras alınır.
    }
}
