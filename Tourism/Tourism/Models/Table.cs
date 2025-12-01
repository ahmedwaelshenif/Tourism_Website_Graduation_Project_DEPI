using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Models
{
    public class Table
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int number { get; set; }
        [Required]
        public int numberOfPersons { get; set; }
        [Required]
        public decimal bookingPrice { get; set; }
        [ForeignKey("Restaurant")]
        public int restaurantId { get; set; }
        public Restaurant restaurant { get; set; }
        [Required]
        public bool booked { get; set; } = false;

        
        public DateTime? dateAdded { get; set; }

    }
}
