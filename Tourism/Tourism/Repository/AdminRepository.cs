using Microsoft.EntityFrameworkCore;
using Tourism.IRepository;
using Tourism.Models;
using Tourism.Models.Relations;
using Tourism.ViewModel;

namespace Tourism.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly TourismDbContext _context;

        public AdminRepository(TourismDbContext context)
        {
            _context = context;
        }
        public Admin GetByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(a => a.email == email);
        }

        public List<VerificationViewModel> GetVerificationViewModels(string type)
        {
            List<VerificationViewModel> requests = new();

            if (type == "Merchant")
            {
                requests = _context.Merchants
                    .Join(
                        _context.VerificationRequests,
                        m => m.id,
                        v => v.provider_Id,
                        (m, v) => new { Merchant = m, Request = v }
                    )
                    .Where(joined => joined.Request.role == "Merchant")
                    .Select(joined => new VerificationViewModel
                    {
                        providerId = joined.Merchant.id,
                        name = joined.Merchant.name,
                        email = joined.Merchant.email,
                        pdf = joined.Merchant.verificationDocuments,
                        type = "Merchant",
                        id = joined.Request.id
                    })
                    .ToList();
            }
            else if (type == "TourGuide")
            {
                requests = _context.TourGuides
                    .Join(_context.VerificationRequests,
                          t => t.TourGuideId,
                          v => v.provider_Id,
                          (t, v) => new { TourGuide = t, Request = v })
                    .Where(joined => joined.Request.role == "TourGuide")
                    .Select(joined => new VerificationViewModel
                    {
                        providerId = joined.TourGuide.TourGuideId,
                        name = joined.TourGuide.firstName,
                        email = joined.TourGuide.email,
                        pdf = joined.TourGuide.verificationDocuments,
                        type = "TourGuide",
                        id = joined.Request.id
                    })
                    .ToList();
            }
            else if (type == "Hotel")
            {
                requests = _context.Hotels
                    .Join(_context.VerificationRequests,
                          h => h.id,
                          v => v.provider_Id,
                          (h, v) => new { Hotel = h, Request = v })
                    .Where(joined => joined.Request.role == "Hotel")
                    .Select(joined => new VerificationViewModel
                    {
                        providerId = joined.Hotel.id,
                        name = joined.Hotel.name,
                        email = joined.Hotel.email,
                        pdf = joined.Hotel.verificationDocuments,
                        type = "Hotel",
                        id = joined.Request.id
                    })
                    .ToList();
            }
            else if (type == "Restaurant")
            {
                requests = _context.Restaurants
                    .Join(_context.VerificationRequests,
                          r => r.id,
                          v => v.provider_Id,
                          (r, v) => new { Restaurant = r, Request = v })
                    .Where(joined => joined.Request.role == "Restaurant")
                    .Select(joined => new VerificationViewModel
                    {
                        providerId = joined.Restaurant.id,
                        name = joined.Restaurant.name,
                        email = joined.Restaurant.email,
                        pdf = joined.Restaurant.verificationDocuments,
                        type = "Restaurant",
                        id = joined.Request.id
                    })
                    .ToList();
            }
            return requests;
        }

        public List<ServiceRequestsViewModel> GetServiceRequestsViewModels(string role)
        {
            if (role == "Merchant")
            {
                return (from s in _context.ServiceRequests
                        join p in _context.Products
                        on s.serviceId equals p.id
                        where s.role == role
                        select new ServiceRequestsViewModel
                        {
                            id = s.id,
                            providerId = p.merchantId,
                            serviceId = p.id,
                            service_name = p.name,
                            date = p.dateAdded,
                            serviceType = "Merchant"

                        }).ToList();
            }
            else if (role == "Hotel")
            {
                return (from s in _context.ServiceRequests
                        join r in _context.Rooms
                        on s.serviceId equals r.id
                        where s.role == "Hotel"
                        select new ServiceRequestsViewModel
                        {
                            id = s.id,
                            providerId = r.hotelId,
                            serviceId = r.id,
                            service_name = "Hotel",
                            serviceType = "Hotel",
                            date = r.dateAdded
                        }).ToList();
            }
            else if (role == "TourGuide")
            {
                return (from s in _context.ServiceRequests
                        join t in _context.Trips
                        on s.serviceId equals t.id
                        where s.role == "TourGuide"
                        select new ServiceRequestsViewModel
                        {
                            id = s.id,
                            providerId = t.tourGuideId,
                            serviceId = t.id,
                            service_name = t.name,
                            serviceType = "TourGuide",
                            date = t.dateAdded
                        }).ToList();
            }
            else if (role == "Restaurant")
            {
                return (from s in _context.ServiceRequests
                        join m in _context.Meals
                        on s.serviceId equals m.id
                        where s.role == "Restaurant"
                        select new ServiceRequestsViewModel
                        {
                            id = s.id,
                            providerId = m.restaurantId,
                            serviceId = m.id,
                            service_name = m.name,
                            date = m.dateAdded,
                            serviceType = "Restaurant"
                        }).ToList();
            }
            return null;
        }
        public ServiceRequest GetServiceRequest(int serviceId, string role)
        {
            return _context.ServiceRequests.FirstOrDefault(s => s.serviceId == serviceId && s.role == role);
        }
        public void ShipProduct(int orderId)
        {
            var order = _context.TouristProducts.Find(orderId);
            if (order != null)
            {
                order.status = "Delivered";
                _context.SaveChanges();
            }
        }

        public void RefreshEcommerceTransactions()
        {
            List<TouristProduct> orders = _context.TouristProducts.Where(tp=>tp.funded==false && (DateTime.Today - tp.arrivalDate.Value).Days > 14).ToList();
            var cc = _context.CreditCards.Find("00000000000000");
            foreach(var tp in orders){
                var tourist = _context.Tourists.Find(tp.touristId);
                var product = _context.Products.Find(tp.productId);
                var merchant = _context.Merchants.Find(product.merchantId);
                decimal cost = (decimal)product.price * (decimal)(1 - product.offer / 100) * tp.amount;
                merchant.creditCard.Balance += cost;
                cc.Balance -= cost;
                tp.funded = true;
                var msg = new InboxMsg
                {
                    providerType = "Merchant",
                    content = $"The transaction for Order (Product: {product.name}, Amount: {tp.amount}, Customer: {tourist.firstName}) has been completed successfully.",
                    providerId = tp.product.merchantId,
                    date = DateTime.Now
                };
                _context.InboxMsgs.Add(msg);
            }
            _context.SaveChanges();

        }
    }
}
