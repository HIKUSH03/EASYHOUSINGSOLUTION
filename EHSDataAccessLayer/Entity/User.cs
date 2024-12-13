using System.ComponentModel.DataAnnotations;

namespace EHSDataAccessLayer.Entity
{
    public class User
    {
        [Key]
        [StringLength(25)]

        public string UserName { get; set; }

        [Required]
        [StringLength(25)]

        public string Password { get; set; }


        [Required]
        [StringLength(15)]
        public string UserType { get; set; } // Admin, Buyer, or Seller

    }
}
