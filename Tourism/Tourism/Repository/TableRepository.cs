using Microsoft.EntityFrameworkCore;
using Tourism.IRepository;
using Tourism.Models;
using Tourism.Models.Relations;
using Tourism.ViewModel;

namespace Tourism.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly TourismDbContext _Context;

        public TableRepository(TourismDbContext context)
        {
            _Context = context;
        }

        public List<TableViewModel> GetTableViewModel(int restaurantId)
        {
            return (
                from t in _Context.Tables
                where t.restaurantId == restaurantId
                select new TableViewModel
                {
                    id = t.id,
                    number = t.number,
                    numberOfPersons = t.numberOfPersons,
                    bookingPrice = t.bookingPrice,
                }
            ).ToList();
        }

        public Table RestaurantTable(int tableId, int restaurantId)
        {
            return _Context.Tables.FirstOrDefault(t => t.id == tableId && t.restaurantId == restaurantId);
        }

        public IEnumerable<Table> GetNotBookedTables(int restaurantId)
        {
            return _Context.Tables
                .Where(t => t.restaurantId == restaurantId && t.booked == false)
                .ToList();
        }

        public Table GetById(int id)
        {
            return _Context.Tables.FirstOrDefault(t => t.id == id);
        }

        public void Update(Table table)
        {
            _Context.Tables.Update(table);
            _Context.SaveChanges();
        }

        public Table GetByNumber(int number, int restaurantId)
        {
            return _Context.Tables
                .FirstOrDefault(t => t.number == number && t.restaurantId == restaurantId);
        }

    }
}
