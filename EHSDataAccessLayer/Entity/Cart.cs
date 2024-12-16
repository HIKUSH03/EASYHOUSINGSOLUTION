using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EHSDataAccessLayer.Entity
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int BuyerId { get; set; }



        public ICollection<Property> Properties { get; set; }
    }
}