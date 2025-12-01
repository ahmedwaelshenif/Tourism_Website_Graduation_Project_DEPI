namespace Tourism.ViewModel
{
    public class ProductOrdersViewModel
    {
        public string productName { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime? arrivalDate { get; set; }
        public int amount { get; set; }
        public string status { get; set; }
        public string TouristName {get;set;}
        public string address { get; set; }
        public int orderId { get; set; }
        public bool refund { get; set; }
    }
}
