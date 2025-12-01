using Tourism.Models;
using Tourism.Models.Relations;

namespace Tourism.IRepository
{
    public interface IHotelRepository
    {

        Hotel GetByEmail(string email);
        
        Task<Hotel> GetByIdAsync(int id);
        void UpdateVerificationDocument(Hotel hotel, byte[] pdfBytes);

        int GetHotelRooms(int hotelId);
        
        Task AddAsync(Hotel hotel);
        Task<IEnumerable<Hotel>> GetAllAsync();
        public List<string> GetMessages(int id);
        public ServiceRequest GetServiceRequest(int roomId);

        Task<double> MonthlyEarningsBookingsAsync(int month, int hotelId);
        Task<int> GetSumAmountBookingsAsync(int days, int hotelId);
        Task<double> GetSumPriceBookingsAsync(int days, int hotelId);
        Task<int> GetTotalRoomsAsync(int hotelId);
        byte[]? GetRoomImageBytes(int roomId);
        byte[]? GetHotelImageBytes(int hotelId);
        List<int> GetRoomImageIds(int roomId);
        void Delete(int id);
        Task DirectUpdateVerificationFieldsAsync(int hotelId, byte[] documentBytes);
    }
}
