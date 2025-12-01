using Microsoft.EntityFrameworkCore;
using Tourism.IRepository;
using Tourism.Models;
using Tourism.Models.Relations;
using Tourism.ViewModel;

namespace Tourism.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly TourismDbContext _context;
        public RoomRepository(TourismDbContext context) => _context = context;

        public async Task AddRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
         
        }

        public async Task<Room> GetByIdAsync(int id) => await _context.Rooms.FindAsync(id);

        public void Delete(Room room)
        {
            _context.Rooms.Remove(room);
        }

        public List<Room> GetRoomsByHotel(int hotelId)
        {
            return _context.Rooms.Where(r => r.hotelId == hotelId).ToList();
        }

        public List<Image> GetPics(int roomId)
        {
            return _context.Images
                .Where(img => img.serviceId == roomId && img.serviceType == "Room")
                .ToList();
        }


    } 
}
