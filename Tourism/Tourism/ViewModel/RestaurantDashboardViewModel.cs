using System.ComponentModel.DataAnnotations;
using Tourism.Models.Relations;
using Tourism.Models;

namespace Tourism.ViewModel
{
    public class RestaurantDashboardViewModel
    {
        public List<string> msg { get; set; } = new();

        public List<RestaurantBookingDisplayViewModel> BookedTables { get; set; }
    }
}
