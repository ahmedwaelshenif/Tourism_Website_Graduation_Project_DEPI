using Microsoft.EntityFrameworkCore;
using Tourism.IRepository;
using Tourism.Models;
using Tourism.Models.Relations;

namespace Tourism.Repository
{
    public class HotelRepository :IHotelRepository
    {
        private readonly TourismDbContext _context;

        public HotelRepository(TourismDbContext context)
        {
            _context = context;
        }

        public Hotel GetByEmail(string email)
        {
            return _context.Hotels
                .Include(h => h.creditCard)
                .FirstOrDefault(h => h.email == email);
        }

        public async Task<Hotel> GetByIdAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.creditCard)
                .FirstOrDefaultAsync(h => h.id == id);
        }

        public void UpdateVerificationDocument(Hotel hotel, byte[] pdfBytes)
        {
            hotel.verificationDocuments = pdfBytes;
            _context.Hotels.Update(hotel);
            _context.SaveChanges();
        }

        public async Task AddAsync(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            //await _context.SaveChangesAsync();
        }

        public List<string> GetMessages(int id)
        {
           
            return _context.InboxMsgs
                .Where(m => m.id == id)
                .OrderByDescending(m => m.date)
                .Take(5)
                .Select(m => m.content)
                .ToList();
        }

        public ServiceRequest GetServiceRequest(int roomId)
        {
            return _context.ServiceRequests
                .FirstOrDefault(sr => sr.serviceId == roomId && sr.role == "Hotel");
        }

        int IHotelRepository.GetHotelRooms(int hotelId)
        {
            throw new NotImplementedException();
        }

        public async Task<double> MonthlyEarningsBookingsAsync(int month, int hotelId)
        {
            var currentYear = DateTime.Now.Year;

           
            var bookings = await _context.TouristRooms 
                .Include(tr => tr.room)
                .Where(tr => tr.room.hotelId == hotelId &&
                             tr.startDate.Year == currentYear &&
                             tr.startDate.Month == month)
                .ToListAsync();

            double totalRevenue = bookings.Sum(tr =>
                (tr.endDate - tr.startDate).TotalDays * (double)tr.room.cost
            );

            return totalRevenue;
        }

      
        public async Task<int> GetSumAmountBookingsAsync(int days, int hotelId)
        {
          
            var dateLimit = (days == 0) ? DateTime.Today : DateTime.Today.AddDays(-days);

         
            var count = await _context.TouristRooms
                .Include(tr => tr.room)
                .CountAsync(tr => tr.room.hotelId == hotelId &&
                                   tr.startDate >= dateLimit);

            return count;
        }



        public async Task<double> GetSumPriceBookingsAsync(int days, int hotelId)
        {
            var dateLimit = (days == 0) ? DateTime.Today : DateTime.Today.AddDays(-days);

            var bookings = await _context.TouristRooms
                .Include(tr => tr.room)
                .Where(tr => tr.room.hotelId == hotelId &&
                             tr.startDate >= dateLimit)
                .ToListAsync();

         
            double totalRevenue = bookings.Sum(tr =>
               
                (tr.endDate - tr.startDate).TotalDays * (double)tr.room.cost
            );

            return totalRevenue;
        }


        public async Task<int> GetTotalRoomsAsync(int hotelId)
        {
          
            return await _context.Rooms
                .CountAsync(r => r.hotelId == hotelId);
        }

        public byte[]? GetRoomImageBytes(int roomId)
        {
         
            var image = _context.Images
                                .FirstOrDefault(i => i.serviceId == roomId && i.serviceType == "Room");

            return image?.imageData;
        }


        public byte[]? GetHotelImageBytes(int hotelId)
        {
           
            return _context.Hotels
                           .Where(h => h.id == hotelId)
                           .Select(h => h.pic) 
                           .FirstOrDefault();
        }


        public List<int> GetRoomImageIds(int roomId)
        {
            return _context.Images
                           .Where(i => i.serviceId == roomId && i.serviceType == "Room")
                           .Select(i => i.id)
                           .ToList();
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels
                                
                                 .ToListAsync();
        }


        public void Delete(int id)
        {

            var hotelToDelete = _context.Hotels
            .Include(h => h.rooms)
                .FirstOrDefault(h => h.id == id);

            if (hotelToDelete != null)
            {
                if (hotelToDelete.rooms != null && hotelToDelete.rooms.Any())
                {
                    _context.Rooms.RemoveRange(hotelToDelete.rooms);
                }
                _context.Hotels.Remove(hotelToDelete);
                
                
            }
        }
        // داخل HotelRepository.cs

        public async Task DirectUpdateVerificationFieldsAsync(int hotelId, byte[] documentBytes)
        {
            
            string sql = "UPDATE Hotels SET verificationDocuments = @p0, verified = @p1 WHERE id = @p2";

            await _context.Database.ExecuteSqlRawAsync(
                sql,
                documentBytes,
                false, // تعيين verified إلى false (أو 0)
                hotelId
            );
        }
    }
}
