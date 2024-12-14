using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHSDataAccessLayer.Entity
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(50)]
        public string PropertyName { get; set; }

        [Required]
        [StringLength(15)]
        public string PropertyType { get; set; }

        [Required]
        [StringLength(10)]
        public string PropertyOption { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        public decimal PriceRange { get; set; }

        [Required]
        public decimal InitialDeposit { get; set; }

        [Required]
        [StringLength(25)]
        public string Landmark { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int SellerId { get; set; }

        // Navigation Properties
        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }


        public ICollection<Cart> Carts { get; set; }
    }
}