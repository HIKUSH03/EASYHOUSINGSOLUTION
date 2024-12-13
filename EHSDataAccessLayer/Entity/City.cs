using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHSDataAccessLayer.Entity
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        [StringLength(50)]
        public string CityName { get; set; }

        [Required]
        public int StateId { get; set; }

        // Navigation Properties
        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
