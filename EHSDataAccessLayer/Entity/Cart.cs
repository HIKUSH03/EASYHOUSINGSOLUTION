using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHSDataAccessLayer.Entity
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int BuyerId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        // Navigation Properties
        [ForeignKey("BuyerId")]
        public Buyer Buyer { get; set; }

        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
    }
}