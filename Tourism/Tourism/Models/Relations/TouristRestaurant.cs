using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Models.Relations
{
    public class TouristRestaurant
    {
        [ForeignKey("Tourist")]
        public int touristId { get; set; }
        [ForeignKey("Restaurant")]
        public int restaurantId { get; set; }
        [ForeignKey("Table")]
        public int tableId { get; set; }


        [Key]
        public int bookId { get; set; }
        public int tableNumber { get; set; }
        public int numberOfGuests { get; set; }
        public DateTime date { get; set; }
        public decimal bookingPrice { get; set; }

        public Tourist tourist { get; set; }
        public Restaurant restaurant { get; set; }
        public string? CreditCardId { get; set; } 
        public CreditCard CreditCard { get; set; }
    }
}
