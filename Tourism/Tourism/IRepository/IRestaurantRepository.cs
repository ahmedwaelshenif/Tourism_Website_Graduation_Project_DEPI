using Tourism.Models;
using Tourism.Models.Relations;
using Tourism.ViewModel;

namespace Tourism.IRepository
{
    public interface IRestaurantRepository
    {
        public void Add(Restaurant restaurant);
        public Restaurant GetByEmail(string email);
        public Restaurant GetById(int id);
        public void UpdateVerificationDocument(Restaurant r, byte[] pdfBytes);
        public ServiceRequest GetServiceRequest(int mealId);
        public List<string> GetMessages(int id);
        public List<Restaurant> GetAll();
        public List<RestaurantHomePageViewModel> MergeRestaurantToViewModel(List<Restaurant> restaurants);
        public List<RestaurantHomePageViewModel> GetBySearch(string search);
        public Task<List<RestaurantBookingDisplayViewModel>> GetBookingsForRestaurant(int restaurantId);
    }
}
