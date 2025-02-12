using System.ComponentModel.DataAnnotations;

namespace APIstarts_API.Models.Dto
{
    public class VillaDTO    //Data Transfer Object genelde verilerin taşınmasında kullanılan bir modeldir.
                             //DTO class veritabanı nesnesine ait bilgileri dış dünyaya iletmek için kullanılır.
                             //Gösterilmesi gereken veriler genelde yazılır örn id ve name var sadece 
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]

        public string Name { get; set; }

        public int Occupancy { get; set; }

        public int Sqft { get; set; }

    }
}
 