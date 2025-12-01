namespace Tourism.ViewModel
{
    public class TouristBookingHistoryViewModel
    {
        public int BookingId { get; set; }
        public string RestaurantName { get; set; }
        public int TableNumber { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal BookingPrice { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantHotline { get; set; }
        public bool IsCancelled { get; set; }
    }
}