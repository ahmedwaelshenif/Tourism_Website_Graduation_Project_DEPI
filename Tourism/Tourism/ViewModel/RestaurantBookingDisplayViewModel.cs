namespace Tourism.ViewModel
{
    public class RestaurantBookingDisplayViewModel
    {
        public int BookingId { get; set; }
        public int TouristId { get; set; }
        public string TouristName { get; set; } 
        public string TouristEmail { get; set; }
        public int TableNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal BookingPrice { get; set; }
    }
}
