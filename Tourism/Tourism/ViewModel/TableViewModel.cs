using System.ComponentModel.DataAnnotations;

namespace Tourism.ViewModel
{
    public class TableViewModel
    {
        [Required(ErrorMessage = "Table number is required")]
        public int number { get; set; }
        [Required(ErrorMessage = "Number of persons is required")]
        public int numberOfPersons { get; set; }
        [Required(ErrorMessage = "Booking price is required")]
        public decimal bookingPrice { get; set; }
        public int? id { get; set; }
        public bool booked { get; set; }
        public int restaurantId { get; set; }
        public bool IsInCart { get; set; }
    }
}
