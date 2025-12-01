using Microsoft.EntityFrameworkCore;
using Tourism.Models;
using Tourism.ViewModel;

namespace Tourism.IRepository
{
    public interface ITouristRepository
    {
        public Tourist GetByEmail(string email);
        public double TotalEearningsRestaurants();
        public double TotalEearningsProducts();
        public double TotalEearningsHotels();
        public double TotalEearningsTrips();
        public bool AddToCart(int productId, int touristId);
        public void transaction(CreditCard cardfromrequest, decimal money, string operation,int id);
        public void BuyProduct(int touristId, bool buyCredit, CreditCard? cc, ProductViewModel product,string location);
        public void RemoveFromCart(int productId, int touristId);
        public void CancelOrder(int orderId);
        public List<ProductOrdersViewModel> GetOrders(int touristId);

        public bool review(int touristId, int serviceId, int rate, string comment, string type);

    }
}
