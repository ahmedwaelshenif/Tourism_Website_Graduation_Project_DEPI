using Microsoft.EntityFrameworkCore;
using Tourism.IRepository;
using Tourism.Models;
using Tourism.Models.Relations;
using Tourism.ViewModel;

namespace Tourism.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        TourismDbContext _Context;
        public RestaurantRepository(TourismDbContext tourismDbContext)
        {
            _Context = tourismDbContext;
        }

        public void Add(Restaurant restaurant)
        {
            _Context.Restaurants.Add(restaurant);
            _Context.SaveChanges();
        }

        public Restaurant GetByEmail(string email)
        {
            return _Context.Restaurants.FirstOrDefault(r => r.email == email);
        }

        public Restaurant GetById(int id)
        {
            return _Context.Restaurants.FirstOrDefault(r => r.id == id);
        }

        public void UpdateVerificationDocument(Restaurant r, byte[] pdfBytes)
        {
            r.verificationDocuments = pdfBytes;
            _Context.Update(r);
            _Context.SaveChanges();
        }

        public ServiceRequest GetServiceRequest(int mealId)
        {
            return _Context.ServiceRequests.FirstOrDefault(s => s.serviceId == mealId && s.role == "Restaurant");
        }

        public List<string> GetMessages(int id)
        {
            return _Context.InboxMsgs
                .Where(i => i.providerId == id && i.providerType == "Restaurant")
                .OrderByDescending(i => i.date)
                .Select(i => i.content)
                .Take(10)
                .ToList();
        }

        public List<Restaurant> GetAll()
        {
            return _Context.Restaurants.ToList();
        }

        public List<RestaurantHomePageViewModel> MergeRestaurantToViewModel(List<Restaurant> restaurants)
        {
            return restaurants.Select(r => new RestaurantHomePageViewModel
            {
                id = r.id,
                name = r.name,
                description = r.description,
                address = r.address,
                hotline = r.hotline,
                Image = r.Image 
            }).ToList();
        }

        public List<RestaurantHomePageViewModel> GetBySearch(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return MergeRestaurantToViewModel(_Context.Restaurants.ToList());


            search = search.ToLower();
            var restaurants = _Context.Restaurants
                .Where(r => EF.Functions.Like(r.name.ToLower(), $"%{search}%"))
                .ToList();
            return MergeRestaurantToViewModel(restaurants);
        }

        public async Task<List<RestaurantBookingDisplayViewModel>> GetBookingsForRestaurant(int restaurantId)
        {
            var bookings = await (from tr in _Context.TouristRestaurants
                                  join t in _Context.Tables on tr.tableId equals t.id
                                  join tourist in _Context.Tourists on tr.touristId equals tourist.id
                                  where tr.restaurantId == restaurantId
                                  select new RestaurantBookingDisplayViewModel
                                  {
                                      BookingId = tr.bookId,
                                      TouristId = tr.touristId,
                                      TouristName = $"{tourist.firstName} {tourist.LastName}",
                                      TouristEmail = tourist.email,
                                      TableNumber = tr.tableNumber,
                                      BookingDate = tr.date,
                                      BookingPrice = tr.bookingPrice
                                  })
                                 .ToListAsync();

            return bookings;
        }
    }
}
