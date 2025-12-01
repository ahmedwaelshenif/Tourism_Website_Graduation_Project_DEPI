using Tourism.Models;
using Tourism.ViewModel;

namespace Tourism.IRepository
{
    public interface ITableRepository
    {
        public List<TableViewModel> GetTableViewModel(int restaurantId);
        public Table RestaurantTable(int tableId, int restaurantId);
        public IEnumerable<Table> GetNotBookedTables(int restaurantId);
        public Table GetById(int id);
        public void Update(Table table);
        public Table GetByNumber(int number, int restaurantId);
    }
}
