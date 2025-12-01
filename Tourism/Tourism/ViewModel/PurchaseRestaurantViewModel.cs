using Tourism.Models;

namespace Tourism.ViewModel
{
    public class PurchaseRestaurantViewModel
    {
        public int touristId { get; set; }
        public int restaurantId { get; set; }
        public int tableId { get; set; }
        public int tableNumber { get; set; }
        public int numberOfGuests { get; set; }
        public DateTime date { get; set; }
        public decimal bookingPrice { get; set; }
        public CreditCard creditCard { get; set; }
    }
}
