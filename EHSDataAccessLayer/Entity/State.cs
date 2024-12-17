using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EHSDataAccessLayer.Entity
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required]
        [StringLength(30)]
        public string StateName { get; set; }
        // Navigation Properties
        public ICollection<City> Cities { get; set; }
    }
}
