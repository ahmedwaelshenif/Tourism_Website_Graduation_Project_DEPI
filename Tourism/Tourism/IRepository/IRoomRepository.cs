using Tourism.Models;
using Tourism.Models.Relations;

namespace Tourism.IRepository
{
    public interface IRoomRepository
    {

        Task AddRoomAsync(Room room);
        Task<Room> GetByIdAsync(int id);
        void Delete(Room room);
        List<Room> GetRoomsByHotel(int hotelId);
        List<Image> GetPics(int roomId);

    }
}
